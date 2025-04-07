using Microsoft.AspNet.SignalR.Client;
using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.Win32;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormNotification.Models;

namespace WindowsFormNotification
{
    public class NotificationAppContext : ApplicationContext
    {
        private HubConnection hubConnection;
        private IHubProxy hubProxy;

        public NotificationAppContext()
        {
            Task.Run(() => ConnectToSignalR()); // fire-and-forget
            AddApplicationToStartup();

        }

        private async Task ConnectToSignalR()
        {
            try
            {
                hubConnection = new HubConnection("http://localhost/");
                hubProxy = hubConnection.CreateHubProxy("notificationHub");

                await hubConnection.Start();
                File.WriteAllText(@"C:\Logs\NotificationsForm.txt", "hubConnection: " + hubConnection.State + Environment.NewLine);

                hubProxy.On<NotificationModel>("newNotification", notification =>
                {
                    try
                    {
                        ShowToastNotification("New Post", notification);
                        File.AppendAllText(@"C:\Logs\NotificationsForm.txt", $"New Notification: {notification.Message} - {notification.OnclickUrl} - {notification.ImageUrl}" + Environment.NewLine);
                    }
                    catch (Exception ex)
                    {
                        File.AppendAllText(@"C:\Logs\NotificationsForm.txt", $"Error Message: {ex.Message}" + Environment.NewLine);
                        File.AppendAllText(@"C:\Logs\NotificationsForm.txt", $"New Notification in catch: {notification.Message} - {notification.OnclickUrl} - {notification.ImageUrl}" + Environment.NewLine);
                    }
                });
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"C:\Logs\errorsForm.txt", ex.InnerException.Message);
            }
        }

        private void ShowToastNotification(string title, NotificationModel notification)
        {
            var toast = new ToastContentBuilder()
                .AddText(title)
                .AddText(notification.Message)
                .AddAudio(new ToastAudio
                {
                    Src = new Uri("ms-winsoundevent:Notification.IM"),
                    Loop = false
                });

            if (IsValidUrl(notification.OnclickUrl))
            {
                toast.SetProtocolActivation(new Uri(notification.OnclickUrl));
            }

            if (IsValidUrl(notification.ImageUrl))
            {
                string imageUrl = notification.ImageUrl;
                string tempImagePath = Path.Combine(Path.GetTempPath(), "toast_image.jpg");
                using (var webClient = new WebClient())
                {
                    webClient.Credentials = CredentialCache.DefaultNetworkCredentials;
                    webClient.Headers.Add("Authorization", "NTLM");
                    webClient.DownloadFile(imageUrl, tempImagePath);
                }
                toast.AddAppLogoOverride(new Uri(tempImagePath));
                //.AddInlineImage(new Uri(tempImagePath))
                //.AddHeroImage(new Uri(tempImagePath));
            }

            //.AddHeader("Notification Header", "InternalPortal", "MyAppID")
            //.AddButton(new ToastButton()
            //    .SetContent("Open Interna Portal")
            //    .SetProtocolActivation(new Uri(notification.OnclickUrl))
            //    )


            //.AddAppLogoOverride(new Uri(notification.ImageUrl), ToastGenericAppLogoCrop.Circle)
            //.AddInlineImage(new Uri(notification.ImageUrl))
            //.AddHeroImage(new Uri(notification.ImageUrl));

            #region Available System Sounds
            //"ms-winsoundevent:Notification.Default"
            //"ms-winsoundevent:Notification.IM"
            //"ms-winsoundevent:Notification.Mail"
            //"ms-winsoundevent:Notification.Reminder"
            //"ms-winsoundevent:Notification.SMS"
            #endregion

            toast.Show();
        }

        private bool IsValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        public static void AddApplicationToStartup()
        {
            string appName = "MyNotificationApp"; // Choose any name
            string exePath = Application.ExecutablePath;

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);

            if (key.GetValue(appName) == null)
            {
                key.SetValue(appName, $"\"{exePath}\"");
            }
        }
    }
}

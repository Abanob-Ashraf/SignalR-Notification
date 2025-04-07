using Microsoft.AspNet.SignalR.Client;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.ServiceProcess;

namespace WindowsServiceNotification
{
    public partial class NotificationService : ServiceBase
    {
        private HubConnection hubConnection;
        private IHubProxy hubProxy;
        public NotificationService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ConnectToSignalR();
        }
        private async void ConnectToSignalR()
        {
            try
            {
                hubConnection = new HubConnection("http://localhost/");
                hubProxy = hubConnection.CreateHubProxy("notificationHub");

                await hubConnection.Start();
                System.IO.File.WriteAllText(@"C:\Logs\notifications.txt", "hubConnection:" + hubConnection.State + Environment.NewLine + "hubProxy:" + hubProxy.ToString() + Environment.NewLine);


                hubProxy.On<NotificationModel>("newNotification", notification =>
                {
                    try
                    {
                        ShowToastNotification("New Post", notification);
                        System.IO.File.AppendAllText(@"C:\Logs\notifications.txt", $"New Notification: {notification.Message} - {notification.OnclickUrl} - {notification.ImageUrl}" + Environment.NewLine);


                    }
                    catch (Exception ex)
                    {
                        System.IO.File.AppendAllText(@"C:\Logs\notifications.txt", $"Error Message: {ex.Message}" + Environment.NewLine);

                        System.IO.File.AppendAllText(@"C:\Logs\notifications.txt", $"New Notification in catch: {notification.Message} - {notification.OnclickUrl} - {notification.ImageUrl}" + Environment.NewLine);

                    }
                    // Log or show notifications
                });


            }
            catch (Exception ex)
            {
                System.IO.File.WriteAllText(@"C:\Logs\errors.txt", ex.InnerException.Message);
            }
        }

        protected override void OnStop()
        {
            hubConnection?.Stop();
            hubConnection?.Dispose();
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        private void ShowToastNotification(string title, NotificationModel notificationModel)
        {
            var toast = new ToastContentBuilder()
                .AddText(title)
                .AddText(notificationModel.Message);

            // Add image if available
            //if (!string.IsNullOrEmpty(notificationModel.ImageUrl))
            //{
            //    toast.AddInlineImage(new Uri(notificationModel.ImageUrl));
            //}
            toast.Show();
            // Get the active user's session ID
            //int sessionId = Process.GetProcessesByName("explorer").FirstOrDefault()?.SessionId ?? -1;
            //System.IO.File.AppendAllText(@"C:\Logs\notifications.txt", $"sessionId: {sessionId}" + Environment.NewLine);

            //if (sessionId == -1) return;

            //string script = $@"
            //                [Windows.UI.Notifications.ToastNotificationManager, Windows.UI.Notifications, ContentType=WindowsRuntime] | Out-Null

            //                # Create a toast notification template that includes an image and text
            //                $Template = [Windows.UI.Notifications.ToastTemplateType]::ToastImageAndText02
            //                $ToastXml = [Windows.UI.Notifications.ToastNotificationManager]::GetTemplateContent($Template)

            //                # Set the text elements (title and message)
            //                $TextElements = $ToastXml.GetElementsByTagName('text')
            //                $TextElements.Item(0).InnerText = '{title}'
            //                $TextElements.Item(1).InnerText = '{notificationModel.Message}'

            //                # Set the image source (Dynamic Image)
            //                $ImageElement = $ToastXml.GetElementsByTagName('image').Item(0)
            //                $ImageElement.SetAttribute('src', '{notificationModel.ImageUrl}')
            //                $ImageElement.SetAttribute('alt', 'Notification Image')

            //                # Create and show the toast notification
            //                $Toast = [Windows.UI.Notifications.ToastNotification]::new($ToastXml)
            //                [Windows.UI.Notifications.ToastNotificationManager]::CreateToastNotifier('Internal Portal').Show($Toast)
            //                ";

            //ProcessStartInfo psi = new ProcessStartInfo
            //{
            //    FileName = @"C:\Path\To\psexec.exe",
            //    Arguments = $@"-i {sessionId} -d powershell.exe -ExecutionPolicy Bypass -File ""{script}""",

            //    WindowStyle = ProcessWindowStyle.Hidden,
            //    CreateNoWindow = true,
            //    UseShellExecute = false
            //};
            //System.IO.File.AppendAllText(@"C:\Logs\notifications.txt", $"psi: {psi.FileName} - {psi.Arguments} - {psi.WindowStyle} - {psi.UserName} - {psi.UseShellExecute} - {psi.ErrorDialog} - {psi.StandardErrorEncoding} - {psi.StandardOutputEncoding}" + Environment.NewLine);
            //// Run the PowerShell script as the logged-in user

            //Process.Start(psi);
        }


    }



    public class NotificationModel
    {
        public string Message { get; set; }
        public string ImageUrl { get; set; }
        public string OnclickUrl { get; set; }
    }
}

using Microsoft.AspNet.SignalR.Client;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormNotification
{
    public partial class NotificationForm : Form
    {
        private HubConnection hubConnection;
        private IHubProxy hubProxy;
        public NotificationForm()
        {
            InitializeComponent();
            //this.Load += new EventHandler(NotificationForm_Load);
        }

        private async void NotificationForm_Load(object sender, EventArgs e)
        {
            // Automatically connect to SignalR when the form is loaded
            await ConnectToSignalR();
        }
        private async Task ConnectToSignalR()
        {
            try
            {
                // Log to check if the connection is working
                System.IO.File.WriteAllText(@"C:\Logs\errors.txt", "Connecting to SignalR...");
                hubConnection = new HubConnection("http://localhost/");

                //hubConnection = new HubConnection("https://realtimeapp/");
                hubProxy = hubConnection.CreateHubProxy("notificationHub");

                // Start the connection
                await hubConnection.Start();
                System.IO.File.WriteAllText(@"C:\Logs\notifications.txt", "hubConnection: " + hubConnection.State + Environment.NewLine);

                // Listening for new notifications from SignalR
                hubProxy.On<NotificationModel>("newNotification", notification =>
                {
                    try
                    {
                        // Show a toast notification
                        ShowToastNotification("New Post", notification);
                        System.IO.File.AppendAllText(@"C:\Logs\notifications.txt", $"New Notification: {notification.Message} - {notification.OnclickUrl} - {notification.ImageUrl}" + Environment.NewLine);
                    }
                    catch (Exception ex)
                    {
                        System.IO.File.AppendAllText(@"C:\Logs\notifications.txt", $"Error Message: {ex.Message}" + Environment.NewLine);
                        System.IO.File.AppendAllText(@"C:\Logs\notifications.txt", $"New Notification in catch: {notification.Message} - {notification.OnclickUrl} - {notification.ImageUrl}" + Environment.NewLine);
                    }
                });
            }
            catch (Exception ex)
            {
                System.IO.File.WriteAllText(@"C:\Logs\errors.txt", ex.InnerException.Message);
            }
        }

        private void ShowToastNotification(string title, NotificationModel notification)
        {
            //// Create and configure the toast notification
            //var toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText02);
            //var toastTextElements = toastXml.GetElementsByTagName("text");
            //var toastImageElements = toastXml.GetElementsByTagName("image");

            //// Set the toast message
            //toastTextElements.Item(0).AppendChild(toastXml.CreateTextNode(title));
            //toastTextElements.Item(1).AppendChild(toastXml.CreateTextNode(notification.Message));

            //// Set the image URL if present
            //if (!string.IsNullOrEmpty(notification.ImageUrl))
            //{
            //    toastImageElements.Item(0).Attributes[1].NodeValue = notification.ImageUrl;
            //}

            //var toast = new ToastNotification(toastXml);
            //ToastNotificationManager.CreateToastNotifier().Show(toast);
            var toast = new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationId", 9813)
               .AddText(title)
               .AddText(notification.Message)
               .AddHeader("Notification Header", "InternalPortal", "MyAppID")
               .AddAppLogoOverride(new Uri(notification.ImageUrl), ToastGenericAppLogoCrop.Circle);
            //.AddInlineImage(new Uri(notification.ImageUrl))
            //.AddHeroImage(new Uri(notification.ImageUrl));

            // Add image if available
            //if (!string.IsNullOrEmpty(notificationModel.ImageUrl))
            //{
            //    toast.AddInlineImage(new Uri(notificationModel.ImageUrl));
            //}
            toast.Show();





        }



    }
    public class NotificationModel
    {
        public string Message { get; set; }
        public string OnclickUrl { get; set; }
        public string ImageUrl { get; set; }
    }
}

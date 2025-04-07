using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormNotification
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new NotificationForm());

            //// Your form (hidden)
            //var form = new NotificationForm();
            //form.Load += (s, e) => form.Hide(); // Hide on load

            //// Start app loop with ApplicationContext
            //Application.Run(new HiddenAppContext(form));
            var context = new NotificationAppContext();
            Application.Run(context);
        }
    }

    //public class HiddenAppContext : ApplicationContext
    //{
    //    private Form _form;

    //    public HiddenAppContext(Form form)
    //    {
    //        _form = form;
    //        _form.FormClosed += (s, e) => ExitThread(); // Auto-close app when form closes (if you ever show it)
    //    }
    //}
}

using System;
using System.IO;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Text;

namespace Cosy
{
    public partial class Program : Form
    {
        [STAThread]
        public static void Main(string[] args)
        {
            if (!Directory.Exists("C:\\TTMC"))
            {
                Directory.CreateDirectory("C:\\TTMC");
            }
            if (!Directory.Exists("C:\\TTMC\\Cosy"))
            {
                Directory.CreateDirectory("C:\\TTMC\\Cosy");
            }
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}

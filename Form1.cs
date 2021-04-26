using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;

namespace Cosy
{
    public partial class Form1 : Form
    {
        private bool mouseIsDown;
        private Point firstPoint;
        public List<string> lines = new();
        [DllImport("KERNEL32.DLL", EntryPoint = "RtlZeroMemory")]
        private static extern bool ZeroMemory(IntPtr Destination, int Length);
        public Form1()
        {
            InitializeComponent();
        }
        public static void FileEncrypt(string inputFile, string password)
        {
            byte[] salt = new byte[32];
            FileStream fsCrypt = new(inputFile + ".cosy", FileMode.Create);
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            RijndaelManaged AES = new();
            AES.KeySize = 256;
            AES.BlockSize = 128;
            AES.Padding = PaddingMode.PKCS7;
            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);
            AES.Mode = CipherMode.CFB;
            fsCrypt.Write(salt, 0, salt.Length);

            CryptoStream cs = new(fsCrypt, AES.CreateEncryptor(), CryptoStreamMode.Write);

            FileStream fsIn = new(inputFile, FileMode.Open);
            byte[] buffer = new byte[1048576];
            int read;

            try
            {
                while ((read = fsIn.Read(buffer, 0, buffer.Length)) > 0)
                {
                    Application.DoEvents();
                    cs.Write(buffer, 0, read);
                }

                // Close up
                fsIn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                cs.Close();
                fsCrypt.Close();
            }
        }
        public static void FileDecrypt(string inputFile, string outputFile, string password)
        {
            byte[] salt = new byte[32];
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            FileStream fsCrypt = new(inputFile, FileMode.Open);
            fsCrypt.Read(salt, 0, salt.Length);

            RijndaelManaged AES = new();
            AES.KeySize = 256;
            AES.BlockSize = 128;
            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);
            AES.Padding = PaddingMode.PKCS7;
            AES.Mode = CipherMode.CFB;

            CryptoStream cs = new(fsCrypt, AES.CreateDecryptor(), CryptoStreamMode.Read);

            FileStream fsOut = new(outputFile, FileMode.Create);

            int read;
            byte[] buffer = new byte[1048576];

            try
            {
                while ((read = cs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    Application.DoEvents();
                    fsOut.Write(buffer, 0, read);
                }
            }
            catch (CryptographicException ex_CryptographicException)
            {
                Console.WriteLine("CryptographicException error: " + ex_CryptographicException.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            try
            {
                cs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error by closing CryptoStream: " + ex.Message);
            }
            finally
            {
                fsOut.Close();
                fsCrypt.Close();
            }
        }

        private void Exit_MouseEnter(object sender, EventArgs e)
        {
            Exit.BackColor = Color.Red;
        }

        private void Exit_MouseLeave(object sender, EventArgs e)
        {
            Exit.BackColor = Color.DimGray;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            firstPoint = e.Location;
            mouseIsDown = true;
        }
        private void TitleBar_MouseUp(object sender, MouseEventArgs e)
        {
            mouseIsDown = false;
        }
        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseIsDown)
            {
                int xDiff = firstPoint.X - e.Location.X;
                int yDiff = firstPoint.Y - e.Location.Y;
                int x = this.Location.X - xDiff;
                int y = this.Location.Y - yDiff;
                this.Location = new Point(x, y);
            }
        }
        private void Encrypt_MouseEnter(object sender, EventArgs e)
        {
            Encrypt.BackColor = Color.Silver;
        }

        private void Decrypt_MouseEnter(object sender, EventArgs e)
        {
            Decrypt.BackColor = Color.Silver;
        }

        private void AutoLogin_MouseEnter(object sender, EventArgs e)
        {
            AutoLogin.BackColor = Color.Silver;
        }

        private void Encrypt_MouseLeave(object sender, EventArgs e)
        {
            Encrypt.BackColor = Color.DarkGray;
        }

        private void Decrypt_MouseLeave(object sender, EventArgs e)
        {
            Decrypt.BackColor = Color.DarkGray;
        }

        private void AutoLogin_MouseLeave(object sender, EventArgs e)
        {
            AutoLogin.BackColor = Color.DarkGray;
        }

        private void AutoLogin_Click(object sender, EventArgs e)
        {
            Form from = new AutoLogin();
            from.Show();
        }

        private void Encrypt_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new();
            open.Title = "Encrypt File";
            open.DefaultExt = ".*";
            open.Filter = "All files (*.*)|*.*";
            open.Multiselect = true;
            if (open.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists("C:\\TTMC\\Cosy\\cosy.pswd"))
                {
                    Form from = new Security(open.FileNames, true);
                    from.Show();
                }
            }
        }
        private void Decrypt_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new();
            open.Title = "Decrypt File";
            open.DefaultExt = "cosy";
            open.Filter = "cosy files (*.cosy)|*.cosy";
            open.Multiselect = true;
            if (open.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists("C:\\TTMC\\Cosy\\cosy.pswd"))
                {
                    Form from = new Security(open.FileNames, false);
                    from.Show();
                }
            }
        }
    }
}

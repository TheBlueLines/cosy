using System;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Generic;

namespace Cosy
{
    public partial class Security : Form
    {
        private bool mouseIsDown;
        private Point firstPoint;
        public string[] files;
        public Security(string[] fileNames, bool mode)
        {
            files = fileNames;
            InitializeComponent();
            KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Security_KeyDown);
            if (mode)
            {
                Login.Text = "ENCRYPT";
            }
            else
            {
                Login.Text = "DECRYPT";
            }
        }
        void Security_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Go();
            }
        }
        private static void FileEncrypt(string inputFile, string password)
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
        private static void FileDecrypt(string inputFile, string outputFile, string password)
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
        private void Save_Click(object sender, EventArgs e)
        {
            Go();
        }
        void Go()
        {
            if (File.Exists("C:\\TTMC\\Cosy\\cosy.pswd"))
            {
                if (PassWord.Text == "")
                {
                    MessageBox.Show("Password Missing!");
                }
                else
                {
                    FileDecrypt("C:\\TTMC\\Cosy\\cosy.pswd", "C:\\TTMC\\Cosy\\cosy", PassWord.Text);
                    if (File.ReadAllText("C:\\TTMC\\Cosy\\cosy") == "COSY")
                    {
                        File.Delete("C:\\TTMC\\Cosy\\cosy");
                        if (Login.Text == "ENCRYPT")
                        {
                            List<string> list = new();
                            if (File.Exists("C:\\TTMC\\Cosy\\list.pswd"))
                            {
                                foreach (string value in File.ReadLines("C:\\TTMC\\Cosy\\list.pswd"))
                                {
                                    list.Add(value);
                                }
                            }
                            foreach (string file in files)
                            {
                                FileEncrypt(file, PassWord.Text);
                                File.Delete(file);
                                if (!list.Contains(file))
                                {
                                    list.Add(file);
                                }
                            }
                            File.WriteAllLines("C:\\TTMC\\Cosy\\list.pswd", list);
                        }
                        else if (Login.Text == "DECRYPT")
                        {
                            List<string> list = new();
                            if (File.Exists("C:\\TTMC\\Cosy\\list.pswd"))
                            {
                                foreach (string value in File.ReadLines("C:\\TTMC\\Cosy\\list.pswd"))
                                {
                                    list.Add(value);
                                }
                            }
                            foreach (string file in files)
                            {
                                FileDecrypt(file, file[0..^5], PassWord.Text);
                                File.Delete(file);
                                if (list.Contains(file[0..^5]))
                                {
                                    list.Remove(file[0..^5]);
                                }
                            }
                            File.WriteAllLines("C:\\TTMC\\Cosy\\list.pswd", list);
                        }
                        this.Close();
                    }
                    else
                    {
                        File.Delete("C:\\TTMC\\Cosy\\cosy");
                        MessageBox.Show("Wrong Password!");
                    }
                }
            }
        }

        private void Save_MouseEnter(object sender, EventArgs e)
        {
            Login.BackColor = Color.Silver;
        }

        private void Save_MouseLeave(object sender, EventArgs e)
        {
            Login.BackColor = Color.DarkGray;
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
            this.Close();
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
    }
}

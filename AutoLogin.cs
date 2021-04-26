using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Runtime.InteropServices;

namespace Cosy
{
    public partial class AutoLogin : Form
    {
        private bool mouseIsDown;
        private Point firstPoint;
        public AutoLogin()
        {
            InitializeComponent();
            KeyPreview = true;
            this.KeyDown += new KeyEventHandler(AutoLogin_KeyDown);
        }
        private void AutoLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SavePassword();
            }
        }
        public static void FileEncrypt(string inputFile, string outputFile, string password)
        {
            byte[] salt = new byte[32];
            FileStream fsCrypt = new(outputFile, FileMode.Create);
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
        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Exit_MouseEnter(object sender, EventArgs e)
        {
            Exit.BackColor = Color.Red;
        }

        private void Exit_MouseLeave(object sender, EventArgs e)
        {
            Exit.BackColor = Color.DimGray;
        }
        private void Save_MouseEnter(object sender, EventArgs e)
        {
            Save.BackColor = Color.Silver;
        }

        private void Save_MouseLeave(object sender, EventArgs e)
        {
            Save.BackColor = Color.DarkGray;
        }
        private void Save_Click(object sender, EventArgs e)
        {
            SavePassword();
        }
        void SavePassword()
        {
            if (PassWord2.Text == PassWord3.Text)
            {
                if (PassWord2.Text == "")
                {
                    MessageBox.Show("Password cannot be empty!");
                }
                else
                {
                    if (File.Exists("C:\\TTMC\\Cosy\\cosy.pswd"))
                    {
                        if (PassWord1.Text == "")
                        {
                            MessageBox.Show("Old Password Missing!");
                        }
                        else
                        {
                            FileDecrypt("C:\\TTMC\\Cosy\\cosy.pswd", "C:\\TTMC\\Cosy\\cosy", PassWord1.Text);
                            if (File.ReadAllText("C:\\TTMC\\Cosy\\cosy") == "COSY")
                            {
                                FileEncrypt("C:\\TTMC\\Cosy\\cosy", "C:\\TTMC\\Cosy\\cosy.pswd", PassWord2.Text);
                                File.Delete("C:\\TTMC\\Cosy\\cosy");
                                if (File.Exists("C:\\TTMC\\Cosy\\list.pswd"))
                                {
                                    foreach (string item in File.ReadLines("C:\\TTMC\\Cosy\\list.pswd"))
                                    {
                                        FileDecrypt(item + ".cosy", item, PassWord1.Text);
                                        FileEncrypt(item, item + ".cosy", PassWord2.Text);
                                        File.Delete(item);
                                    }
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
                    else
                    {
                        File.WriteAllText("C:\\TTMC\\Cosy\\cosy", "COSY");
                        FileEncrypt("C:\\TTMC\\Cosy\\cosy", "C:\\TTMC\\Cosy\\cosy.pswd", PassWord2.Text);
                        File.Delete("C:\\TTMC\\Cosy\\cosy");
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Passwords do not match!");
            }
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


namespace Cosy
{
    partial class AutoLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoLogin));
            this.TitleBar = new System.Windows.Forms.Panel();
            this.Exit = new System.Windows.Forms.Panel();
            this.Head = new System.Windows.Forms.Label();
            this.PassWord1 = new System.Windows.Forms.TextBox();
            this.Save = new System.Windows.Forms.Label();
            this.PassWord2 = new System.Windows.Forms.TextBox();
            this.PassWord3 = new System.Windows.Forms.TextBox();
            this.TitleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // TitleBar
            // 
            this.TitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TitleBar.Controls.Add(this.Exit);
            this.TitleBar.Location = new System.Drawing.Point(0, 0);
            this.TitleBar.Name = "TitleBar";
            this.TitleBar.Size = new System.Drawing.Size(420, 30);
            this.TitleBar.TabIndex = 0;
            this.TitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleBar_MouseDown);
            this.TitleBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TitleBar_MouseMove);
            this.TitleBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TitleBar_MouseUp);
            // 
            // Exit
            // 
            this.Exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Exit.BackColor = System.Drawing.Color.DimGray;
            this.Exit.Location = new System.Drawing.Point(395, 5);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(20, 20);
            this.Exit.TabIndex = 1;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            this.Exit.MouseEnter += new System.EventHandler(this.Exit_MouseEnter);
            this.Exit.MouseLeave += new System.EventHandler(this.Exit_MouseLeave);
            // 
            // Head
            // 
            this.Head.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Head.BackColor = System.Drawing.Color.Transparent;
            this.Head.Font = new System.Drawing.Font("Roboto", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Head.ForeColor = System.Drawing.Color.DarkGray;
            this.Head.Location = new System.Drawing.Point(10, 40);
            this.Head.Name = "Head";
            this.Head.Size = new System.Drawing.Size(400, 50);
            this.Head.TabIndex = 1;
            this.Head.Text = "Password:";
            this.Head.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PassWord1
            // 
            this.PassWord1.BackColor = System.Drawing.Color.Gray;
            this.PassWord1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PassWord1.Font = new System.Drawing.Font("Roboto Medium", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.PassWord1.ForeColor = System.Drawing.Color.Silver;
            this.PassWord1.Location = new System.Drawing.Point(10, 100);
            this.PassWord1.Name = "PassWord1";
            this.PassWord1.PasswordChar = '×';
            this.PassWord1.Size = new System.Drawing.Size(400, 33);
            this.PassWord1.TabIndex = 2;
            this.PassWord1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Save
            // 
            this.Save.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Save.BackColor = System.Drawing.Color.DarkGray;
            this.Save.Font = new System.Drawing.Font("Roboto", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Save.ForeColor = System.Drawing.Color.DimGray;
            this.Save.Location = new System.Drawing.Point(10, 220);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(400, 50);
            this.Save.TabIndex = 3;
            this.Save.Text = "SAVE";
            this.Save.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            this.Save.MouseEnter += new System.EventHandler(this.Save_MouseEnter);
            this.Save.MouseLeave += new System.EventHandler(this.Save_MouseLeave);
            // 
            // PassWord2
            // 
            this.PassWord2.BackColor = System.Drawing.Color.Gray;
            this.PassWord2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PassWord2.Font = new System.Drawing.Font("Roboto Medium", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.PassWord2.ForeColor = System.Drawing.Color.Silver;
            this.PassWord2.Location = new System.Drawing.Point(10, 140);
            this.PassWord2.Name = "PassWord2";
            this.PassWord2.PasswordChar = '×';
            this.PassWord2.Size = new System.Drawing.Size(400, 33);
            this.PassWord2.TabIndex = 4;
            this.PassWord2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PassWord3
            // 
            this.PassWord3.BackColor = System.Drawing.Color.Gray;
            this.PassWord3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PassWord3.Font = new System.Drawing.Font("Roboto Medium", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.PassWord3.ForeColor = System.Drawing.Color.Silver;
            this.PassWord3.Location = new System.Drawing.Point(10, 180);
            this.PassWord3.Name = "PassWord3";
            this.PassWord3.PasswordChar = '×';
            this.PassWord3.Size = new System.Drawing.Size(400, 33);
            this.PassWord3.TabIndex = 5;
            this.PassWord3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AutoLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(420, 280);
            this.Controls.Add(this.PassWord3);
            this.Controls.Add(this.PassWord2);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.PassWord1);
            this.Controls.Add(this.Head);
            this.Controls.Add(this.TitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AutoLogin";
            this.Text = "AutoLogin";
            this.TitleBar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel TitleBar;
        private System.Windows.Forms.Panel Exit;
        private System.Windows.Forms.Label Head;
        private System.Windows.Forms.TextBox PassWord1;
        private System.Windows.Forms.Label Save;
        private System.Windows.Forms.TextBox PassWord2;
        private System.Windows.Forms.TextBox PassWord3;
    }
}
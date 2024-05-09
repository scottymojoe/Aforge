namespace Forms
{
    partial class MainForm
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
            this._pictureBox = new System.Windows.Forms.PictureBox();
            this._controlPanel = new System.Windows.Forms.Panel();
            this._thread2 = new System.Windows.Forms.Label();
            this._thread3 = new System.Windows.Forms.Label();
            this._thread1 = new System.Windows.Forms.Label();
            this._keepOpen = new System.Windows.Forms.CheckBox();
            this._close = new System.Windows.Forms.Button();
            this._hideLive = new System.Windows.Forms.CheckBox();
            this._live = new System.Windows.Forms.PictureBox();
            this._alwaysHide = new System.Windows.Forms.CheckBox();
            this._hide = new System.Windows.Forms.Button();
            this._show = new System.Windows.Forms.Button();
            this._mainPanel = new System.Windows.Forms.Panel();
            this._showParent = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).BeginInit();
            this._controlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._live)).BeginInit();
            this._mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _pictureBox
            // 
            this._pictureBox.Dock = System.Windows.Forms.DockStyle.Top;
            this._pictureBox.Location = new System.Drawing.Point(0, 0);
            this._pictureBox.Name = "_pictureBox";
            this._pictureBox.Size = new System.Drawing.Size(946, 632);
            this._pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._pictureBox.TabIndex = 0;
            this._pictureBox.TabStop = false;
            // 
            // _controlPanel
            // 
            this._controlPanel.Controls.Add(this._thread2);
            this._controlPanel.Controls.Add(this._thread3);
            this._controlPanel.Controls.Add(this._thread1);
            this._controlPanel.Controls.Add(this._keepOpen);
            this._controlPanel.Controls.Add(this._close);
            this._controlPanel.Controls.Add(this._hideLive);
            this._controlPanel.Controls.Add(this._live);
            this._controlPanel.Controls.Add(this._alwaysHide);
            this._controlPanel.Controls.Add(this._hide);
            this._controlPanel.Controls.Add(this._show);
            this._controlPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this._controlPanel.Location = new System.Drawing.Point(946, 0);
            this._controlPanel.Name = "_controlPanel";
            this._controlPanel.Size = new System.Drawing.Size(316, 758);
            this._controlPanel.TabIndex = 2;
            // 
            // _thread2
            // 
            this._thread2.AutoSize = true;
            this._thread2.ForeColor = System.Drawing.Color.White;
            this._thread2.Location = new System.Drawing.Point(3, 330);
            this._thread2.Name = "_thread2";
            this._thread2.Size = new System.Drawing.Size(49, 13);
            this._thread2.TabIndex = 10;
            this._thread2.Text = "_thread2";
            // 
            // _thread3
            // 
            this._thread3.AutoSize = true;
            this._thread3.ForeColor = System.Drawing.Color.White;
            this._thread3.Location = new System.Drawing.Point(3, 357);
            this._thread3.Name = "_thread3";
            this._thread3.Size = new System.Drawing.Size(49, 13);
            this._thread3.TabIndex = 9;
            this._thread3.Text = "_thread3";
            // 
            // _thread1
            // 
            this._thread1.AutoSize = true;
            this._thread1.ForeColor = System.Drawing.Color.White;
            this._thread1.Location = new System.Drawing.Point(3, 305);
            this._thread1.Name = "_thread1";
            this._thread1.Size = new System.Drawing.Size(49, 13);
            this._thread1.TabIndex = 3;
            this._thread1.Text = "_thread1";
            // 
            // _keepOpen
            // 
            this._keepOpen.AutoSize = true;
            this._keepOpen.Location = new System.Drawing.Point(223, 16);
            this._keepOpen.Name = "_keepOpen";
            this._keepOpen.Size = new System.Drawing.Size(32, 17);
            this._keepOpen.TabIndex = 8;
            this._keepOpen.Text = "1";
            this._keepOpen.UseVisualStyleBackColor = true;
            // 
            // _close
            // 
            this._close.Location = new System.Drawing.Point(261, 12);
            this._close.Name = "_close";
            this._close.Size = new System.Drawing.Size(47, 23);
            this._close.TabIndex = 7;
            this._close.Text = "X";
            this._close.UseVisualStyleBackColor = true;
            this._close.Click += new System.EventHandler(this.Close_Click);
            // 
            // _hideLive
            // 
            this._hideLive.AutoSize = true;
            this._hideLive.Location = new System.Drawing.Point(3, 273);
            this._hideLive.Name = "_hideLive";
            this._hideLive.Size = new System.Drawing.Size(32, 17);
            this._hideLive.TabIndex = 6;
            this._hideLive.Text = "0";
            this._hideLive.UseVisualStyleBackColor = true;
            // 
            // _live
            // 
            this._live.Location = new System.Drawing.Point(3, 51);
            this._live.Name = "_live";
            this._live.Size = new System.Drawing.Size(305, 216);
            this._live.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._live.TabIndex = 5;
            this._live.TabStop = false;
            // 
            // _alwaysHide
            // 
            this._alwaysHide.AutoSize = true;
            this._alwaysHide.Location = new System.Drawing.Point(109, 16);
            this._alwaysHide.Name = "_alwaysHide";
            this._alwaysHide.Size = new System.Drawing.Size(32, 17);
            this._alwaysHide.TabIndex = 4;
            this._alwaysHide.Text = "0";
            this._alwaysHide.UseVisualStyleBackColor = true;
            // 
            // _hide
            // 
            this._hide.Location = new System.Drawing.Point(56, 12);
            this._hide.Name = "_hide";
            this._hide.Size = new System.Drawing.Size(47, 23);
            this._hide.TabIndex = 3;
            this._hide.Text = "0";
            this._hide.UseVisualStyleBackColor = true;
            this._hide.Click += new System.EventHandler(this.Hide_Click);
            // 
            // _show
            // 
            this._show.Location = new System.Drawing.Point(3, 12);
            this._show.Name = "_show";
            this._show.Size = new System.Drawing.Size(47, 23);
            this._show.TabIndex = 2;
            this._show.Text = "1";
            this._show.UseVisualStyleBackColor = true;
            this._show.Click += new System.EventHandler(this.Show_Click);
            // 
            // _mainPanel
            // 
            this._mainPanel.Controls.Add(this._pictureBox);
            this._mainPanel.Controls.Add(this._controlPanel);
            this._mainPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._mainPanel.Location = new System.Drawing.Point(0, 0);
            this._mainPanel.Name = "_mainPanel";
            this._mainPanel.Size = new System.Drawing.Size(1262, 758);
            this._mainPanel.TabIndex = 3;
            this._mainPanel.Visible = false;
            // 
            // _showParent
            // 
            this._showParent.BackColor = System.Drawing.Color.DimGray;
            this._showParent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._showParent.Location = new System.Drawing.Point(12, 764);
            this._showParent.Name = "_showParent";
            this._showParent.Size = new System.Drawing.Size(100, 13);
            this._showParent.TabIndex = 3;
            this._showParent.TextChanged += new System.EventHandler(this.ShowParent_TextChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1262, 829);
            this.Controls.Add(this._showParent);
            this.Controls.Add(this._mainPanel);
            this.Name = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).EndInit();
            this._controlPanel.ResumeLayout(false);
            this._controlPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._live)).EndInit();
            this._mainPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox _pictureBox;
        private System.Windows.Forms.Panel _controlPanel;
        private System.Windows.Forms.Button _hide;
        private System.Windows.Forms.Button _show;
        private System.Windows.Forms.CheckBox _alwaysHide;
        private System.Windows.Forms.PictureBox _live;
        private System.Windows.Forms.CheckBox _hideLive;
        private System.Windows.Forms.Button _close;
        private System.Windows.Forms.CheckBox _keepOpen;
        private System.Windows.Forms.Label _thread2;
        private System.Windows.Forms.Label _thread3;
        private System.Windows.Forms.Label _thread1;
        private System.Windows.Forms.Panel _mainPanel;
        private System.Windows.Forms.TextBox _showParent;
    }
}
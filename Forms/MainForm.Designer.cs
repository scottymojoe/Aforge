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
            this._controlPanel = new System.Windows.Forms.Panel();
            this._close = new System.Windows.Forms.Button();
            this._thread2 = new System.Windows.Forms.Label();
            this._thread3 = new System.Windows.Forms.Label();
            this._thread1 = new System.Windows.Forms.Label();
            this._live = new System.Windows.Forms.PictureBox();
            this._mainPanel = new System.Windows.Forms.Panel();
            this._showParent = new System.Windows.Forms.TextBox();
            this._timeOpen = new System.Windows.Forms.Label();
            this._controlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._live)).BeginInit();
            this._mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _controlPanel
            // 
            this._controlPanel.Controls.Add(this._timeOpen);
            this._controlPanel.Controls.Add(this._close);
            this._controlPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._controlPanel.Location = new System.Drawing.Point(0, 0);
            this._controlPanel.Name = "_controlPanel";
            this._controlPanel.Size = new System.Drawing.Size(372, 29);
            this._controlPanel.TabIndex = 2;
            // 
            // _close
            // 
            this._close.Location = new System.Drawing.Point(3, 2);
            this._close.Name = "_close";
            this._close.Size = new System.Drawing.Size(47, 23);
            this._close.TabIndex = 7;
            this._close.Text = "X";
            this._close.UseVisualStyleBackColor = true;
            this._close.Click += new System.EventHandler(this.Close_Click);
            // 
            // _thread2
            // 
            this._thread2.AutoSize = true;
            this._thread2.ForeColor = System.Drawing.Color.White;
            this._thread2.Location = new System.Drawing.Point(9, 822);
            this._thread2.Name = "_thread2";
            this._thread2.Size = new System.Drawing.Size(49, 13);
            this._thread2.TabIndex = 10;
            this._thread2.Text = "_thread2";
            // 
            // _thread3
            // 
            this._thread3.AutoSize = true;
            this._thread3.ForeColor = System.Drawing.Color.White;
            this._thread3.Location = new System.Drawing.Point(9, 849);
            this._thread3.Name = "_thread3";
            this._thread3.Size = new System.Drawing.Size(49, 13);
            this._thread3.TabIndex = 9;
            this._thread3.Text = "_thread3";
            // 
            // _thread1
            // 
            this._thread1.AutoSize = true;
            this._thread1.ForeColor = System.Drawing.Color.White;
            this._thread1.Location = new System.Drawing.Point(9, 797);
            this._thread1.Name = "_thread1";
            this._thread1.Size = new System.Drawing.Size(49, 13);
            this._thread1.TabIndex = 3;
            this._thread1.Text = "_thread1";
            // 
            // _live
            // 
            this._live.Dock = System.Windows.Forms.DockStyle.Fill;
            this._live.Location = new System.Drawing.Point(0, 0);
            this._live.Name = "_live";
            this._live.Size = new System.Drawing.Size(372, 758);
            this._live.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._live.TabIndex = 5;
            this._live.TabStop = false;
            // 
            // _mainPanel
            // 
            this._mainPanel.Controls.Add(this._controlPanel);
            this._mainPanel.Controls.Add(this._live);
            this._mainPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._mainPanel.Location = new System.Drawing.Point(0, 0);
            this._mainPanel.Name = "_mainPanel";
            this._mainPanel.Size = new System.Drawing.Size(372, 758);
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
            // _timeOpen
            // 
            this._timeOpen.AutoSize = true;
            this._timeOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._timeOpen.ForeColor = System.Drawing.Color.White;
            this._timeOpen.Location = new System.Drawing.Point(56, 2);
            this._timeOpen.Name = "_timeOpen";
            this._timeOpen.Size = new System.Drawing.Size(66, 24);
            this._timeOpen.TabIndex = 8;
            this._timeOpen.Text = "label1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(372, 535);
            this.Controls.Add(this._thread2);
            this.Controls.Add(this._showParent);
            this.Controls.Add(this._thread3);
            this.Controls.Add(this._mainPanel);
            this.Controls.Add(this._thread1);
            this.Name = "MainForm";
            this._controlPanel.ResumeLayout(false);
            this._controlPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._live)).EndInit();
            this._mainPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel _controlPanel;
        private System.Windows.Forms.PictureBox _live;
        private System.Windows.Forms.Button _close;
        private System.Windows.Forms.Label _thread2;
        private System.Windows.Forms.Label _thread3;
        private System.Windows.Forms.Label _thread1;
        private System.Windows.Forms.Panel _mainPanel;
        private System.Windows.Forms.TextBox _showParent;
        private System.Windows.Forms.Label _timeOpen;
    }
}
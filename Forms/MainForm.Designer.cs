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
            this._keep = new System.Windows.Forms.CheckBox();
            this._top = new System.Windows.Forms.CheckBox();
            this._timeOpen = new System.Windows.Forms.Label();
            this._close = new System.Windows.Forms.Button();
            this._messages = new System.Windows.Forms.ListBox();
            this._live = new System.Windows.Forms.PictureBox();
            this._mainPanel = new System.Windows.Forms.Panel();
            this._rightBorder = new System.Windows.Forms.Panel();
            this._leftBorder = new System.Windows.Forms.Panel();
            this._showParent = new System.Windows.Forms.TextBox();
            this._bottomBorder = new System.Windows.Forms.Panel();
            this._placeholder = new System.Windows.Forms.Panel();
            this._output = new System.Windows.Forms.TextBox();
            this._messagesPanel = new System.Windows.Forms.Panel();
            this._clearMessages = new System.Windows.Forms.Button();
            this._controlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._live)).BeginInit();
            this._mainPanel.SuspendLayout();
            this._placeholder.SuspendLayout();
            this._messagesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _controlPanel
            // 
            this._controlPanel.Controls.Add(this._keep);
            this._controlPanel.Controls.Add(this._top);
            this._controlPanel.Controls.Add(this._timeOpen);
            this._controlPanel.Controls.Add(this._close);
            this._controlPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._controlPanel.Location = new System.Drawing.Point(0, 0);
            this._controlPanel.Name = "_controlPanel";
            this._controlPanel.Size = new System.Drawing.Size(1161, 29);
            this._controlPanel.TabIndex = 2;
            // 
            // _keep
            // 
            this._keep.AutoSize = true;
            this._keep.Location = new System.Drawing.Point(86, 6);
            this._keep.Name = "_keep";
            this._keep.Size = new System.Drawing.Size(15, 14);
            this._keep.TabIndex = 13;
            this._keep.UseVisualStyleBackColor = true;
            // 
            // _top
            // 
            this._top.AutoSize = true;
            this._top.Location = new System.Drawing.Point(65, 6);
            this._top.Name = "_top";
            this._top.Size = new System.Drawing.Size(15, 14);
            this._top.TabIndex = 12;
            this._top.UseVisualStyleBackColor = true;
            this._top.CheckedChanged += new System.EventHandler(this.Top_CheckedChanged);
            // 
            // _timeOpen
            // 
            this._timeOpen.AutoSize = true;
            this._timeOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._timeOpen.ForeColor = System.Drawing.Color.White;
            this._timeOpen.Location = new System.Drawing.Point(118, 2);
            this._timeOpen.Name = "_timeOpen";
            this._timeOpen.Size = new System.Drawing.Size(66, 24);
            this._timeOpen.TabIndex = 8;
            this._timeOpen.Text = "label1";
            // 
            // _close
            // 
            this._close.Location = new System.Drawing.Point(12, 2);
            this._close.Name = "_close";
            this._close.Size = new System.Drawing.Size(47, 23);
            this._close.TabIndex = 7;
            this._close.Text = "X";
            this._close.UseVisualStyleBackColor = true;
            this._close.Click += new System.EventHandler(this.Close_Click);
            // 
            // _messages
            // 
            this._messages.BackColor = System.Drawing.Color.Black;
            this._messages.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._messages.Dock = System.Windows.Forms.DockStyle.Fill;
            this._messages.ForeColor = System.Drawing.Color.White;
            this._messages.Location = new System.Drawing.Point(0, 0);
            this._messages.Name = "_messages";
            this._messages.Size = new System.Drawing.Size(1137, 96);
            this._messages.TabIndex = 3;
            this._messages.DoubleClick += new System.EventHandler(this.Messages_DoubleClick);
            // 
            // _live
            // 
            this._live.Dock = System.Windows.Forms.DockStyle.Fill;
            this._live.Location = new System.Drawing.Point(0, 0);
            this._live.Name = "_live";
            this._live.Size = new System.Drawing.Size(1161, 758);
            this._live.TabIndex = 5;
            this._live.TabStop = false;
            // 
            // _mainPanel
            // 
            this._mainPanel.Controls.Add(this._rightBorder);
            this._mainPanel.Controls.Add(this._leftBorder);
            this._mainPanel.Controls.Add(this._controlPanel);
            this._mainPanel.Controls.Add(this._live);
            this._mainPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._mainPanel.Location = new System.Drawing.Point(0, 0);
            this._mainPanel.Name = "_mainPanel";
            this._mainPanel.Size = new System.Drawing.Size(1161, 758);
            this._mainPanel.TabIndex = 3;
            this._mainPanel.Visible = false;
            // 
            // _rightBorder
            // 
            this._rightBorder.Dock = System.Windows.Forms.DockStyle.Right;
            this._rightBorder.Location = new System.Drawing.Point(1151, 29);
            this._rightBorder.Name = "_rightBorder";
            this._rightBorder.Size = new System.Drawing.Size(10, 729);
            this._rightBorder.TabIndex = 7;
            // 
            // _leftBorder
            // 
            this._leftBorder.Dock = System.Windows.Forms.DockStyle.Left;
            this._leftBorder.Location = new System.Drawing.Point(0, 29);
            this._leftBorder.Name = "_leftBorder";
            this._leftBorder.Size = new System.Drawing.Size(10, 729);
            this._leftBorder.TabIndex = 6;
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
            // _bottomBorder
            // 
            this._bottomBorder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._bottomBorder.Location = new System.Drawing.Point(0, 888);
            this._bottomBorder.Name = "_bottomBorder";
            this._bottomBorder.Size = new System.Drawing.Size(1161, 10);
            this._bottomBorder.TabIndex = 11;
            // 
            // _placeholder
            // 
            this._placeholder.AutoScroll = true;
            this._placeholder.BackColor = System.Drawing.Color.Black;
            this._placeholder.Controls.Add(this._output);
            this._placeholder.ForeColor = System.Drawing.Color.White;
            this._placeholder.Location = new System.Drawing.Point(12, 885);
            this._placeholder.Name = "_placeholder";
            this._placeholder.Size = new System.Drawing.Size(200, 100);
            this._placeholder.TabIndex = 12;
            // 
            // _output
            // 
            this._output.BackColor = System.Drawing.Color.Black;
            this._output.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._output.Dock = System.Windows.Forms.DockStyle.Fill;
            this._output.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._output.ForeColor = System.Drawing.Color.White;
            this._output.Location = new System.Drawing.Point(0, 0);
            this._output.Multiline = true;
            this._output.Name = "_output";
            this._output.ReadOnly = true;
            this._output.Size = new System.Drawing.Size(200, 100);
            this._output.TabIndex = 13;
            this._output.Text = "_output";
            // 
            // _messagesPanel
            // 
            this._messagesPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._messagesPanel.Controls.Add(this._messages);
            this._messagesPanel.Location = new System.Drawing.Point(12, 783);
            this._messagesPanel.Name = "_messagesPanel";
            this._messagesPanel.Size = new System.Drawing.Size(1137, 96);
            this._messagesPanel.TabIndex = 13;
            // 
            // _clearMessages
            // 
            this._clearMessages.Location = new System.Drawing.Point(118, 759);
            this._clearMessages.Name = "_clearMessages";
            this._clearMessages.Size = new System.Drawing.Size(75, 23);
            this._clearMessages.TabIndex = 0;
            this._clearMessages.Text = "Clear";
            this._clearMessages.UseVisualStyleBackColor = true;
            this._clearMessages.Click += new System.EventHandler(this.ClearMessages_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1161, 898);
            this.Controls.Add(this._clearMessages);
            this.Controls.Add(this._messagesPanel);
            this.Controls.Add(this._placeholder);
            this.Controls.Add(this._bottomBorder);
            this.Controls.Add(this._showParent);
            this.Controls.Add(this._mainPanel);
            this.Name = "MainForm";
            this.Text = "Command";
            this._controlPanel.ResumeLayout(false);
            this._controlPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._live)).EndInit();
            this._mainPanel.ResumeLayout(false);
            this._placeholder.ResumeLayout(false);
            this._placeholder.PerformLayout();
            this._messagesPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel _controlPanel;
        private System.Windows.Forms.PictureBox _live;
        private System.Windows.Forms.Button _close;
        private System.Windows.Forms.ListBox _messages;
        private System.Windows.Forms.Panel _mainPanel;
        private System.Windows.Forms.TextBox _showParent;
        private System.Windows.Forms.Label _timeOpen;
        private System.Windows.Forms.Panel _rightBorder;
        private System.Windows.Forms.Panel _leftBorder;
        private System.Windows.Forms.Panel _bottomBorder;
        private System.Windows.Forms.CheckBox _top;
        private System.Windows.Forms.CheckBox _keep;
        private System.Windows.Forms.Panel _placeholder;
        private System.Windows.Forms.TextBox _output;
        private System.Windows.Forms.Panel _messagesPanel;
        private System.Windows.Forms.Button _clearMessages;
    }
}
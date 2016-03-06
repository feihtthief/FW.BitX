namespace FW.BitX.TestApp
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
			this.lvTrades = new System.Windows.Forms.ListView();
			this.chTrade_TimeStamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chTrade_Price = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chTrade_Volume = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.btnGetTrades = new System.Windows.Forms.Button();
			this.btnGetBalances = new System.Windows.Forms.Button();
			this.lblApiKeyPrompt = new System.Windows.Forms.Label();
			this.txtApiKey = new System.Windows.Forms.TextBox();
			this.txtApiSecret = new System.Windows.Forms.TextBox();
			this.lblApiSecretPrompt = new System.Windows.Forms.Label();
			this.lblMessagesPrompt = new System.Windows.Forms.Label();
			this.lblMessages = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lvTrades
			// 
			this.lvTrades.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chTrade_TimeStamp,
            this.chTrade_Price,
            this.chTrade_Volume});
			this.lvTrades.FullRowSelect = true;
			this.lvTrades.GridLines = true;
			this.lvTrades.Location = new System.Drawing.Point(202, 21);
			this.lvTrades.Name = "lvTrades";
			this.lvTrades.Size = new System.Drawing.Size(555, 176);
			this.lvTrades.TabIndex = 1;
			this.lvTrades.UseCompatibleStateImageBehavior = false;
			this.lvTrades.View = System.Windows.Forms.View.Details;
			// 
			// chTrade_TimeStamp
			// 
			this.chTrade_TimeStamp.Text = "TimeStamp";
			this.chTrade_TimeStamp.Width = 119;
			// 
			// chTrade_Price
			// 
			this.chTrade_Price.Text = "Price";
			this.chTrade_Price.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.chTrade_Price.Width = 86;
			// 
			// chTrade_Volume
			// 
			this.chTrade_Volume.Text = "Volume";
			this.chTrade_Volume.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.chTrade_Volume.Width = 346;
			// 
			// btnGetTrades
			// 
			this.btnGetTrades.Location = new System.Drawing.Point(28, 21);
			this.btnGetTrades.Name = "btnGetTrades";
			this.btnGetTrades.Size = new System.Drawing.Size(160, 23);
			this.btnGetTrades.TabIndex = 2;
			this.btnGetTrades.Text = "Get Trades";
			this.btnGetTrades.UseVisualStyleBackColor = true;
			this.btnGetTrades.Click += new System.EventHandler(this.btnGetTrades_Click);
			// 
			// btnGetBalances
			// 
			this.btnGetBalances.Location = new System.Drawing.Point(28, 50);
			this.btnGetBalances.Name = "btnGetBalances";
			this.btnGetBalances.Size = new System.Drawing.Size(160, 23);
			this.btnGetBalances.TabIndex = 3;
			this.btnGetBalances.Text = "Get Balances";
			this.btnGetBalances.UseVisualStyleBackColor = true;
			this.btnGetBalances.Click += new System.EventHandler(this.btnGetTransactions_Click);
			// 
			// lblApiKeyPrompt
			// 
			this.lblApiKeyPrompt.AutoSize = true;
			this.lblApiKeyPrompt.Location = new System.Drawing.Point(31, 221);
			this.lblApiKeyPrompt.Name = "lblApiKeyPrompt";
			this.lblApiKeyPrompt.Size = new System.Drawing.Size(45, 13);
			this.lblApiKeyPrompt.TabIndex = 4;
			this.lblApiKeyPrompt.Text = "API Key";
			// 
			// txtApiKey
			// 
			this.txtApiKey.Location = new System.Drawing.Point(134, 218);
			this.txtApiKey.Name = "txtApiKey";
			this.txtApiKey.PasswordChar = '*';
			this.txtApiKey.Size = new System.Drawing.Size(100, 20);
			this.txtApiKey.TabIndex = 5;
			// 
			// txtApiSecret
			// 
			this.txtApiSecret.Location = new System.Drawing.Point(134, 244);
			this.txtApiSecret.Name = "txtApiSecret";
			this.txtApiSecret.PasswordChar = '*';
			this.txtApiSecret.Size = new System.Drawing.Size(100, 20);
			this.txtApiSecret.TabIndex = 7;
			// 
			// lblApiSecretPrompt
			// 
			this.lblApiSecretPrompt.AutoSize = true;
			this.lblApiSecretPrompt.Location = new System.Drawing.Point(31, 247);
			this.lblApiSecretPrompt.Name = "lblApiSecretPrompt";
			this.lblApiSecretPrompt.Size = new System.Drawing.Size(58, 13);
			this.lblApiSecretPrompt.TabIndex = 6;
			this.lblApiSecretPrompt.Text = "API Secret";
			// 
			// lblMessagesPrompt
			// 
			this.lblMessagesPrompt.AutoSize = true;
			this.lblMessagesPrompt.Location = new System.Drawing.Point(25, 291);
			this.lblMessagesPrompt.Name = "lblMessagesPrompt";
			this.lblMessagesPrompt.Size = new System.Drawing.Size(58, 13);
			this.lblMessagesPrompt.TabIndex = 8;
			this.lblMessagesPrompt.Text = "Messages:";
			// 
			// lblMessages
			// 
			this.lblMessages.AutoSize = true;
			this.lblMessages.Location = new System.Drawing.Point(89, 291);
			this.lblMessages.Name = "lblMessages";
			this.lblMessages.Size = new System.Drawing.Size(61, 13);
			this.lblMessages.TabIndex = 8;
			this.lblMessages.Text = "[Messages]";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(769, 348);
			this.Controls.Add(this.lblMessages);
			this.Controls.Add(this.lblMessagesPrompt);
			this.Controls.Add(this.txtApiSecret);
			this.Controls.Add(this.lblApiSecretPrompt);
			this.Controls.Add(this.txtApiKey);
			this.Controls.Add(this.lblApiKeyPrompt);
			this.Controls.Add(this.btnGetBalances);
			this.Controls.Add(this.btnGetTrades);
			this.Controls.Add(this.lvTrades);
			this.Name = "MainForm";
			this.Text = "BitX Test App";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView lvTrades;
		private System.Windows.Forms.ColumnHeader chTrade_TimeStamp;
		private System.Windows.Forms.Button btnGetTrades;
		private System.Windows.Forms.ColumnHeader chTrade_Price;
		private System.Windows.Forms.ColumnHeader chTrade_Volume;
		private System.Windows.Forms.Button btnGetBalances;
		private System.Windows.Forms.Label lblApiKeyPrompt;
		private System.Windows.Forms.TextBox txtApiKey;
		private System.Windows.Forms.TextBox txtApiSecret;
		private System.Windows.Forms.Label lblApiSecretPrompt;
		private System.Windows.Forms.Label lblMessagesPrompt;
		private System.Windows.Forms.Label lblMessages;
	}
}


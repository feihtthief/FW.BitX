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
            this.btnGetTrades = new System.Windows.Forms.Button();
            this.chTrade_Price = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTrade_Volume = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.lvTrades.Location = new System.Drawing.Point(112, 65);
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
            // btnGetTrades
            // 
            this.btnGetTrades.Location = new System.Drawing.Point(28, 21);
            this.btnGetTrades.Name = "btnGetTrades";
            this.btnGetTrades.Size = new System.Drawing.Size(75, 23);
            this.btnGetTrades.TabIndex = 2;
            this.btnGetTrades.Text = "Get Trades";
            this.btnGetTrades.UseVisualStyleBackColor = true;
            this.btnGetTrades.Click += new System.EventHandler(this.btnGetTrades_Click);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 348);
            this.Controls.Add(this.btnGetTrades);
            this.Controls.Add(this.lvTrades);
            this.Name = "MainForm";
            this.Text = "BitX Test App";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvTrades;
        private System.Windows.Forms.ColumnHeader chTrade_TimeStamp;
        private System.Windows.Forms.Button btnGetTrades;
        private System.Windows.Forms.ColumnHeader chTrade_Price;
        private System.Windows.Forms.ColumnHeader chTrade_Volume;
    }
}


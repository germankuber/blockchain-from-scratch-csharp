
namespace MyBlockChain.UI
{
    partial class Wallet
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button2 = new System.Windows.Forms.Button();
            this.DataGridBlocks = new System.Windows.Forms.DataGridView();
            this.Hash = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.LblBalance3 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listView3 = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.BtnToWallet1 = new System.Windows.Forms.Button();
            this.BtnToWallet2 = new System.Windows.Forms.Button();
            this.TxtTransfer = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.LblBalance2 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.LblBalance = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.listView2 = new System.Windows.Forms.ListView();
            this.PublicKey = new System.Windows.Forms.ColumnHeader();
            this.button3 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridBlocks)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(16, 350);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(140, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Mine";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // DataGridBlocks
            // 
            this.DataGridBlocks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridBlocks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Hash});
            this.DataGridBlocks.Location = new System.Drawing.Point(18, 22);
            this.DataGridBlocks.MultiSelect = false;
            this.DataGridBlocks.Name = "DataGridBlocks";
            this.DataGridBlocks.RowTemplate.Height = 25;
            this.DataGridBlocks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridBlocks.Size = new System.Drawing.Size(1267, 365);
            this.DataGridBlocks.TabIndex = 3;
            this.DataGridBlocks.VirtualMode = true;
            // 
            // Hash
            // 
            this.Hash.DataPropertyName = "Hash";
            this.Hash.HeaderText = "Hash";
            this.Hash.Name = "Hash";
            this.Hash.Width = 400;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DataGridBlocks);
            this.groupBox1.Location = new System.Drawing.Point(12, 431);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1305, 404);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Blocks";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.LblBalance3);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.listView3);
            this.groupBox2.Controls.Add(this.BtnToWallet1);
            this.groupBox2.Controls.Add(this.BtnToWallet2);
            this.groupBox2.Controls.Add(this.TxtTransfer);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.LblBalance2);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.listView1);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.LblBalance);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.listView2);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Location = new System.Drawing.Point(31, 30);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1286, 379);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Wallet";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(916, 344);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(140, 23);
            this.button4.TabIndex = 17;
            this.button4.Text = "Mine";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // LblBalance3
            // 
            this.LblBalance3.AutoSize = true;
            this.LblBalance3.Location = new System.Drawing.Point(1039, 93);
            this.LblBalance3.Name = "LblBalance3";
            this.LblBalance3.Size = new System.Drawing.Size(48, 15);
            this.LblBalance3.TabIndex = 19;
            this.LblBalance3.Text = "balance";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(950, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 15);
            this.label3.TabIndex = 20;
            this.label3.Text = "Balance : ";
            // 
            // listView3
            // 
            this.listView3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listView3.HideSelection = false;
            this.listView3.Location = new System.Drawing.Point(916, 122);
            this.listView3.MultiSelect = false;
            this.listView3.Name = "listView3";
            this.listView3.Size = new System.Drawing.Size(290, 216);
            this.listView3.TabIndex = 18;
            this.listView3.UseCompatibleStateImageBehavior = false;
            this.listView3.View = System.Windows.Forms.View.Details;
            this.listView3.SelectedIndexChanged += new System.EventHandler(this.listView3_SelectedIndexChanged);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Address";
            this.columnHeader2.Width = 850;
            // 
            // BtnToWallet1
            // 
            this.BtnToWallet1.Location = new System.Drawing.Point(333, 203);
            this.BtnToWallet1.Name = "BtnToWallet1";
            this.BtnToWallet1.Size = new System.Drawing.Size(43, 23);
            this.BtnToWallet1.TabIndex = 16;
            this.BtnToWallet1.Text = "<<";
            this.BtnToWallet1.UseVisualStyleBackColor = true;
            this.BtnToWallet1.Click += new System.EventHandler(this.BtnToWallet1_Click);
            // 
            // BtnToWallet2
            // 
            this.BtnToWallet2.Location = new System.Drawing.Point(473, 203);
            this.BtnToWallet2.Name = "BtnToWallet2";
            this.BtnToWallet2.Size = new System.Drawing.Size(43, 23);
            this.BtnToWallet2.TabIndex = 15;
            this.BtnToWallet2.Text = ">>";
            this.BtnToWallet2.UseVisualStyleBackColor = true;
            this.BtnToWallet2.Click += new System.EventHandler(this.BtnToWallet2_Click);
            // 
            // TxtTransfer
            // 
            this.TxtTransfer.Location = new System.Drawing.Point(382, 204);
            this.TxtTransfer.Name = "TxtTransfer";
            this.TxtTransfer.Size = new System.Drawing.Size(85, 23);
            this.TxtTransfer.TabIndex = 14;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(538, 344);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Mine";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // LblBalance2
            // 
            this.LblBalance2.AutoSize = true;
            this.LblBalance2.Location = new System.Drawing.Point(661, 93);
            this.LblBalance2.Name = "LblBalance2";
            this.LblBalance2.Size = new System.Drawing.Size(48, 15);
            this.LblBalance2.TabIndex = 12;
            this.LblBalance2.Text = "balance";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(572, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 15);
            this.label2.TabIndex = 13;
            this.label2.Text = "Balance : ";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(538, 122);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(290, 216);
            this.listView1.TabIndex = 11;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Address";
            this.columnHeader1.Width = 850;
            // 
            // LblBalance
            // 
            this.LblBalance.AutoSize = true;
            this.LblBalance.Location = new System.Drawing.Point(139, 93);
            this.LblBalance.Name = "LblBalance";
            this.LblBalance.Size = new System.Drawing.Size(48, 15);
            this.LblBalance.TabIndex = 9;
            this.LblBalance.Text = "balance";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(50, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 15);
            this.label8.TabIndex = 10;
            this.label8.Text = "Balance : ";
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PublicKey});
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(16, 122);
            this.listView2.MultiSelect = false;
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(284, 216);
            this.listView2.TabIndex = 8;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            this.listView2.SelectedIndexChanged += new System.EventHandler(this.listView2_SelectedIndexChanged);
            // 
            // PublicKey
            // 
            this.PublicKey.Text = "Address";
            this.PublicKey.Width = 850;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1140, 13);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(140, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "Create New Wallet";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button1_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1323, 43);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(140, 23);
            this.button5.TabIndex = 21;
            this.button5.Text = "Refresh";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Wallet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1790, 875);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Wallet";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridBlocks)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView DataGridBlocks;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label LblBalance;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ColumnHeader PublicKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn Hash;
        private System.Windows.Forms.Label LblBalance2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button BtnToWallet1;
        private System.Windows.Forms.Button BtnToWallet2;
        private System.Windows.Forms.TextBox TxtTransfer;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label LblBalance3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView listView3;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button button5;
    }
}


using DarkUI.Forms;

namespace RSTBPatcher
{
    partial class MainForm : DarkForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox_Files = new DarkUI.Controls.DarkGroupBox();
            this.tlp_Files = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_Output = new System.Windows.Forms.TableLayoutPanel();
            this.txt_Output = new DarkUI.Controls.DarkTextBox();
            this.btn_Output = new DarkUI.Controls.DarkButton();
            this.lbl_Output = new DarkUI.Controls.DarkLabel();
            this.tlp_Mod = new System.Windows.Forms.TableLayoutPanel();
            this.txt_Mod = new DarkUI.Controls.DarkTextBox();
            this.btn_Mod = new DarkUI.Controls.DarkButton();
            this.lbl_Mod = new DarkUI.Controls.DarkLabel();
            this.tlp_RSTB = new System.Windows.Forms.TableLayoutPanel();
            this.txt_RSTB = new DarkUI.Controls.DarkTextBox();
            this.btn_RSTB = new DarkUI.Controls.DarkButton();
            this.lbl_RSTB = new DarkUI.Controls.DarkLabel();
            this.tlp_Main = new System.Windows.Forms.TableLayoutPanel();
            this.btn_Patch = new DarkUI.Controls.DarkButton();
            this.txt_Status = new DarkUI.Controls.DarkTextBox();
            this.groupBox_Files.SuspendLayout();
            this.tlp_Files.SuspendLayout();
            this.tlp_Output.SuspendLayout();
            this.tlp_Mod.SuspendLayout();
            this.tlp_RSTB.SuspendLayout();
            this.tlp_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_Files
            // 
            this.groupBox_Files.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.tlp_Main.SetColumnSpan(this.groupBox_Files, 2);
            this.groupBox_Files.Controls.Add(this.tlp_Files);
            this.groupBox_Files.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_Files.Location = new System.Drawing.Point(3, 3);
            this.groupBox_Files.Name = "groupBox_Files";
            this.groupBox_Files.Size = new System.Drawing.Size(712, 312);
            this.groupBox_Files.TabIndex = 0;
            this.groupBox_Files.TabStop = false;
            this.groupBox_Files.Text = "File Paths";
            // 
            // tlp_Files
            // 
            this.tlp_Files.ColumnCount = 1;
            this.tlp_Files.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Files.Controls.Add(this.tlp_Output, 0, 2);
            this.tlp_Files.Controls.Add(this.tlp_Mod, 0, 1);
            this.tlp_Files.Controls.Add(this.tlp_RSTB, 0, 0);
            this.tlp_Files.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Files.Location = new System.Drawing.Point(3, 27);
            this.tlp_Files.Name = "tlp_Files";
            this.tlp_Files.RowCount = 3;
            this.tlp_Files.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlp_Files.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlp_Files.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlp_Files.Size = new System.Drawing.Size(706, 282);
            this.tlp_Files.TabIndex = 1;
            // 
            // tlp_Output
            // 
            this.tlp_Output.ColumnCount = 2;
            this.tlp_Output.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 88.97764F));
            this.tlp_Output.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.02236F));
            this.tlp_Output.Controls.Add(this.txt_Output, 0, 1);
            this.tlp_Output.Controls.Add(this.btn_Output, 1, 1);
            this.tlp_Output.Controls.Add(this.lbl_Output, 0, 0);
            this.tlp_Output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Output.Location = new System.Drawing.Point(3, 191);
            this.tlp_Output.Name = "tlp_Output";
            this.tlp_Output.RowCount = 2;
            this.tlp_Output.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Output.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Output.Size = new System.Drawing.Size(700, 88);
            this.tlp_Output.TabIndex = 2;
            // 
            // txt_Output
            // 
            this.txt_Output.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txt_Output.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Output.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txt_Output.Location = new System.Drawing.Point(3, 47);
            this.txt_Output.Name = "txt_Output";
            this.txt_Output.Size = new System.Drawing.Size(616, 31);
            this.txt_Output.TabIndex = 0;
            this.txt_Output.TextChanged += new System.EventHandler(this.ValidatePaths);
            // 
            // btn_Output
            // 
            this.btn_Output.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_Output.Location = new System.Drawing.Point(625, 47);
            this.btn_Output.Name = "btn_Output";
            this.btn_Output.Padding = new System.Windows.Forms.Padding(5);
            this.btn_Output.Size = new System.Drawing.Size(72, 31);
            this.btn_Output.TabIndex = 1;
            this.btn_Output.Text = "...";
            this.btn_Output.Click += new System.EventHandler(this.Output_Click);
            // 
            // lbl_Output
            // 
            this.lbl_Output.AutoSize = true;
            this.lbl_Output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Output.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lbl_Output.Location = new System.Drawing.Point(3, 0);
            this.lbl_Output.Name = "lbl_Output";
            this.lbl_Output.Size = new System.Drawing.Size(616, 44);
            this.lbl_Output.TabIndex = 2;
            this.lbl_Output.Text = "Output RSTB File:";
            // 
            // tlp_Mod
            // 
            this.tlp_Mod.ColumnCount = 2;
            this.tlp_Mod.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 88.97764F));
            this.tlp_Mod.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.02236F));
            this.tlp_Mod.Controls.Add(this.txt_Mod, 0, 1);
            this.tlp_Mod.Controls.Add(this.btn_Mod, 1, 1);
            this.tlp_Mod.Controls.Add(this.lbl_Mod, 0, 0);
            this.tlp_Mod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Mod.Location = new System.Drawing.Point(3, 97);
            this.tlp_Mod.Name = "tlp_Mod";
            this.tlp_Mod.RowCount = 2;
            this.tlp_Mod.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Mod.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Mod.Size = new System.Drawing.Size(700, 88);
            this.tlp_Mod.TabIndex = 1;
            // 
            // txt_Mod
            // 
            this.txt_Mod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txt_Mod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Mod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Mod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txt_Mod.Location = new System.Drawing.Point(3, 47);
            this.txt_Mod.Name = "txt_Mod";
            this.txt_Mod.Size = new System.Drawing.Size(616, 31);
            this.txt_Mod.TabIndex = 0;
            this.txt_Mod.TextChanged += new System.EventHandler(this.ValidatePaths);
            // 
            // btn_Mod
            // 
            this.btn_Mod.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_Mod.Location = new System.Drawing.Point(625, 47);
            this.btn_Mod.Name = "btn_Mod";
            this.btn_Mod.Padding = new System.Windows.Forms.Padding(5);
            this.btn_Mod.Size = new System.Drawing.Size(72, 31);
            this.btn_Mod.TabIndex = 1;
            this.btn_Mod.Text = "...";
            this.btn_Mod.Click += new System.EventHandler(this.Mod_Click);
            // 
            // lbl_Mod
            // 
            this.lbl_Mod.AutoSize = true;
            this.lbl_Mod.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_Mod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lbl_Mod.Location = new System.Drawing.Point(3, 19);
            this.lbl_Mod.Name = "lbl_Mod";
            this.lbl_Mod.Size = new System.Drawing.Size(616, 25);
            this.lbl_Mod.TabIndex = 2;
            this.lbl_Mod.Text = "Mod Folder:";
            // 
            // tlp_RSTB
            // 
            this.tlp_RSTB.ColumnCount = 2;
            this.tlp_RSTB.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 88.97764F));
            this.tlp_RSTB.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.02236F));
            this.tlp_RSTB.Controls.Add(this.txt_RSTB, 0, 1);
            this.tlp_RSTB.Controls.Add(this.btn_RSTB, 1, 1);
            this.tlp_RSTB.Controls.Add(this.lbl_RSTB, 0, 0);
            this.tlp_RSTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_RSTB.Location = new System.Drawing.Point(3, 3);
            this.tlp_RSTB.Name = "tlp_RSTB";
            this.tlp_RSTB.RowCount = 2;
            this.tlp_RSTB.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_RSTB.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_RSTB.Size = new System.Drawing.Size(700, 88);
            this.tlp_RSTB.TabIndex = 0;
            // 
            // txt_RSTB
            // 
            this.txt_RSTB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txt_RSTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_RSTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_RSTB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txt_RSTB.Location = new System.Drawing.Point(3, 47);
            this.txt_RSTB.Name = "txt_RSTB";
            this.txt_RSTB.Size = new System.Drawing.Size(616, 31);
            this.txt_RSTB.TabIndex = 0;
            this.txt_RSTB.TextChanged += new System.EventHandler(this.ValidatePaths);
            // 
            // btn_RSTB
            // 
            this.btn_RSTB.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_RSTB.Location = new System.Drawing.Point(625, 47);
            this.btn_RSTB.Name = "btn_RSTB";
            this.btn_RSTB.Padding = new System.Windows.Forms.Padding(5);
            this.btn_RSTB.Size = new System.Drawing.Size(72, 31);
            this.btn_RSTB.TabIndex = 1;
            this.btn_RSTB.Text = "...";
            this.btn_RSTB.Click += new System.EventHandler(this.RSTB_Click);
            // 
            // lbl_RSTB
            // 
            this.lbl_RSTB.AutoSize = true;
            this.lbl_RSTB.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_RSTB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lbl_RSTB.Location = new System.Drawing.Point(3, 19);
            this.lbl_RSTB.Name = "lbl_RSTB";
            this.lbl_RSTB.Size = new System.Drawing.Size(616, 25);
            this.lbl_RSTB.TabIndex = 2;
            this.lbl_RSTB.Text = "Input RSTB File:";
            // 
            // tlp_Main
            // 
            this.tlp_Main.ColumnCount = 2;
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Main.Controls.Add(this.btn_Patch, 1, 1);
            this.tlp_Main.Controls.Add(this.groupBox_Files, 0, 0);
            this.tlp_Main.Controls.Add(this.txt_Status, 0, 1);
            this.tlp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Main.Location = new System.Drawing.Point(0, 0);
            this.tlp_Main.Name = "tlp_Main";
            this.tlp_Main.RowCount = 2;
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_Main.Size = new System.Drawing.Size(718, 424);
            this.tlp_Main.TabIndex = 1;
            // 
            // btn_Patch
            // 
            this.btn_Patch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Patch.Location = new System.Drawing.Point(368, 327);
            this.btn_Patch.Margin = new System.Windows.Forms.Padding(9);
            this.btn_Patch.Name = "btn_Patch";
            this.btn_Patch.Padding = new System.Windows.Forms.Padding(5);
            this.btn_Patch.Size = new System.Drawing.Size(341, 88);
            this.btn_Patch.TabIndex = 0;
            this.btn_Patch.Text = "Patch RSTB";
            this.btn_Patch.Click += new System.EventHandler(this.Patch_Click);
            // 
            // txt_Status
            // 
            this.txt_Status.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.txt_Status.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_Status.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txt_Status.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txt_Status.Location = new System.Drawing.Point(3, 397);
            this.txt_Status.Name = "txt_Status";
            this.txt_Status.ReadOnly = true;
            this.txt_Status.Size = new System.Drawing.Size(353, 24);
            this.txt_Status.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 424);
            this.Controls.Add(this.tlp_Main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "RSTBPatcher GUI v1.2";
            this.groupBox_Files.ResumeLayout(false);
            this.tlp_Files.ResumeLayout(false);
            this.tlp_Output.ResumeLayout(false);
            this.tlp_Output.PerformLayout();
            this.tlp_Mod.ResumeLayout(false);
            this.tlp_Mod.PerformLayout();
            this.tlp_RSTB.ResumeLayout(false);
            this.tlp_RSTB.PerformLayout();
            this.tlp_Main.ResumeLayout(false);
            this.tlp_Main.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox groupBox_Files;
        private System.Windows.Forms.TableLayoutPanel tlp_Main;
        private DarkUI.Controls.DarkButton btn_Patch;
        private DarkUI.Controls.DarkTextBox txt_Status;
        private System.Windows.Forms.TableLayoutPanel tlp_RSTB;
        private DarkUI.Controls.DarkTextBox txt_RSTB;
        private DarkUI.Controls.DarkButton btn_RSTB;
        private DarkUI.Controls.DarkLabel lbl_RSTB;
        private System.Windows.Forms.TableLayoutPanel tlp_Files;
        private System.Windows.Forms.TableLayoutPanel tlp_Output;
        private DarkUI.Controls.DarkTextBox txt_Output;
        private DarkUI.Controls.DarkButton btn_Output;
        private DarkUI.Controls.DarkLabel lbl_Output;
        private System.Windows.Forms.TableLayoutPanel tlp_Mod;
        private DarkUI.Controls.DarkTextBox txt_Mod;
        private DarkUI.Controls.DarkButton btn_Mod;
        private DarkUI.Controls.DarkLabel lbl_Mod;
    }
}
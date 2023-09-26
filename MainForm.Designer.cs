using DarkUI.Controls;
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
            groupBox_Files = new DarkGroupBox();
            tlp_Files = new System.Windows.Forms.TableLayoutPanel();
            tlp_Output = new System.Windows.Forms.TableLayoutPanel();
            txt_Output = new DarkTextBox();
            btn_Output = new DarkButton();
            lbl_Output = new DarkLabel();
            tlp_Mod = new System.Windows.Forms.TableLayoutPanel();
            txt_Mod = new DarkTextBox();
            btn_Mod = new DarkButton();
            lbl_Mod = new DarkLabel();
            tlp_RSTB = new System.Windows.Forms.TableLayoutPanel();
            txt_RSTB = new DarkTextBox();
            btn_RSTB = new DarkButton();
            lbl_RSTB = new DarkLabel();
            tlp_Main = new System.Windows.Forms.TableLayoutPanel();
            chk_DeleteMode = new DarkCheckBox();
            btn_Patch = new DarkButton();
            splitContainer_Main = new System.Windows.Forms.SplitContainer();
            rtb_Log = new System.Windows.Forms.RichTextBox();
            groupBox_Files.SuspendLayout();
            tlp_Files.SuspendLayout();
            tlp_Output.SuspendLayout();
            tlp_Mod.SuspendLayout();
            tlp_RSTB.SuspendLayout();
            tlp_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer_Main).BeginInit();
            splitContainer_Main.Panel1.SuspendLayout();
            splitContainer_Main.Panel2.SuspendLayout();
            splitContainer_Main.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox_Files
            // 
            groupBox_Files.BorderColor = System.Drawing.Color.FromArgb(51, 51, 51);
            tlp_Main.SetColumnSpan(groupBox_Files, 2);
            groupBox_Files.Controls.Add(tlp_Files);
            groupBox_Files.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox_Files.Location = new System.Drawing.Point(2, 2);
            groupBox_Files.Margin = new System.Windows.Forms.Padding(2);
            groupBox_Files.Name = "groupBox_Files";
            groupBox_Files.Padding = new System.Windows.Forms.Padding(2);
            groupBox_Files.Size = new System.Drawing.Size(666, 220);
            groupBox_Files.TabIndex = 0;
            groupBox_Files.TabStop = false;
            groupBox_Files.Text = "File Paths";
            // 
            // tlp_Files
            // 
            tlp_Files.ColumnCount = 1;
            tlp_Files.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tlp_Files.Controls.Add(tlp_Output, 0, 2);
            tlp_Files.Controls.Add(tlp_Mod, 0, 1);
            tlp_Files.Controls.Add(tlp_RSTB, 0, 0);
            tlp_Files.Dock = System.Windows.Forms.DockStyle.Fill;
            tlp_Files.Location = new System.Drawing.Point(2, 22);
            tlp_Files.Margin = new System.Windows.Forms.Padding(2);
            tlp_Files.Name = "tlp_Files";
            tlp_Files.RowCount = 3;
            tlp_Files.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tlp_Files.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tlp_Files.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tlp_Files.Size = new System.Drawing.Size(662, 196);
            tlp_Files.TabIndex = 1;
            // 
            // tlp_Output
            // 
            tlp_Output.ColumnCount = 2;
            tlp_Output.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 88.97764F));
            tlp_Output.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.02236F));
            tlp_Output.Controls.Add(txt_Output, 0, 1);
            tlp_Output.Controls.Add(btn_Output, 1, 1);
            tlp_Output.Controls.Add(lbl_Output, 0, 0);
            tlp_Output.Dock = System.Windows.Forms.DockStyle.Fill;
            tlp_Output.Location = new System.Drawing.Point(2, 132);
            tlp_Output.Margin = new System.Windows.Forms.Padding(2);
            tlp_Output.Name = "tlp_Output";
            tlp_Output.RowCount = 2;
            tlp_Output.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tlp_Output.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tlp_Output.Size = new System.Drawing.Size(658, 62);
            tlp_Output.TabIndex = 2;
            // 
            // txt_Output
            // 
            txt_Output.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            txt_Output.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txt_Output.Dock = System.Windows.Forms.DockStyle.Fill;
            txt_Output.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            txt_Output.Location = new System.Drawing.Point(2, 33);
            txt_Output.Margin = new System.Windows.Forms.Padding(2);
            txt_Output.Name = "txt_Output";
            txt_Output.Size = new System.Drawing.Size(581, 27);
            txt_Output.TabIndex = 0;
            txt_Output.TextChanged += ValidatePaths;
            // 
            // btn_Output
            // 
            btn_Output.Dock = System.Windows.Forms.DockStyle.Top;
            btn_Output.Location = new System.Drawing.Point(587, 33);
            btn_Output.Margin = new System.Windows.Forms.Padding(2);
            btn_Output.Name = "btn_Output";
            btn_Output.Padding = new System.Windows.Forms.Padding(4);
            btn_Output.Size = new System.Drawing.Size(69, 25);
            btn_Output.TabIndex = 1;
            btn_Output.Text = "...";
            btn_Output.Click += Output_Click;
            // 
            // lbl_Output
            // 
            lbl_Output.AutoSize = true;
            lbl_Output.Dock = System.Windows.Forms.DockStyle.Fill;
            lbl_Output.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lbl_Output.Location = new System.Drawing.Point(2, 0);
            lbl_Output.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            lbl_Output.Name = "lbl_Output";
            lbl_Output.Size = new System.Drawing.Size(581, 31);
            lbl_Output.TabIndex = 2;
            lbl_Output.Text = "Output RSTB File:";
            // 
            // tlp_Mod
            // 
            tlp_Mod.ColumnCount = 2;
            tlp_Mod.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 88.97764F));
            tlp_Mod.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.02236F));
            tlp_Mod.Controls.Add(txt_Mod, 0, 1);
            tlp_Mod.Controls.Add(btn_Mod, 1, 1);
            tlp_Mod.Controls.Add(lbl_Mod, 0, 0);
            tlp_Mod.Dock = System.Windows.Forms.DockStyle.Fill;
            tlp_Mod.Location = new System.Drawing.Point(2, 67);
            tlp_Mod.Margin = new System.Windows.Forms.Padding(2);
            tlp_Mod.Name = "tlp_Mod";
            tlp_Mod.RowCount = 2;
            tlp_Mod.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tlp_Mod.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tlp_Mod.Size = new System.Drawing.Size(658, 61);
            tlp_Mod.TabIndex = 1;
            // 
            // txt_Mod
            // 
            txt_Mod.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            txt_Mod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txt_Mod.Dock = System.Windows.Forms.DockStyle.Fill;
            txt_Mod.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            txt_Mod.Location = new System.Drawing.Point(2, 32);
            txt_Mod.Margin = new System.Windows.Forms.Padding(2);
            txt_Mod.Name = "txt_Mod";
            txt_Mod.Size = new System.Drawing.Size(581, 27);
            txt_Mod.TabIndex = 0;
            txt_Mod.TextChanged += ValidatePaths;
            // 
            // btn_Mod
            // 
            btn_Mod.Dock = System.Windows.Forms.DockStyle.Top;
            btn_Mod.Location = new System.Drawing.Point(587, 32);
            btn_Mod.Margin = new System.Windows.Forms.Padding(2);
            btn_Mod.Name = "btn_Mod";
            btn_Mod.Padding = new System.Windows.Forms.Padding(4);
            btn_Mod.Size = new System.Drawing.Size(69, 25);
            btn_Mod.TabIndex = 1;
            btn_Mod.Text = "...";
            btn_Mod.Click += Mod_Click;
            // 
            // lbl_Mod
            // 
            lbl_Mod.AutoSize = true;
            lbl_Mod.Dock = System.Windows.Forms.DockStyle.Bottom;
            lbl_Mod.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lbl_Mod.Location = new System.Drawing.Point(2, 10);
            lbl_Mod.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            lbl_Mod.Name = "lbl_Mod";
            lbl_Mod.Size = new System.Drawing.Size(581, 20);
            lbl_Mod.TabIndex = 2;
            lbl_Mod.Text = "Mod Folder:";
            // 
            // tlp_RSTB
            // 
            tlp_RSTB.ColumnCount = 2;
            tlp_RSTB.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 88.97764F));
            tlp_RSTB.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.02236F));
            tlp_RSTB.Controls.Add(txt_RSTB, 0, 1);
            tlp_RSTB.Controls.Add(btn_RSTB, 1, 1);
            tlp_RSTB.Controls.Add(lbl_RSTB, 0, 0);
            tlp_RSTB.Dock = System.Windows.Forms.DockStyle.Fill;
            tlp_RSTB.Location = new System.Drawing.Point(2, 2);
            tlp_RSTB.Margin = new System.Windows.Forms.Padding(2);
            tlp_RSTB.Name = "tlp_RSTB";
            tlp_RSTB.RowCount = 2;
            tlp_RSTB.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tlp_RSTB.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tlp_RSTB.Size = new System.Drawing.Size(658, 61);
            tlp_RSTB.TabIndex = 0;
            // 
            // txt_RSTB
            // 
            txt_RSTB.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            txt_RSTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txt_RSTB.Dock = System.Windows.Forms.DockStyle.Fill;
            txt_RSTB.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            txt_RSTB.Location = new System.Drawing.Point(2, 32);
            txt_RSTB.Margin = new System.Windows.Forms.Padding(2);
            txt_RSTB.Name = "txt_RSTB";
            txt_RSTB.Size = new System.Drawing.Size(581, 27);
            txt_RSTB.TabIndex = 0;
            txt_RSTB.TextChanged += ValidatePaths;
            // 
            // btn_RSTB
            // 
            btn_RSTB.Dock = System.Windows.Forms.DockStyle.Top;
            btn_RSTB.Location = new System.Drawing.Point(587, 32);
            btn_RSTB.Margin = new System.Windows.Forms.Padding(2);
            btn_RSTB.Name = "btn_RSTB";
            btn_RSTB.Padding = new System.Windows.Forms.Padding(4);
            btn_RSTB.Size = new System.Drawing.Size(69, 25);
            btn_RSTB.TabIndex = 1;
            btn_RSTB.Text = "...";
            btn_RSTB.Click += RSTB_Click;
            // 
            // lbl_RSTB
            // 
            lbl_RSTB.AutoSize = true;
            lbl_RSTB.Dock = System.Windows.Forms.DockStyle.Bottom;
            lbl_RSTB.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lbl_RSTB.Location = new System.Drawing.Point(2, 10);
            lbl_RSTB.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            lbl_RSTB.Name = "lbl_RSTB";
            lbl_RSTB.Size = new System.Drawing.Size(581, 20);
            lbl_RSTB.TabIndex = 2;
            lbl_RSTB.Text = "Input RSTB File:";
            // 
            // tlp_Main
            // 
            tlp_Main.ColumnCount = 2;
            tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tlp_Main.Controls.Add(chk_DeleteMode, 0, 1);
            tlp_Main.Controls.Add(btn_Patch, 1, 1);
            tlp_Main.Controls.Add(groupBox_Files, 0, 0);
            tlp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            tlp_Main.Location = new System.Drawing.Point(0, 0);
            tlp_Main.Margin = new System.Windows.Forms.Padding(2);
            tlp_Main.Name = "tlp_Main";
            tlp_Main.RowCount = 2;
            tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            tlp_Main.Size = new System.Drawing.Size(670, 299);
            tlp_Main.TabIndex = 1;
            // 
            // chk_DeleteMode
            // 
            chk_DeleteMode.Anchor = System.Windows.Forms.AnchorStyles.Right;
            chk_DeleteMode.AutoSize = true;
            chk_DeleteMode.Location = new System.Drawing.Point(152, 249);
            chk_DeleteMode.Name = "chk_DeleteMode";
            chk_DeleteMode.Size = new System.Drawing.Size(180, 24);
            chk_DeleteMode.TabIndex = 2;
            chk_DeleteMode.Text = "Remove CRC32 Entries";
            // 
            // btn_Patch
            // 
            btn_Patch.Dock = System.Windows.Forms.DockStyle.Fill;
            btn_Patch.Location = new System.Drawing.Point(342, 231);
            btn_Patch.Margin = new System.Windows.Forms.Padding(7);
            btn_Patch.Name = "btn_Patch";
            btn_Patch.Padding = new System.Windows.Forms.Padding(4);
            btn_Patch.Size = new System.Drawing.Size(321, 61);
            btn_Patch.TabIndex = 0;
            btn_Patch.Text = "Patch RSTB";
            btn_Patch.Click += Patch_Click;
            // 
            // splitContainer_Main
            // 
            splitContainer_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer_Main.ForeColor = System.Drawing.Color.Silver;
            splitContainer_Main.Location = new System.Drawing.Point(0, 0);
            splitContainer_Main.Name = "splitContainer_Main";
            splitContainer_Main.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_Main.Panel1
            // 
            splitContainer_Main.Panel1.Controls.Add(tlp_Main);
            // 
            // splitContainer_Main.Panel2
            // 
            splitContainer_Main.Panel2.Controls.Add(rtb_Log);
            splitContainer_Main.Size = new System.Drawing.Size(670, 409);
            splitContainer_Main.SplitterDistance = 299;
            splitContainer_Main.TabIndex = 2;
            // 
            // rtb_Log
            // 
            rtb_Log.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            rtb_Log.BorderStyle = System.Windows.Forms.BorderStyle.None;
            rtb_Log.Dock = System.Windows.Forms.DockStyle.Fill;
            rtb_Log.ForeColor = System.Drawing.Color.Silver;
            rtb_Log.Location = new System.Drawing.Point(0, 0);
            rtb_Log.Name = "rtb_Log";
            rtb_Log.Size = new System.Drawing.Size(670, 106);
            rtb_Log.TabIndex = 3;
            rtb_Log.Text = "";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(670, 409);
            Controls.Add(splitContainer_Main);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(2);
            Name = "MainForm";
            Text = "RSTBPatcher GUI";
            groupBox_Files.ResumeLayout(false);
            tlp_Files.ResumeLayout(false);
            tlp_Output.ResumeLayout(false);
            tlp_Output.PerformLayout();
            tlp_Mod.ResumeLayout(false);
            tlp_Mod.PerformLayout();
            tlp_RSTB.ResumeLayout(false);
            tlp_RSTB.PerformLayout();
            tlp_Main.ResumeLayout(false);
            tlp_Main.PerformLayout();
            splitContainer_Main.Panel1.ResumeLayout(false);
            splitContainer_Main.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer_Main).EndInit();
            splitContainer_Main.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DarkGroupBox groupBox_Files;
        private System.Windows.Forms.TableLayoutPanel tlp_Main;
        private DarkButton btn_Patch;
        private DarkTextBox txt_Status;
        private System.Windows.Forms.TableLayoutPanel tlp_RSTB;
        private DarkTextBox txt_RSTB;
        private DarkButton btn_RSTB;
        private DarkLabel lbl_RSTB;
        private System.Windows.Forms.TableLayoutPanel tlp_Files;
        private System.Windows.Forms.TableLayoutPanel tlp_Output;
        private DarkTextBox txt_Output;
        private DarkButton btn_Output;
        private DarkLabel lbl_Output;
        private System.Windows.Forms.TableLayoutPanel tlp_Mod;
        private DarkTextBox txt_Mod;
        private DarkButton btn_Mod;
        private DarkLabel lbl_Mod;
        private DarkCheckBox chk_DeleteMode;
        private System.Windows.Forms.SplitContainer splitContainer_Main;
        private System.Windows.Forms.RichTextBox rtb_Log;
    }
}
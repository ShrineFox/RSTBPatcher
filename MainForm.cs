using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DarkUI.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using static RSTBPatcher.Program;
using TGE.SimpleCommandLine;

namespace RSTBPatcher
{
    public partial class MainForm : DarkForm
    {
        public MainForm()
        {
            InitializeComponent();
            txt_Status.Text = $"{Program.Version} by ShrineFox";
        }

        public static void SetOptions()
        {

        }

        private void RSTB_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.Title = "Input RSTB File";
            dialog.Filters.Add(new CommonFileDialogFilter("RSTB File", ".srsizetable, .rsizetable, .txt"));
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                txt_RSTB.Text = dialog.FileName;
        }

        private void Mod_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.Title = "Mod Folder";
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                txt_Mod.Text = dialog.FileName;
        }

        private void Output_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.Title = "Output RSTB File";
            dialog.Filters.Add(new CommonFileDialogFilter("RSTB File", ".srsizetable, .rsizetable, .txt"));
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                txt_Output.Text = dialog.FileName;
        }

        private void Patch_Click(object sender, EventArgs e)
        {
            Program.Options = SimpleCommandLineParser.Default.Parse<ProgramOptions>(new string[] { "-i", $"{txt_RSTB.Text}", "-m", $"{txt_Mod.Text}", "-o", $"{txt_Output.Text}" });
            Program.DoOptions();
            txt_Status.Text = $"[{DateTime.Now.ToString("MM/dd/yyyy HH:mm tt")}] Done patching RSTB.";
        }

        private void ValidatePaths(object sender, EventArgs e)
        {
            if (File.Exists(txt_RSTB.Text) && Directory.Exists(txt_Mod.Text)
                && !string.IsNullOrEmpty(txt_Output.Text))
                btn_Patch.Enabled = true;
            else
                btn_Patch.Enabled = false;
        }
    }
}

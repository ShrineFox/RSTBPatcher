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
using System.Linq;
using ShrineFox.IO;

namespace RSTBPatcher
{
    public partial class MainForm : DarkForm
    {
        public MainForm()
        {
            InitializeComponent();
            Output.Logging = true;
            Output.LogControl = rtb_Log;

            Output.Log($"{Program.Version} by ShrineFox");
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
            List<string> args = new List<string>() { "-i", $"{txt_RSTB.Text}", "-m", $"{txt_Mod.Text}", "-o", $"{txt_Output.Text}" };

            if (chk_DeleteMode.Checked)
            {
                args.Add("-da");
                args.Add("true");
            }

            Program.Options = SimpleCommandLineParser.Default.Parse<ProgramOptions>(args.ToArray());
            Program.DoOptions();
            Output.Log($"Done patching RSTB.");
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

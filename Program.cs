﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using Soft160.Data.Cryptography;
using TGE.SimpleCommandLine;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ShrineFox.IO;

namespace RSTBPatcher
{
    class Program
    {
        public static string Version { get; set; } = "RSTBPatcher v1.2.2";
        public static ProgramOptions Options { get; set; }
        public class ProgramOptions
        {
            [Option("i", "input", "filepath", "Specifies the path to the ResourceTable file to use as input.", Required = true)]
            public string Input { get; set; }

            [Option("o", "output", "filepath", "Specifies the path to the ResourceTable file to save the output to.")]
            public string Output { get; set; }

            [Option("m", "mod-dir", "directorypath", "Directory containing mod filestructure to update ResourceTable with.")]
            public string ModDir { get; set; }

            [Option("c", "check", "filepath", "Shows the CRC32 and size of a filepath in the RSTB if it exists.")]
            public string Check { get; set; }

            [Option("n", "named", "true|false", "When true, the full path will be saved rather than converted to CRC32.")]
            public bool UseNamedTable { get; set; } = false;

            [Option("u", "unknown", "true|false", "When true, the unknown value will be set to 1 instead of 0.")]
            public bool SetUnknown { get; set; } = false;

            [Option("sp", "skipprocessing", "true|false", "When true, uncompressed size won't be automatically derived from compressed files, and extensions won't be modified.")]
            public bool SkipProcessing { get; set; } = false;

            [Option("da", "delete-all", "true|false", "When true, all updated entries using -m will be deleted instead.")]
            public bool DeleteAll { get; set; } = false;

            [Group("a")]
            public AddOptions Add { get; set; }

            [Group("d")]
            public DeleteOptions Delete { get; set; }

            public class AddOptions
            {
                [Option("p", "path", "filepath", "The path (or CRC32) of the entry to add to the ResourceTable. If it exists, it will be replaced.")]
                public string Path { get; set; }

                [Option("s", "size", "integer", "The size to reserve for the file. By default, this will be calculated automatically.")]
                public int Size { get; set; } = -1;
            }

            public class DeleteOptions
            {
                [Option("p", "path", "The path (or CRC32) of the entry to delete from the ResourceTable.")]
                public string Path { get; set; }
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);
        private const int ATTACH_PARENT_PROCESS = -1;

        [STAThread]
        static void Main(string[] args)
        {
            // Activate console output
            AttachConsole(ATTACH_PARENT_PROCESS);
            Output.Logging = true;

            if (args.Length > 0)
            {
                // Process commandline arguments
                try
                {
                    string about = SimpleCommandLineFormatter.Default.FormatAbout<ProgramOptions>("ShrineFox", 
                        $"{Version} - ResourceSizeTable parser and patcher for Nintendo Switch games.");
                    Output.Log(about);
                    Options = SimpleCommandLineParser.Default.Parse<ProgramOptions>(args);
                }
                catch (Exception e)
                {
                    Output.Log(e.Message);
                    return;
                }
                Output.Log("Reading Resource Table data...");

                DoOptions();
            }
            else
            {
                // Set DPI Awareness
                Application.EnableVisualStyles();
                if (Environment.OSVersion.Version.Major >= 6)
                    SetProcessDPIAware();
                Application.SetCompatibleTextRenderingDefault(false);
                // Launch GUI
                Application.Run(new MainForm());
            }
            
        }

        public static void DoOptions()
        {
            bool fileChanged = false;

            // Decode Yaz0
            if (Path.GetExtension(Options.Input) == ".srsizetable")
            {
                string newPath = Path.Combine(Path.GetDirectoryName(Options.Input), Path.GetFileNameWithoutExtension(Options.Input) + ".rsizetable");
                Output.Log($"Decompressing RSTB to new file: {newPath}");
                Yaz0.Decode(Options.Input, newPath);
                Options.Input = newPath;
            }

            RSTB rstb = LoadRSTB();

            if (rstb != null)
            {
                if (!string.IsNullOrEmpty(Options.Check))
                {
                    string path = ProcessPath(Options.Check, false).Item1;
                    RSTB.Check(rstb, path);
                }
                else if (!string.IsNullOrEmpty(Options.Add.Path))
                {
                    // Add manually specified path/size from RSTB
                    var processedPath = ProcessPath(Options.Add.Path);
                    string relativePath = processedPath.Item1;
                    int size = processedPath.Item2;
                    rstb = RSTB.Add(rstb, relativePath, size, Options.UseNamedTable, Options.SetUnknown);
                    fileChanged = true;
                }
                else if (!string.IsNullOrEmpty(Options.Delete.Path))
                {
                    // Remove manually specified path from RSTB
                    string path = ProcessPath(Options.Delete.Path, false).Item1;
                    rstb = RSTB.Delete(rstb, path, Options.UseNamedTable);
                    fileChanged = true;
                }
                else if (!string.IsNullOrEmpty(Options.ModDir))
                {
                    // Update RSTB with files from mod directory
                    if (Directory.Exists(Options.ModDir))
                    {
                        foreach (var file in Directory.GetFiles(Options.ModDir, "*", SearchOption.AllDirectories).Where(x => !x.Contains(".git")))
                        {
                            var processedPath = ProcessPath(file);
                            string path = processedPath.Item1;
                            int size = processedPath.Item2;
                            if (!Options.DeleteAll)
                                rstb = RSTB.Add(rstb, path, size, Options.UseNamedTable, Options.SetUnknown);
                            else
                                rstb = RSTB.Delete(rstb, path);

                            fileChanged = true;
                        }
                    }
                    else
                        Output.Log($"Could not find mod directory: \"{Options.ModDir}\"");
                }

                if (fileChanged || Path.GetExtension(Options.Input).ToLower() == ".txt" || Path.GetExtension(Options.Output).ToLower() == ".txt")
                {
                    if (!string.IsNullOrEmpty(Options.Output))
                    {
                        if (Path.GetExtension(Options.Output).ToLower() == ".txt")
                        {
                            Output.Log("Saving RSTB as text document...");
                            RSTB.DumpTxt(rstb, Options.Input + ".txt");
                        }
                        else
                        {
                            Output.Log("Saving and compressing new RSTB...");
                            RSTB.Save(rstb, Options.Output);
                        }
                        Output.Log($"Done, RSTB file saved to: {Options.Output}");
                    }
                    else
                    {
                        Output.Log("Done, output file was no specified so new RSTB was not saved.");
                    }
                }
                else
                    Output.Log("Done, no changes were made so new RSTB was not saved.");
            }
        }

        private static Tuple<string, int> ProcessPath(string file, bool print = true)
        {
            string relativePath = file.Replace("\\", "/");
            int size = Options.Add.Size;

            if (File.Exists(file))
            {
                if (!string.IsNullOrEmpty(Options.ModDir))
                {
                    relativePath = Path.GetRelativePath(Options.ModDir, relativePath).Replace("\\", "/");
                    Output.Log($"Made path relative to mod directory: {relativePath}");
                }

                if (Path.GetExtension(relativePath) == ".zs" && !Options.SkipProcessing)
                {
                    // Get size of file before ZSTD compression
                    relativePath = relativePath.Replace(".zs", "");
                    Output.Log($"Removed .zs extension from path: {relativePath}");
                    int decompressedSize = Compression.DecompressedSize(file);
                    if (decompressedSize != -1 && size == -1 && print)
                    {
                        Output.Log($"Got ZSTD decompressed size: {decompressedSize}");
                        size = decompressedSize + 50000;
                        Output.Log($"Added 50,000 bytes of padding to entry. Size is now: {size}");
                    }
                }

                // Automatically calculate size of file if not manually specified
                // or determined through processing
                if (size == -1)
                    size = Convert.ToInt32(new FileInfo(file).Length);
            }
            else
                Output.Log($"Could not find local file, skipping processing path: {file}");

            return new Tuple<string, int>(relativePath, size);
        }

        private static RSTB LoadRSTB()
        {
            if (File.Exists(Options.Input))
            {
                switch(Path.GetExtension(Options.Input))
                {
                    case ".rsizetable":
                        return RSTB.Load(Options.Input);
                    case ".txt":
                        return RSTB.LoadTxt(Options.Input);
                    default:
                        Output.Log($"Unknown extension: \"{Path.GetExtension(Options.Input)}\"" +
                            $"\n\tThis tool can only parse: *.srsizetable, *.rsizetable, *.txt");
                        break;
                }
            }
            else
            {
                Output.Log($"Could not find input file: \"{Options.Input}\"");
            }

            return null;
        }
    }
}

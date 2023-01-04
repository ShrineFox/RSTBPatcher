using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using Soft160.Data.Cryptography;
using TGE.SimpleCommandLine;

namespace RSTBPatcher
{
    class Program
    {
        public static ProgramOptions Options { get; private set; }
        public class ProgramOptions
        {
            [Option("i", "input", "filepath", "Specifies the path to the ResourceTable file to use as input.", Required = true)]
            public string Input { get; set; }

            [Option("o", "output", "filepath", "Specifies the path to the ResourceTable file to save the output to.", Required = true)]
            public string Output { get; set; }

            [Option("m", "mod-dir", "directorypath", "Directory containing mod filestructure to update ResourceTable with.")]
            public string ModDir { get; set; }

            [Option("t", "txt", "true|false", "When true, dump output RSTB data to a text file.")]
            public bool DumpTxt { get; set; } = false;

            [Option("n", "named", "true|false", "When true, the full path will be saved rather than converted to CRC32.")]
            public bool UseNamedTable { get; set; } = false;

            [Option("u", "unknown", "true|false", "When true, the unknown value will be set to 1 instead of 0.")]
            public bool SetUnknown { get; set; } = false;

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

        static void Main(string[] args)
        {
            try
            {
                string about = SimpleCommandLineFormatter.Default.FormatAbout<ProgramOptions>("ShrineFox", "ResourceSizeTable parser and patcher for Nintendo Switch games.");
                Console.WriteLine(about);
                Options = SimpleCommandLineParser.Default.Parse<ProgramOptions>(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            Console.WriteLine("Reading Resource Table data...");

            DoOptions();
        }

        private static void DoOptions()
        {

            // Decode Yaz0
            if (Path.GetExtension(Options.Input) == ".srsizetable")
            {
                string newPath = Path.Combine(Path.GetDirectoryName(Options.Input), Path.GetFileNameWithoutExtension(Options.Input) + ".rsizetable");
                Console.WriteLine($"Decompressing RSTB to new file: {newPath}");
                Yaz0.Decode(Options.Input, newPath);
                Options.Input = newPath;
            }

            RSTB rstb = LoadRSTB();

            if (rstb != null)
            {
                // Update table with files from mod directory
                if (!string.IsNullOrEmpty(Options.ModDir))
                {
                    if (Directory.Exists(Options.ModDir))
                    {
                        foreach (var file in Directory.GetFiles(Options.ModDir, "*", SearchOption.AllDirectories))
                        {
                            string relativePath = Path.GetRelativePath(Options.ModDir, file).Replace("\\","/").Replace(".zs","");
                            long size = new FileInfo(file).Length;
                            rstb = RSTB.Add(rstb, relativePath, Convert.ToInt32(size), Options.UseNamedTable, Options.SetUnknown);
                        }
                    }
                    else
                        Console.WriteLine($"Could not find mod directory: \"{Options.ModDir}\"");
                }
                else if (!string.IsNullOrEmpty(Options.Add.Path))
                {
                    int size = Options.Add.Size;
                    if (size == -1)
                    {
                        // Try to get file size if none specified
                        if (File.Exists(Options.Add.Path))
                        {
                            size = Convert.ToInt32(new FileInfo(Options.Add.Path).Length);
                        }
                        else
                        {
                            size = 0; // Set size to 0
                            Console.WriteLine($"Could not find file path: \"{Options.Add.Path}\"");
                        }
                    }
                    rstb = RSTB.Add(rstb, Options.Add.Path, size, Options.UseNamedTable, Options.SetUnknown);
                }
                else if (!string.IsNullOrEmpty(Options.Delete.Path))
                {
                    rstb = RSTB.Delete(rstb, Options.Delete.Path, Options.UseNamedTable);
                }

                // Output new RSTB and optionally .txt
                if (Options.DumpTxt)
                {
                    RSTB.DumpTxt(rstb, Options.Input + ".txt");
                    Console.WriteLine($"Dumped RSTB to .txt: {Options.Input}.txt");
                }

                Console.WriteLine("Saving and compressing new RSTB...");
                RSTB.Save(rstb, Options.Output);
                Console.WriteLine($"Done, RSTB file saved to: {Options.Output}");
            }
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
                        Console.WriteLine($"Unknown extension: \"{Path.GetExtension(Options.Input)}\"" +
                            $"\n\tThis tool can only parse: *.srsizetable, *.rsizetable, *.txt");
                        break;
                }
            }
            else
            {
                Console.WriteLine($"Could not find input file: \"{Options.Input}\"");
            }

            return null;
        }
    }
}

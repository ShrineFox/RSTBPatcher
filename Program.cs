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

            [Option("o", "output", "filepath", "Specifies the path to the ResourceTable file to save the output to.")]
            public string Output { get; set; }

            [Option("m", "mod-dir", "directorypath", "Directory containing mod filestructure to update ResourceTable with.")]
            public string ModDir { get; set; }

            [Option("t", "txt", "true|false", "When true, dump output RSTB data to a text file.")]
            public bool DumpTxt { get; set; } = false;

            [Option("c", "check", "filepath", "Shows the CRC32 and size of a filepath in the RSTB if it exists.")]
            public string Check { get; set; }

            [Option("n", "named", "true|false", "When true, the full path will be saved rather than converted to CRC32.")]
            public bool UseNamedTable { get; set; } = false;

            [Option("u", "unknown", "true|false", "When true, the unknown value will be set to 1 instead of 0.")]
            public bool SetUnknown { get; set; } = false;

            [Option("sp", "skipprocessing", "true|false", "When true, uncompressed size won't be automatically derived from compressed files, and extensions won't be modified.")]
            public bool SkipProcessing { get; set; } = false;

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
            bool fileChanged = false;

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
                        foreach (var file in Directory.GetFiles(Options.ModDir, "*", SearchOption.AllDirectories))
                        {
                            var processedPath = ProcessPath(file);
                            string path = processedPath.Item1;
                            int size = processedPath.Item2;
                            rstb = RSTB.Add(rstb, path, size, Options.UseNamedTable, Options.SetUnknown);
                            fileChanged = true;
                        }
                    }
                    else
                        Console.WriteLine($"Could not find mod directory: \"{Options.ModDir}\"");
                }

                // Output new RSTB and optionally .txt
                if (Options.DumpTxt)
                {
                    RSTB.DumpTxt(rstb, Options.Input + ".txt");
                    Console.WriteLine($"Dumped RSTB to .txt: {Options.Input}.txt");
                }

                if (fileChanged || Path.GetExtension(Options.Input) == ".txt")
                {
                    if (!string.IsNullOrEmpty(Options.Output))
                    {
                        Console.WriteLine("Saving and compressing new RSTB...");
                        RSTB.Save(rstb, Options.Output);
                        Console.WriteLine($"Done, RSTB file saved to: {Options.Output}");
                    }
                    else
                    {
                        Console.WriteLine("Done, output file was no specified so new RSTB was not saved.");
                    }
                }
                else
                    Console.WriteLine("Done, no changes were made so new RSTB was not saved.");
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
                    Console.WriteLine($"Made path relative to mod directory: {relativePath}");
                }

                if (Path.GetExtension(relativePath) == ".zs" && !Options.SkipProcessing)
                {
                    // Get size of file before ZSTD compression
                    relativePath = relativePath.Replace(".zs", "");
                    Console.WriteLine($"Removed .zs extension from path: {relativePath}");
                    int decompressedSize = Compression.DecompressedSize(file);
                    if (decompressedSize != -1 && size == -1 && print)
                    {
                        Console.WriteLine($"Got ZSTD decompressed size: {decompressedSize}");
                        size = decompressedSize + 50000;
                        Console.WriteLine($"Added 50,000 bytes of padding to entry. Size is now: {size}");
                    }
                }

                // Automatically calculate size of file if not manually specified
                // or determined through processing
                if (size == -1)
                    size = Convert.ToInt32(new FileInfo(file).Length);
            }
            else
                Console.WriteLine($"Could not find local file, skipping processing path: {file}");

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

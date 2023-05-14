using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using Zstandard.Net;

namespace RSTBPatcher
{
    class Compression
    {
        public static void Decompress(string input, string output)
        {
            if (File.Exists(input))
            {
                if (!File.Exists(output))
                {
                    using (var ms = new MemoryStream(File.ReadAllBytes(input)))
                    using (var compressionStream = new ZstandardStream(ms, CompressionMode.Decompress))
                    using (var temp = new MemoryStream())
                    {
                        compressionStream.CopyTo(temp);
                        byte[] outputBytes = temp.ToArray();
                        File.WriteAllBytes(output, outputBytes);
                    }
                    Console.WriteLine($"Decompressed ZSTD file: {Path.GetFileName(input)}");
                }
                else
                    Console.WriteLine($"File already exists, skipping ZSTD decompression: {Path.GetFileName(output)}");
            }
            else
                Console.WriteLine($"Could not find input file: {Path.GetFileName(input)}");
        }

        public static void Compress(string input, string output)
        {
            if (File.Exists(input))
            {
                ZstdNet.Compressor compressor = new ZstdNet.Compressor();
                var outputBytes = compressor.Wrap(File.ReadAllBytes(input));
                File.WriteAllBytes(output, outputBytes);

                Console.WriteLine($"Compressed file with ZSTD: {Path.GetFileName(input)}");
            }
            else
                Console.WriteLine($"Could not find input file: {Path.GetFileName(input)}");
        }

        public static int DecompressedSize(string input)
        {
            /*
            if (File.Exists(input))
            {
                using (var ms = new MemoryStream(File.ReadAllBytes(input)))
                using (var compressionStream = new ZstandardStream(ms, CompressionMode.Decompress))
                using (var temp = new MemoryStream())
                {
                    compressionStream.CopyTo(temp);
                    int size = Convert.ToInt32(temp.Length);
                    return size;
                }
            }
            else
                Console.WriteLine($"Could not find input file: {Path.GetFileName(input)}");
            */
            // TODO: Use dictionary for TOTK
            return -1;
        }
    }
}

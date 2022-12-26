using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using DamienG.Security.Cryptography;

namespace RSTBPatcher
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Reading Resource Table data...");

            RSTB rstb = RSTB.Load(".\\2.0.6\\ResourceSizeTable.rsizetable");

            Console.WriteLine($"Header Magic: {Encoding.ASCII.GetString(rstb.Header.Magic)}");
            Console.WriteLine($"CRC32 Table Entries: {rstb.Header.EntryTableSize}");
            Console.WriteLine($"Named Table Entries: {rstb.Header.NameTableSize}\n");

            Console.WriteLine("Saving Resource Table data...");

            RSTB.Save(rstb, "test.rstc");
            Console.WriteLine($"Saved: test.rstc");

            DumpRSTBTxt(rstb);
        }

        public static void DumpRSTBTxt(RSTB rstb, string outPath = ".\\ResourceSizeTableDump.txt")
        {
            Console.WriteLine("Writing txt...");

            // Header Magic
            string txt = $"{Encoding.ASCII.GetString(rstb.Header.Magic)}\n";
            // CRC32 Table Entries
            foreach (var entry in rstb.EntryTable)
                txt += $"{CRC32ToHexString(entry.Crc32)} {entry.Size} {entry.Unknown}\n";
            txt += "\n";
            // Named Table Entries
            foreach (var entry in rstb.NameTable)
                txt += $"{NameEntryToString(entry.Name)} {entry.Size} {entry.Unknown}\n";
            txt = txt.TrimEnd('\n');
            File.WriteAllText(outPath, txt);
            Console.WriteLine($"Done writing txt: {outPath}");
        }

        private static string NameEntryToString(byte[] name)
        {
            return Encoding.ASCII.GetString(name).TrimEnd('\0');
        }

        private static string CRC32ToHexString(uint crc32)
        {
            return BitConverter.ToString(BitConverter.GetBytes(crc32).Reverse().ToArray()).Replace("-", "");
        }

        private static uint HexStringToCRC32(string hexString)
        {
            // https://stackoverflow.com/questions/321370/how-can-i-convert-a-hex-string-to-a-byte-array#comment112899143_321370
            byte[] data = BigInteger.Parse("00" + hexString, System.Globalization.NumberStyles.HexNumber).ToByteArray();
            while (data.Count() < 4)
                data = data.Concat(new byte[] { 0x00 }).ToArray();
            return BitConverter.ToUInt32(data);
        }

        public static RSTB ReadRSTBTxt(string path = ".\\2.0.6\\ResourceSizeTable.rsizetable.txt")
        {
            // Load RSTB data from .txt dump
            Console.WriteLine($"Reading txt: {path}");
            RSTB rstb = new RSTB();

            var lines = File.ReadAllLines(path);
            bool namedTable = false;
            // Start from line 2 since line 1 is reserved for header magic
            for (int i = 1; i < lines.Count(); i++)
            {
                string[] splitLine = lines[i].Split(' ');

                if (splitLine.Count() == 3)
                {
                    if (!namedTable)
                        rstb.EntryTable.Add(new RSTBTableEntry(HexStringToCRC32(splitLine[0]), Convert.ToUInt32(splitLine[1]), Convert.ToUInt32(splitLine[2])));
                    else
                        rstb.NameTable.Add(new RSTBNameEntry(splitLine[0], Convert.ToUInt32(splitLine[1]), Convert.ToUInt32(splitLine[2])));
                }
                else
                {
                    // Switch to named table entries
                    namedTable = true;
                }
            }
            
            // Create header
            if (lines[0] == "RSTC")
                rstb.Header.Magic = new byte[4] { 0x52, 0x53, 0x54, 0x43 };
            rstb.Header.EntryTableSize = Convert.ToUInt32(rstb.EntryTable.Count);
            rstb.Header.NameTableSize = Convert.ToUInt32(rstb.NameTable.Count);

            Console.WriteLine("Done reading txt");
            return rstb;
        }
    }
}

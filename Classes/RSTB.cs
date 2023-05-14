using Soft160.Data.Cryptography;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace RSTBPatcher
{
    public class RSTB
    {
        public RSTB() { }
        public RSTB(RSTBHeader header, List<RSTBTableEntry> entryTable, List<RSTBNameEntry> nameTable)
        {
            Header = header;
            EntryTable = entryTable;
            NameTable = nameTable;
        }
        public RSTBHeader Header { get; set; } = new RSTBHeader();
        public List<RSTBTableEntry> EntryTable { get; set; } = new List<RSTBTableEntry>();
        public List<RSTBNameEntry> NameTable { get; set; } = new List<RSTBNameEntry>();
        public static RSTB Load(string path)
        {
            RSTB rstb = new RSTB();
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    // Read header
                    byte[] magic = reader.ReadBytes(4);
                    if (magic.SequenceEqual(new byte[4] { 0x52, 0x53, 0x54, 0x42 }) || magic.SequenceEqual(new byte[4] { 0x52, 0x53, 0x54, 0x43 }))
                        rstb.Header = new RSTBHeader(magic, reader.ReadUInt32(), reader.ReadUInt32());
                    else
                        throw new Exception("Invalid header. Must start with RSTB or RSTC.");

                    // Read table entries
                    List<RSTBTableEntry> entryTable = new List<RSTBTableEntry>();
                    for (int i = 0; i < (int)rstb.Header.EntryTableSize; i++)
                    {
                        if (magic.SequenceEqual(new byte[4] { 0x52, 0x53, 0x54, 0x43 })) // RSTC
                            entryTable.Add(new RSTBTableEntry(reader.ReadUInt32(), reader.ReadUInt32(), reader.ReadUInt32()));
                        else // RSTB
                            entryTable.Add(new RSTBTableEntry(reader.ReadUInt32(), reader.ReadUInt32()));
                    }
                    rstb.EntryTable = entryTable;

                    // Read name entries
                    List<RSTBNameEntry> nameTable = new List<RSTBNameEntry>();
                    for (int i = 0; i < (int)rstb.Header.NameTableSize; i++)
                    {
                        if (magic.SequenceEqual(new byte[4] { 0x52, 0x53, 0x54, 0x43 })) // RSTC
                            nameTable.Add(new RSTBNameEntry(Encoding.ASCII.GetString(reader.ReadBytes(128)), reader.ReadUInt32(), reader.ReadUInt32()));
                        else // RSTB
                            nameTable.Add(new RSTBNameEntry(Encoding.ASCII.GetString(reader.ReadBytes(128)), reader.ReadUInt32()));
                    }
                    rstb.NameTable = nameTable;
                }
            }
            return rstb;
        }
        public static void Save(RSTB rstb, string outPath)
        {
            if (!Directory.Exists(Path.GetDirectoryName(outPath)))
                Directory.CreateDirectory(Path.GetDirectoryName(outPath));

            using (FileStream stream = new FileStream(outPath + ".temp", FileMode.OpenOrCreate))
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    // Write Header
                    writer.Write(rstb.Header.Magic);
                    writer.Write(rstb.Header.EntryTableSize);
                    writer.Write(rstb.Header.NameTableSize);
                    // Write Table Entries
                    foreach (var entry in rstb.EntryTable)
                    {
                        writer.Write(entry.Crc32);
                        writer.Write(entry.Size);
                        if (rstb.Header.Magic.SequenceEqual(new byte[4] { 0x52, 0x53, 0x54, 0x43 })) // RSTC
                            writer.Write(entry.Unknown); // 4 bytes of padding
                    }
                    // Write Name Entries
                    foreach (var entry in rstb.NameTable)
                    {
                        writer.Write(entry.Name);
                        writer.Write(entry.Size);
                        if (rstb.Header.Magic.SequenceEqual(new byte[4] { 0x52, 0x53, 0x54, 0x43 })) // RSTC
                            writer.Write(entry.Unknown); // 4 bytes of padding
                    }
                }
            }

            // Yaz0 Encode
            if (Path.GetExtension(outPath) != ".rsizetable")
            {
                Console.WriteLine("Encoding output file with Yaz0...");
                Yaz0.Encode(outPath + ".temp", outPath);

#if !DEBUG
                Console.WriteLine("Deleting temporary unencoded file...");
                if (File.Exists(outPath + ".temp"))
                    File.Delete(outPath + ".temp");
#endif
            }
            else
                Console.WriteLine("Skipping Yaz0 encoding since output extension is .rsizetable.");

        }
        public static RSTB Add(RSTB rstb, string path, int size = -1, bool useNamed = false, bool unknown = false)
        {
            // Don't convert path if it's already CRC32 in hex form
            uint crc32 = 0;
            try
            {
                crc32 = HexStringToCRC32(path);
            }
            catch { }
            if (crc32 == 0)
                crc32 = StringToCRC32(path);

            if (size == -1)
            {
                Console.WriteLine($"Skipping adding path, no size set: {path}");
                return rstb;
            }

            // Remove existing entry with matching CRC32 or path
            rstb = RSTB.Delete(rstb, path, useNamed);

            if (!useNamed)
            {
                rstb.EntryTable.Add(new RSTBTableEntry { Crc32 = crc32, Size = Convert.ToUInt32(size), Unknown = Convert.ToUInt32(unknown) });
                Console.WriteLine($"Added Entry to CRC32 Table: {path} {size} {unknown}\n\tCRC32: 0x{CRC32ToHexString(crc32)} ({crc32})");
            }
            else
            {
                byte[] name = Encoding.ASCII.GetBytes(path).Concat(new byte[128 - path.Length]).ToArray();
                rstb.NameTable.Add(new RSTBNameEntry { Name = name, Size = Convert.ToUInt32(size), Unknown = Convert.ToUInt32(unknown) });
                Console.WriteLine($"Added Entry to Named Table: {path} {size} {unknown}");
            }

            // Sort CRC32 values from lowest to highest
            rstb.EntryTable = rstb.EntryTable.OrderBy(x => x.Crc32).ToList();

            // Update header sizes
            rstb.Header.EntryTableSize = Convert.ToUInt32(rstb.EntryTable.Count);
            rstb.Header.NameTableSize = Convert.ToUInt32(rstb.NameTable.Count);

            return rstb;
        }
        public static RSTB Delete(RSTB rstb, string path, bool useNamed = false)
        {
            // Don't convert path if it's already CRC32 in hex form
            uint crc32 = 0;
            try
            {
                crc32 = HexStringToCRC32(path);
            }
            catch { }
            if (crc32 == 0)
                crc32 = StringToCRC32(path);

            if (!useNamed)
            {
                if (rstb.EntryTable.Any(x => x.Crc32.Equals(crc32)))
                {
                    rstb.EntryTable.RemoveAll(x => x.Crc32.Equals(crc32));
                    Console.WriteLine($"Removing existing CRC32 entries: 0x{CRC32ToHexString(crc32)} ({crc32})");
                }
                else
                    Console.WriteLine($"Could not find existing CRC32 entry to remove: 0x{CRC32ToHexString(crc32)} ({crc32})");
            }
            else
            {
                byte[] name = Encoding.ASCII.GetBytes(path).Concat(new byte[128 - path.Length]).ToArray();
                if (rstb.NameTable.Any(x => x.Name.Equals(name)))
                {
                    rstb.NameTable.RemoveAll(x => x.Name.Equals(name));
                    Console.WriteLine($"Removing existing Name entry: {path}");
                }
                else
                    Console.WriteLine($"Could not find existing Name entry to remove: {path}");
            }

            // Update header sizes
            rstb.Header.EntryTableSize = Convert.ToUInt32(rstb.EntryTable.Count);
            rstb.Header.NameTableSize = Convert.ToUInt32(rstb.NameTable.Count);

            return rstb;
        }
        public static void DumpTxt(RSTB rstb, string path)
        {
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

            // Write to .txt file
            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, txt);
        }
        public static void Check(RSTB rstb, string path)
        {
            // Don't convert path if it's already CRC32 in hex form
            uint crc32 = 0;
            try
            {
                crc32 = HexStringToCRC32(path);
            }
            catch { }
            if (crc32 == 0)
                crc32 = StringToCRC32(path);

            if (rstb.EntryTable.Any(x => x.Crc32.Equals(crc32)))
            {
                foreach (var entry in rstb.EntryTable.Where(x => x.Crc32.Equals(crc32)))
                    Console.WriteLine($"Found existing CRC32 entry:" +
                        $"\n\tCRC32 (hex): 0x{CRC32ToHexString(entry.Crc32)}" +
                        $"\n\tCRC32 (uint32): {entry.Crc32}" +
                        $"\n\tSize (bytes): {entry.Size}" +
                        $"\n\tUnknown: {entry.Unknown}");
            }
            else
                Console.WriteLine($"Could not find existing CRC32 entry: 0x{CRC32ToHexString(crc32)} ({crc32})");
            
            byte[] name = Encoding.ASCII.GetBytes(path).Concat(new byte[128 - path.Length]).ToArray();
            if (rstb.NameTable.Any(x => x.Name.Equals(name)))
            {
                foreach (var entry in rstb.NameTable.Where(x => x.Name.Equals(name)))
                    Console.WriteLine($"Found existing CRC32 entry:" +
                        $"\n\tName: {path}" +
                        $"\n\tSize (bytes): {entry.Size}" +
                        $"\n\tUnknown: {entry.Unknown}");
            }
            else
                Console.WriteLine($"Could not find existing Name entry: {path}");

        }
        public static RSTB LoadTxt(string path)
        {
            // Load RSTB data from .txt
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

            return rstb;
        }
        private static string NameEntryToString(byte[] name)
        {
            return Encoding.ASCII.GetString(name).TrimEnd('\0');
        }

        public static uint StringToCRC32(string path)
        {
            return CRC.Crc32(Encoding.ASCII.GetBytes(path));
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
    }

    public class RSTBHeader
    {
        public RSTBHeader() { }
        public RSTBHeader(byte[] magic, uint entryTableSize, uint nameTableSize)
        {
            Magic = magic;
            EntryTableSize = entryTableSize;
            NameTableSize = nameTableSize;
        }
        public byte[] Magic { get; set; } = new byte[4] { 0x52, 0x53, 0x54, 0x42 };
        public uint EntryTableSize { get; set; } = 0;
        public uint NameTableSize { get; set; } = 0;
    } // sizeof() = 12

    public class RSTBTableEntry
    {
        public RSTBTableEntry() { }
        public RSTBTableEntry(uint crc32, uint size, uint unknown = 0)
        {
            Crc32 = crc32;
            Size = size;
            Unknown = unknown;
        }
        public uint Crc32 { get; set; } = 0;
        public uint Size { get; set; } = 0;
        public uint Unknown { get; set; } = 0; // Only used in RSTC
    } // sizeof() = 8 (12)

    public class RSTBNameEntry
    {
        public RSTBNameEntry() { }
        public RSTBNameEntry(string name, uint size, uint unknown = 0)
        {
            Name = Encoding.ASCII.GetBytes(name).Concat(new byte[128 - Encoding.ASCII.GetBytes(name).Length]).Take(128).ToArray();
            Size = size;
            Unknown = unknown;
        }
        public byte[] Name { get; set; } = new byte[128];
        public uint Size { get; set; } = 0;
        public uint Unknown { get; set; } = 0; // Only used in RSTC
    } // sizeof() = 132 (136)
}

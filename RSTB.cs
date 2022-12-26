using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            using (FileStream stream = new FileStream(outPath, FileMode.OpenOrCreate))
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

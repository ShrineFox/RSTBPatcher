# RSTBPatcher
Starting with *Zelda: Breath of the Wild* on the Wii U, first-party Nintendo games started using the **ResourceSizeTable** (**RSTB**) format.  
This format stores filepaths (usually condensed into a ``CRC32`` hash) and unsigned 32-bit integers to denote their size, presumably for efficiently allocating memory.  
Table entries can also use a 128-byte string to denote the filepath, but this only rarely used in case of CRC32 collisions.   
If this table is not updated, modded files that are larger than the originals will crash the game, hence the need for a patcher.  
Other patchers already exist, such as [zeldamods/rstb](https://github.com/zeldamods/rstb) and [BCML](https://github.com/NiceneNerd/BCML),  
but I wanted to create my own version that doesn't depend on Python.  
  
*Animal Crossing: New Horizons* added a variant of the format, which I call **RSTC** (after the first 4 bytes of the header).  
This adds an additional 4 bytes to each entry, which seems to be another uint32 with a value of either ``0`` or ``1``.  
The extra value's purpose is presently unknown.

# Known Issues
- Although I intended for this tool to be able to set the new unknown value in RSTC to 1, it doesn't work yet.

# Usage
![](https://i.imgur.com/HI4774a.png)  

Open with the command prompt and use the following arguments and parameters to add/remove entries to either the CRC32 Table or Named Table.  
Existing entries will be removed first if their path/CRC32 matches.  

A Yaz0-encoded input RSTB (``.srsizetable``) will be automatically decoded. Output will be automatically encoded with Yaz0 unless the output path's extension is ``.rsizetable``.  
Lastly, using ``-t`` will output a text file that lists the ``CRC32`` values in hex, as well as their sizes and the unknown value (if present).

## Adding/Updating a CRC32 Entry
```RSTBPatcher.exe -i ./rstb/ResourceSizeTable.srsizetable -o ./output/ResourceSizeTable.srsizetable -m ./mod/ -a-p ./mod/Model/PlayerHair00.Nin_NX_NVN.zs```  
This would add ``Model\PlayerHair00.Nin_NX_NVN`` (after automatically removing the .zs extension and mod path) to the CRC32 table (as ``0x61685DFC``).  
Its size will be automatically calculated from the local file. In this case, the file ends with ``.zs``, so its ZSTD uncompressed size will be used.  
Any existing ``0x61685DFC`` entry would be removed first.

### Adding/Updating a Named Entry
Add ``-n`` to use the Named Table instead of the CRC32 table. This stores the filepath as a string (up to 128 bytes) instead of a 4-byte CRC32.

### Manually specifying path
Note that if the mod directory (``-m ./mod/``) is not specified, the program would be unable to ascertain the root (in this case, the ``Model`` folder).  
It would instead add ``./mod/Model/PlayerHair00.Nin_NX_NVN.zs`` to the table, which isn't a path the game recognizes.  
In that case, you would want to also manually specify the path using ``-a-p Model\PlayerHair00.Nin_NX_NVN``.  

### Manually specifying size
If the path isn't to a local file that the program can find, you must manually specify the size.  
To specify the size (in bytes), you would add ``-a-s 631424``.  
Example:  
```RSTBPatcher.exe -i ./rstb/ResourceSizeTable.srsizetable -o ./output/ResourceSizeTable.srsizetable -a-p Model\PlayerHair00.Nin_NX_NVN -a-s 631424

Reading Resource Table data...
Decompressing RSTB to new file: .\rstb\ResourceSizeTable.rsizetable
Could not find local file, skipping processing path: Model\PlayerHair00.Nin_NX_NVN
Removing existing CRC32 entries: 0x61685DFC (1634229756)
Added Entry to CRC32 Table: Model/PlayerHair00.Nin_NX_NVN 631424 False
        CRC32: 0x61685DFC (1634229756)
Saving and compressing new RSTB...
Encoding output file with Yaz0...
Done, RSTB file saved to: ./output/ResourceSizeTable.srsizetable```  
If you don't specify a size, it will attempt to automatically derive it from the local file.  

### Adding/Updating Multiple Entries
To add/replace multiple entries, simply specify ``-m`` without ``-a-p``. It will automatically get the size and relative path of each file in the directory and add them to the table.  
  
Example:  
```-i ./rstb/ResourceSizeTable.srsizetable -o ./output/ResourceSizeTable.srsizetable -m ./mod/

Reading Resource Table data...
Decompressing RSTB to new file: .\rstb\ResourceSizeTable.rsizetable
Made path relative to mod directory: Model/PlayerBody.Nin_NX_NVN.zs
Removed .zs extension from path: Model/PlayerBody.Nin_NX_NVN
Got ZSTD decompressed size: 456040
Added 50,000 bytes of padding to entry. Size is now: 506040
Removing existing CRC32 entries: 0xE372591C (3815921948)
Added Entry to CRC32 Table: Model/PlayerBody.Nin_NX_NVN 506040 False
        CRC32: 0xE372591C (3815921948)
Made path relative to mod directory: Model/PlayerEye00.Nin_NX_NVN.zs
Removed .zs extension from path: Model/PlayerEye00.Nin_NX_NVN
Got ZSTD decompressed size: 1159344
Added 50,000 bytes of padding to entry. Size is now: 1209344
Removing existing CRC32 entries: 0x80051A2F (2147818031)
Added Entry to CRC32 Table: Model/PlayerEye00.Nin_NX_NVN 1209344 False
        CRC32: 0x80051A2F (2147818031)
Made path relative to mod directory: Model/PlayerHair00.Nin_NX_NVN.zs
Removed .zs extension from path: Model/PlayerHair00.Nin_NX_NVN
Got ZSTD decompressed size: 631424
Added 50,000 bytes of padding to entry. Size is now: 681424
Removing existing CRC32 entries: 0x61685DFC (1634229756)
Added Entry to CRC32 Table: Model/PlayerHair00.Nin_NX_NVN 681424 False
        CRC32: 0x61685DFC (1634229756)
Saving and compressing new RSTB...
Encoding output file with Yaz0...
Done, RSTB file saved to: ./output/ResourceSizeTable.srsizetable```

## Deleting an Existing Entry
Use ``-d-p`` to remove a path instead of adding a path. Use ``-n`` if you are removing from the Named table, otherwise it will remove entries from the CRC32 table.
  
Example:  
```RSTBPatcher.exe -i ./rstb/ResourceSizeTable.srsizetable -o ./output/ResourceSizeTable.srsizetable -m ./mod/ -d-p ./mod/Model/PlayerHair00.Nin_NX_NVN.zs

Reading Resource Table data...
Decompressing RSTB to new file: .\rstb\ResourceSizeTable.rsizetable
Made path relative to mod directory: Model/PlayerHair00.Nin_NX_NVN.zs
Removed .zs extension from path: Model/PlayerHair00.Nin_NX_NVN
Removing existing CRC32 entries: 0x61685DFC (1634229756)
Saving and compressing new RSTB...
Encoding output file with Yaz0...
Done, RSTB file saved to: ./output/ResourceSizeTable.srsizetable```  
Alternatively:
```RSTBPatcher.exe -i ./rstb/ResourceSizeTable.srsizetable -o ./output/ResourceSizeTable.srsizetable -d-p Model\PlayerHair00.Nin_NX_NVN

Reading Resource Table data...
Decompressing RSTB to new file: .\rstb\ResourceSizeTable.rsizetable
Could not find local file, skipping processing path: Model\PlayerHair00.Nin_NX_NVN
Removing existing CRC32 entries: 0x61685DFC (1634229756)
Saving and compressing new RSTB...
Encoding output file with Yaz0...
Done, RSTB file saved to: ./output/ResourceSizeTable.srsizetable```

## Checking an Existing Entry
Use ``-c`` to check a path. The console will print the current CRC32 (in hex and uint32) value, its size in bytes, and whether the unknown value is set.  
  
Example:  
```RSTBPatcher.exe -i ./rstb/ResourceSizeTable.srsizetable -m ./mod/ -c ./mod/Model/PlayerHair00.Nin_NX_NVN.zs

Reading Resource Table data...
Decompressing RSTB to new file: .\rstb\ResourceSizeTable.rsizetable
Made path relative to mod directory: Model/PlayerHair00.Nin_NX_NVN.zs
Removed .zs extension from path: Model/PlayerHair00.Nin_NX_NVN
Found existing CRC32 entry:
        CRC32 (hex): 0x61685DFC
        CRC32 (uint32): 1634229756
        Size (bytes): 287653
        Unknown: 0
Could not find existing Name entry: Model/PlayerHair00.Nin_NX_NVN
Done, no changes were made so new RSTB was not saved.```  
Alternatively:  
```RSTBPatcher.exe -i ./rstb/ResourceSizeTable.srsizetable -c Model\PlayerHair00.Nin_NX_NVN

Reading Resource Table data...
Decompressing RSTB to new file: .\rstb\ResourceSizeTable.rsizetable
Could not find local file, skipping processing path: Model\PlayerHair00.Nin_NX_NVN
Found existing CRC32 entry:
        CRC32 (hex): 0x61685DFC
        CRC32 (uint32): 1634229756
        Size (bytes): 287653
        Unknown: 0
Could not find existing Name entry: Model/PlayerHair00.Nin_NX_NVN
Done, no changes were made so new RSTB was not saved.```  

## Outputting a .txt File
If you want an easy way to review the data stored in the RSTB file, add ``-t`` to output the data in ``.txt`` format.  
You can also edit this document and use it in place of an RSTB file as input.  

## Skipping processing
If you don't want to use the automatically calculated uncompressed size of a local file, or have the file's extension automatically changed, you can add ``-sp`` to skip processing.
At the moment, this only applies to ``.zs`` files when the size isn't manually specified using ``-a-s``.  
Even with processing skipped, a relative path will still be automatically used when a mod directory is specified with ``-m``. 

# List of Parameters
```RSTBPatcher <option> <args> [optional parameters]

Options:
-i
--input <filepath> (required)
Specifies the path to the ResourceTable file to use as input.

-o
--output <filepath>
Specifies the path to the ResourceTable file to save the output to.

-m
--mod-dir <directorypath>
Directory containing mod filestructure to update ResourceTable with.

-t
--txt <true|false>
When true, dump output RSTB data to a text file.

-c
--check <filepath>
Shows the CRC32 and size of a filepath in the RSTB if it exists.

-n
--named <true|false>
When true, the full path will be saved rather than converted to CRC32.

-u
--unknown <true|false>
When true, the unknown value will be set to 1 instead of 0.

-sp
--skipprocessing <true|false>
When true, uncompressed size won't be automatically derived from compressed files, and extensions won't be modified.

-a-p
--a-path <filepath>
The path (or CRC32) of the entry to add to the ResourceTable. If it exists, it will be replaced.

-a-s
--a-size <integer>
The size to reserve for the file. By default, this will be calculated automatically.

-d-p
--d-path
The path (or CRC32) of the entry to delete from the ResourceTable.```

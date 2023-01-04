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
You can also use Add to update existing entries. You can even process an entire directory of files at once using ``-m``.  
Yaz0 encoded files will be automatically decoded. Output will be automatically encoded with Yaz0 unless the output path's extension is ``.rsizetable``.  
Lastly, using ``-t`` will output a text file that lists the ``CRC32`` values in hex, as well as their sizes and the unknown value (if present).
  
```RSTBPatcher <option> <args> [optional parameters]

Options:
-i
--input <filepath> (required)
Specifies the path to the ResourceTable file to use as input.

-o
--output <filepath> (required)
Specifies the path to the ResourceTable file to save the output to.

-m
--mod-dir <directorypath>
Directory containing mod filestructure to update ResourceTable with.

-t
--txt <true|false>
When true, dump output RSTB data to a text file.

-n
--named <true|false>
When true, the full path will be saved rather than converted to CRC32.

-u
--unknown <true|false>
When true, the unknown value will be set to 1 instead of 0.

-a-p
--a-path <filepath>
The path (or CRC32) of the entry to add to the ResourceTable. If it exists, it will be replaced.

-a-s
--a-size <integer>
The size to reserve for the file. By default, this will be calculated automatically.

-d-p
--d-path
The path (or CRC32) of the entry to delete from the ResourceTable.```

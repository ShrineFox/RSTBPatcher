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

# Usage
## GUI
![](https://i.imgur.com/oLzFTnR.png)  
The graphical user interface provides a simple, straightforward way to quickly patch the size table to account for your modded files.  
1. Double-click RSTBPatcher.exe to launch the GUI.  
2. Use it to enter the paths to your input ``.srsizetable`` file, your folder containing modded files, and an output destination. 
3. Click the "Patch" button, and a new .srsizetable file will be generated, with the filesizes added/updated for each file in the mod folder.
Note: the mod folder must match the structure of the game! A good example: your game's ``sd:/atmosphere/contents/TITLEID/romfs`` folder.
## Commandline
This feature is intended for advanced users and very specific scenarios.  
![](https://i.imgur.com/HI4774a.png)  
See [the wiki](https://github.com/ShrineFox/RSTBPatcher/wiki) for all information on using the commandline mode.

# Known Issues
- The program does not start unless you [install the proper version of .NET Core](https://github.com/ShrineFox/RSTBPatcher/issues/3#issuecomment-1475409778)
- Although I intended for this tool to be able to set the new unknown value in RSTC to 1, it doesn't work yet.
- Not all files have their paths automatically corrected yet (i.e. files inside .sbactorpacks or .pack in BotW), right now just .sbfres files automatically have their entries renamed to .bfres (and their decompressed size used), and .zs files automatically have .zs removed from the entry and the decompressed size used.
- Files ending with .zs that aren't actually ZSTD compressed [need to be re-saved with compression using Switch Toolbox](https://github.com/ShrineFox/RSTBPatcher/issues/4), or else user gets "unknown frame descriptor" error

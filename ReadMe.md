DoL-Custom-Scripts
Dawn of Light - Dark Age of Camelot Server Emulator (DOL)
DOL Server is a server emulator for the game Dark Age of Camelot written by the Dawn of Light community
It does the following:
Provides the network communication needed to allow a DAOC game client to connect to the server
Provides a database layer between the server and MySQL~SQLite to allow storage of characters, npcs, items and so on
Provides a persistent world framework for customisation of game rulesets and behaviours

Latest Release : https://github.com/Dawn-of-Light/DOLSharp/releases/latest
How To Use
 Just copy the working folder of the correct version of DOL you are using to Server/scripts/customnpc folder, then start server like usual. There is one scripts in the working directory that requires 1 line change to server core. BPTransferNpc.cs in order to use this you must edit GamerPlayer.cs line:187. Change the word internal to public, save file then recompile DOL.
Documentation 
•	Coding: Wiki Home


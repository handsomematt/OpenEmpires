using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GenieLib
{
    public static class Blendomatic
    {
        public struct BlendingMode
        {
            public uint TileSize;
            public Dictionary<byte, byte[]> Tiles;
        }

        public static uint NumberOfBlendingModes;
        public static uint NumberOfTiles;

        public static Dictionary<byte, BlendingMode> BlendingModes;

        public static void ReadBlendomatic(FileStream file)
        {
            BinaryReader reader = new BinaryReader(file);

            NumberOfBlendingModes = reader.ReadUInt32();
            NumberOfTiles = reader.ReadUInt32();

            BlendingModes = new Dictionary<byte, BlendingMode>();

            Console.WriteLine("Number of blending modes: " + NumberOfBlendingModes);
            Console.WriteLine("Number of tiles: " + NumberOfTiles);

            for (var i = 0; i < NumberOfBlendingModes; i++ )
            {
                BlendingMode mode;
                mode.TileSize = reader.ReadUInt32();
                mode.Tiles = new Dictionary<byte, byte[]>();

                var TileFlags = reader.ReadBytes(31); // all 1 wtf

                reader.ReadBytes((int)mode.TileSize * 4);
                for (var j = 0; j < NumberOfTiles; j++)
                {
                    mode.Tiles[(byte)j] = reader.ReadBytes((int)mode.TileSize);
                }

                BlendingModes[(byte)i] = mode;
            }
        }
        
    }
}

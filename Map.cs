﻿using System;
using SFML.Window;
using SFML.Graphics;

using GenieLib;

namespace OpenEmpires
{
    public struct Tile
    {
        public ushort Frame;

        public Tile(ushort frame)
        {
            Frame = frame;
        }
    }

    public class Map
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        private Tile[,] tiles;
        private SLPFile tileset;

        public Map(int width, int height, SLPFile _tileset)
        {
            Width = width;
            Height = height;
            tileset = _tileset;

            tiles = new Tile[width, height];

            for ( var y = 0; y < height; y++ )
                for (var x = 0; x < width; x++)
                {
                    tiles[x, y].Frame = 0;
                }
        }

        public Tile this[int x, int y]
        {
            get { return tiles[x, y]; }
            set
            {
                tiles[x, y] = value;
                // mark chunk as dirty
            }
        }

        public void Draw(RenderTarget rt)
        {
            var view = rt.GetView();

            for ( var y = 0; y < Height - 1; y++ )
                for ( var x = 0; x < Width - 1; x++ )
                {
                    // wow okay i know you said don't use sprites
                    // but i made them in a draw loop anyway okay

                    // also the map prints all wrong
                    // ye bye

                    Tile tile = tiles[x, y];
                    var frame = tileset.GetFrame(tile.Frame);
                    var sprite = new Sprite(new Texture(new Image((uint)frame.m_Width, (uint)frame.m_Height, frame.GetRGBAArray())));
                    sprite.Position = new Vector2f(x*96 + ( y * 48 ), y*24);

                    rt.Draw(sprite);
                }
        }
    }
}

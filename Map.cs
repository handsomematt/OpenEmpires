﻿using System;
using System.Collections.Generic;
using SFML.Window;
using SFML.Graphics;

using GenieLib;

namespace OpenEmpires
{
    public struct Tile
    {
        public byte Elevation;
        public byte Cnst;

        public Tile(byte cnst)
        {
            Elevation = 1;
            Cnst = cnst;
        }
    }

    public class Map : Drawable
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        private Tile[,] tiles;

        private Dictionary<byte, VertexArray> vertexArrays;
        private Dictionary<byte, Texture> tilesetTextures;

        public Map()
        {
            //Width = width;
            //Height = height;

            var map = new Scenario("default0.scx").Map;
            Width = (int)map.width;
            Height = (int)map.height;
            tiles = new Tile[Width, Height];

            for ( var y = 0; y < Height; y++ )
                for (var x = 0; x < Width; x++)
                {
                    tiles[x, y].Cnst = map.terrain[x, y].cnst;
                }

            vertexArrays = new Dictionary<byte, VertexArray>();
            tilesetTextures = new Dictionary<byte, Texture>();

            Build();
        }

        public Tile this[int x, int y]
        {
            get { return tiles[x, y]; }
            set
            {
                tiles[x, y] = value;
            }
        }

        private void Build()
        {
            for ( var y = 0; y < Height - 1; y++ )
                for (var x = 0; x < Width - 1; x++)
                {
                    var tile = tiles[x, y];

                    if (!vertexArrays.ContainsKey(tile.Cnst))
                    {
                        vertexArrays[tile.Cnst] = new VertexArray(PrimitiveType.Quads);

                        // this also means we've never grabbed the texture before.. maybe?
                        LoadTerrainTexture(tile.Cnst);
                    }

                    var vertexArray = vertexArrays[tile.Cnst];

                    var topleft = new Vector2f(
                        (y * 96 / 2) + (x * 96 / 2),
                        (x * 48 / 2) - (y * 48 / 2) + (tile.Elevation * 24)
                    );

                    var topright = new Vector2f(
                        topleft.X + 96,
                        topleft.Y
                    );

                    var bottomleft = new Vector2f(
                        topleft.X,
                        topleft.Y + 48
                    );

                    var bottomright = new Vector2f(
                        topright.X,
                        topright.Y + 48
                    );

                    var last = vertexArray.VertexCount;
                    vertexArray.Resize(last + 4);

                    //var itexY = tiles[x, y].Frame * 49;
                    var itexY = 0;

                    vertexArray[last + 0] = new Vertex(topleft, new Vector2f(0, itexY));
                    vertexArray[last + 1] = new Vertex(topright, new Vector2f(96, itexY));
                    vertexArray[last + 2] = new Vertex(bottomright, new Vector2f(96, itexY + 48));
                    vertexArray[last + 3] = new Vertex(bottomleft, new Vector2f(0, itexY + 48));

                    /*
                    
                    for each tile
                     - render the base terrain type without any mask
                     - get the 4 neighbours in straight lines around the tile
                     - for each blend type superior to the center tile in ascending order:
                        - get the tiles of that blend type among the neighbours
                        - match that against one of the patterns corresponding to each row of blendomatic.dat to find the mask
                        - render the dominant terrain over the center tile using the inverse of the mask 
                    
                    */
                }
        }

        private void LoadTerrainTexture(byte tex)
        {
            var slpFile = new SLPFile();
            slpFile.LoadFile("textures/ter" + Configuration.TerrainTextureLookups[tex] + ".slp");

            var spritewidth = slpFile.GetFrame(0).m_Width;
            var spriteheight = slpFile.GetFrame(0).m_Height;

            List<byte> constructorbot = new List<byte>(spritewidth * spriteheight * slpFile.m_Frames.Count);

            foreach (var frame in slpFile.m_Frames)
            {
                byte[] b = frame.GetRGBAArray();
                foreach (byte by in b)
                    constructorbot.Add(by);
            }

            var img = new Image((uint)spritewidth, (uint)(spriteheight * slpFile.m_Frames.Count), constructorbot.ToArray());
       
            tilesetTextures[tex] = new Texture(img);
        }

        public void Draw(RenderTarget rt, RenderStates states)
        {
            var view = rt.GetView();


            //states.Texture = tilesettexture;

            foreach (var kv in vertexArrays)
            {
                states.Texture = tilesetTextures[kv.Key];
                kv.Value.Draw(rt, states);
            }

            /*
            for ( var y = 0; y < Height - 1; y++ )
                for ( var x = 0; x < Width - 1; x++ )
                {
                    Tile tile = tiles[x, y];
                    var sprite = new Sprite(tilesettexture);
                    sprite.Position = new Vector2f(
                        (y * 96 / 2) + (x * 96 / 2),
                        (x * 48 / 2) - (y * 48 / 2)
                    );

                    rt.Draw(sprite);
                }
             */
        }
    }
}

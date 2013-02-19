using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GenieLib
{
    public class PaletteColor
    {
        public byte R, G, B, A;

        public PaletteColor(byte _r, byte _g, byte _b, byte _a)
        {
            R = _r;
            G = _g;
            B = _b;
            A = _a;
        }

        public PaletteColor(byte r, byte g, byte b) : this(r, g, b, 255) { }
    }

    static class Palette
    {
        public static PaletteColor[] aoe2_palette = new PaletteColor[] {
            new PaletteColor(0, 0, 0),
            new PaletteColor(0, 74, 161),
            new PaletteColor(0, 97, 155),
            new PaletteColor(0, 74, 187),
            new PaletteColor(0, 84, 176),
            new PaletteColor(0, 90, 184),
            new PaletteColor(0, 110, 176),
            new PaletteColor(0, 110, 187),
            new PaletteColor(0, 84, 197),
            new PaletteColor(0, 98, 210),
            new PaletteColor(0, 0, 0),
            new PaletteColor(47, 47, 47),
            new PaletteColor(67, 67, 67),
            new PaletteColor(87, 87, 87),
            new PaletteColor(37, 16, 6),
            new PaletteColor(47, 26, 17),
            new PaletteColor(0, 0, 82),
            new PaletteColor(0, 21, 130),
            new PaletteColor(19, 49, 161),
            new PaletteColor(48, 93, 182),
            new PaletteColor(74, 121, 208),
            new PaletteColor(110, 166, 235),
            new PaletteColor(151, 206, 255),
            new PaletteColor(205, 250, 255),
            new PaletteColor(64, 43, 23),
            new PaletteColor(67, 51, 27),
            new PaletteColor(70, 32, 6),
            new PaletteColor(75, 57, 42),
            new PaletteColor(84, 64, 43),
            new PaletteColor(87, 69, 37),
            new PaletteColor(87, 57, 27),
            new PaletteColor(94, 74, 48),
            new PaletteColor(65, 0, 0),
            new PaletteColor(105, 11, 0),
            new PaletteColor(160, 21, 0),
            new PaletteColor(230, 11, 0),
            new PaletteColor(255, 0, 0),
            new PaletteColor(255, 100, 100),
            new PaletteColor(255, 160, 160),
            new PaletteColor(255, 220, 220),
            new PaletteColor(97, 77, 67),
            new PaletteColor(103, 58, 21),
            new PaletteColor(113, 75, 51),
            new PaletteColor(113, 75, 13),
            new PaletteColor(115, 105, 84),
            new PaletteColor(125, 97, 72),
            new PaletteColor(125, 74, 0),
            new PaletteColor(129, 116, 95),
            new PaletteColor(0, 0, 0),
            new PaletteColor(0, 7, 0),
            new PaletteColor(0, 32, 0),
            new PaletteColor(0, 59, 0),
            new PaletteColor(0, 87, 0),
            new PaletteColor(0, 114, 0),
            new PaletteColor(0, 141, 0),
            new PaletteColor(0, 169, 0),
            new PaletteColor(134, 126, 118),
            new PaletteColor(135, 64, 0),
            new PaletteColor(136, 108, 79),
            new PaletteColor(144, 100, 12),
            new PaletteColor(146, 125, 105),
            new PaletteColor(153, 106, 55),
            new PaletteColor(159, 121, 88),
            new PaletteColor(166, 74, 0),
            new PaletteColor(80, 51, 26),
            new PaletteColor(140, 78, 9),
            new PaletteColor(191, 123, 0),
            new PaletteColor(255, 199, 0),
            new PaletteColor(255, 247, 37),
            new PaletteColor(255, 255, 97),
            new PaletteColor(255, 255, 166),
            new PaletteColor(255, 255, 227),
            new PaletteColor(167, 135, 102),
            new PaletteColor(172, 144, 115),
            new PaletteColor(175, 126, 36),
            new PaletteColor(175, 151, 128),
            new PaletteColor(185, 151, 146),
            new PaletteColor(186, 166, 135),
            new PaletteColor(187, 84, 0),
            new PaletteColor(187, 156, 125),
            new PaletteColor(110, 23, 0),
            new PaletteColor(150, 36, 0),
            new PaletteColor(210, 55, 0),
            new PaletteColor(255, 80, 0),
            new PaletteColor(255, 130, 1),
            new PaletteColor(255, 180, 21),
            new PaletteColor(255, 210, 75),
            new PaletteColor(255, 235, 160),
            new PaletteColor(189, 150, 111),
            new PaletteColor(191, 169, 115),
            new PaletteColor(195, 174, 156),
            new PaletteColor(196, 170, 146),
            new PaletteColor(196, 128, 88),
            new PaletteColor(196, 166, 135),
            new PaletteColor(197, 187, 176),
            new PaletteColor(204, 160, 36),
            new PaletteColor(0, 16, 16),
            new PaletteColor(0, 37, 41),
            new PaletteColor(0, 80, 80),
            new PaletteColor(0, 120, 115),
            new PaletteColor(0, 172, 150),
            new PaletteColor(38, 223, 170),
            new PaletteColor(109, 252, 191),
            new PaletteColor(186, 255, 222),
            new PaletteColor(206, 169, 133),
            new PaletteColor(207, 105, 12),
            new PaletteColor(207, 176, 156),
            new PaletteColor(208, 155, 67),
            new PaletteColor(215, 186, 155),
            new PaletteColor(216, 162, 121),
            new PaletteColor(217, 114, 24),
            new PaletteColor(217, 187, 166),
            new PaletteColor(47, 0, 46),
            new PaletteColor(79, 0, 75),
            new PaletteColor(133, 12, 121),
            new PaletteColor(170, 47, 155),
            new PaletteColor(211, 58, 201),
            new PaletteColor(241, 108, 232),
            new PaletteColor(255, 169, 255),
            new PaletteColor(255, 210, 255),
            new PaletteColor(218, 156, 105),
            new PaletteColor(222, 177, 136),
            new PaletteColor(225, 177, 90),
            new PaletteColor(226, 195, 170),
            new PaletteColor(232, 180, 120),
            new PaletteColor(235, 202, 181),
            new PaletteColor(235, 216, 190),
            new PaletteColor(237, 199, 165),
            new PaletteColor(28, 28, 28),
            new PaletteColor(67, 67, 67),
            new PaletteColor(106, 106, 106),
            new PaletteColor(145, 145, 145),
            new PaletteColor(185, 185, 185),
            new PaletteColor(223, 223, 223),
            new PaletteColor(255, 255, 255),
            new PaletteColor(255, 255, 255),
            new PaletteColor(247, 211, 191),
            new PaletteColor(248, 201, 138),
            new PaletteColor(255, 206, 157),
            new PaletteColor(255, 225, 201),
            new PaletteColor(255, 238, 217),
            new PaletteColor(255, 226, 161),
            new PaletteColor(255, 243, 220),
            new PaletteColor(255, 255, 243),
            new PaletteColor(21, 21, 0),
            new PaletteColor(37, 37, 17),
            new PaletteColor(27, 47, 0),
            new PaletteColor(47, 57, 17),
            new PaletteColor(67, 77, 7),
            new PaletteColor(77, 77, 47),
            new PaletteColor(44, 77, 3),
            new PaletteColor(94, 84, 53),
            new PaletteColor(95, 97, 39),
            new PaletteColor(97, 97, 67),
            new PaletteColor(67, 97, 29),
            new PaletteColor(106, 115, 57),
            new PaletteColor(116, 115, 75),
            new PaletteColor(87, 116, 7),
            new PaletteColor(118, 130, 65),
            new PaletteColor(130, 136, 77),
            new PaletteColor(138, 139, 87),
            new PaletteColor(148, 155, 100),
            new PaletteColor(156, 156, 139),
            new PaletteColor(128, 157, 84),
            new PaletteColor(149, 166, 97),
            new PaletteColor(175, 165, 106),
            new PaletteColor(176, 176, 159),
            new PaletteColor(146, 176, 67),
            new PaletteColor(194, 190, 148),
            new PaletteColor(165, 196, 108),
            new PaletteColor(166, 196, 77),
            new PaletteColor(206, 187, 128),
            new PaletteColor(206, 204, 155),
            new PaletteColor(204, 217, 77),
            new PaletteColor(221, 218, 166),
            new PaletteColor(196, 226, 116),
            new PaletteColor(247, 204, 17),
            new PaletteColor(3, 28, 0),
            new PaletteColor(7, 38, 0),
            new PaletteColor(7, 47, 7),
            new PaletteColor(19, 48, 0),
            new PaletteColor(27, 57, 17),
            new PaletteColor(47, 57, 47),
            new PaletteColor(28, 62, 0),
            new PaletteColor(14, 68, 14),
            new PaletteColor(41, 69, 28),
            new PaletteColor(33, 73, 18),
            new PaletteColor(47, 87, 47),
            new PaletteColor(77, 97, 57),
            new PaletteColor(67, 97, 67),
            new PaletteColor(87, 116, 77),
            new PaletteColor(70, 119, 48),
            new PaletteColor(85, 119, 52),
            new PaletteColor(106, 136, 97),
            new PaletteColor(196, 236, 166),
            new PaletteColor(23, 53, 33),
            new PaletteColor(43, 84, 64),
            new PaletteColor(37, 116, 57),
            new PaletteColor(23, 43, 53),
            new PaletteColor(2, 33, 53),
            new PaletteColor(2, 23, 53),
            new PaletteColor(33, 64, 64),
            new PaletteColor(0, 34, 97),
            new PaletteColor(0, 51, 115),
            new PaletteColor(43, 64, 74),
            new PaletteColor(0, 43, 74),
            new PaletteColor(4, 6, 9),
            new PaletteColor(0, 123, 189),
            new PaletteColor(64, 84, 84),
            new PaletteColor(0, 115, 207),
            new PaletteColor(23, 23, 74),
            new PaletteColor(12, 23, 64),
            new PaletteColor(0, 0, 2),
            new PaletteColor(0, 64, 125),
            new PaletteColor(2, 23, 84),
            new PaletteColor(0, 138, 186),
            new PaletteColor(64, 105, 105),
            new PaletteColor(0, 146, 197),
            new PaletteColor(94, 105, 105),
            new PaletteColor(0, 74, 125),
            new PaletteColor(0, 125, 207),
            new PaletteColor(0, 120, 227),
            new PaletteColor(84, 115, 125),
            new PaletteColor(64, 105, 125),
            new PaletteColor(0, 64, 146),
            new PaletteColor(0, 53, 135),
            new PaletteColor(115, 156, 156),
            new PaletteColor(84, 146, 176),
            new PaletteColor(146, 176, 187),
            new PaletteColor(207, 248, 255),
            new PaletteColor(105, 166, 197),
            new PaletteColor(125, 197, 217),
            new PaletteColor(156, 197, 217),
            new PaletteColor(109, 126, 33),
            new PaletteColor(113, 153, 36),
            new PaletteColor(21, 118, 21),
            new PaletteColor(51, 151, 39),
            new PaletteColor(70, 181, 59),
            new PaletteColor(89, 223, 89),
            new PaletteColor(131, 245, 120),
            new PaletteColor(174, 255, 174),
            new PaletteColor(0, 255, 0),
            new PaletteColor(0, 0, 255),
            new PaletteColor(255, 255, 0),
            new PaletteColor(255, 213, 0),
            new PaletteColor(226, 154, 73),
            new PaletteColor(241, 164, 82),
            new PaletteColor(255, 171, 88),
            new PaletteColor(255, 197, 113),
            new PaletteColor(85, 125, 57),
            new PaletteColor(129, 151, 49),
            new PaletteColor(0, 255, 255),
            new PaletteColor(255, 0, 255),
            new PaletteColor(0, 139, 210),
            new PaletteColor(0, 160, 243),
            new PaletteColor(255, 255, 255)
        };
    }

    public class SLPFile
    {
        public string m_FileName;
        public bool m_Loaded;

        private BinaryReader m_BinaryReader;

        public string m_Version;
        public uint m_NumFrames;
        public string m_Comment;

        public List<SLPFrame> m_Frames;


        public void LoadFile( string sFileName )
        {
            m_FileName = sFileName;

            if ( File.Exists( sFileName ) )
            {
                using ( m_BinaryReader = new BinaryReader( File.Open( sFileName, FileMode.Open ) ) )
                {
                    ReadHeader();

                    m_Frames = new List<SLPFrame>((int)m_NumFrames);

                    for (uint i = 0; i < m_NumFrames; i++) {
                        var frame = new SLPFrame();
                        frame.ReadHeader(m_BinaryReader);
                        m_Frames.Add(frame);
                    }

                    foreach (SLPFrame frame in m_Frames)
                    {
                        frame.ReadFrame(m_BinaryReader);
                    }

                }
            }
            else
            {
                throw new FileNotFoundException( "File not found.", sFileName );
            }
        }

        public SLPFrame GetFrame(int frame)
        {
            return m_Frames[frame];
        }

        private void ReadHeader()
        {
            m_Version = Encoding.ASCII.GetString(m_BinaryReader.ReadBytes(4));
            m_NumFrames = m_BinaryReader.ReadUInt32();
            m_Comment = Encoding.ASCII.GetString(m_BinaryReader.ReadBytes(24));
        }
    }

    public class SLPFrame
    {
        public uint m_CmdTableOffset;
        public uint m_OutlineTableOffset;
        public uint m_PaletteOffset;
        public uint m_Properties;

        public int m_Width;
        public int m_Height;
        public int m_HotSpotX;
        public int m_HotSpotY;

        private short[] m_LeftEdges;
        private short[] m_RightEdges;

        private byte[] m_IndicesArray;
        private byte[] m_RGBAArray;

        public SLPFrame()
        {

        }

        public void ReadHeader(BinaryReader reader)
        {
            m_CmdTableOffset = reader.ReadUInt32();
            m_OutlineTableOffset = reader.ReadUInt32();
            m_PaletteOffset = reader.ReadUInt32();
            m_Properties = reader.ReadUInt32();

            m_Width = reader.ReadInt32();
            m_Height = reader.ReadInt32();
            m_HotSpotX = reader.ReadInt32();
            m_HotSpotY = reader.ReadInt32();
        }

        public void ReadFrame(BinaryReader reader)
        {
            // Initialize our indices array as a table full of transparent colours.
            m_IndicesArray = new byte[m_Width * m_Height];
            for (int i = 0; i < m_IndicesArray.Length; i++)
                m_IndicesArray[i] = 255; // transparent index

            reader.BaseStream.Seek(m_OutlineTableOffset, SeekOrigin.Begin);
            m_LeftEdges = new short[m_Height];
            m_RightEdges = new short[m_Height];
            for (int i = 0; i < m_Height; i++)
            {
                m_LeftEdges[i] = reader.ReadInt16();
                m_RightEdges[i] = reader.ReadInt16();

                //Console.WriteLine("Edges: 0x" + m_LeftEdges[i].ToString("X4") + " | 0x" + m_RightEdges[i].ToString("X4"));
            }

            // Command offsets
            var commandOffsets = new uint[m_Height];
            reader.BaseStream.Seek(m_CmdTableOffset, SeekOrigin.Begin);
            for ( int i = 0; i < m_Height; i++ )
                commandOffsets[i] = reader.ReadUInt32();

            for (int y = 0; y < m_Height; y++)
            {
                reader.BaseStream.Seek(commandOffsets[y], SeekOrigin.Begin);

                var x = m_LeftEdges[y];

                byte opcode = 0;
                while ((opcode = reader.ReadByte()) != 0x0F) // whilst the row is still not ended
                {
                    var twobit = opcode & 0x3;
                    var command = opcode & 0xF; // fourbit

                    if (command == 0x0F)
                        break;

                    switch (command)
                    {
                        // Color list:
                        // An array of palette indices. This is about as bitmap as it gets in SLPs.
                        case 0:
                        case 4:
                        case 8:
                        case 0xC:
                            var numpixels0 = opcode >> 2;
                            for (int i = 0; i < numpixels0; i++) {
                                m_IndicesArray[y * m_Width + x] = reader.ReadByte();
                                x += 1;
                            }
                            break;

                        // Skip
                        // The specified number of pixels are transparent.
                        case 1:
                        case 5:
                        case 9:
                        case 0xD:
                            var numpixels1 = opcode >> 2;
                            if (numpixels1 == 0)
                                numpixels1 = reader.ReadByte();

                            x += (short)numpixels1;
                            break;

                        // Big color list:
                        // An array of palette indexes. Supports a greater number of pixels than the above color list.
                        case 2:
                            var numpixels2 = ((opcode & 0xF0) << 4) + reader.ReadByte();
                            for (int i = 0; i < numpixels2; i++) {
                                m_IndicesArray[y * m_Width + x] = reader.ReadByte();
                                x += 1;
                            }

                            break;

                        // Big Skip:
                        // The specified number of pixels are transparent. Supports a greater number of pixels than the above skip.
                        case 3:
                            var numpixels3 = ((opcode & 0xF0) << 4) + reader.ReadByte();
                            x += (short)numpixels3;
                            break;

                        // Player color list:
                        // An array of player color indexes. The actual palette index is given by adding ([player number] * 16) + 16 to these values.
                        case 6:
                            var numpixels6 = (opcode & 0xF0) >> 4;
                            if (numpixels6 == 0)
                                numpixels6 = reader.ReadByte();

                            for (int i = 0; i < numpixels6; i++) {
                                // have to do some fancy stuff with player indices
                                // reader.ReadByte is actually just a relative palette index which should be grabbed
                                // later on using a palette index function.

                                m_IndicesArray[y * m_Width + x] = reader.ReadByte();
                                x += 1;
                            }

                            break;

                        // Fill:
                        // Fills the specified number of pixels with the following palette index.
                        case 7:
                            var numpixels7 = (opcode & 0xF0) >> 4;
                            if (numpixels7 == 0)
                                numpixels7 = reader.ReadByte();

                            var colorindex = reader.ReadByte();

                            for (int i = 0; i < numpixels7; i++) {
                                m_IndicesArray[y * m_Width + x] = colorindex;
                                x += 1;
                            }

                            break;

                        // Player color fill:
                        // Same as above, but using the player color formula (see Player color list).
                        case 0xA:
                            var numpixelsa = (opcode & 0xF0) >> 4;
                            if (numpixelsa == 0)
                                numpixelsa = reader.ReadByte();

                            // the value below is incorrect
                            // this is just a relative player palette colour
                            // a function should be made to translate this into a usable palette colour
                            // or handled manually to allow for any RGBA colour to be used for a team
                            // color.
                            var playercolorindex = reader.ReadByte();

                            for (int i = 0; i < numpixelsa; i++) {
                                m_IndicesArray[y * m_Width + x] = playercolorindex;
                                x += 1;
                            }

                            break;
                            
                        case 0xB:
                            // todo
                            var numpixelsb = (opcode & 0xF0) >> 4;
                            if (numpixelsb == 0)
                                numpixelsb = reader.ReadByte();

                            x += (short)numpixelsb;

                            break;

                        case 0x0E:
                            var extendedcmd = opcode >> 4;

                            switch (extendedcmd)
                            {
                                case 0x4E:
                                case 0x6E:
                                    // todo
                                    x += 1;
                                    break;

                                case 0x5E:
                                case 0x7E:
                                    // todo
                                    var outlinespanamount = reader.ReadByte();
                                    x += outlinespanamount;

                                    break;
                            }

                            break;

                        default:
                            throw new Exception("Unknown cmd: " + command.ToString("X2"));
                    }
                }
            }
        }

        public byte[] GetRGBAArray()
        {
            if (m_RGBAArray == null)
            {
                m_RGBAArray = new byte[m_IndicesArray.Length * 4];

                for (int i = 0; i < m_IndicesArray.Length; i++)
                {
                    PaletteColor rgba = Palette.aoe2_palette[m_IndicesArray[i]];
                    if (m_IndicesArray[i] == 255)
                        rgba = new PaletteColor(0, 0, 0, 0);

                    m_RGBAArray[i * 4 + 0] = rgba.R;
                    m_RGBAArray[i * 4 + 1] = rgba.G;
                    m_RGBAArray[i * 4 + 2] = rgba.B;
                    m_RGBAArray[i * 4 + 3] = rgba.A;
                }
            }

            return m_RGBAArray;
        }
    }
}

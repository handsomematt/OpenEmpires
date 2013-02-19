using System;
using System.IO;
using System.Text;
using zlib;

namespace GenieLib
{
    public enum AOEVersion
    {
        AOE2 = 18,
        AOE2TC = 21,
        SWGB = 21,
        AOE1 = 10
    }

    public class ScenarioMap
    {
        public uint width, height;

        public struct Terrain
        {
            public byte cnst;
            public byte elev;
        };
        
        public Terrain[,] terrain;

        public void Read(BinaryReader reader, AOEVersion version)
        {
            if (version >= AOEVersion.AOE2TC)
                reader.ReadInt32(); // aitype

            width = reader.ReadUInt32();
            height = reader.ReadUInt32();

            Console.WriteLine(width + " : " + height);

            terrain = new Terrain[width, height];

            for (uint x = 0; x < width; x++)
            {
                for (uint y = 0; y < height; y++)
                {
                    terrain[x, y].cnst = reader.ReadByte();
                    terrain[x, y].elev = reader.ReadByte();

                    reader.ReadByte();
                }
            }
        }
    }

    public class Scenario
    {
        const long sect = 0xFFFFFF9D;
        BinaryReader m_BinaryReader;
        AOEVersion FileVersion;
        public ScenarioMap Map;

        public Scenario(string path)
        {
            Console.WriteLine("Opening scenario: " + path);

            if (File.Exists(path))
            {
                using (m_BinaryReader = new BinaryReader(File.Open(path, FileMode.Open)))
                {
                    var version = Encoding.ASCII.GetString(m_BinaryReader.ReadBytes(4));

                    if (version == "1.18") // AOE2:AOK
                        FileVersion = AOEVersion.AOE2;
                    else if (version == "1.21") // AOE2:TC
                        FileVersion = AOEVersion.AOE2TC;
                    else if (version == "1.10" || version == "1.11") // AOE1
                        FileVersion = AOEVersion.AOE1;
                    else
                        throw new Exception("Unknown scenario version: " + version);

                    Console.WriteLine("Scenario Version: " + FileVersion);

                    var headerLength = m_BinaryReader.ReadInt32();
                    var checkSum = m_BinaryReader.ReadInt32();
                    var timeStamp = m_BinaryReader.ReadInt32();
                    var instructionsLength = m_BinaryReader.ReadInt32();
                    var instructions = m_BinaryReader.ReadBytes(instructionsLength);
                    m_BinaryReader.ReadInt32();
                    m_BinaryReader.ReadInt32(); // playerCount

                    var compressedlength = m_BinaryReader.BaseStream.Length - ( headerLength + 8 );
                    Console.WriteLine("Compressed Length: " + compressedlength);

                    byte[] outData = inflate_file(m_BinaryReader, compressedlength);

                    Console.WriteLine("Time to read the decompressed data.");

                    var reader = new BinaryReader(new MemoryStream(outData));
                    long next_uid = reader.ReadInt32();
                    float version2 = reader.ReadSingle();

                    Console.WriteLine(version2);

                    for (int i = 0; i < 16; i++)
                        reader.ReadBytes(256); // player name

                    for (int i = 0; i < 16; i++)
                        reader.ReadInt32(); // string table

                    for (int i = 0; i < 16; i++)
                    {
                        Console.Write(i + ": ");
                        Console.Write(" Enabled: " + reader.ReadInt32());
                        Console.Write(" Human: " + reader.ReadInt32());
                        Console.Write(" Civ: " + reader.ReadInt32());
                        Console.Write(" Unk: " + reader.ReadInt32());
                        Console.WriteLine();
                    }

                    reader.ReadInt32();
                    reader.ReadByte();
                    reader.ReadSingle();

                    ushort origname = reader.ReadUInt16();
                    Console.WriteLine(origname);
                    Console.WriteLine(Encoding.ASCII.GetString(reader.ReadBytes(origname)));

                    long[] mstrings = new long[6];
                    string[] messages = new string[6];
                    string[] cinem = new string[4];
                    string[] unk = new string[32];
                    string[] ai = new string[16];

                    for (int i = 0; i < 6; i++)
                        mstrings[i] = reader.ReadInt32();

                    for (int i = 0; i < 6; i++)
                        messages[i] = Encoding.ASCII.GetString(reader.ReadBytes(reader.ReadUInt16()));

                    for (int i = 0; i < 4; i++)
                        cinem[i] = Encoding.ASCII.GetString(reader.ReadBytes(reader.ReadUInt16()));

                    // bitmap

                    var bitmap = reader.ReadInt32();

                    Console.WriteLine("bitmap: " + bitmap);
                    Console.WriteLine("x: " + reader.ReadInt32());
                    Console.WriteLine("y: " + reader.ReadInt32());
                    reader.ReadInt16(); // unknown

                    // read some big ass bitmap file if bitmap != 0

                    for (var i = 0; i < 32; i++)
                        unk[i] = Encoding.ASCII.GetString(reader.ReadBytes(reader.ReadUInt16()));

                    for (int i = 0; i < 16; i++)
                        ai[i] = Encoding.ASCII.GetString(reader.ReadBytes(reader.ReadUInt16()));

                    for (int i = 0; i < 16; i++)
                    {
                        Console.Write(i + ": ");
                        Console.Write(reader.ReadInt32() + " ");
                        Console.WriteLine(reader.ReadInt32());
                        Console.WriteLine(Encoding.ASCII.GetString(reader.ReadBytes((int)reader.ReadUInt32())));
                    }

                    for (int i = 0; i < 16; i++)
                        Console.WriteLine(i + ": aimode=" + reader.ReadByte());

                    Console.WriteLine(reader.ReadInt32().ToString("x"));

                    Console.WriteLine("Resources:");

                    for (int i = 0; i < 16; i++)
                    {
                        Console.WriteLine("Player " + i);
                        Console.WriteLine("\tGold: " + reader.ReadInt32());
                        Console.WriteLine("\tWood: " + reader.ReadInt32());
                        Console.WriteLine("\tFood: " + reader.ReadInt32());
                        Console.WriteLine("\tStone: " + reader.ReadInt32());
                        Console.WriteLine("\tOrex: " + reader.ReadInt32());
                        Console.WriteLine("\tUnknown: " + reader.ReadInt32());
                    }
                    

                    Console.WriteLine(reader.ReadInt32().ToString("x"));

                    reader.ReadBytes(40); // victory stuff

                    for (int i = 0; i < 16; i++)
                        for (int j = 0; j < 16; j++)
                            Console.WriteLine(i + " - " + j + ": diplomacy=" + reader.ReadInt32());

                    reader.ReadBytes(0x2D00); // idc

                    Console.WriteLine(reader.ReadInt32().ToString("x"));

                    reader.ReadBytes(4 * 16); // idc

                    for (int i = 0; i < 16; i++)
                        reader.ReadInt32();
                    for (int i = 0; i < 16; i++)
                        for (int j = 0; j < 30; j++)
                            reader.ReadInt32();
                    for (int i = 0; i < 16; i++)
                        reader.ReadInt32();
                    for (int i = 0; i < 16; i++)
                        for (int j = 0; j < 30; j++)
                            reader.ReadInt32();
                    for (int i = 0; i < 16; i++)
                        reader.ReadInt32();
                    for (int i = 0; i < 16; i++)
                        for (int j = 0; j < 20; j++)
                            reader.ReadInt32();

                    reader.ReadInt32();
                    reader.ReadInt32();

                    Console.WriteLine("alltech: " + reader.ReadInt32());

                    for (int i = 0; i < 16; i++)
                        Console.WriteLine("Player " + i + " age: " + reader.ReadInt32());

                    // Map

                    Console.WriteLine(reader.ReadInt32().ToString("x"));

                    Console.WriteLine("Actual Map Stuff Now: ");
                    Console.WriteLine("CameraY: " + reader.ReadInt32());
                    Console.WriteLine("CameraX: " + reader.ReadInt32());

                    Map = new ScenarioMap();
                    Map.Read(reader, FileVersion);
                }
            }
            else
            {
                throw new FileNotFoundException("File not found.", path);
            }
        }

        private byte[] inflate_file(BinaryReader reader, long length)
        {
            byte[] uncompressed = new byte[length*16];

            ZStream strm = new ZStream();
            strm.next_in = reader.ReadBytes((int)length);
            strm.avail_in = (int)length;
            strm.avail_out = 0;
            var code = strm.inflateInit(-15);

            var output = new System.Collections.Generic.List<byte>();

            while (code == zlibConst.Z_OK && strm.avail_out == 0)
            {
                strm.next_out = uncompressed;
                strm.avail_out = uncompressed.Length;
                code = strm.inflate(zlibConst.Z_SYNC_FLUSH);

                switch (code)
                {
                    case zlibConst.Z_OK:
                        for (int i = 0; i < uncompressed.Length; i++)
                            output.Add(uncompressed[i]);
                        break;

                    case zlibConst.Z_STREAM_END:
                        for (int i = 0; i < uncompressed.Length - strm.avail_out; i++)
                            output.Add(uncompressed[i]);
                        break;
                }
            }

            return output.ToArray();
        }
    }
}

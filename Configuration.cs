namespace OpenEmpires
{
    public static class Configuration
    {
        public static uint Width = 800;
        public static uint Height = 600;

        public static bool VSync = false;
        public static uint Framerate = 60;
        public static float Timestep = 1f / 60;

        public static int TileSize = 16;
        public static int TileChunkSize = 32;

        public static uint SpriteBatchSize = 512;

        public static string TextureLocation = "textures/"; // gross

        public static short[] TerrainTextureLookups = {
            15001, // 0 grass
            15002, // 1 water
            15017, // 2 beach
            15007, // 3 dirt 3
            15014, // 4 shallows
            15011, // 5 leaves ( forest replacement )
            15000, // 6 dirt
            15004, // 7 farm 1
            15005, // 8 farm 2
            15009, // 9 grass 3
            15011, // 10 forest
            15006, // 11 dirt 2
            15008, // 12 grass 2
            15010, // 13 palm desert
            15010, // 14 desert ( palm desert replacement )
            15002, // 15 old water ( water replacement )
            15001, // 16 old grass ( grass replacement )
            15011, // 17 jungle ( forest replacement )
            15011, // 18 bamboo ( forest replacement )
            15011, // 19 pine forest ( forest replacement )
            15011, // 20 oak forest ( forest replacement )
            15029, // 21 snow forest
            15015, // 22 water 2
            15016, // 23 water 3
            15018, // 24 road
            15019, // 25 road 2
            15024, // 26 ice
            15006, // 27 foundation
            15002, // 28 water bridge ( water replacement )
            15021, // 29 farm cnst1
            15022, // 30 farm cnst2
            15023, // 31 farm cnst3
            15026, // 32 snow
            15027, // 33 snow dirt
            15028, // 34 snow grass
            15024, // 35 ice 2 ( ice replacement )
            15027, // 36 snow foundation ( snow dirt replacement )
            15024, // 37 ice beach ( ice replacement )
            15030, // 38 snow road
            15031, // 39 snow road 2
            15018, // KOH?
        };
    }
}

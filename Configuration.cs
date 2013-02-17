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
    }
}

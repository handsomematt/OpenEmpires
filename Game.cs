using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;

using SFML.Graphics;
using SFML.Window;
using Tao.OpenGl;

using GenieLib;

namespace OpenEmpires
{
    public static class Game
    {
        public static RenderWindow Window;
        public static View DefaultView
        {
            get
            {
                var view = Window.DefaultView;
                view.Size = size;
                return view;
            }
        }
        public static View GameView;

        private static readonly bool[] KeyStates;
        private static Vector2f size;

        private static Map map;
        private static Sprite selected;
        private static Sprite selected2;

        public static EntityManager EntManager
        {
            get;
            private set;
        }

        static Game()
        {
            KeyStates = new bool[(int)Keyboard.Key.KeyCount];
        }

        public static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        public static void Initialize()
        {
            var style = Styles.Titlebar | Styles.Close;
            size = new Vector2f(Configuration.Width, Configuration.Height);

            Window = new RenderWindow(new VideoMode(Configuration.Width, Configuration.Height), "Open Empires", style);
            Window.SetFramerateLimit(Configuration.Framerate);
            Window.SetVerticalSyncEnabled(Configuration.VSync);

            Window.Closed += (sender, args) => Window.Close();
            Window.Resized += (sender, args) => Resize(new Vector2f(args.Width, args.Height));
            /*Window.MouseButtonPressed += (sender, args) => DispatchEvent(new MouseButtonInputArgs(args.Button, true, args.X, args.Y));
            Window.MouseButtonReleased += (sender, args) => DispatchEvent(new MouseButtonInputArgs(args.Button, false, args.X, args.Y));
            Window.MouseWheelMoved += (sender, args) => DispatchEvent(new MouseWheelInputArgs(args.Delta, args.X, args.Y));
            Window.MouseMoved += (sender, args) => DispatchEvent(new MouseMoveInputArgs(args.X, args.Y));*/

            Window.MouseMoved += new EventHandler<MouseMoveEventArgs>(Window_MouseMoved);
            Window.MouseWheelMoved += (sender, args) => GameView.Zoom(1 - (args.Delta * 0.1f));

            Window.KeyPressed += (sender, args) =>
            {
                KeyStates[(int)args.Code] = true;
            };

            Window.KeyReleased += (sender, args) =>
            {
                KeyStates[(int)args.Code] = false;
            };

            Blendomatic.ReadBlendomatic(new System.IO.FileStream("blendomatic.dat", System.IO.FileMode.Open));
            map = new Map();

            GameView = new View(DefaultView);
            GameView.Center = new Vector2f((map.Width/2)*96, 0);

            var blend = Blendomatic.BlendingModes[2];
            var tile = blend.Tiles[12];

            var img = new Image(97, 49);
            for (var y = 0; y < img.Size.Y; y++)
                for (var x = 0; x < img.Size.X; x++)
                    img.SetPixel((uint)x, (uint)y, new Color(255, 0, 0, 0));

            var i = 0;
            for (var y = 0; y < img.Size.X; y++)
            {
                var bytesPerRow = y < 24 ? 1 + (4 * y) : 97 - (4 * (y - 24));

                var startX = 48 - (bytesPerRow / 2);
                for (var x = 0; x < bytesPerRow; x++)
                    img.SetPixel((uint)(startX + x), (uint)y, new Color(0, 0, 0, tile[i++]));
            }

            var slpFile = new SLPFile();
            slpFile.LoadFile("textures/ter15002.slp");

            var spritewidth = slpFile.GetFrame(0).m_Width;
            var spriteheight = slpFile.GetFrame(0).m_Height;

            var imgg = new Image((uint)spritewidth, (uint)(spriteheight ), slpFile.GetFrame(0).GetRGBAArray());

            for (var y = 0; y < imgg.Size.Y; y++)
                for (var x = 0; x < imgg.Size.X; x++)
                {
                    var col = imgg.GetPixel((uint)x, (uint)y);
                    col.A = (byte)(Math.Min(255, (128-img.GetPixel((uint)x, (uint)y).A)*2));

                    if (img.GetPixel((uint)x, (uint)y).R == 255)
                        col.A = 0;

                    imgg.SetPixel((uint)x, (uint)y, col);
                }

            selected = new Sprite(new Texture(imgg));




            /* blend sand to forest now */

            var blend2 = Blendomatic.BlendingModes[2];
            var tile2 = blend2.Tiles[12];

            var img2 = new Image(97, 49);
            for (var y = 0; y < img2.Size.Y; y++)
                for (var x = 0; x < img2.Size.X; x++)
                    img2.SetPixel((uint)x, (uint)y, new Color(255, 0, 0, 0));

            var i2 = 0;
            for (var y = 0; y < img.Size.X; y++)
            {
                var bytesPerRow = y < 24 ? 1 + (4 * y) : 97 - (4 * (y - 24));

                var startX = 48 - (bytesPerRow / 2);
                for (var x = 0; x < bytesPerRow; x++)
                    img2.SetPixel((uint)(startX + x), (uint)y, new Color(0, 0, 0, tile[i2++]));
            }

            var slpFile2 = new SLPFile();
            slpFile2.LoadFile("textures/ter15017.slp");

            var spritewidth2 = slpFile2.GetFrame(0).m_Width;
            var spriteheight2 = slpFile2.GetFrame(0).m_Height;

            var imgg2 = new Image((uint)spritewidth2, (uint)(spriteheight2), slpFile2.GetFrame(0).GetRGBAArray());

            for (var y = 0; y < imgg2.Size.Y; y++)
                for (var x = 0; x < imgg2.Size.X; x++)
                {
                    var col = imgg2.GetPixel((uint)x, (uint)y);
                    col.A = (byte)(Math.Min(255, (128 - img.GetPixel((uint)x, (uint)y).A) * 2));

                    if (img2.GetPixel((uint)x, (uint)y).R == 255)
                        col.A = 0;

                    imgg2.SetPixel((uint)x, (uint)y, col);
                }

            selected2 = new Sprite(new Texture(imgg2));

            EntManager = new EntityManager();
        }

        static void Window_MouseMoved(object sender, MouseMoveEventArgs e)
        {

        }

        public static void Run()
        {
            var timer = new Stopwatch();
            double accumulator = 0;

            while (Window.IsOpen())
            {
                var time = timer.Elapsed.TotalSeconds;
                timer.Restart();

                accumulator += time;

                while (accumulator >= Configuration.Timestep)
                {
                    Window.DispatchEvents();
                    Update(time);
                    accumulator -= Configuration.Timestep;
                }

                Window.Clear(Color.Black);

                Draw(time);

                Window.Display();
            }
        }

        public static void Exit()
        {
            Window.Close();
        }

        private static void Resize(Vector2f newSize)
        {
            size = newSize;
        }

        private static void Update(double time)
        {
            var movex = 0.0f;
            var movey = 0.0f;
            var multiplier = 1f;

            if (KeyStates[(int)Keyboard.Key.LShift])
                multiplier = 8f;
            
            if (KeyStates[(int)Keyboard.Key.Up])
                movey -= (float)time * 250 * multiplier;

            if (KeyStates[(int)Keyboard.Key.Down])
                movey += (float)time * 250 * multiplier;

            if (KeyStates[(int)Keyboard.Key.Left])
                movex -= (float)time * 250 * multiplier;

            if (KeyStates[(int)Keyboard.Key.Right])
                movex += (float)time * 250 * multiplier;

            movex *= GameView.Size.X / Window.Size.X;
            movey *= GameView.Size.Y / Window.Size.Y;

            GameView.Move(new Vector2f(movex, movey));
        }

        private static void Draw(double time)
        {
            Window.SetView(GameView);
            Window.Draw(map);

            Window.Draw(EntManager);

            selected.Position = new Vector2f((map.Width / 2) * 96 - 96 - 96 - 48, 24 * -48 + 24);
            Window.Draw(selected);
            selected.Position = new Vector2f((map.Width / 2) * 96 - 96 - 96, 24 * -48);
            Window.Draw(selected);
            selected.Position = new Vector2f((map.Width / 2) * 96 - 96 - 48, 24 * -48 - 24);
            Window.Draw(selected);
            selected.Position = new Vector2f((map.Width / 2) * 96 - 96, 25 * -48);
            Window.Draw(selected);

            selected2.Position = new Vector2f((map.Width / 2) * 96 - 96 - 96 - 48 + 48, 24 * -48 + 24 + 24);
            Window.Draw(selected2);
            selected2.Position = new Vector2f((map.Width / 2) * 96 - 96 - 96 + 48, 24 * -48 + 24);
            Window.Draw(selected2);
            selected2.Position = new Vector2f((map.Width / 2) * 96 - 96 - 48 + 48, 24 * -48 - 24 + 24);
            Window.Draw(selected2);
            selected2.Position = new Vector2f((map.Width / 2) * 96 - 96 + 48, 25 * -48 + 24);
            Window.Draw(selected2);
        }
    }
}

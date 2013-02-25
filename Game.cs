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

        static Game()
        {
            KeyStates = new bool[(int)Keyboard.Key.KeyCount];
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

            var blend = Blendomatic.BlendingModes[5];
            var tile = blend.Tiles[2];

            // todo convert this into an isometric 97*49 texture from a 48*49 texture??

            var img = new Image(97, 49, tile);
            selected = new Sprite(new Texture(img));
            selected.Position = new Vector2f(0, 0);

            GameView = new View(DefaultView);
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
            var multiplier = 1;

            if (KeyStates[(int)Keyboard.Key.LShift])
                multiplier = 8;

            if (KeyStates[(int)Keyboard.Key.Up])
                movey -= (float)time * 250 * multiplier;

            if (KeyStates[(int)Keyboard.Key.Down])
                movey += (float)time * 250 * multiplier;

            if (KeyStates[(int)Keyboard.Key.Left])
                movex -= (float)time * 250 * multiplier;

            if (KeyStates[(int)Keyboard.Key.Right])
                movex += (float)time * 250 * multiplier;

            GameView.Move(new Vector2f(movex, movey));
        }

        private static void Draw(double time)
        {
            Window.SetView(GameView);
            Window.Draw(map);

            Window.Draw(selected);
        }
    }
}

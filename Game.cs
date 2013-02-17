﻿using System;
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

            Window.KeyPressed += (sender, args) =>
            {
                KeyStates[(int)args.Code] = true;
            };

            Window.KeyReleased += (sender, args) =>
            {
                KeyStates[(int)args.Code] = false;
            };

            var terrain = new SLPFile();
            terrain.LoadFile("textures/terrain.slp");
            map = new Map(16, 16, terrain);

            Console.WriteLine(terrain.m_Frames.Count);

            GameView = new View(DefaultView);
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
            if (KeyStates[(int)Keyboard.Key.Up])
            {
                GameView.Move(new Vector2f(0, (float)time * -250));
            }

            if (KeyStates[(int)Keyboard.Key.Down])
            {
                GameView.Move(new Vector2f(0, (float)time * 250));
            }

            if (KeyStates[(int)Keyboard.Key.Left])
            {
                GameView.Move(new Vector2f((float)time * -250, 0));
            }

            if (KeyStates[(int)Keyboard.Key.Right])
            {
                GameView.Move(new Vector2f((float)time * 250, 0));
            }
        }

        private static void Draw(double time)
        {
            Window.SetView(GameView);
            Window.Draw(map);
        }
    }
}
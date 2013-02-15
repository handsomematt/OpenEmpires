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
    public class Program
    {
        static void Main(string[] args)
        {
            Game.Initialize();
            Game.Run();
        }
    }
}

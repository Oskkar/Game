using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace RiverRaid2
{
    class Menu
    {
        private static bool _IsShowMainMenuScene;
        private static bool _IsShowGameScene;


        public static bool IsShowMainMenuScene { get { return _IsShowMainMenuScene; } set { if (value == true) { _IsShowGameScene = false; } _IsShowMainMenuScene = value; } }
        public static bool IsShowGameScene { get { return _IsShowGameScene; } set { if (value == true) { _IsShowMainMenuScene = false; } _IsShowGameScene = value; } }
    }
}
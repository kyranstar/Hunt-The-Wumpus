using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MonoGame.Framework;
using HuntTheWumpus.GameCore;

namespace HuntTheWumpus.Platform.ModernWindows
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : SwapChainBackgroundPanel
    {
        readonly GameHost _game;

        public GamePage(string launchArguments)
        {
            this.InitializeComponent();

            // Create the game.

            _game = XamlGame<GameHost>.Create(launchArguments, Window.Current.CoreWindow, this);
        }
    }
}

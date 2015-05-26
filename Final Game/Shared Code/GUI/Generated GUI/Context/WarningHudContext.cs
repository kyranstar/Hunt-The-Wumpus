using EmptyKeys.UserInterface;
using HuntTheWumpus.SharedCode.GameControl;
using HuntTheWumpus.SharedCode.GameMap;
using HuntTheWumpus.SharedCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Controls;

namespace HuntTheWumpus.SharedCode.GUI
{
    class WarningHudContext
    {
        public const string WarningGroup = "WarningText";

        private GameController GameController;

        Action<string> RaisePropertyChangedForGroup;

        private PlayerWarnings[] PreviousWarnings = new PlayerWarnings[0];

        public WarningHudContext(GameController gameController, Action<string> RaisePropertyChangedForGroup)
        {
            this.RaisePropertyChangedForGroup = RaisePropertyChangedForGroup;
            GameController = gameController;
        }

        public void Reset()
        {

        }

        [PropertyGroup(WarningGroup)]
        public string WarningText
        {
            get; protected set;
        }

        [PropertyGroup(WarningGroup)]
        public Visibility WarningVisibility
        {
            get
            {
                return string.IsNullOrEmpty(WarningText) ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public void Update(GameTime gameTime)
        {
            PlayerWarnings[] NewWarnings = GameController.Map.GetPlayerWarnings().ToArray();
            if (NewWarnings.Length > 0)
                WarningText = NewWarnings.First().GetDescription();
            else
                WarningText = null;

            int NumChanged = NewWarnings.Except(PreviousWarnings).Union(PreviousWarnings.Except(NewWarnings)).Count();
            if (NumChanged > 0)
                RaisePropertyChangedForGroup(WarningGroup);

            PreviousWarnings = NewWarnings;
        }
    }
}

using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Mvvm;
using EmptyKeys.UserInterface;
using HuntTheWumpus.SharedCode.GameControl;
using HuntTheWumpus.SharedCode.GameMap;
using HuntTheWumpus.SharedCode.Scores;
using System;
using EmptyKeys.UserInterface.Controls;
using System.Linq;
using Microsoft.Xna.Framework;
using HuntTheWumpus.SharedCode.Helpers;
using HuntTheWumpus.SharedCode.Trivia;

namespace HuntTheWumpus.SharedCode.GUI
{
    class HUDContext : ViewModelBase
    {
        Player Player;

        GameController GameController;

        public ScoreHudContext ScoreContext { get; set; }

        [PropertyGroup(TriviaHudContext.QuestionBindingGroup)]
        [PropertyGroup(XamlBoundAnimation.XamlAnimationGroupName)]
        public TriviaHudContext TriviaContext { get; set; }

        [PropertyGroup(FlyoutHudContext.FlyoutDataGroup)]
        [PropertyGroup(FlyoutHudContext.FlyoutVisibilityGroup)]
        [PropertyGroup(XamlBoundAnimation.XamlAnimationGroupName)]
        public FlyoutHudContext FlyoutContext { get; set; }

        [PropertyGroup(GameOverHudContext.GameOverBindingGroup)]
        [PropertyGroup(GameOverHudContext.GameOverVisibilityGroup)]
        public GameOverHudContext GameOverContext { get; set; }

        [PropertyGroup(WarningHudContext.WarningGroup)]
        public WarningHudContext WarningContext { get; set; }

        public HUDContext(GameController GameController)
        {
            this.GameController = GameController;
            this.Player = GameController.Map.Player;

            Player.PropertyChanged += Player_PropertyChanged;
            GameController.OnNewQuestion += Trivia_NewQuestion;

            ScoreContext = new ScoreHudContext(Player);
            TriviaContext = new TriviaHudContext(GameController, RaisePropertyChangedForGroup);
            GameOverContext = new GameOverHudContext(GameController, RaisePropertyChangedForGroup);
            WarningContext = new WarningHudContext(GameController, RaisePropertyChangedForGroup);
            FlyoutContext = new FlyoutHudContext(GameController, RaisePropertyChangedForGroup);
        }

        public void Reset()
        {
            ScoreContext.Reset();
            TriviaContext.Reset();
            GameOverContext.Reset();
        }

        private void RaisePropertyChangedForGroup(string PrimaryGroupName, string SecondaryGroupName)
        {
            string[] QuestionBindingProps = PropertyGroupAttribute.GetPropertyNamesByGroup(this.GetType(), PrimaryGroupName, SecondaryGroupName);

            foreach (string Prop in QuestionBindingProps)
                RaisePropertyChanged(Prop);
        }

        private void RaisePropertyChangedForGroup(string PrimaryGroupName)
        {
            RaisePropertyChangedForGroup(PrimaryGroupName, null);
        }

        private void Player_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(e.PropertyName);
        }

        private void Trivia_NewQuestion(object sender, EventArgs e)
        {
            TriviaContext.SelectedAnswer = null;
            RaisePropertyChangedForGroup(TriviaHudContext.QuestionBindingGroup);
        }

        public void Update(GameTime GameTime)
        {
            TriviaContext.Update(GameTime);
            GameOverContext.Update(GameTime);
            WarningContext.Update(GameTime);
        }
    }
}

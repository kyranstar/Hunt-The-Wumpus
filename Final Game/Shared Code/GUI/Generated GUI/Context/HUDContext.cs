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

        [PropertyGroup(GameOverHudContext.GameOverBindingGroup)]
        [PropertyGroup(GameOverHudContext.GameOverVisibilityGroup)]
        public GameOverHudContext GameOverContext { get; set; }

        [PropertyGroup(WarningHudContext.WarningGroup)]
        public WarningHudContext WarningContext { get; set; }

        private const string HintGroup = "HintVisibility";

        public HUDContext(GameController GameController)
        {
            this.GameController = GameController;
            this.Player = GameController.Map.Player;

            Player.PropertyChanged += Player_PropertyChanged;
            GameController.OnNewQuestion += Trivia_NewQuestion;
            GameController.OnNewHintAvailable += GameController_OnNewHintAvailable;

            ShowHintsCommand = new RelayCommand(new Action<object>(ShowHints));
            BuyHintsCommand = new RelayCommand(new Action<object>(BuyHints));

            HintFlyoutVisibility = Visibility.Hidden;

            ScoreContext = new ScoreHudContext(Player);
            TriviaContext = new TriviaHudContext(GameController, RaisePropertyChangedForGroup);
            GameOverContext = new GameOverHudContext(GameController, RaisePropertyChangedForGroup);
            WarningContext = new WarningHudContext(GameController, RaisePropertyChangedForGroup);
        }

        public void Reset()
        {
            HintFlyoutVisibility = Visibility.Hidden;
            ScoreContext.Reset();
            TriviaContext.Reset();
            GameOverContext.Reset();
        }

        private void GameController_OnNewHintAvailable(object sender, EventArgs e)
        {
            RaisePropertyChangedForGroup(HintGroup);
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

        public ICommand ShowHintsCommand
        {
            get;
            protected set;
        }

        public ICommand BuyHintsCommand
        {
            get;
            protected set;
        }

        [PropertyGroup(HintGroup)]
        public Visibility HintFlyoutVisibility
        {
            get; set;
        }

        [PropertyGroup(HintGroup)]
        public string[] UnlockedHints
        {
            get
            {
                return Trivia.Trivia.AvailableHints.ToArray();
            }
        }

        public void ShowHints(object o)
        {
            if(HintFlyoutVisibility == Visibility.Visible)
                HintFlyoutVisibility = Visibility.Hidden;
            else
                HintFlyoutVisibility = Visibility.Visible;

            RaisePropertyChangedForGroup(HintGroup);
        }

        public void BuyHints(object o)
        {
            GameController.LoadNewTrivia(TriviaQuestionState.PurchasingHint, 3);
        }

        public void Update(GameTime GameTime)
        {
            TriviaContext.Update(GameTime);
            GameOverContext.Update(GameTime);
            WarningContext.Update(GameTime);
        }
    }
}

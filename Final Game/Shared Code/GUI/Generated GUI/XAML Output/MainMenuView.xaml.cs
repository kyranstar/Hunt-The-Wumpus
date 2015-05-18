// -----------------------------------------------------------
//  
//  This file was generated, please do not modify.
//  
// -----------------------------------------------------------
namespace EmptyKeys.UserInterface.Generated {
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.ObjectModel;
    using EmptyKeys.UserInterface;
    using EmptyKeys.UserInterface.Data;
    using EmptyKeys.UserInterface.Controls;
    using EmptyKeys.UserInterface.Controls.Primitives;
    using EmptyKeys.UserInterface.Input;
    using EmptyKeys.UserInterface.Media;
    using EmptyKeys.UserInterface.Media.Animation;
    using EmptyKeys.UserInterface.Media.Imaging;
    using EmptyKeys.UserInterface.Shapes;
    using EmptyKeys.UserInterface.Renderers;
    using EmptyKeys.UserInterface.Themes;
    
    
    [GeneratedCodeAttribute("Empty Keys UI Generator", "1.6.5.0")]
    public partial class MainMenuView : UIRoot {
        
        private Grid e_17;
        
        private TextBlock e_18;
        
        private Button StartButton;
        
        private Button HighScoreButton;
        
        public MainMenuView(int width, int height) : 
                base(width, height) {
            Style style = RootStyle.CreateRootStyle();
            style.TargetType = this.GetType();
            this.Style = style;
            this.InitializeComponent();
        }
        
        private void InitializeComponent() {
            FontManager.Instance.AddFont("Segoe UI", 12F, FontStyle.Regular, "Segoe_UI_9_Regular");
            // e_17 element
            this.e_17 = new Grid();
            this.Content = this.e_17;
            this.e_17.Name = "e_17";
            RowDefinition row_e_17_0 = new RowDefinition();
            row_e_17_0.Height = new GridLength(1F, GridUnitType.Star);
            this.e_17.RowDefinitions.Add(row_e_17_0);
            RowDefinition row_e_17_1 = new RowDefinition();
            row_e_17_1.Height = new GridLength(2F, GridUnitType.Star);
            this.e_17.RowDefinitions.Add(row_e_17_1);
            RowDefinition row_e_17_2 = new RowDefinition();
            row_e_17_2.Height = new GridLength(2F, GridUnitType.Star);
            this.e_17.RowDefinitions.Add(row_e_17_2);
            RowDefinition row_e_17_3 = new RowDefinition();
            row_e_17_3.Height = new GridLength(2F, GridUnitType.Star);
            this.e_17.RowDefinitions.Add(row_e_17_3);
            RowDefinition row_e_17_4 = new RowDefinition();
            row_e_17_4.Height = new GridLength(2F, GridUnitType.Star);
            this.e_17.RowDefinitions.Add(row_e_17_4);
            ColumnDefinition col_e_17_0 = new ColumnDefinition();
            this.e_17.ColumnDefinitions.Add(col_e_17_0);
            ColumnDefinition col_e_17_1 = new ColumnDefinition();
            this.e_17.ColumnDefinitions.Add(col_e_17_1);
            ColumnDefinition col_e_17_2 = new ColumnDefinition();
            this.e_17.ColumnDefinitions.Add(col_e_17_2);
            // e_18 element
            this.e_18 = new TextBlock();
            this.e_17.Children.Add(this.e_18);
            this.e_18.Name = "e_18";
            this.e_18.HorizontalAlignment = HorizontalAlignment.Center;
            this.e_18.VerticalAlignment = VerticalAlignment.Center;
            this.e_18.Foreground = new SolidColorBrush(new ColorW(211, 211, 211, 255));
            this.e_18.Text = "Hunt the Wumpus";
            FontManager.Instance.AddFont("Segoe UI", 20F, FontStyle.Bold, "Segoe_UI_15_Bold");
            this.e_18.FontFamily = new FontFamily("Segoe UI");
            this.e_18.FontSize = 20F;
            this.e_18.FontStyle = FontStyle.Bold;
            // StartButton element
            this.StartButton = new Button();
            this.e_17.Children.Add(this.StartButton);
            this.StartButton.Name = "StartButton";
            this.StartButton.Margin = new Thickness(5F, 5F, 5F, 5F);
            FontManager.Instance.AddFont("Segoe UI", 12F, FontStyle.Regular, "Segoe_UI_9_Regular");
            this.StartButton.Content = "Start Game";
            Grid.SetColumn(this.StartButton, 1);
            Grid.SetRow(this.StartButton, 2);
            Binding binding_StartButton_Command = new Binding("RunGameCommand");
            this.StartButton.SetBinding(Button.CommandProperty, binding_StartButton_Command);
            // HighScoreButton element
            this.HighScoreButton = new Button();
            this.e_17.Children.Add(this.HighScoreButton);
            this.HighScoreButton.Name = "HighScoreButton";
            this.HighScoreButton.Margin = new Thickness(5F, 5F, 5F, 5F);
            FontManager.Instance.AddFont("Segoe UI", 12F, FontStyle.Regular, "Segoe_UI_9_Regular");
            this.HighScoreButton.Content = "View Scores";
            Grid.SetColumn(this.HighScoreButton, 1);
            Grid.SetRow(this.HighScoreButton, 3);
            Binding binding_HighScoreButton_Command = new Binding("ShowScoresCommand");
            this.HighScoreButton.SetBinding(Button.CommandProperty, binding_HighScoreButton_Command);
        }
    }
}

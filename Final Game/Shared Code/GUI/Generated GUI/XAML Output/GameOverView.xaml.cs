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
    public partial class GameOverView : UIRoot {
        
        private Grid e_0;
        
        private TextBlock e_1;
        
        private TextBlock e_2;
        
        private Button MenuButton;
        
        private Button HighScoreButton;
        
        public GameOverView(int width, int height) : 
                base(width, height) {
            Style style = RootStyle.CreateRootStyle();
            style.TargetType = this.GetType();
            this.Style = style;
            this.InitializeComponent();
        }
        
        private void InitializeComponent() {
            FontManager.Instance.AddFont("Segoe UI", 12F, FontStyle.Regular, "Segoe_UI_9_Regular");
            // e_0 element
            this.e_0 = new Grid();
            this.Content = this.e_0;
            this.e_0.Name = "e_0";
            RowDefinition row_e_0_0 = new RowDefinition();
            row_e_0_0.Height = new GridLength(1F, GridUnitType.Star);
            this.e_0.RowDefinitions.Add(row_e_0_0);
            RowDefinition row_e_0_1 = new RowDefinition();
            row_e_0_1.Height = new GridLength(2F, GridUnitType.Star);
            this.e_0.RowDefinitions.Add(row_e_0_1);
            RowDefinition row_e_0_2 = new RowDefinition();
            row_e_0_2.Height = new GridLength(2F, GridUnitType.Star);
            this.e_0.RowDefinitions.Add(row_e_0_2);
            RowDefinition row_e_0_3 = new RowDefinition();
            row_e_0_3.Height = new GridLength(2F, GridUnitType.Star);
            this.e_0.RowDefinitions.Add(row_e_0_3);
            RowDefinition row_e_0_4 = new RowDefinition();
            row_e_0_4.Height = new GridLength(2F, GridUnitType.Star);
            this.e_0.RowDefinitions.Add(row_e_0_4);
            ColumnDefinition col_e_0_0 = new ColumnDefinition();
            this.e_0.ColumnDefinitions.Add(col_e_0_0);
            ColumnDefinition col_e_0_1 = new ColumnDefinition();
            this.e_0.ColumnDefinitions.Add(col_e_0_1);
            ColumnDefinition col_e_0_2 = new ColumnDefinition();
            this.e_0.ColumnDefinitions.Add(col_e_0_2);
            // e_1 element
            this.e_1 = new TextBlock();
            this.e_0.Children.Add(this.e_1);
            this.e_1.Name = "e_1";
            this.e_1.HorizontalAlignment = HorizontalAlignment.Center;
            this.e_1.VerticalAlignment = VerticalAlignment.Center;
            this.e_1.Foreground = new SolidColorBrush(new ColorW(211, 211, 211, 255));
            this.e_1.Text = "Hunt the Wumpus";
            FontManager.Instance.AddFont("Segoe UI", 20F, FontStyle.Bold, "Segoe_UI_15_Bold");
            this.e_1.FontFamily = new FontFamily("Segoe UI");
            this.e_1.FontSize = 20F;
            this.e_1.FontStyle = FontStyle.Bold;
            // e_2 element
            this.e_2 = new TextBlock();
            this.e_0.Children.Add(this.e_2);
            this.e_2.Name = "e_2";
            this.e_2.HorizontalAlignment = HorizontalAlignment.Center;
            this.e_2.VerticalAlignment = VerticalAlignment.Center;
            this.e_2.Foreground = new SolidColorBrush(new ColorW(211, 211, 211, 255));
            this.e_2.Text = "Game over!";
            FontManager.Instance.AddFont("Segoe UI", 53.33333F, FontStyle.Bold, "Segoe_UI_40_Bold");
            this.e_2.FontFamily = new FontFamily("Segoe UI");
            this.e_2.FontSize = 53.33333F;
            this.e_2.FontStyle = FontStyle.Bold;
            Grid.SetRow(this.e_2, 1);
            Grid.SetColumnSpan(this.e_2, 3);
            // MenuButton element
            this.MenuButton = new Button();
            this.e_0.Children.Add(this.MenuButton);
            this.MenuButton.Name = "MenuButton";
            this.MenuButton.Margin = new Thickness(5F, 5F, 5F, 5F);
            FontManager.Instance.AddFont("Segoe UI", 12F, FontStyle.Regular, "Segoe_UI_9_Regular");
            this.MenuButton.Content = "Back to Main Menu";
            Grid.SetColumn(this.MenuButton, 1);
            Grid.SetRow(this.MenuButton, 2);
            Binding binding_MenuButton_Command = new Binding("MainMenuCommand");
            this.MenuButton.SetBinding(Button.CommandProperty, binding_MenuButton_Command);
            // HighScoreButton element
            this.HighScoreButton = new Button();
            this.e_0.Children.Add(this.HighScoreButton);
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

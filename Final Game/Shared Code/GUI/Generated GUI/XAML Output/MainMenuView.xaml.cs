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
        
        private Grid e_21;
        
        private TextBlock e_22;
        
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
            // e_21 element
            this.e_21 = new Grid();
            this.Content = this.e_21;
            this.e_21.Name = "e_21";
            RowDefinition row_e_21_0 = new RowDefinition();
            row_e_21_0.Height = new GridLength(1F, GridUnitType.Star);
            this.e_21.RowDefinitions.Add(row_e_21_0);
            RowDefinition row_e_21_1 = new RowDefinition();
            row_e_21_1.Height = new GridLength(2F, GridUnitType.Star);
            this.e_21.RowDefinitions.Add(row_e_21_1);
            RowDefinition row_e_21_2 = new RowDefinition();
            row_e_21_2.Height = new GridLength(2F, GridUnitType.Star);
            this.e_21.RowDefinitions.Add(row_e_21_2);
            RowDefinition row_e_21_3 = new RowDefinition();
            row_e_21_3.Height = new GridLength(2F, GridUnitType.Star);
            this.e_21.RowDefinitions.Add(row_e_21_3);
            RowDefinition row_e_21_4 = new RowDefinition();
            row_e_21_4.Height = new GridLength(2F, GridUnitType.Star);
            this.e_21.RowDefinitions.Add(row_e_21_4);
            ColumnDefinition col_e_21_0 = new ColumnDefinition();
            this.e_21.ColumnDefinitions.Add(col_e_21_0);
            ColumnDefinition col_e_21_1 = new ColumnDefinition();
            this.e_21.ColumnDefinitions.Add(col_e_21_1);
            ColumnDefinition col_e_21_2 = new ColumnDefinition();
            this.e_21.ColumnDefinitions.Add(col_e_21_2);
            // e_22 element
            this.e_22 = new TextBlock();
            this.e_21.Children.Add(this.e_22);
            this.e_22.Name = "e_22";
            this.e_22.HorizontalAlignment = HorizontalAlignment.Center;
            this.e_22.VerticalAlignment = VerticalAlignment.Center;
            this.e_22.Foreground = new SolidColorBrush(new ColorW(211, 211, 211, 255));
            this.e_22.Text = "Hunt the Wumpus";
            FontManager.Instance.AddFont("Arcadepix", 20F, FontStyle.Bold, "Arcadepix_15_Bold");
            this.e_22.FontFamily = new FontFamily("Arcadepix");
            this.e_22.FontSize = 20F;
            this.e_22.FontStyle = FontStyle.Bold;
            // StartButton element
            this.StartButton = new Button();
            this.e_21.Children.Add(this.StartButton);
            this.StartButton.Name = "StartButton";
            this.StartButton.Margin = new Thickness(5F, 5F, 5F, 5F);
            FontManager.Instance.AddFont("Arcadepix", 20F, FontStyle.Regular, "Arcadepix_15_Regular");
            this.StartButton.FontFamily = new FontFamily("Arcadepix");
            this.StartButton.FontSize = 20F;
            this.StartButton.Content = "Start Game";
            Grid.SetColumn(this.StartButton, 1);
            Grid.SetRow(this.StartButton, 2);
            Binding binding_StartButton_Command = new Binding("RunGameCommand");
            this.StartButton.SetBinding(Button.CommandProperty, binding_StartButton_Command);
            // HighScoreButton element
            this.HighScoreButton = new Button();
            this.e_21.Children.Add(this.HighScoreButton);
            this.HighScoreButton.Name = "HighScoreButton";
            this.HighScoreButton.Margin = new Thickness(5F, 5F, 5F, 5F);
            FontManager.Instance.AddFont("Arcadepix", 20F, FontStyle.Regular, "Arcadepix_15_Regular");
            this.HighScoreButton.FontFamily = new FontFamily("Arcadepix");
            this.HighScoreButton.FontSize = 20F;
            this.HighScoreButton.Content = "View Scores";
            Grid.SetColumn(this.HighScoreButton, 1);
            Grid.SetRow(this.HighScoreButton, 3);
            Binding binding_HighScoreButton_Command = new Binding("ShowScoresCommand");
            this.HighScoreButton.SetBinding(Button.CommandProperty, binding_HighScoreButton_Command);
        }
    }
}

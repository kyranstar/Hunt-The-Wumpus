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
    public partial class HighScoreView : UIRoot {
        
        private Grid LayoutRoot;
        
        private Button BackButton;
        
        private DataGrid ScoreTable;
        
        public HighScoreView(int width, int height) : 
                base(width, height) {
            Style style = RootStyle.CreateRootStyle();
            style.TargetType = this.GetType();
            this.Style = style;
            this.InitializeComponent();
        }
        
        private void InitializeComponent() {
            FontManager.Instance.AddFont("Segoe UI", 12F, FontStyle.Regular, "Segoe_UI_9_Regular");
            // LayoutRoot element
            this.LayoutRoot = new Grid();
            this.Content = this.LayoutRoot;
            this.LayoutRoot.Name = "LayoutRoot";
            RowDefinition row_LayoutRoot_0 = new RowDefinition();
            row_LayoutRoot_0.Height = new GridLength(40F, GridUnitType.Pixel);
            this.LayoutRoot.RowDefinitions.Add(row_LayoutRoot_0);
            RowDefinition row_LayoutRoot_1 = new RowDefinition();
            this.LayoutRoot.RowDefinitions.Add(row_LayoutRoot_1);
            // BackButton element
            this.BackButton = new Button();
            this.LayoutRoot.Children.Add(this.BackButton);
            this.BackButton.Name = "BackButton";
            this.BackButton.Width = 150F;
            this.BackButton.Margin = new Thickness(5F, 5F, 5F, 5F);
            this.BackButton.HorizontalAlignment = HorizontalAlignment.Left;
            FontManager.Instance.AddFont("Segoe UI", 12F, FontStyle.Regular, "Segoe_UI_9_Regular");
            this.BackButton.Content = "← Back to Main Menu";
            Grid.SetRow(this.BackButton, 0);
            Binding binding_BackButton_Command = new Binding("BackCommand");
            this.BackButton.SetBinding(Button.CommandProperty, binding_BackButton_Command);
            // ScoreTable element
            this.ScoreTable = new DataGrid();
            this.LayoutRoot.Children.Add(this.ScoreTable);
            this.ScoreTable.Name = "ScoreTable";
            FontManager.Instance.AddFont("Segoe UI", 12F, FontStyle.Regular, "Segoe_UI_9_Regular");
            Grid.SetRow(this.ScoreTable, 1);
            Binding binding_ScoreTable_ItemsSource = new Binding("HighScores");
            this.ScoreTable.SetBinding(DataGrid.ItemsSourceProperty, binding_ScoreTable_ItemsSource);
        }
    }
}

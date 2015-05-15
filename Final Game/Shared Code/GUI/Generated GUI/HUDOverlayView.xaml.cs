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
    public partial class HUDOverlayView : UIRoot {
        
        private Grid UIRoot;
        
        private Grid GameHUD;
        
        private StackPanel e_0;
        
        private Image e_1;
        
        private TextBlock e_2;
        
        private TextBlock e_3;
        
        private StackPanel e_4;
        
        private TextBlock e_5;
        
        private Grid TriviaDisplay;
        
        private TextBlock QuestionText;
        
        private ComboBox TriviaAnswerSelector;
        
        public HUDOverlayView(int width, int height) : 
                base(width, height) {
            Style style = RootStyle.CreateRootStyle();
            style.TargetType = this.GetType();
            this.Style = style;
            this.InitializeComponent();
        }
        
        private void InitializeComponent() {
            this.Background = new SolidColorBrush(new ColorW(255, 255, 255, 0));
            FontManager.Instance.AddFont("Segoe UI", 12F, FontStyle.Regular, "Segoe_UI_9_Regular");
            // UIRoot element
            this.UIRoot = new Grid();
            this.Content = this.UIRoot;
            this.UIRoot.Name = "UIRoot";
            this.UIRoot.Background = new SolidColorBrush(new ColorW(255, 255, 255, 0));
            // GameHUD element
            this.GameHUD = new Grid();
            this.UIRoot.Children.Add(this.GameHUD);
            this.GameHUD.Name = "GameHUD";
            this.GameHUD.Background = new SolidColorBrush(new ColorW(255, 255, 255, 0));
            RowDefinition row_GameHUD_0 = new RowDefinition();
            row_GameHUD_0.Height = new GridLength(1F, GridUnitType.Star);
            this.GameHUD.RowDefinitions.Add(row_GameHUD_0);
            RowDefinition row_GameHUD_1 = new RowDefinition();
            row_GameHUD_1.Height = new GridLength(30F, GridUnitType.Pixel);
            this.GameHUD.RowDefinitions.Add(row_GameHUD_1);
            // e_0 element
            this.e_0 = new StackPanel();
            this.GameHUD.Children.Add(this.e_0);
            this.e_0.Name = "e_0";
            this.e_0.Background = new SolidColorBrush(new ColorW(169, 169, 169, 255));
            this.e_0.Orientation = Orientation.Horizontal;
            Grid.SetRow(this.e_0, 1);
            // e_1 element
            this.e_1 = new Image();
            this.e_0.Children.Add(this.e_1);
            this.e_1.Name = "e_1";
            this.e_1.Height = 30F;
            this.e_1.Width = 30F;
            BitmapImage e_1_bm = new BitmapImage();
            e_1_bm.TextureAsset = "XAML Assets/Gold";
            ImageManager.Instance.AddImage("XAML Assets/Gold");
            this.e_1.Source = e_1_bm;
            // e_2 element
            this.e_2 = new TextBlock();
            this.e_0.Children.Add(this.e_2);
            this.e_2.Name = "e_2";
            this.e_2.Margin = new Thickness(4F, 0F, 4F, 0F);
            this.e_2.Foreground = new SolidColorBrush(new ColorW(211, 211, 211, 255));
            FontManager.Instance.AddFont("Segoe UI", 20F, FontStyle.Bold, "Segoe_UI_15_Bold");
            this.e_2.FontFamily = new FontFamily("Segoe UI");
            this.e_2.FontSize = 20F;
            this.e_2.FontStyle = FontStyle.Bold;
            Binding binding_e_2_Text = new Binding("Gold");
            this.e_2.SetBinding(TextBlock.TextProperty, binding_e_2_Text);
            // e_3 element
            this.e_3 = new TextBlock();
            this.e_0.Children.Add(this.e_3);
            this.e_3.Name = "e_3";
            this.e_3.Margin = new Thickness(4F, 0F, 4F, 0F);
            this.e_3.Foreground = new SolidColorBrush(new ColorW(211, 211, 211, 255));
            FontManager.Instance.AddFont("Segoe UI", 20F, FontStyle.Bold, "Segoe_UI_15_Bold");
            this.e_3.FontFamily = new FontFamily("Segoe UI");
            this.e_3.FontSize = 20F;
            this.e_3.FontStyle = FontStyle.Bold;
            Binding binding_e_3_Text = new Binding("Arrows");
            this.e_3.SetBinding(TextBlock.TextProperty, binding_e_3_Text);
            // e_4 element
            this.e_4 = new StackPanel();
            this.GameHUD.Children.Add(this.e_4);
            this.e_4.Name = "e_4";
            this.e_4.HorizontalAlignment = HorizontalAlignment.Right;
            this.e_4.Background = new SolidColorBrush(new ColorW(169, 169, 169, 255));
            this.e_4.Orientation = Orientation.Horizontal;
            Grid.SetRow(this.e_4, 1);
            // e_5 element
            this.e_5 = new TextBlock();
            this.e_4.Children.Add(this.e_5);
            this.e_5.Name = "e_5";
            this.e_5.Margin = new Thickness(4F, 0F, 4F, 0F);
            this.e_5.Foreground = new SolidColorBrush(new ColorW(211, 211, 211, 255));
            FontManager.Instance.AddFont("Segoe UI", 20F, FontStyle.Bold, "Segoe_UI_15_Bold");
            this.e_5.FontFamily = new FontFamily("Segoe UI");
            this.e_5.FontSize = 20F;
            this.e_5.FontStyle = FontStyle.Bold;
            Binding binding_e_5_Text = new Binding("Turns");
            this.e_5.SetBinding(TextBlock.TextProperty, binding_e_5_Text);
            // TriviaDisplay element
            this.TriviaDisplay = new Grid();
            this.UIRoot.Children.Add(this.TriviaDisplay);
            this.TriviaDisplay.Name = "TriviaDisplay";
            this.TriviaDisplay.Background = new SolidColorBrush(new ColorW(51, 51, 51, 204));
            RowDefinition row_TriviaDisplay_0 = new RowDefinition();
            row_TriviaDisplay_0.Height = new GridLength(1F, GridUnitType.Star);
            this.TriviaDisplay.RowDefinitions.Add(row_TriviaDisplay_0);
            RowDefinition row_TriviaDisplay_1 = new RowDefinition();
            row_TriviaDisplay_1.Height = new GridLength(70F, GridUnitType.Pixel);
            this.TriviaDisplay.RowDefinitions.Add(row_TriviaDisplay_1);
            RowDefinition row_TriviaDisplay_2 = new RowDefinition();
            row_TriviaDisplay_2.Height = new GridLength(5F, GridUnitType.Pixel);
            this.TriviaDisplay.RowDefinitions.Add(row_TriviaDisplay_2);
            RowDefinition row_TriviaDisplay_3 = new RowDefinition();
            row_TriviaDisplay_3.Height = new GridLength(40F, GridUnitType.Pixel);
            this.TriviaDisplay.RowDefinitions.Add(row_TriviaDisplay_3);
            RowDefinition row_TriviaDisplay_4 = new RowDefinition();
            row_TriviaDisplay_4.Height = new GridLength(1F, GridUnitType.Star);
            this.TriviaDisplay.RowDefinitions.Add(row_TriviaDisplay_4);
            // QuestionText element
            this.QuestionText = new TextBlock();
            this.TriviaDisplay.Children.Add(this.QuestionText);
            this.QuestionText.Name = "QuestionText";
            this.QuestionText.HorizontalAlignment = HorizontalAlignment.Center;
            this.QuestionText.VerticalAlignment = VerticalAlignment.Center;
            this.QuestionText.Foreground = new SolidColorBrush(new ColorW(255, 255, 255, 255));
            this.QuestionText.Text = "What color is blue paint?";
            FontManager.Instance.AddFont("Segoe UI", 40F, FontStyle.Regular, "Segoe_UI_30_Regular");
            this.QuestionText.FontSize = 40F;
            Grid.SetRow(this.QuestionText, 1);
            // TriviaAnswerSelector element
            this.TriviaAnswerSelector = new ComboBox();
            this.TriviaDisplay.Children.Add(this.TriviaAnswerSelector);
            this.TriviaAnswerSelector.Name = "TriviaAnswerSelector";
            this.TriviaAnswerSelector.Width = 100F;
            this.TriviaAnswerSelector.HorizontalAlignment = HorizontalAlignment.Center;
            this.TriviaAnswerSelector.VerticalAlignment = VerticalAlignment.Center;
            FontManager.Instance.AddFont("Segoe UI", 20F, FontStyle.Regular, "Segoe_UI_15_Regular");
            this.TriviaAnswerSelector.FontSize = 20F;
            this.TriviaAnswerSelector.ItemsSource = this.Get_TriviaAnswerSelector_Items();
            Grid.SetRow(this.TriviaAnswerSelector, 3);
        }
        
        private System.Collections.ObjectModel.ObservableCollection<object> Get_TriviaAnswerSelector_Items() {
            System.Collections.ObjectModel.ObservableCollection<object> items = new System.Collections.ObjectModel.ObservableCollection<object>();
            // e_6 element
            ComboBoxItem e_6 = new ComboBoxItem();
            e_6.Name = "e_6";
            FontManager.Instance.AddFont("Segoe UI", 20F, FontStyle.Regular, "Segoe_UI_15_Regular");
            e_6.Content = "Red";
            items.Add(e_6);
            // e_7 element
            ComboBoxItem e_7 = new ComboBoxItem();
            e_7.Name = "e_7";
            FontManager.Instance.AddFont("Segoe UI", 20F, FontStyle.Regular, "Segoe_UI_15_Regular");
            e_7.Content = "Blue";
            items.Add(e_7);
            // e_8 element
            ComboBoxItem e_8 = new ComboBoxItem();
            e_8.Name = "e_8";
            FontManager.Instance.AddFont("Segoe UI", 20F, FontStyle.Regular, "Segoe_UI_15_Regular");
            e_8.Content = "Green";
            items.Add(e_8);
            // e_9 element
            ComboBoxItem e_9 = new ComboBoxItem();
            e_9.Name = "e_9";
            FontManager.Instance.AddFont("Segoe UI", 20F, FontStyle.Regular, "Segoe_UI_15_Regular");
            e_9.Content = "It depends...";
            items.Add(e_9);
            return items;
        }
    }
}

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
        
        private Image e_3;
        
        private TextBlock e_4;
        
        private StackPanel e_5;
        
        private TextBlock e_6;
        
        private Grid TriviaDisplay;
        
        private TextBlock QuestionText;
        
        private StackPanel e_7;
        
        private ComboBox TriviaAnswerSelector;
        
        private Button e_8;
        
        private Grid e_9;
        
        private TextBlock e_10;
        
        private ItemsControl e_11;
        
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
            ColumnDefinition col_UIRoot_0 = new ColumnDefinition();
            col_UIRoot_0.Width = new GridLength(3F, GridUnitType.Star);
            this.UIRoot.ColumnDefinitions.Add(col_UIRoot_0);
            ColumnDefinition col_UIRoot_1 = new ColumnDefinition();
            col_UIRoot_1.Width = new GridLength(1F, GridUnitType.Star);
            this.UIRoot.ColumnDefinitions.Add(col_UIRoot_1);
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
            Grid.SetColumnSpan(this.GameHUD, 2);
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
            this.e_1.Margin = new Thickness(5F, 5F, 5F, 5F);
            BitmapImage e_1_bm = new BitmapImage();
            e_1_bm.TextureAsset = "XAML Assets/Gold";
            ImageManager.Instance.AddImage("XAML Assets/Gold");
            this.e_1.Source = e_1_bm;
            this.e_1.Stretch = Stretch.Uniform;
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
            this.e_3 = new Image();
            this.e_0.Children.Add(this.e_3);
            this.e_3.Name = "e_3";
            this.e_3.Margin = new Thickness(5F, 5F, 5F, 5F);
            BitmapImage e_3_bm = new BitmapImage();
            e_3_bm.TextureAsset = "XAML Assets/Arrow";
            ImageManager.Instance.AddImage("XAML Assets/Arrow");
            this.e_3.Source = e_3_bm;
            this.e_3.Stretch = Stretch.Uniform;
            // e_4 element
            this.e_4 = new TextBlock();
            this.e_0.Children.Add(this.e_4);
            this.e_4.Name = "e_4";
            this.e_4.Margin = new Thickness(4F, 0F, 4F, 0F);
            this.e_4.Foreground = new SolidColorBrush(new ColorW(211, 211, 211, 255));
            FontManager.Instance.AddFont("Segoe UI", 20F, FontStyle.Bold, "Segoe_UI_15_Bold");
            this.e_4.FontFamily = new FontFamily("Segoe UI");
            this.e_4.FontSize = 20F;
            this.e_4.FontStyle = FontStyle.Bold;
            Binding binding_e_4_Text = new Binding("Arrows");
            this.e_4.SetBinding(TextBlock.TextProperty, binding_e_4_Text);
            // e_5 element
            this.e_5 = new StackPanel();
            this.GameHUD.Children.Add(this.e_5);
            this.e_5.Name = "e_5";
            this.e_5.HorizontalAlignment = HorizontalAlignment.Right;
            this.e_5.Background = new SolidColorBrush(new ColorW(169, 169, 169, 255));
            this.e_5.Orientation = Orientation.Horizontal;
            Grid.SetRow(this.e_5, 1);
            // e_6 element
            this.e_6 = new TextBlock();
            this.e_5.Children.Add(this.e_6);
            this.e_6.Name = "e_6";
            this.e_6.Margin = new Thickness(4F, 0F, 4F, 0F);
            this.e_6.Foreground = new SolidColorBrush(new ColorW(211, 211, 211, 255));
            FontManager.Instance.AddFont("Segoe UI", 20F, FontStyle.Bold, "Segoe_UI_15_Bold");
            this.e_6.FontFamily = new FontFamily("Segoe UI");
            this.e_6.FontSize = 20F;
            this.e_6.FontStyle = FontStyle.Bold;
            Binding binding_e_6_Text = new Binding("Turns");
            this.e_6.SetBinding(TextBlock.TextProperty, binding_e_6_Text);
            // TriviaDisplay element
            this.TriviaDisplay = new Grid();
            this.UIRoot.Children.Add(this.TriviaDisplay);
            this.TriviaDisplay.Name = "TriviaDisplay";
            this.TriviaDisplay.HorizontalAlignment = HorizontalAlignment.Stretch;
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
            Grid.SetColumnSpan(this.TriviaDisplay, 2);
            Binding binding_TriviaDisplay_Visibility = new Binding("TriviaProgressAsVisibility");
            this.TriviaDisplay.SetBinding(Grid.VisibilityProperty, binding_TriviaDisplay_Visibility);
            // QuestionText element
            this.QuestionText = new TextBlock();
            this.TriviaDisplay.Children.Add(this.QuestionText);
            this.QuestionText.Name = "QuestionText";
            this.QuestionText.HorizontalAlignment = HorizontalAlignment.Center;
            this.QuestionText.VerticalAlignment = VerticalAlignment.Center;
            this.QuestionText.Foreground = new SolidColorBrush(new ColorW(255, 255, 255, 255));
            FontManager.Instance.AddFont("Segoe UI", 40F, FontStyle.Regular, "Segoe_UI_30_Regular");
            this.QuestionText.FontSize = 40F;
            Grid.SetRow(this.QuestionText, 1);
            Binding binding_QuestionText_Text = new Binding("CurrentTriviaQuestionText");
            this.QuestionText.SetBinding(TextBlock.TextProperty, binding_QuestionText_Text);
            // e_7 element
            this.e_7 = new StackPanel();
            this.TriviaDisplay.Children.Add(this.e_7);
            this.e_7.Name = "e_7";
            this.e_7.HorizontalAlignment = HorizontalAlignment.Center;
            this.e_7.VerticalAlignment = VerticalAlignment.Center;
            this.e_7.Orientation = Orientation.Horizontal;
            Grid.SetRow(this.e_7, 3);
            // TriviaAnswerSelector element
            this.TriviaAnswerSelector = new ComboBox();
            this.e_7.Children.Add(this.TriviaAnswerSelector);
            this.TriviaAnswerSelector.Name = "TriviaAnswerSelector";
            this.TriviaAnswerSelector.Width = 250F;
            FontManager.Instance.AddFont("Segoe UI", 20F, FontStyle.Regular, "Segoe_UI_15_Regular");
            this.TriviaAnswerSelector.FontSize = 20F;
            Binding binding_TriviaAnswerSelector_ItemsSource = new Binding("CurrentTriviaQuestionAnswersAsComboBoxItems");
            this.TriviaAnswerSelector.SetBinding(ComboBox.ItemsSourceProperty, binding_TriviaAnswerSelector_ItemsSource);
            Binding binding_TriviaAnswerSelector_SelectedIndex = new Binding("SelectedAnswerIndex");
            this.TriviaAnswerSelector.SetBinding(ComboBox.SelectedIndexProperty, binding_TriviaAnswerSelector_SelectedIndex);
            // e_8 element
            this.e_8 = new Button();
            this.e_7.Children.Add(this.e_8);
            this.e_8.Name = "e_8";
            this.e_8.Width = 70F;
            this.e_8.Margin = new Thickness(5F, 0F, 0F, 0F);
            FontManager.Instance.AddFont("Segoe UI", 12F, FontStyle.Regular, "Segoe_UI_9_Regular");
            this.e_8.Content = "Submit";
            Grid.SetRow(this.e_8, 3);
            Binding binding_e_8_Command = new Binding("SubmitAnswerCommand");
            this.e_8.SetBinding(Button.CommandProperty, binding_e_8_Command);
            // e_9 element
            this.e_9 = new Grid();
            this.UIRoot.Children.Add(this.e_9);
            this.e_9.Name = "e_9";
            this.e_9.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.e_9.Background = new SolidColorBrush(new ColorW(51, 51, 51, 255));
            RowDefinition row_e_9_0 = new RowDefinition();
            row_e_9_0.Height = new GridLength(60F, GridUnitType.Pixel);
            this.e_9.RowDefinitions.Add(row_e_9_0);
            RowDefinition row_e_9_1 = new RowDefinition();
            row_e_9_1.Height = new GridLength(1F, GridUnitType.Star);
            this.e_9.RowDefinitions.Add(row_e_9_1);
            RowDefinition row_e_9_2 = new RowDefinition();
            row_e_9_2.Height = new GridLength(5F, GridUnitType.Star);
            this.e_9.RowDefinitions.Add(row_e_9_2);
            RowDefinition row_e_9_3 = new RowDefinition();
            row_e_9_3.Height = new GridLength(1F, GridUnitType.Star);
            this.e_9.RowDefinitions.Add(row_e_9_3);
            Grid.SetColumn(this.e_9, 1);
            // e_10 element
            this.e_10 = new TextBlock();
            this.e_9.Children.Add(this.e_10);
            this.e_10.Name = "e_10";
            this.e_10.HorizontalAlignment = HorizontalAlignment.Center;
            this.e_10.Foreground = new SolidColorBrush(new ColorW(255, 255, 255, 255));
            this.e_10.Text = "Hints";
            FontManager.Instance.AddFont("Segoe UI", 45F, FontStyle.Regular, "Segoe_UI_33.75_Regular");
            this.e_10.FontSize = 45F;
            // e_11 element
            this.e_11 = new ItemsControl();
            this.e_9.Children.Add(this.e_11);
            this.e_11.Name = "e_11";
            this.e_11.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.e_11.Background = new SolidColorBrush(new ColorW(255, 255, 255, 0));
            this.e_11.HorizontalContentAlignment = HorizontalAlignment.Center;
            FontManager.Instance.AddFont("Segoe UI", 20F, FontStyle.Regular, "Segoe_UI_15_Regular");
            this.e_11.FontSize = 20F;
            Func<UIElement, UIElement> e_11_iptFunc = e_11_iptMethod;
            ControlTemplate e_11_ipt = new ControlTemplate(e_11_iptFunc);
            this.e_11.ItemsPanel = e_11_ipt;
            Grid.SetRow(this.e_11, 2);
            Binding binding_e_11_ItemsSource = new Binding("UnlockedHints");
            this.e_11.SetBinding(ItemsControl.ItemsSourceProperty, binding_e_11_ItemsSource);
        }
        
        private static UIElement e_11_iptMethod(UIElement parent) {
            // e_12 element
            StackPanel e_12 = new StackPanel();
            e_12.Parent = parent;
            e_12.Name = "e_12";
            e_12.HorizontalAlignment = HorizontalAlignment.Center;
            e_12.IsItemsHost = true;
            return e_12;
        }
    }
}

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
        
        private StackPanel e_3;
        
        private Image e_4;
        
        private TextBlock e_5;
        
        private Image e_6;
        
        private TextBlock e_7;
        
        private StackPanel e_8;
        
        private TextBlock e_9;
        
        private Grid TriviaDisplay;
        
        private TextBlock QuestionText;
        
        private StackPanel e_10;
        
        private ComboBox TriviaAnswerSelector;
        
        private Button e_11;
        
        private StackPanel e_12;
        
        private TextBlock e_13;
        
        private TextBlock e_14;
        
        private TextBlock e_15;
        
        private Grid e_16;
        
        private TextBlock e_17;
        
        private ItemsControl e_18;
        
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
            // e_3 element
            this.e_3 = new StackPanel();
            this.GameHUD.Children.Add(this.e_3);
            this.e_3.Name = "e_3";
            this.e_3.Background = new SolidColorBrush(new ColorW(169, 169, 169, 255));
            this.e_3.Orientation = Orientation.Horizontal;
            Grid.SetRow(this.e_3, 1);
            // e_4 element
            this.e_4 = new Image();
            this.e_3.Children.Add(this.e_4);
            this.e_4.Name = "e_4";
            this.e_4.Margin = new Thickness(5F, 5F, 5F, 5F);
            BitmapImage e_4_bm = new BitmapImage();
            e_4_bm.TextureAsset = "XAML Assets/Gold";
            ImageManager.Instance.AddImage("XAML Assets/Gold");
            this.e_4.Source = e_4_bm;
            this.e_4.Stretch = Stretch.Uniform;
            // e_5 element
            this.e_5 = new TextBlock();
            this.e_3.Children.Add(this.e_5);
            this.e_5.Name = "e_5";
            this.e_5.Margin = new Thickness(4F, 0F, 4F, 0F);
            this.e_5.Foreground = new SolidColorBrush(new ColorW(211, 211, 211, 255));
            FontManager.Instance.AddFont("Segoe UI", 20F, FontStyle.Bold, "Segoe_UI_15_Bold");
            this.e_5.FontFamily = new FontFamily("Segoe UI");
            this.e_5.FontSize = 20F;
            this.e_5.FontStyle = FontStyle.Bold;
            Binding binding_e_5_Text = new Binding("Gold");
            this.e_5.SetBinding(TextBlock.TextProperty, binding_e_5_Text);
            // e_6 element
            this.e_6 = new Image();
            this.e_3.Children.Add(this.e_6);
            this.e_6.Name = "e_6";
            this.e_6.Margin = new Thickness(5F, 5F, 5F, 5F);
            BitmapImage e_6_bm = new BitmapImage();
            e_6_bm.TextureAsset = "XAML Assets/Arrow";
            ImageManager.Instance.AddImage("XAML Assets/Arrow");
            this.e_6.Source = e_6_bm;
            this.e_6.Stretch = Stretch.Uniform;
            // e_7 element
            this.e_7 = new TextBlock();
            this.e_3.Children.Add(this.e_7);
            this.e_7.Name = "e_7";
            this.e_7.Margin = new Thickness(4F, 0F, 4F, 0F);
            this.e_7.Foreground = new SolidColorBrush(new ColorW(211, 211, 211, 255));
            FontManager.Instance.AddFont("Segoe UI", 20F, FontStyle.Bold, "Segoe_UI_15_Bold");
            this.e_7.FontFamily = new FontFamily("Segoe UI");
            this.e_7.FontSize = 20F;
            this.e_7.FontStyle = FontStyle.Bold;
            Binding binding_e_7_Text = new Binding("Arrows");
            this.e_7.SetBinding(TextBlock.TextProperty, binding_e_7_Text);
            // e_8 element
            this.e_8 = new StackPanel();
            this.GameHUD.Children.Add(this.e_8);
            this.e_8.Name = "e_8";
            this.e_8.HorizontalAlignment = HorizontalAlignment.Right;
            this.e_8.Background = new SolidColorBrush(new ColorW(169, 169, 169, 255));
            this.e_8.Orientation = Orientation.Horizontal;
            Grid.SetRow(this.e_8, 1);
            // e_9 element
            this.e_9 = new TextBlock();
            this.e_8.Children.Add(this.e_9);
            this.e_9.Name = "e_9";
            this.e_9.Margin = new Thickness(4F, 0F, 4F, 0F);
            this.e_9.Foreground = new SolidColorBrush(new ColorW(211, 211, 211, 255));
            FontManager.Instance.AddFont("Segoe UI", 20F, FontStyle.Bold, "Segoe_UI_15_Bold");
            this.e_9.FontFamily = new FontFamily("Segoe UI");
            this.e_9.FontSize = 20F;
            this.e_9.FontStyle = FontStyle.Bold;
            Binding binding_e_9_Text = new Binding("Turns");
            this.e_9.SetBinding(TextBlock.TextProperty, binding_e_9_Text);
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
            row_TriviaDisplay_4.Height = new GridLength(40F, GridUnitType.Pixel);
            this.TriviaDisplay.RowDefinitions.Add(row_TriviaDisplay_4);
            RowDefinition row_TriviaDisplay_5 = new RowDefinition();
            row_TriviaDisplay_5.Height = new GridLength(1F, GridUnitType.Star);
            this.TriviaDisplay.RowDefinitions.Add(row_TriviaDisplay_5);
            Grid.SetColumnSpan(this.TriviaDisplay, 2);
            Binding binding_TriviaDisplay_Opacity = new Binding("TriviaModalOpacity");
            this.TriviaDisplay.SetBinding(Grid.OpacityProperty, binding_TriviaDisplay_Opacity);
            Binding binding_TriviaDisplay_Visibility = new Binding("TriviaModalVisibility");
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
            // e_10 element
            this.e_10 = new StackPanel();
            this.TriviaDisplay.Children.Add(this.e_10);
            this.e_10.Name = "e_10";
            this.e_10.HorizontalAlignment = HorizontalAlignment.Center;
            this.e_10.VerticalAlignment = VerticalAlignment.Center;
            this.e_10.Orientation = Orientation.Horizontal;
            Grid.SetRow(this.e_10, 3);
            // TriviaAnswerSelector element
            this.TriviaAnswerSelector = new ComboBox();
            this.e_10.Children.Add(this.TriviaAnswerSelector);
            this.TriviaAnswerSelector.Name = "TriviaAnswerSelector";
            this.TriviaAnswerSelector.Width = 250F;
            FontManager.Instance.AddFont("Segoe UI", 20F, FontStyle.Regular, "Segoe_UI_15_Regular");
            this.TriviaAnswerSelector.FontSize = 20F;
            Binding binding_TriviaAnswerSelector_ItemsSource = new Binding("CurrentTriviaQuestionAnswersAsComboBoxItems");
            this.TriviaAnswerSelector.SetBinding(ComboBox.ItemsSourceProperty, binding_TriviaAnswerSelector_ItemsSource);
            Binding binding_TriviaAnswerSelector_SelectedIndex = new Binding("SelectedAnswerIndex");
            this.TriviaAnswerSelector.SetBinding(ComboBox.SelectedIndexProperty, binding_TriviaAnswerSelector_SelectedIndex);
            Binding binding_TriviaAnswerSelector_SelectedItem = new Binding("SelectedAnswer");
            this.TriviaAnswerSelector.SetBinding(ComboBox.SelectedItemProperty, binding_TriviaAnswerSelector_SelectedItem);
            // e_11 element
            this.e_11 = new Button();
            this.e_10.Children.Add(this.e_11);
            this.e_11.Name = "e_11";
            this.e_11.Width = 70F;
            this.e_11.Margin = new Thickness(5F, 0F, 0F, 0F);
            FontManager.Instance.AddFont("Segoe UI", 12F, FontStyle.Regular, "Segoe_UI_9_Regular");
            this.e_11.Content = "Submit";
            Grid.SetRow(this.e_11, 3);
            Binding binding_e_11_Command = new Binding("SubmitAnswerCommand");
            this.e_11.SetBinding(Button.CommandProperty, binding_e_11_Command);
            // e_12 element
            this.e_12 = new StackPanel();
            this.TriviaDisplay.Children.Add(this.e_12);
            this.e_12.Name = "e_12";
            this.e_12.HorizontalAlignment = HorizontalAlignment.Center;
            this.e_12.VerticalAlignment = VerticalAlignment.Center;
            this.e_12.Orientation = Orientation.Horizontal;
            Grid.SetRow(this.e_12, 4);
            // e_13 element
            this.e_13 = new TextBlock();
            this.e_12.Children.Add(this.e_13);
            this.e_13.Name = "e_13";
            this.e_13.Foreground = new SolidColorBrush(new ColorW(255, 255, 255, 255));
            FontManager.Instance.AddFont("Segoe UI", 20F, FontStyle.Regular, "Segoe_UI_15_Regular");
            this.e_13.FontSize = 20F;
            Binding binding_e_13_Text = new Binding("NumTriviaQuestionsCorrect");
            this.e_13.SetBinding(TextBlock.TextProperty, binding_e_13_Text);
            // e_14 element
            this.e_14 = new TextBlock();
            this.e_12.Children.Add(this.e_14);
            this.e_14.Name = "e_14";
            this.e_14.Foreground = new SolidColorBrush(new ColorW(255, 255, 255, 255));
            this.e_14.Text = "/";
            FontManager.Instance.AddFont("Segoe UI", 20F, FontStyle.Regular, "Segoe_UI_15_Regular");
            this.e_14.FontSize = 20F;
            // e_15 element
            this.e_15 = new TextBlock();
            this.e_12.Children.Add(this.e_15);
            this.e_15.Name = "e_15";
            this.e_15.Foreground = new SolidColorBrush(new ColorW(255, 255, 255, 255));
            FontManager.Instance.AddFont("Segoe UI", 20F, FontStyle.Regular, "Segoe_UI_15_Regular");
            this.e_15.FontSize = 20F;
            Binding binding_e_15_Text = new Binding("NumTriviaQuestionsTotal");
            this.e_15.SetBinding(TextBlock.TextProperty, binding_e_15_Text);
            // e_16 element
            this.e_16 = new Grid();
            this.UIRoot.Children.Add(this.e_16);
            this.e_16.Name = "e_16";
            this.e_16.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.e_16.Background = new SolidColorBrush(new ColorW(51, 51, 51, 255));
            RowDefinition row_e_16_0 = new RowDefinition();
            row_e_16_0.Height = new GridLength(60F, GridUnitType.Pixel);
            this.e_16.RowDefinitions.Add(row_e_16_0);
            RowDefinition row_e_16_1 = new RowDefinition();
            row_e_16_1.Height = new GridLength(1F, GridUnitType.Star);
            this.e_16.RowDefinitions.Add(row_e_16_1);
            RowDefinition row_e_16_2 = new RowDefinition();
            row_e_16_2.Height = new GridLength(5F, GridUnitType.Star);
            this.e_16.RowDefinitions.Add(row_e_16_2);
            RowDefinition row_e_16_3 = new RowDefinition();
            row_e_16_3.Height = new GridLength(1F, GridUnitType.Star);
            this.e_16.RowDefinitions.Add(row_e_16_3);
            Grid.SetColumn(this.e_16, 1);
            // e_17 element
            this.e_17 = new TextBlock();
            this.e_16.Children.Add(this.e_17);
            this.e_17.Name = "e_17";
            this.e_17.HorizontalAlignment = HorizontalAlignment.Center;
            this.e_17.Foreground = new SolidColorBrush(new ColorW(255, 255, 255, 255));
            this.e_17.Text = "Hints";
            FontManager.Instance.AddFont("Segoe UI", 45F, FontStyle.Regular, "Segoe_UI_33.75_Regular");
            this.e_17.FontSize = 45F;
            // e_18 element
            this.e_18 = new ItemsControl();
            this.e_16.Children.Add(this.e_18);
            this.e_18.Name = "e_18";
            this.e_18.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.e_18.Background = new SolidColorBrush(new ColorW(255, 255, 255, 0));
            this.e_18.HorizontalContentAlignment = HorizontalAlignment.Center;
            FontManager.Instance.AddFont("Segoe UI", 20F, FontStyle.Regular, "Segoe_UI_15_Regular");
            this.e_18.FontSize = 20F;
            Func<UIElement, UIElement> e_18_iptFunc = e_18_iptMethod;
            ControlTemplate e_18_ipt = new ControlTemplate(e_18_iptFunc);
            this.e_18.ItemsPanel = e_18_ipt;
            Grid.SetRow(this.e_18, 2);
            Binding binding_e_18_ItemsSource = new Binding("UnlockedHints");
            this.e_18.SetBinding(ItemsControl.ItemsSourceProperty, binding_e_18_ItemsSource);
        }
        
        private static UIElement e_18_iptMethod(UIElement parent) {
            // e_19 element
            StackPanel e_19 = new StackPanel();
            e_19.Parent = parent;
            e_19.Name = "e_19";
            e_19.HorizontalAlignment = HorizontalAlignment.Center;
            e_19.IsItemsHost = true;
            return e_19;
        }
    }
}

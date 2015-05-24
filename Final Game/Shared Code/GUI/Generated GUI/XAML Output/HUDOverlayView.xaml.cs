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
        
        private TextBlock e_7;
        
        private Button HintButton;
        
        private Grid TriviaDisplay;
        
        private TextBlock QuestionText;
        
        private StackPanel e_8;
        
        private ComboBox TriviaAnswerSelector;
        
        private Button e_9;
        
        private StackPanel e_10;
        
        private TextBlock e_11;
        
        private TextBlock e_12;
        
        private TextBlock e_13;
        
        private Grid HintFlyout;
        
        private Grid e_14;
        
        private StackPanel e_15;
        
        private TextBlock e_16;
        
        private Button HintCloseButton;
        
        private Button HintPurchaseButton;
        
        private ItemsControl e_17;
        
        private Grid GameOverModalDialog;
        
        private TextBlock e_19;
        
        private TextBlock e_20;
        
        private TextBox e_21;
        
        private Button NameSubmitButton;
        
        private Button MenuButton;
        
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
            this.e_0.Background = new SolidColorBrush(new ColorW(51, 51, 51, 255));
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
            this.e_2.VerticalAlignment = VerticalAlignment.Center;
            this.e_2.Foreground = new SolidColorBrush(new ColorW(211, 211, 211, 255));
            FontManager.Instance.AddFont("Arcadepix", 20F, FontStyle.Bold, "Arcadepix_15_Bold");
            this.e_2.FontFamily = new FontFamily("Arcadepix");
            this.e_2.FontSize = 20F;
            this.e_2.FontStyle = FontStyle.Bold;
            Binding binding_e_2_Text = new Binding("ScoreContext.Gold");
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
            this.e_4.VerticalAlignment = VerticalAlignment.Center;
            this.e_4.Foreground = new SolidColorBrush(new ColorW(211, 211, 211, 255));
            FontManager.Instance.AddFont("Arcadepix", 20F, FontStyle.Bold, "Arcadepix_15_Bold");
            this.e_4.FontFamily = new FontFamily("Arcadepix");
            this.e_4.FontSize = 20F;
            this.e_4.FontStyle = FontStyle.Bold;
            Binding binding_e_4_Text = new Binding("ScoreContext.Arrows");
            this.e_4.SetBinding(TextBlock.TextProperty, binding_e_4_Text);
            // e_5 element
            this.e_5 = new StackPanel();
            this.GameHUD.Children.Add(this.e_5);
            this.e_5.Name = "e_5";
            this.e_5.HorizontalAlignment = HorizontalAlignment.Right;
            this.e_5.Background = new SolidColorBrush(new ColorW(51, 51, 51, 255));
            this.e_5.Orientation = Orientation.Horizontal;
            Grid.SetRow(this.e_5, 1);
            // e_6 element
            this.e_6 = new TextBlock();
            this.e_5.Children.Add(this.e_6);
            this.e_6.Name = "e_6";
            this.e_6.Margin = new Thickness(4F, 0F, 4F, 0F);
            this.e_6.VerticalAlignment = VerticalAlignment.Center;
            this.e_6.Foreground = new SolidColorBrush(new ColorW(211, 211, 211, 255));
            FontManager.Instance.AddFont("Arcadepix", 20F, FontStyle.Regular, "Arcadepix_15_Regular");
            this.e_6.FontFamily = new FontFamily("Arcadepix");
            this.e_6.FontSize = 20F;
            Binding binding_e_6_Text = new Binding("WarningText");
            this.e_6.SetBinding(TextBlock.TextProperty, binding_e_6_Text);
            // e_7 element
            this.e_7 = new TextBlock();
            this.e_5.Children.Add(this.e_7);
            this.e_7.Name = "e_7";
            this.e_7.Margin = new Thickness(4F, 0F, 4F, 0F);
            this.e_7.VerticalAlignment = VerticalAlignment.Center;
            this.e_7.Foreground = new SolidColorBrush(new ColorW(211, 211, 211, 255));
            FontManager.Instance.AddFont("Arcadepix", 20F, FontStyle.Bold, "Arcadepix_15_Bold");
            this.e_7.FontFamily = new FontFamily("Arcadepix");
            this.e_7.FontSize = 20F;
            this.e_7.FontStyle = FontStyle.Bold;
            Binding binding_e_7_Text = new Binding("ScoreContext.Turns");
            this.e_7.SetBinding(TextBlock.TextProperty, binding_e_7_Text);
            // HintButton element
            this.HintButton = new Button();
            this.e_5.Children.Add(this.HintButton);
            this.HintButton.Name = "HintButton";
            this.HintButton.Margin = new Thickness(4F, 0F, 4F, 0F);
            FontManager.Instance.AddFont("Arcadepix", 12F, FontStyle.Regular, "Arcadepix_9_Regular");
            this.HintButton.FontFamily = new FontFamily("Arcadepix");
            this.HintButton.Content = "Hints";
            Binding binding_HintButton_Command = new Binding("ShowHintsCommand");
            this.HintButton.SetBinding(Button.CommandProperty, binding_HintButton_Command);
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
            row_TriviaDisplay_1.Height = new GridLength(150F, GridUnitType.Pixel);
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
            Binding binding_TriviaDisplay_Opacity = new Binding("TriviaContext.TriviaModalOpacity");
            this.TriviaDisplay.SetBinding(Grid.OpacityProperty, binding_TriviaDisplay_Opacity);
            Binding binding_TriviaDisplay_Visibility = new Binding("TriviaContext.TriviaModalVisibility");
            this.TriviaDisplay.SetBinding(Grid.VisibilityProperty, binding_TriviaDisplay_Visibility);
            // QuestionText element
            this.QuestionText = new TextBlock();
            this.TriviaDisplay.Children.Add(this.QuestionText);
            this.QuestionText.Name = "QuestionText";
            this.QuestionText.HorizontalAlignment = HorizontalAlignment.Center;
            this.QuestionText.VerticalAlignment = VerticalAlignment.Center;
            this.QuestionText.Foreground = new SolidColorBrush(new ColorW(255, 255, 255, 255));
            this.QuestionText.TextAlignment = TextAlignment.Center;
            this.QuestionText.TextWrapping = TextWrapping.Wrap;
            FontManager.Instance.AddFont("Arcadepix", 40F, FontStyle.Regular, "Arcadepix_30_Regular");
            this.QuestionText.FontFamily = new FontFamily("Arcadepix");
            this.QuestionText.FontSize = 40F;
            Grid.SetRow(this.QuestionText, 1);
            Binding binding_QuestionText_Text = new Binding("TriviaContext.CurrentTriviaQuestionText");
            this.QuestionText.SetBinding(TextBlock.TextProperty, binding_QuestionText_Text);
            // e_8 element
            this.e_8 = new StackPanel();
            this.TriviaDisplay.Children.Add(this.e_8);
            this.e_8.Name = "e_8";
            this.e_8.HorizontalAlignment = HorizontalAlignment.Center;
            this.e_8.VerticalAlignment = VerticalAlignment.Center;
            this.e_8.Orientation = Orientation.Horizontal;
            Grid.SetRow(this.e_8, 3);
            // TriviaAnswerSelector element
            this.TriviaAnswerSelector = new ComboBox();
            this.e_8.Children.Add(this.TriviaAnswerSelector);
            this.TriviaAnswerSelector.Name = "TriviaAnswerSelector";
            this.TriviaAnswerSelector.Width = 350F;
            FontManager.Instance.AddFont("Arcadepix", 20F, FontStyle.Regular, "Arcadepix_15_Regular");
            this.TriviaAnswerSelector.FontFamily = new FontFamily("Arcadepix");
            this.TriviaAnswerSelector.FontSize = 20F;
            Binding binding_TriviaAnswerSelector_ItemsSource = new Binding("TriviaContext.CurrentTriviaQuestionAnswersAsComboBoxItems");
            this.TriviaAnswerSelector.SetBinding(ComboBox.ItemsSourceProperty, binding_TriviaAnswerSelector_ItemsSource);
            Binding binding_TriviaAnswerSelector_SelectedIndex = new Binding("TriviaContext.SelectedAnswerIndex");
            this.TriviaAnswerSelector.SetBinding(ComboBox.SelectedIndexProperty, binding_TriviaAnswerSelector_SelectedIndex);
            Binding binding_TriviaAnswerSelector_SelectedItem = new Binding("TriviaContext.SelectedAnswer");
            this.TriviaAnswerSelector.SetBinding(ComboBox.SelectedItemProperty, binding_TriviaAnswerSelector_SelectedItem);
            // e_9 element
            this.e_9 = new Button();
            this.e_8.Children.Add(this.e_9);
            this.e_9.Name = "e_9";
            this.e_9.Width = 70F;
            this.e_9.Margin = new Thickness(5F, 0F, 0F, 0F);
            FontManager.Instance.AddFont("Arcadepix", 15F, FontStyle.Regular, "Arcadepix_11.25_Regular");
            this.e_9.FontFamily = new FontFamily("Arcadepix");
            this.e_9.FontSize = 15F;
            this.e_9.Content = "Submit";
            Grid.SetRow(this.e_9, 3);
            Binding binding_e_9_Command = new Binding("TriviaContext.SubmitAnswerCommand");
            this.e_9.SetBinding(Button.CommandProperty, binding_e_9_Command);
            // e_10 element
            this.e_10 = new StackPanel();
            this.TriviaDisplay.Children.Add(this.e_10);
            this.e_10.Name = "e_10";
            this.e_10.HorizontalAlignment = HorizontalAlignment.Center;
            this.e_10.VerticalAlignment = VerticalAlignment.Center;
            this.e_10.Orientation = Orientation.Horizontal;
            Grid.SetRow(this.e_10, 4);
            // e_11 element
            this.e_11 = new TextBlock();
            this.e_10.Children.Add(this.e_11);
            this.e_11.Name = "e_11";
            this.e_11.Foreground = new SolidColorBrush(new ColorW(255, 255, 255, 255));
            FontManager.Instance.AddFont("Arcadepix", 20F, FontStyle.Regular, "Arcadepix_15_Regular");
            this.e_11.FontFamily = new FontFamily("Arcadepix");
            this.e_11.FontSize = 20F;
            Binding binding_e_11_Text = new Binding("TriviaContext.NumTriviaQuestionsCorrect");
            this.e_11.SetBinding(TextBlock.TextProperty, binding_e_11_Text);
            // e_12 element
            this.e_12 = new TextBlock();
            this.e_10.Children.Add(this.e_12);
            this.e_12.Name = "e_12";
            this.e_12.Foreground = new SolidColorBrush(new ColorW(255, 255, 255, 255));
            this.e_12.Text = "/";
            FontManager.Instance.AddFont("Arcadepix", 20F, FontStyle.Regular, "Arcadepix_15_Regular");
            this.e_12.FontFamily = new FontFamily("Arcadepix");
            this.e_12.FontSize = 20F;
            // e_13 element
            this.e_13 = new TextBlock();
            this.e_10.Children.Add(this.e_13);
            this.e_13.Name = "e_13";
            this.e_13.Foreground = new SolidColorBrush(new ColorW(255, 255, 255, 255));
            FontManager.Instance.AddFont("Arcadepix", 20F, FontStyle.Regular, "Arcadepix_15_Regular");
            this.e_13.FontFamily = new FontFamily("Arcadepix");
            this.e_13.FontSize = 20F;
            Binding binding_e_13_Text = new Binding("TriviaContext.NumTriviaQuestionsTotal");
            this.e_13.SetBinding(TextBlock.TextProperty, binding_e_13_Text);
            // HintFlyout element
            this.HintFlyout = new Grid();
            this.UIRoot.Children.Add(this.HintFlyout);
            this.HintFlyout.Name = "HintFlyout";
            this.HintFlyout.Margin = new Thickness(0F, 0F, 0F, 30F);
            this.HintFlyout.HorizontalAlignment = HorizontalAlignment.Stretch;
            RowDefinition row_HintFlyout_0 = new RowDefinition();
            row_HintFlyout_0.Height = new GridLength(60F, GridUnitType.Pixel);
            this.HintFlyout.RowDefinitions.Add(row_HintFlyout_0);
            RowDefinition row_HintFlyout_1 = new RowDefinition();
            row_HintFlyout_1.Height = new GridLength(1F, GridUnitType.Star);
            this.HintFlyout.RowDefinitions.Add(row_HintFlyout_1);
            RowDefinition row_HintFlyout_2 = new RowDefinition();
            row_HintFlyout_2.Height = new GridLength(5F, GridUnitType.Star);
            this.HintFlyout.RowDefinitions.Add(row_HintFlyout_2);
            Grid.SetColumn(this.HintFlyout, 1);
            Binding binding_HintFlyout_Visibility = new Binding("HintFlyoutVisibility");
            this.HintFlyout.SetBinding(Grid.VisibilityProperty, binding_HintFlyout_Visibility);
            // e_14 element
            this.e_14 = new Grid();
            this.HintFlyout.Children.Add(this.e_14);
            this.e_14.Name = "e_14";
            this.e_14.Background = new SolidColorBrush(new ColorW(51, 51, 51, 255));
            Grid.SetRowSpan(this.e_14, 3);
            // e_15 element
            this.e_15 = new StackPanel();
            this.HintFlyout.Children.Add(this.e_15);
            this.e_15.Name = "e_15";
            this.e_15.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.e_15.Orientation = Orientation.Horizontal;
            // e_16 element
            this.e_16 = new TextBlock();
            this.e_15.Children.Add(this.e_16);
            this.e_16.Name = "e_16";
            this.e_16.Foreground = new SolidColorBrush(new ColorW(255, 255, 255, 255));
            this.e_16.Text = "Hints";
            FontManager.Instance.AddFont("Arcadepix", 45F, FontStyle.Regular, "Arcadepix_33.75_Regular");
            this.e_16.FontFamily = new FontFamily("Arcadepix");
            this.e_16.FontSize = 45F;
            // HintCloseButton element
            this.HintCloseButton = new Button();
            this.e_15.Children.Add(this.HintCloseButton);
            this.HintCloseButton.Name = "HintCloseButton";
            this.HintCloseButton.Height = 33F;
            this.HintCloseButton.Background = new SolidColorBrush(new ColorW(255, 255, 255, 0));
            this.HintCloseButton.Foreground = new SolidColorBrush(new ColorW(255, 255, 255, 255));
            FontManager.Instance.AddFont("Arcadepix", 12F, FontStyle.Regular, "Arcadepix_9_Regular");
            this.HintCloseButton.FontFamily = new FontFamily("Arcadepix");
            this.HintCloseButton.Content = "X";
            Binding binding_HintCloseButton_Command = new Binding("ShowHintsCommand");
            this.HintCloseButton.SetBinding(Button.CommandProperty, binding_HintCloseButton_Command);
            // HintPurchaseButton element
            this.HintPurchaseButton = new Button();
            this.e_15.Children.Add(this.HintPurchaseButton);
            this.HintPurchaseButton.Name = "HintPurchaseButton";
            this.HintPurchaseButton.Height = 33F;
            this.HintPurchaseButton.Background = new SolidColorBrush(new ColorW(255, 255, 255, 0));
            this.HintPurchaseButton.Foreground = new SolidColorBrush(new ColorW(255, 255, 255, 255));
            FontManager.Instance.AddFont("Arcadepix", 12F, FontStyle.Regular, "Arcadepix_9_Regular");
            this.HintPurchaseButton.FontFamily = new FontFamily("Arcadepix");
            this.HintPurchaseButton.Content = "Purchase hint";
            Binding binding_HintPurchaseButton_Command = new Binding("BuyHintsCommand");
            this.HintPurchaseButton.SetBinding(Button.CommandProperty, binding_HintPurchaseButton_Command);
            // e_17 element
            this.e_17 = new ItemsControl();
            this.HintFlyout.Children.Add(this.e_17);
            this.e_17.Name = "e_17";
            this.e_17.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.e_17.Background = new SolidColorBrush(new ColorW(255, 255, 255, 0));
            this.e_17.HorizontalContentAlignment = HorizontalAlignment.Center;
            FontManager.Instance.AddFont("Arcadepix", 20F, FontStyle.Regular, "Arcadepix_15_Regular");
            this.e_17.FontFamily = new FontFamily("Arcadepix");
            this.e_17.FontSize = 20F;
            Func<UIElement, UIElement> e_17_iptFunc = e_17_iptMethod;
            ControlTemplate e_17_ipt = new ControlTemplate(e_17_iptFunc);
            this.e_17.ItemsPanel = e_17_ipt;
            Grid.SetRow(this.e_17, 2);
            Binding binding_e_17_ItemsSource = new Binding("UnlockedHints");
            this.e_17.SetBinding(ItemsControl.ItemsSourceProperty, binding_e_17_ItemsSource);
            // GameOverModalDialog element
            this.GameOverModalDialog = new Grid();
            this.UIRoot.Children.Add(this.GameOverModalDialog);
            this.GameOverModalDialog.Name = "GameOverModalDialog";
            this.GameOverModalDialog.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.GameOverModalDialog.Background = new SolidColorBrush(new ColorW(51, 51, 51, 153));
            RowDefinition row_GameOverModalDialog_0 = new RowDefinition();
            row_GameOverModalDialog_0.Height = new GridLength(1F, GridUnitType.Star);
            this.GameOverModalDialog.RowDefinitions.Add(row_GameOverModalDialog_0);
            RowDefinition row_GameOverModalDialog_1 = new RowDefinition();
            row_GameOverModalDialog_1.Height = new GridLength(2F, GridUnitType.Star);
            this.GameOverModalDialog.RowDefinitions.Add(row_GameOverModalDialog_1);
            RowDefinition row_GameOverModalDialog_2 = new RowDefinition();
            row_GameOverModalDialog_2.Height = new GridLength(1F, GridUnitType.Star);
            this.GameOverModalDialog.RowDefinitions.Add(row_GameOverModalDialog_2);
            RowDefinition row_GameOverModalDialog_3 = new RowDefinition();
            row_GameOverModalDialog_3.Height = new GridLength(1F, GridUnitType.Star);
            this.GameOverModalDialog.RowDefinitions.Add(row_GameOverModalDialog_3);
            RowDefinition row_GameOverModalDialog_4 = new RowDefinition();
            row_GameOverModalDialog_4.Height = new GridLength(2F, GridUnitType.Star);
            this.GameOverModalDialog.RowDefinitions.Add(row_GameOverModalDialog_4);
            ColumnDefinition col_GameOverModalDialog_0 = new ColumnDefinition();
            col_GameOverModalDialog_0.Width = new GridLength(8F, GridUnitType.Star);
            this.GameOverModalDialog.ColumnDefinitions.Add(col_GameOverModalDialog_0);
            ColumnDefinition col_GameOverModalDialog_1 = new ColumnDefinition();
            col_GameOverModalDialog_1.Width = new GridLength(8F, GridUnitType.Star);
            this.GameOverModalDialog.ColumnDefinitions.Add(col_GameOverModalDialog_1);
            ColumnDefinition col_GameOverModalDialog_2 = new ColumnDefinition();
            col_GameOverModalDialog_2.Width = new GridLength(3F, GridUnitType.Star);
            this.GameOverModalDialog.ColumnDefinitions.Add(col_GameOverModalDialog_2);
            ColumnDefinition col_GameOverModalDialog_3 = new ColumnDefinition();
            col_GameOverModalDialog_3.Width = new GridLength(8F, GridUnitType.Star);
            this.GameOverModalDialog.ColumnDefinitions.Add(col_GameOverModalDialog_3);
            Grid.SetColumnSpan(this.GameOverModalDialog, 2);
            Binding binding_GameOverModalDialog_Margin = new Binding("GameOverContext.GameOverModalMargin");
            this.GameOverModalDialog.SetBinding(Grid.MarginProperty, binding_GameOverModalDialog_Margin);
            Binding binding_GameOverModalDialog_Opacity = new Binding("GameOverContext.GameOverModalOpacity");
            this.GameOverModalDialog.SetBinding(Grid.OpacityProperty, binding_GameOverModalDialog_Opacity);
            Binding binding_GameOverModalDialog_Visibility = new Binding("GameOverContext.GameOverModalVisibility");
            this.GameOverModalDialog.SetBinding(Grid.VisibilityProperty, binding_GameOverModalDialog_Visibility);
            // e_19 element
            this.e_19 = new TextBlock();
            this.GameOverModalDialog.Children.Add(this.e_19);
            this.e_19.Name = "e_19";
            this.e_19.HorizontalAlignment = HorizontalAlignment.Center;
            this.e_19.VerticalAlignment = VerticalAlignment.Center;
            this.e_19.Foreground = new SolidColorBrush(new ColorW(211, 211, 211, 255));
            this.e_19.Text = "Hunt the Wumpus";
            FontManager.Instance.AddFont("Arcadepix", 20F, FontStyle.Bold, "Arcadepix_15_Bold");
            this.e_19.FontFamily = new FontFamily("Arcadepix");
            this.e_19.FontSize = 20F;
            this.e_19.FontStyle = FontStyle.Bold;
            // e_20 element
            this.e_20 = new TextBlock();
            this.GameOverModalDialog.Children.Add(this.e_20);
            this.e_20.Name = "e_20";
            this.e_20.HorizontalAlignment = HorizontalAlignment.Center;
            this.e_20.VerticalAlignment = VerticalAlignment.Center;
            this.e_20.Foreground = new SolidColorBrush(new ColorW(211, 211, 211, 255));
            this.e_20.TextWrapping = TextWrapping.Wrap;
            FontManager.Instance.AddFont("Arcadepix", 53.33333F, FontStyle.Bold, "Arcadepix_40_Bold");
            this.e_20.FontFamily = new FontFamily("Arcadepix");
            this.e_20.FontSize = 53.33333F;
            this.e_20.FontStyle = FontStyle.Bold;
            Grid.SetRow(this.e_20, 1);
            Grid.SetColumnSpan(this.e_20, 4);
            Binding binding_e_20_Text = new Binding("GameOverContext.GameOverMessage");
            this.e_20.SetBinding(TextBlock.TextProperty, binding_e_20_Text);
            // e_21 element
            this.e_21 = new TextBox();
            this.GameOverModalDialog.Children.Add(this.e_21);
            this.e_21.Name = "e_21";
            this.e_21.Height = 33F;
            this.e_21.Margin = new Thickness(4F, 4F, 4F, 4F);
            FontManager.Instance.AddFont("Arcadepix", 12F, FontStyle.Regular, "Arcadepix_9_Regular");
            this.e_21.FontFamily = new FontFamily("Arcadepix");
            Grid.SetColumn(this.e_21, 1);
            Grid.SetRow(this.e_21, 2);
            Binding binding_e_21_Visibility = new Binding("GameOverContext.UsernameBoxVisibility");
            this.e_21.SetBinding(TextBox.VisibilityProperty, binding_e_21_Visibility);
            Binding binding_e_21_Text = new Binding("GameOverContext.GameOverUsernameText");
            this.e_21.SetBinding(TextBox.TextProperty, binding_e_21_Text);
            // NameSubmitButton element
            this.NameSubmitButton = new Button();
            this.GameOverModalDialog.Children.Add(this.NameSubmitButton);
            this.NameSubmitButton.Name = "NameSubmitButton";
            this.NameSubmitButton.Height = 33F;
            this.NameSubmitButton.Margin = new Thickness(4F, 4F, 4F, 4F);
            FontManager.Instance.AddFont("Arcadepix", 12F, FontStyle.Regular, "Arcadepix_9_Regular");
            this.NameSubmitButton.FontFamily = new FontFamily("Arcadepix");
            this.NameSubmitButton.Content = "Save score";
            Grid.SetColumn(this.NameSubmitButton, 2);
            Grid.SetRow(this.NameSubmitButton, 2);
            Binding binding_NameSubmitButton_Visibility = new Binding("GameOverContext.UsernameBoxVisibility");
            this.NameSubmitButton.SetBinding(Button.VisibilityProperty, binding_NameSubmitButton_Visibility);
            Binding binding_NameSubmitButton_Command = new Binding("GameOverContext.SubmitNameCommand");
            this.NameSubmitButton.SetBinding(Button.CommandProperty, binding_NameSubmitButton_Command);
            // MenuButton element
            this.MenuButton = new Button();
            this.GameOverModalDialog.Children.Add(this.MenuButton);
            this.MenuButton.Name = "MenuButton";
            this.MenuButton.Height = 66F;
            this.MenuButton.Margin = new Thickness(5F, 5F, 5F, 5F);
            FontManager.Instance.AddFont("Arcadepix", 12F, FontStyle.Regular, "Arcadepix_9_Regular");
            this.MenuButton.FontFamily = new FontFamily("Arcadepix");
            this.MenuButton.Content = "Back to Main Menu";
            Grid.SetColumn(this.MenuButton, 1);
            Grid.SetRow(this.MenuButton, 3);
            Grid.SetColumnSpan(this.MenuButton, 2);
            Binding binding_MenuButton_Command = new Binding("GameOverContext.ReturnToMenuCommand");
            this.MenuButton.SetBinding(Button.CommandProperty, binding_MenuButton_Command);
        }
        
        private static UIElement e_17_iptMethod(UIElement parent) {
            // e_18 element
            StackPanel e_18 = new StackPanel();
            e_18.Parent = parent;
            e_18.Name = "e_18";
            e_18.HorizontalAlignment = HorizontalAlignment.Center;
            e_18.IsItemsHost = true;
            return e_18;
        }
    }
}

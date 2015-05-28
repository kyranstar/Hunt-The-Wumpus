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
        
        private Image e_6;
        
        private TextBlock e_7;
        
        private StackPanel e_8;
        
        private Image e_9;
        
        private TextBlock e_10;
        
        private Button FlyoutButton;
        
        private Grid DataFlyout;
        
        private Grid e_11;
        
        private Grid e_12;
        
        private Grid e_13;
        
        private Grid e_14;
        
        private TextBlock e_15;
        
        private StackPanel e_16;
        
        private Button CloseFlyoutButton;
        
        private ScrollViewer e_17;
        
        private ItemsControl e_18;
        
        private TextBlock e_20;
        
        private ScrollViewer e_21;
        
        private ItemsControl e_22;
        
        private StackPanel e_24;
        
        private Button SecretPurchaseButton;
        
        private Button ArrowPurchaseButton;
        
        private Grid TriviaDisplay;
        
        private TextBlock QuestionText;
        
        private StackPanel e_25;
        
        private ComboBox TriviaAnswerSelector;
        
        private Button e_26;
        
        private StackPanel e_27;
        
        private TextBlock e_28;
        
        private TextBlock e_29;
        
        private TextBlock e_30;
        
        private Grid GameOverModalDialog;
        
        private TextBlock e_31;
        
        private TextBox e_32;
        
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
            RowDefinition row_UIRoot_0 = new RowDefinition();
            row_UIRoot_0.Height = new GridLength(90F, GridUnitType.Star);
            this.UIRoot.RowDefinitions.Add(row_UIRoot_0);
            RowDefinition row_UIRoot_1 = new RowDefinition();
            row_UIRoot_1.Height = new GridLength(1.8F, GridUnitType.Star);
            this.UIRoot.RowDefinitions.Add(row_UIRoot_1);
            RowDefinition row_UIRoot_2 = new RowDefinition();
            row_UIRoot_2.Height = new GridLength(6.3F, GridUnitType.Star);
            this.UIRoot.RowDefinitions.Add(row_UIRoot_2);
            RowDefinition row_UIRoot_3 = new RowDefinition();
            row_UIRoot_3.Height = new GridLength(1.5F, GridUnitType.Star);
            this.UIRoot.RowDefinitions.Add(row_UIRoot_3);
            ColumnDefinition col_UIRoot_0 = new ColumnDefinition();
            col_UIRoot_0.Width = new GridLength(1.65F, GridUnitType.Star);
            this.UIRoot.ColumnDefinitions.Add(col_UIRoot_0);
            ColumnDefinition col_UIRoot_1 = new ColumnDefinition();
            col_UIRoot_1.Width = new GridLength(17F, GridUnitType.Star);
            this.UIRoot.ColumnDefinitions.Add(col_UIRoot_1);
            ColumnDefinition col_UIRoot_2 = new ColumnDefinition();
            col_UIRoot_2.Width = new GridLength(7F, GridUnitType.Star);
            this.UIRoot.ColumnDefinitions.Add(col_UIRoot_2);
            ColumnDefinition col_UIRoot_3 = new ColumnDefinition();
            col_UIRoot_3.Width = new GridLength(1.65F, GridUnitType.Star);
            this.UIRoot.ColumnDefinitions.Add(col_UIRoot_3);
            // GameHUD element
            this.GameHUD = new Grid();
            this.UIRoot.Children.Add(this.GameHUD);
            this.GameHUD.Name = "GameHUD";
            this.GameHUD.Background = new SolidColorBrush(new ColorW(255, 255, 255, 0));
            Grid.SetColumn(this.GameHUD, 1);
            Grid.SetRow(this.GameHUD, 2);
            Grid.SetColumnSpan(this.GameHUD, 2);
            // e_0 element
            this.e_0 = new StackPanel();
            this.GameHUD.Children.Add(this.e_0);
            this.e_0.Name = "e_0";
            this.e_0.Background = new SolidColorBrush(new ColorW(91, 91, 91, 255));
            this.e_0.Orientation = Orientation.Horizontal;
            // e_1 element
            this.e_1 = new Image();
            this.e_0.Children.Add(this.e_1);
            this.e_1.Name = "e_1";
            this.e_1.Margin = new Thickness(4F, 4F, 4F, 4F);
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
            this.e_3.Margin = new Thickness(4F, 4F, 4F, 4F);
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
            this.e_5.HorizontalAlignment = HorizontalAlignment.Center;
            this.e_5.Background = new SolidColorBrush(new ColorW(91, 91, 91, 255));
            this.e_5.Orientation = Orientation.Horizontal;
            Binding binding_e_5_Visibility = new Binding("WarningContext.WarningVisibility");
            this.e_5.SetBinding(StackPanel.VisibilityProperty, binding_e_5_Visibility);
            // e_6 element
            this.e_6 = new Image();
            this.e_5.Children.Add(this.e_6);
            this.e_6.Name = "e_6";
            this.e_6.Margin = new Thickness(6F, 6F, 6F, 6F);
            BitmapImage e_6_bm = new BitmapImage();
            e_6_bm.TextureAsset = "XAML Assets/Warning";
            ImageManager.Instance.AddImage("XAML Assets/Warning");
            this.e_6.Source = e_6_bm;
            this.e_6.Stretch = Stretch.Uniform;
            Grid.SetRow(this.e_6, 1);
            // e_7 element
            this.e_7 = new TextBlock();
            this.e_5.Children.Add(this.e_7);
            this.e_7.Name = "e_7";
            this.e_7.Margin = new Thickness(4F, 0F, 4F, 0F);
            this.e_7.VerticalAlignment = VerticalAlignment.Center;
            this.e_7.Foreground = new SolidColorBrush(new ColorW(211, 211, 211, 255));
            FontManager.Instance.AddFont("Arcadepix", 20F, FontStyle.Regular, "Arcadepix_15_Regular");
            this.e_7.FontFamily = new FontFamily("Arcadepix");
            this.e_7.FontSize = 20F;
            Binding binding_e_7_Text = new Binding("WarningContext.WarningText");
            this.e_7.SetBinding(TextBlock.TextProperty, binding_e_7_Text);
            // e_8 element
            this.e_8 = new StackPanel();
            this.GameHUD.Children.Add(this.e_8);
            this.e_8.Name = "e_8";
            this.e_8.HorizontalAlignment = HorizontalAlignment.Right;
            this.e_8.Background = new SolidColorBrush(new ColorW(91, 91, 91, 255));
            this.e_8.Orientation = Orientation.Horizontal;
            // e_9 element
            this.e_9 = new Image();
            this.e_8.Children.Add(this.e_9);
            this.e_9.Name = "e_9";
            this.e_9.Margin = new Thickness(5F, 5F, 5F, 5F);
            BitmapImage e_9_bm = new BitmapImage();
            e_9_bm.TextureAsset = "XAML Assets/TurnIcon";
            ImageManager.Instance.AddImage("XAML Assets/TurnIcon");
            this.e_9.Source = e_9_bm;
            this.e_9.Stretch = Stretch.Uniform;
            // e_10 element
            this.e_10 = new TextBlock();
            this.e_8.Children.Add(this.e_10);
            this.e_10.Name = "e_10";
            this.e_10.Margin = new Thickness(4F, 0F, 20F, 0F);
            this.e_10.VerticalAlignment = VerticalAlignment.Center;
            this.e_10.Foreground = new SolidColorBrush(new ColorW(211, 211, 211, 255));
            FontManager.Instance.AddFont("Arcadepix", 20F, FontStyle.Bold, "Arcadepix_15_Bold");
            this.e_10.FontFamily = new FontFamily("Arcadepix");
            this.e_10.FontSize = 20F;
            this.e_10.FontStyle = FontStyle.Bold;
            Binding binding_e_10_Text = new Binding("ScoreContext.Turns");
            this.e_10.SetBinding(TextBlock.TextProperty, binding_e_10_Text);
            // FlyoutButton element
            this.FlyoutButton = new Button();
            this.e_8.Children.Add(this.FlyoutButton);
            this.FlyoutButton.Name = "FlyoutButton";
            this.FlyoutButton.Margin = new Thickness(4F, 0F, 4F, 0F);
            FontManager.Instance.AddFont("Arcadepix", 12F, FontStyle.Regular, "Arcadepix_9_Regular");
            this.FlyoutButton.FontFamily = new FontFamily("Arcadepix");
            this.FlyoutButton.Content = "Info";
            Binding binding_FlyoutButton_Command = new Binding("FlyoutContext.ShowFlyoutCommand");
            this.FlyoutButton.SetBinding(Button.CommandProperty, binding_FlyoutButton_Command);
            // DataFlyout element
            this.DataFlyout = new Grid();
            this.UIRoot.Children.Add(this.DataFlyout);
            this.DataFlyout.Name = "DataFlyout";
            this.DataFlyout.Margin = new Thickness(0F, 0F, 75F, 0F);
            this.DataFlyout.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.DataFlyout.VerticalAlignment = VerticalAlignment.Stretch;
            RowDefinition row_DataFlyout_0 = new RowDefinition();
            row_DataFlyout_0.Height = new GridLength(4F, GridUnitType.Star);
            this.DataFlyout.RowDefinitions.Add(row_DataFlyout_0);
            RowDefinition row_DataFlyout_1 = new RowDefinition();
            row_DataFlyout_1.Height = new GridLength(3F, GridUnitType.Star);
            this.DataFlyout.RowDefinitions.Add(row_DataFlyout_1);
            RowDefinition row_DataFlyout_2 = new RowDefinition();
            row_DataFlyout_2.Height = new GridLength(1F, GridUnitType.Star);
            this.DataFlyout.RowDefinitions.Add(row_DataFlyout_2);
            RowDefinition row_DataFlyout_3 = new RowDefinition();
            row_DataFlyout_3.Height = new GridLength(14F, GridUnitType.Star);
            this.DataFlyout.RowDefinitions.Add(row_DataFlyout_3);
            RowDefinition row_DataFlyout_4 = new RowDefinition();
            row_DataFlyout_4.Height = new GridLength(0.6F, GridUnitType.Star);
            this.DataFlyout.RowDefinitions.Add(row_DataFlyout_4);
            RowDefinition row_DataFlyout_5 = new RowDefinition();
            row_DataFlyout_5.Height = new GridLength(3F, GridUnitType.Star);
            this.DataFlyout.RowDefinitions.Add(row_DataFlyout_5);
            RowDefinition row_DataFlyout_6 = new RowDefinition();
            row_DataFlyout_6.Height = new GridLength(0.6F, GridUnitType.Star);
            this.DataFlyout.RowDefinitions.Add(row_DataFlyout_6);
            RowDefinition row_DataFlyout_7 = new RowDefinition();
            row_DataFlyout_7.Height = new GridLength(6F, GridUnitType.Star);
            this.DataFlyout.RowDefinitions.Add(row_DataFlyout_7);
            RowDefinition row_DataFlyout_8 = new RowDefinition();
            row_DataFlyout_8.Height = new GridLength(2.5F, GridUnitType.Star);
            this.DataFlyout.RowDefinitions.Add(row_DataFlyout_8);
            Grid.SetColumn(this.DataFlyout, 2);
            Grid.SetColumnSpan(this.DataFlyout, 2);
            Binding binding_DataFlyout_Visibility = new Binding("FlyoutContext.FlyoutVisibility");
            this.DataFlyout.SetBinding(Grid.VisibilityProperty, binding_DataFlyout_Visibility);
            // e_11 element
            this.e_11 = new Grid();
            this.DataFlyout.Children.Add(this.e_11);
            this.e_11.Name = "e_11";
            this.e_11.Background = new SolidColorBrush(new ColorW(51, 51, 51, 255));
            Grid.SetRowSpan(this.e_11, 9);
            // e_12 element
            this.e_12 = new Grid();
            this.DataFlyout.Children.Add(this.e_12);
            this.e_12.Name = "e_12";
            this.e_12.Background = new SolidColorBrush(new ColorW(34, 34, 34, 255));
            Grid.SetRow(this.e_12, 1);
            // e_13 element
            this.e_13 = new Grid();
            this.DataFlyout.Children.Add(this.e_13);
            this.e_13.Name = "e_13";
            this.e_13.Background = new SolidColorBrush(new ColorW(34, 34, 34, 255));
            Grid.SetRow(this.e_13, 5);
            // e_14 element
            this.e_14 = new Grid();
            this.DataFlyout.Children.Add(this.e_14);
            this.e_14.Name = "e_14";
            this.e_14.Background = new SolidColorBrush(new ColorW(34, 34, 34, 255));
            Grid.SetRow(this.e_14, 8);
            // e_15 element
            this.e_15 = new TextBlock();
            this.DataFlyout.Children.Add(this.e_15);
            this.e_15.Name = "e_15";
            this.e_15.HorizontalAlignment = HorizontalAlignment.Center;
            this.e_15.VerticalAlignment = VerticalAlignment.Center;
            this.e_15.Foreground = new SolidColorBrush(new ColorW(255, 255, 255, 255));
            this.e_15.Text = "Hints";
            FontManager.Instance.AddFont("Arcadepix", 45F, FontStyle.Regular, "Arcadepix_33.75_Regular");
            this.e_15.FontFamily = new FontFamily("Arcadepix");
            this.e_15.FontSize = 45F;
            Grid.SetRow(this.e_15, 1);
            // e_16 element
            this.e_16 = new StackPanel();
            this.DataFlyout.Children.Add(this.e_16);
            this.e_16.Name = "e_16";
            this.e_16.Margin = new Thickness(0F, 0F, 30F, 0F);
            this.e_16.HorizontalAlignment = HorizontalAlignment.Right;
            this.e_16.VerticalAlignment = VerticalAlignment.Center;
            this.e_16.Orientation = Orientation.Horizontal;
            Grid.SetRow(this.e_16, 1);
            // CloseFlyoutButton element
            this.CloseFlyoutButton = new Button();
            this.e_16.Children.Add(this.CloseFlyoutButton);
            this.CloseFlyoutButton.Name = "CloseFlyoutButton";
            this.CloseFlyoutButton.Height = 25F;
            this.CloseFlyoutButton.Width = 25F;
            this.CloseFlyoutButton.Background = new SolidColorBrush(new ColorW(68, 68, 68, 255));
            this.CloseFlyoutButton.Padding = new Thickness(0F, 3F, 0F, 0F);
            this.CloseFlyoutButton.Foreground = new SolidColorBrush(new ColorW(255, 255, 255, 255));
            this.CloseFlyoutButton.VerticalContentAlignment = VerticalAlignment.Center;
            FontManager.Instance.AddFont("Arcadepix", 20F, FontStyle.Regular, "Arcadepix_15_Regular");
            this.CloseFlyoutButton.FontFamily = new FontFamily("Arcadepix");
            this.CloseFlyoutButton.FontSize = 20F;
            this.CloseFlyoutButton.FontStyle = FontStyle.Regular;
            this.CloseFlyoutButton.Content = "X";
            Binding binding_CloseFlyoutButton_Command = new Binding("FlyoutContext.ShowFlyoutCommand");
            this.CloseFlyoutButton.SetBinding(Button.CommandProperty, binding_CloseFlyoutButton_Command);
            // e_17 element
            this.e_17 = new ScrollViewer();
            this.DataFlyout.Children.Add(this.e_17);
            this.e_17.Name = "e_17";
            this.e_17.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.e_17.VerticalAlignment = VerticalAlignment.Stretch;
            FontManager.Instance.AddFont("Segoe UI", 12F, FontStyle.Regular, "Segoe_UI_9_Regular");
            this.e_17.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            this.e_17.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            Grid.SetRow(this.e_17, 3);
            // e_18 element
            this.e_18 = new ItemsControl();
            this.e_17.Content = this.e_18;
            this.e_18.Name = "e_18";
            this.e_18.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.e_18.Background = new SolidColorBrush(new ColorW(255, 255, 255, 0));
            this.e_18.HorizontalContentAlignment = HorizontalAlignment.Center;
            FontManager.Instance.AddFont("Arcadepix", 15F, FontStyle.Regular, "Arcadepix_11.25_Regular");
            this.e_18.FontFamily = new FontFamily("Arcadepix");
            this.e_18.FontSize = 15F;
            Func<UIElement, UIElement> e_18_iptFunc = e_18_iptMethod;
            ControlTemplate e_18_ipt = new ControlTemplate(e_18_iptFunc);
            this.e_18.ItemsPanel = e_18_ipt;
            Binding binding_e_18_ItemsSource = new Binding("FlyoutContext.UnlockedHints");
            this.e_18.SetBinding(ItemsControl.ItemsSourceProperty, binding_e_18_ItemsSource);
            // e_20 element
            this.e_20 = new TextBlock();
            this.DataFlyout.Children.Add(this.e_20);
            this.e_20.Name = "e_20";
            this.e_20.HorizontalAlignment = HorizontalAlignment.Center;
            this.e_20.VerticalAlignment = VerticalAlignment.Center;
            this.e_20.Foreground = new SolidColorBrush(new ColorW(255, 255, 255, 255));
            this.e_20.Text = "Secrets";
            FontManager.Instance.AddFont("Arcadepix", 45F, FontStyle.Regular, "Arcadepix_33.75_Regular");
            this.e_20.FontFamily = new FontFamily("Arcadepix");
            this.e_20.FontSize = 45F;
            Grid.SetRow(this.e_20, 5);
            // e_21 element
            this.e_21 = new ScrollViewer();
            this.DataFlyout.Children.Add(this.e_21);
            this.e_21.Name = "e_21";
            this.e_21.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.e_21.VerticalAlignment = VerticalAlignment.Stretch;
            FontManager.Instance.AddFont("Segoe UI", 12F, FontStyle.Regular, "Segoe_UI_9_Regular");
            this.e_21.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            this.e_21.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            Grid.SetRow(this.e_21, 7);
            // e_22 element
            this.e_22 = new ItemsControl();
            this.e_21.Content = this.e_22;
            this.e_22.Name = "e_22";
            this.e_22.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.e_22.Background = new SolidColorBrush(new ColorW(255, 255, 255, 0));
            this.e_22.HorizontalContentAlignment = HorizontalAlignment.Center;
            FontManager.Instance.AddFont("Arcadepix", 15F, FontStyle.Regular, "Arcadepix_11.25_Regular");
            this.e_22.FontFamily = new FontFamily("Arcadepix");
            this.e_22.FontSize = 15F;
            Func<UIElement, UIElement> e_22_iptFunc = e_22_iptMethod;
            ControlTemplate e_22_ipt = new ControlTemplate(e_22_iptFunc);
            this.e_22.ItemsPanel = e_22_ipt;
            Binding binding_e_22_ItemsSource = new Binding("FlyoutContext.UnlockedSecrets");
            this.e_22.SetBinding(ItemsControl.ItemsSourceProperty, binding_e_22_ItemsSource);
            // e_24 element
            this.e_24 = new StackPanel();
            this.DataFlyout.Children.Add(this.e_24);
            this.e_24.Name = "e_24";
            this.e_24.HorizontalAlignment = HorizontalAlignment.Center;
            this.e_24.VerticalAlignment = VerticalAlignment.Center;
            this.e_24.Orientation = Orientation.Horizontal;
            Grid.SetRow(this.e_24, 8);
            // SecretPurchaseButton element
            this.SecretPurchaseButton = new Button();
            this.e_24.Children.Add(this.SecretPurchaseButton);
            this.SecretPurchaseButton.Name = "SecretPurchaseButton";
            this.SecretPurchaseButton.Height = 33F;
            this.SecretPurchaseButton.Margin = new Thickness(0F, 0F, 10F, 0F);
            this.SecretPurchaseButton.Background = new SolidColorBrush(new ColorW(255, 255, 255, 0));
            this.SecretPurchaseButton.Foreground = new SolidColorBrush(new ColorW(255, 255, 255, 255));
            FontManager.Instance.AddFont("Arcadepix", 12F, FontStyle.Regular, "Arcadepix_9_Regular");
            this.SecretPurchaseButton.FontFamily = new FontFamily("Arcadepix");
            this.SecretPurchaseButton.Content = "Buy Secret";
            Binding binding_SecretPurchaseButton_Command = new Binding("FlyoutContext.BuySecretCommand");
            this.SecretPurchaseButton.SetBinding(Button.CommandProperty, binding_SecretPurchaseButton_Command);
            // ArrowPurchaseButton element
            this.ArrowPurchaseButton = new Button();
            this.e_24.Children.Add(this.ArrowPurchaseButton);
            this.ArrowPurchaseButton.Name = "ArrowPurchaseButton";
            this.ArrowPurchaseButton.Height = 33F;
            this.ArrowPurchaseButton.Margin = new Thickness(10F, 0F, 0F, 0F);
            this.ArrowPurchaseButton.Background = new SolidColorBrush(new ColorW(255, 255, 255, 0));
            this.ArrowPurchaseButton.Foreground = new SolidColorBrush(new ColorW(255, 255, 255, 255));
            FontManager.Instance.AddFont("Arcadepix", 12F, FontStyle.Regular, "Arcadepix_9_Regular");
            this.ArrowPurchaseButton.FontFamily = new FontFamily("Arcadepix");
            this.ArrowPurchaseButton.Content = "Buy Arrow";
            Binding binding_ArrowPurchaseButton_Command = new Binding("FlyoutContext.BuyArrowCommand");
            this.ArrowPurchaseButton.SetBinding(Button.CommandProperty, binding_ArrowPurchaseButton_Command);
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
            ColumnDefinition col_TriviaDisplay_0 = new ColumnDefinition();
            col_TriviaDisplay_0.Width = new GridLength(1F, GridUnitType.Star);
            this.TriviaDisplay.ColumnDefinitions.Add(col_TriviaDisplay_0);
            ColumnDefinition col_TriviaDisplay_1 = new ColumnDefinition();
            col_TriviaDisplay_1.Width = new GridLength(8F, GridUnitType.Star);
            this.TriviaDisplay.ColumnDefinitions.Add(col_TriviaDisplay_1);
            ColumnDefinition col_TriviaDisplay_2 = new ColumnDefinition();
            col_TriviaDisplay_2.Width = new GridLength(1F, GridUnitType.Star);
            this.TriviaDisplay.ColumnDefinitions.Add(col_TriviaDisplay_2);
            Grid.SetColumn(this.TriviaDisplay, 0);
            Grid.SetColumnSpan(this.TriviaDisplay, 4);
            Binding binding_TriviaDisplay_Opacity = new Binding("TriviaContext.TriviaModalFadeAnimation.CurrentValue");
            this.TriviaDisplay.SetBinding(Grid.OpacityProperty, binding_TriviaDisplay_Opacity);
            binding_TriviaDisplay_Opacity.FallbackValue = "0";
            Binding binding_TriviaDisplay_Visibility = new Binding("TriviaContext.TriviaModalFadeAnimation.Visibility");
            this.TriviaDisplay.SetBinding(Grid.VisibilityProperty, binding_TriviaDisplay_Visibility);
            binding_TriviaDisplay_Visibility.FallbackValue = "Hidden";
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
            Grid.SetColumn(this.QuestionText, 1);
            Grid.SetRow(this.QuestionText, 1);
            Binding binding_QuestionText_Text = new Binding("TriviaContext.CurrentTriviaQuestionText");
            this.QuestionText.SetBinding(TextBlock.TextProperty, binding_QuestionText_Text);
            // e_25 element
            this.e_25 = new StackPanel();
            this.TriviaDisplay.Children.Add(this.e_25);
            this.e_25.Name = "e_25";
            this.e_25.HorizontalAlignment = HorizontalAlignment.Center;
            this.e_25.VerticalAlignment = VerticalAlignment.Center;
            this.e_25.Orientation = Orientation.Horizontal;
            Grid.SetColumn(this.e_25, 1);
            Grid.SetRow(this.e_25, 3);
            // TriviaAnswerSelector element
            this.TriviaAnswerSelector = new ComboBox();
            this.e_25.Children.Add(this.TriviaAnswerSelector);
            this.TriviaAnswerSelector.Name = "TriviaAnswerSelector";
            this.TriviaAnswerSelector.Width = 450F;
            FontManager.Instance.AddFont("Arcadepix", 20F, FontStyle.Regular, "Arcadepix_15_Regular");
            this.TriviaAnswerSelector.FontFamily = new FontFamily("Arcadepix");
            this.TriviaAnswerSelector.FontSize = 20F;
            Binding binding_TriviaAnswerSelector_ItemsSource = new Binding("TriviaContext.CurrentTriviaQuestionAnswersAsComboBoxItems");
            this.TriviaAnswerSelector.SetBinding(ComboBox.ItemsSourceProperty, binding_TriviaAnswerSelector_ItemsSource);
            Binding binding_TriviaAnswerSelector_SelectedIndex = new Binding("TriviaContext.SelectedAnswerIndex");
            this.TriviaAnswerSelector.SetBinding(ComboBox.SelectedIndexProperty, binding_TriviaAnswerSelector_SelectedIndex);
            Binding binding_TriviaAnswerSelector_SelectedItem = new Binding("TriviaContext.SelectedAnswer");
            this.TriviaAnswerSelector.SetBinding(ComboBox.SelectedItemProperty, binding_TriviaAnswerSelector_SelectedItem);
            // e_26 element
            this.e_26 = new Button();
            this.e_25.Children.Add(this.e_26);
            this.e_26.Name = "e_26";
            this.e_26.Width = 70F;
            this.e_26.Margin = new Thickness(5F, 0F, 0F, 0F);
            FontManager.Instance.AddFont("Arcadepix", 15F, FontStyle.Regular, "Arcadepix_11.25_Regular");
            this.e_26.FontFamily = new FontFamily("Arcadepix");
            this.e_26.FontSize = 15F;
            this.e_26.Content = "Submit";
            Grid.SetRow(this.e_26, 3);
            Binding binding_e_26_Command = new Binding("TriviaContext.SubmitAnswerCommand");
            this.e_26.SetBinding(Button.CommandProperty, binding_e_26_Command);
            // e_27 element
            this.e_27 = new StackPanel();
            this.TriviaDisplay.Children.Add(this.e_27);
            this.e_27.Name = "e_27";
            this.e_27.HorizontalAlignment = HorizontalAlignment.Center;
            this.e_27.VerticalAlignment = VerticalAlignment.Center;
            this.e_27.Orientation = Orientation.Horizontal;
            Grid.SetColumn(this.e_27, 1);
            Grid.SetRow(this.e_27, 4);
            // e_28 element
            this.e_28 = new TextBlock();
            this.e_27.Children.Add(this.e_28);
            this.e_28.Name = "e_28";
            this.e_28.Foreground = new SolidColorBrush(new ColorW(255, 255, 255, 255));
            FontManager.Instance.AddFont("Arcadepix", 20F, FontStyle.Regular, "Arcadepix_15_Regular");
            this.e_28.FontFamily = new FontFamily("Arcadepix");
            this.e_28.FontSize = 20F;
            Binding binding_e_28_Text = new Binding("TriviaContext.NumTriviaQuestionsCorrect");
            this.e_28.SetBinding(TextBlock.TextProperty, binding_e_28_Text);
            // e_29 element
            this.e_29 = new TextBlock();
            this.e_27.Children.Add(this.e_29);
            this.e_29.Name = "e_29";
            this.e_29.Foreground = new SolidColorBrush(new ColorW(255, 255, 255, 255));
            this.e_29.Text = "/";
            FontManager.Instance.AddFont("Arcadepix", 20F, FontStyle.Regular, "Arcadepix_15_Regular");
            this.e_29.FontFamily = new FontFamily("Arcadepix");
            this.e_29.FontSize = 20F;
            // e_30 element
            this.e_30 = new TextBlock();
            this.e_27.Children.Add(this.e_30);
            this.e_30.Name = "e_30";
            this.e_30.Foreground = new SolidColorBrush(new ColorW(255, 255, 255, 255));
            FontManager.Instance.AddFont("Arcadepix", 20F, FontStyle.Regular, "Arcadepix_15_Regular");
            this.e_30.FontFamily = new FontFamily("Arcadepix");
            this.e_30.FontSize = 20F;
            Binding binding_e_30_Text = new Binding("TriviaContext.NumTriviaQuestionsTotal");
            this.e_30.SetBinding(TextBlock.TextProperty, binding_e_30_Text);
            // GameOverModalDialog element
            this.GameOverModalDialog = new Grid();
            this.UIRoot.Children.Add(this.GameOverModalDialog);
            this.GameOverModalDialog.Name = "GameOverModalDialog";
            this.GameOverModalDialog.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.GameOverModalDialog.Background = new SolidColorBrush(new ColorW(51, 51, 51, 187));
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
            col_GameOverModalDialog_0.Width = new GridLength(4F, GridUnitType.Star);
            this.GameOverModalDialog.ColumnDefinitions.Add(col_GameOverModalDialog_0);
            ColumnDefinition col_GameOverModalDialog_1 = new ColumnDefinition();
            col_GameOverModalDialog_1.Width = new GridLength(6F, GridUnitType.Star);
            this.GameOverModalDialog.ColumnDefinitions.Add(col_GameOverModalDialog_1);
            ColumnDefinition col_GameOverModalDialog_2 = new ColumnDefinition();
            col_GameOverModalDialog_2.Width = new GridLength(2F, GridUnitType.Star);
            this.GameOverModalDialog.ColumnDefinitions.Add(col_GameOverModalDialog_2);
            ColumnDefinition col_GameOverModalDialog_3 = new ColumnDefinition();
            col_GameOverModalDialog_3.Width = new GridLength(4F, GridUnitType.Star);
            this.GameOverModalDialog.ColumnDefinitions.Add(col_GameOverModalDialog_3);
            Grid.SetColumn(this.GameOverModalDialog, 0);
            Grid.SetColumnSpan(this.GameOverModalDialog, 4);
            Binding binding_GameOverModalDialog_Margin = new Binding("GameOverContext.GameOverModalMargin");
            this.GameOverModalDialog.SetBinding(Grid.MarginProperty, binding_GameOverModalDialog_Margin);
            Binding binding_GameOverModalDialog_Opacity = new Binding("GameOverContext.GameOverModalOpacity");
            this.GameOverModalDialog.SetBinding(Grid.OpacityProperty, binding_GameOverModalDialog_Opacity);
            Binding binding_GameOverModalDialog_Visibility = new Binding("GameOverContext.GameOverModalVisibility");
            this.GameOverModalDialog.SetBinding(Grid.VisibilityProperty, binding_GameOverModalDialog_Visibility);
            binding_GameOverModalDialog_Visibility.FallbackValue = "Collapsed";
            // e_31 element
            this.e_31 = new TextBlock();
            this.GameOverModalDialog.Children.Add(this.e_31);
            this.e_31.Name = "e_31";
            this.e_31.HorizontalAlignment = HorizontalAlignment.Center;
            this.e_31.VerticalAlignment = VerticalAlignment.Center;
            this.e_31.Foreground = new SolidColorBrush(new ColorW(211, 211, 211, 255));
            this.e_31.TextWrapping = TextWrapping.Wrap;
            FontManager.Instance.AddFont("Arcadepix", 53.33333F, FontStyle.Bold, "Arcadepix_40_Bold");
            this.e_31.FontFamily = new FontFamily("Arcadepix");
            this.e_31.FontSize = 53.33333F;
            this.e_31.FontStyle = FontStyle.Bold;
            Grid.SetColumn(this.e_31, 1);
            Grid.SetRow(this.e_31, 1);
            Grid.SetColumnSpan(this.e_31, 2);
            Binding binding_e_31_Text = new Binding("GameOverContext.GameOverMessage");
            this.e_31.SetBinding(TextBlock.TextProperty, binding_e_31_Text);
            // e_32 element
            this.e_32 = new TextBox();
            this.GameOverModalDialog.Children.Add(this.e_32);
            this.e_32.Name = "e_32";
            this.e_32.Height = 33F;
            this.e_32.Margin = new Thickness(4F, 4F, 4F, 4F);
            FontManager.Instance.AddFont("Arcadepix", 20F, FontStyle.Regular, "Arcadepix_15_Regular");
            this.e_32.FontFamily = new FontFamily("Arcadepix");
            this.e_32.FontSize = 20F;
            Grid.SetColumn(this.e_32, 1);
            Grid.SetRow(this.e_32, 2);
            Binding binding_e_32_Visibility = new Binding("GameOverContext.UsernameBoxVisibility");
            this.e_32.SetBinding(TextBox.VisibilityProperty, binding_e_32_Visibility);
            Binding binding_e_32_Text = new Binding("GameOverContext.GameOverUsernameText");
            this.e_32.SetBinding(TextBox.TextProperty, binding_e_32_Text);
            // NameSubmitButton element
            this.NameSubmitButton = new Button();
            this.GameOverModalDialog.Children.Add(this.NameSubmitButton);
            this.NameSubmitButton.Name = "NameSubmitButton";
            this.NameSubmitButton.Height = 33F;
            this.NameSubmitButton.Margin = new Thickness(4F, 4F, 4F, 4F);
            FontManager.Instance.AddFont("Arcadepix", 15F, FontStyle.Regular, "Arcadepix_11.25_Regular");
            this.NameSubmitButton.FontFamily = new FontFamily("Arcadepix");
            this.NameSubmitButton.FontSize = 15F;
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
            FontManager.Instance.AddFont("Arcadepix", 20F, FontStyle.Regular, "Arcadepix_15_Regular");
            this.MenuButton.FontFamily = new FontFamily("Arcadepix");
            this.MenuButton.FontSize = 20F;
            this.MenuButton.Content = "Back to Main Menu";
            Grid.SetColumn(this.MenuButton, 1);
            Grid.SetRow(this.MenuButton, 3);
            Grid.SetColumnSpan(this.MenuButton, 2);
            Binding binding_MenuButton_Command = new Binding("GameOverContext.ReturnToMenuCommand");
            this.MenuButton.SetBinding(Button.CommandProperty, binding_MenuButton_Command);
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
        
        private static UIElement e_22_iptMethod(UIElement parent) {
            // e_23 element
            StackPanel e_23 = new StackPanel();
            e_23.Parent = parent;
            e_23.Name = "e_23";
            e_23.HorizontalAlignment = HorizontalAlignment.Center;
            e_23.IsItemsHost = true;
            return e_23;
        }
    }
}

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
        
        private Grid e_0;
        
        private StackPanel e_1;
        
        private Image e_2;
        
        private TextBlock e_3;
        
        private TextBlock e_4;
        
        private StackPanel e_5;
        
        private TextBlock e_6;
        
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
            // e_0 element
            this.e_0 = new Grid();
            this.Content = this.e_0;
            this.e_0.Name = "e_0";
            this.e_0.Background = new SolidColorBrush(new ColorW(255, 255, 255, 0));
            RowDefinition row_e_0_0 = new RowDefinition();
            row_e_0_0.Height = new GridLength(1F, GridUnitType.Star);
            this.e_0.RowDefinitions.Add(row_e_0_0);
            RowDefinition row_e_0_1 = new RowDefinition();
            row_e_0_1.Height = new GridLength(30F, GridUnitType.Pixel);
            this.e_0.RowDefinitions.Add(row_e_0_1);
            // e_1 element
            this.e_1 = new StackPanel();
            this.e_0.Children.Add(this.e_1);
            this.e_1.Name = "e_1";
            this.e_1.Background = new SolidColorBrush(new ColorW(169, 169, 169, 255));
            this.e_1.Orientation = Orientation.Horizontal;
            Grid.SetRow(this.e_1, 1);
            // e_2 element
            this.e_2 = new Image();
            this.e_1.Children.Add(this.e_2);
            this.e_2.Name = "e_2";
            this.e_2.Height = 30F;
            this.e_2.Width = 100F;
            BitmapImage e_2_bm = new BitmapImage();
            e_2_bm.TextureAsset = "XAML Assets/Gold";
            ImageManager.Instance.AddImage("XAML Assets/Gold");
            this.e_2.Source = e_2_bm;
            // e_3 element
            this.e_3 = new TextBlock();
            this.e_1.Children.Add(this.e_3);
            this.e_3.Name = "e_3";
            this.e_3.Margin = new Thickness(4F, 0F, 4F, 0F);
            this.e_3.Foreground = new SolidColorBrush(new ColorW(211, 211, 211, 255));
            FontManager.Instance.AddFont("Segoe UI", 20F, FontStyle.Bold, "Segoe_UI_15_Bold");
            this.e_3.FontFamily = new FontFamily("Segoe UI");
            this.e_3.FontSize = 20F;
            this.e_3.FontStyle = FontStyle.Bold;
            Binding binding_e_3_Text = new Binding("Gold");
            this.e_3.SetBinding(TextBlock.TextProperty, binding_e_3_Text);
            // e_4 element
            this.e_4 = new TextBlock();
            this.e_1.Children.Add(this.e_4);
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
            this.e_0.Children.Add(this.e_5);
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
        }
    }
}

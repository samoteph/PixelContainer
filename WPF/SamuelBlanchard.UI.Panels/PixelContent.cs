#if UWP
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#endif

#if WPF
using System.Windows;
using System.Windows.Controls;
#endif

namespace SamuelBlanchard.UI.Panels
{
    public class PixelContent : ContentControl
    {
        public PixelContent()
        {
            this.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            this.VerticalContentAlignment = VerticalAlignment.Stretch;
        }

        /// <summary>
        /// Pixel X
        /// </summary>

        public int PixelX
        {
            get { return (int)Pixel.GetX(this); }
            set { SetValue(PixelXProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PixelX.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PixelXProperty =
            DependencyProperty.Register("PixelX", typeof(int), typeof(PixelContent), new PropertyMetadata(0, OnPixelXChange));

        private static void OnPixelXChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var value = (int)e.NewValue;
            Pixel.SetX(d, value);
        }

        /// <summary>
        /// Pixel Y
        /// </summary>

        public int PixelY
        {
            get { return (int)Pixel.GetY(this); }
            set { SetValue(PixelYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PixelY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PixelYProperty =
            DependencyProperty.Register("PixelY", typeof(int), typeof(PixelContent), new PropertyMetadata(0, OnPixelYChange));

        private static void OnPixelYChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var value= (int)e.NewValue;
            Pixel.SetY(d, value);
        }

        /// <summary>
        /// Pixel Width
        /// </summary>

        public int PixelWidth
        {
            get { return (int)Pixel.GetWidth(this); }
            set { SetValue(PixelWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PixelWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PixelWidthProperty =
            DependencyProperty.Register("PixelWidth", typeof(int), typeof(PixelContent), new PropertyMetadata(0, OnPixelWidthChange));

        private static void OnPixelWidthChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var value = (int)e.NewValue;
            Pixel.SetWidth(d, value);
        }

        /// <summary>
        /// Pixel Height
        /// </summary>

        public int PixelHeight
        {
            get { return (int)Pixel.GetHeight(this); }
            set { SetValue(PixelHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PixelHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PixelHeightProperty =
            DependencyProperty.Register("PixelHeight", typeof(int), typeof(PixelContent), new PropertyMetadata(0, OnPixelHeightChange));

        private static void OnPixelHeightChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var value = (int)e.NewValue;
            Pixel.SetHeight(d, value);
        }

        /// <summary>
        /// Pixel Origin
        /// </summary>

        public Point PixelOrigin
        {
            get { return (Point)Pixel.GetOrigin(this); }
            set { SetValue(PixelOriginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PixelOrigin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PixelOriginProperty =
            DependencyProperty.Register("PixelOrigin", typeof(Point), typeof(PixelContent), new PropertyMetadata(new Point(0,0), OnPixelOriginChange));

        private static void OnPixelOriginChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var value = (Point)e.NewValue;
            Pixel.SetOrigin(d, value);
        }
    }
}

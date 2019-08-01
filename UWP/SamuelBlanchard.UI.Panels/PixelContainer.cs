using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace SamBlanchard.UI.Panels
{
    public sealed class PixelContainer : ItemsControl
    {
        private Image image;

        public PixelContainer()
        {
            this.DefaultStyleKey = typeof(PixelContainer);
        }

        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(PixelContainer), new PropertyMetadata(null, OnImageChange));

        private static void OnImageChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var me = d as PixelContainer;

            var newSource = e.NewValue as BitmapImage;
            var oldSource = e.OldValue as BitmapImage;

            me.ChangeImageSource(newSource, oldSource);
        }

        private void ChangeImageSource(BitmapImage newBitmapImage, BitmapImage oldBitmapImage)
        {
           var panel = this.ItemsPanelRoot as PixelPanel;

            if(panel != null)
            {
                panel.ClearSize();
            }

            if(oldBitmapImage != null)
            {
                if (oldBitmapImage.PixelWidth == 0 && oldBitmapImage.PixelHeight == 0)
                {
                    oldBitmapImage.ImageOpened -= BitmapImage_ImageOpened;
                }
            }

            if (newBitmapImage != null)
            {
                if (newBitmapImage.PixelWidth == 0 && newBitmapImage.PixelHeight == 0)
                {
                    newBitmapImage.ImageOpened += BitmapImage_ImageOpened;
                }
                else
                {
                    this.BitmapOpened(newBitmapImage as BitmapSource);
                }
            }
        }

        private void BitmapImage_ImageOpened(object sender, RoutedEventArgs e)
        {
            this.BitmapOpened(sender as BitmapSource);
        }

        public event RoutedEventHandler ImageOpened;

        /// <summary>
        /// Bitmap Opened
        /// </summary>
        /// <param name="source"></param>

        private void BitmapOpened(BitmapSource source)
        {
            this.PixelWidth = source.PixelWidth;
            this.PixelHeight = source.PixelHeight;

            var pixelPanel = this.ItemsPanelRoot as PixelPanel;

            if (pixelPanel != null)
            {
                pixelPanel.SetPixelSize(source.PixelWidth, source.PixelHeight);
            }

            ImageOpened?.Invoke(this, new RoutedEventArgs());
        }

        public Stretch Stretch
        {
            get { return (Stretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Stretch.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StretchProperty =
            DependencyProperty.Register("Stretch", typeof(Stretch), typeof(PixelContainer), new PropertyMetadata(Stretch.Uniform));

        protected override void OnApplyTemplate()
        {
            image = this.GetTemplateChild("Image") as Image;
            image.SizeChanged += Image_SizeChanged;

            base.OnApplyTemplate();
        }

        private void Image_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var pixelPanel = this.ItemsPanelRoot as PixelPanel;

            pixelPanel.SetImageSize(e.NewSize.Width, e.NewSize.Height);
        }

        public int PixelWidth
        {
            get;
            private set;
        }

        public int PixelHeight
        {
            get;
            private set;
        }

        public double ConvertToPixelLayout(CoordinateAlignment alignment, double xamlValue)
        {
            var root = this.ItemsPanelRoot as PixelPanel;
            if (root != null)
            {
                return root.ConvertToPixelLayout(alignment, xamlValue);
            }

            return double.NaN;
        }

        public double ConvertToXamlLayout(CoordinateAlignment alignment, double pixelValue)
        {
            var root = this.ItemsPanelRoot as PixelPanel;
            if (root != null)
            {
                return root.ConvertToXamlLayout(alignment, pixelValue);
            }

            return double.NaN;
        }
    }
}

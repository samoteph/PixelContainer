#if UWP
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
#endif

#if WPF
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
#endif

namespace SamuelBlanchard.UI.Panels
{
    public sealed class PixelContainer : ItemsControl
    {
        private Image image;
        private Grid imageContainer;

#if WPF
        static PixelContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PixelContainer), new FrameworkPropertyMetadata(typeof(PixelContainer)));
        }

        public PixelContainer()
        {
            this.Loaded += PixelContainer_Loaded;
        }

        private void PixelContainer_Loaded(object sender, RoutedEventArgs e)
        {
            ItemsPresenter itemsPresenter = GetVisualChild<ItemsPresenter>(this);
            var pixelPanel = VisualTreeHelper.GetChild(itemsPresenter, 0) as PixelPanel;

            pixelPanel?.SetImageSize(this.image.ActualWidth, this.image.ActualHeight);
            pixelPanel?.SetPixelSize(this.PixelWidth, this.PixelHeight);

            this.ItemsPanelRoot = pixelPanel;
        }
#endif

#if UWP
        public PixelContainer()
        {
            this.DefaultStyleKey = typeof(PixelContainer);
        }
#endif
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
#if UWP
            var newSource = e.NewValue as BitmapImage;
            var oldSource = e.OldValue as BitmapImage;

            me.ChangeImageSource(newSource, oldSource);
#endif

#if WPF
            var newSource = e.NewValue as BitmapSource;
            var oldSource = e.OldValue as BitmapSource;

            me.ChangeImageSource(newSource, oldSource);
#endif
        }
#if UWP
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
#endif

#if WPF
        private void ChangeImageSource(BitmapSource newBitmapImage, BitmapSource oldBitmapImage)
        {
            var panel = this.ItemsPanelRoot as PixelPanel;

            if (panel != null)
            {
                panel.ClearSize();
            }

            if (oldBitmapImage != null)
            {
                if (oldBitmapImage.PixelWidth == 0 && oldBitmapImage.PixelHeight == 0)
                {
                    oldBitmapImage.DownloadCompleted -= NewBitmapImage_DownloadCompleted;
                }
            }

            if (newBitmapImage != null)
            {
                if (newBitmapImage.PixelWidth == 0 && newBitmapImage.PixelHeight == 0)
                {
                    newBitmapImage.DownloadCompleted += NewBitmapImage_DownloadCompleted; ;
                }
                else
                {
                    this.BitmapOpened(newBitmapImage as BitmapSource);
                }
            }
        }

        private void NewBitmapImage_DownloadCompleted(object sender, System.EventArgs e)
        {
            this.BitmapOpened(sender as BitmapSource);
        }
#endif

        public event RoutedEventHandler ImageOpened;

#if WPF
        public PixelPanel ItemsPanelRoot
        {
            get;
            private set;
        }
#endif

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

#if WPF
        public override void OnApplyTemplate()
        {
#endif
#if UWP
            protected override void OnApplyTemplate()
            {
#endif
            image = this.GetTemplateChild("Image") as Image;
            image.SizeChanged += Image_SizeChanged;

            imageContainer = this.GetTemplateChild("ImageContainer") as Grid;
            imageContainer.SizeChanged += ImageContainer_SizeChanged;

            this.ClipImage();

            base.OnApplyTemplate();
        }

        private void ImageContainer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.ClipImage();
        }

        private void Image_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var pixelPanel = this.ItemsPanelRoot as PixelPanel;

            pixelPanel?.SetImageSize(e.NewSize.Width, e.NewSize.Height);
            this.ClipImage();
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

#if WPF
        private static T GetVisualChild<T>(DependencyObject parent) where T : Visual
        {
            T child = default(T);

            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }

            return child;
        }
#endif



        public bool ClipImageToBounds
        {
            get { return (bool)GetValue(ClipImageToBoundsProperty); }
            set { SetValue(ClipImageToBoundsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ClipImageToBounds.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClipImageToBoundsProperty =
            DependencyProperty.Register("ClipImageToBounds", typeof(bool), typeof(PixelContainer), new PropertyMetadata(false, OnClipToBoundsChange));

        private static void OnClipToBoundsChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            bool isClip = (bool)e.NewValue;
            var me = d as PixelContainer;
            me.ClipImage();
        }

        /// <summary>
        /// Clip the 
        /// </summary>

        private void ClipImage()
        {
            if(imageContainer == null)
            {
                return;
            }

            if(this.ClipImageToBounds == true)
            {
                var clip = new RectangleGeometry();

                var x = (this.imageContainer.ActualWidth - this.image.ActualWidth) / 2;
                var y = (this.imageContainer.ActualHeight - this.image.ActualHeight) / 2;

                clip.Rect = new Rect(x,y, this.image.ActualWidth, this.image.ActualHeight);
                this.imageContainer.Clip = clip;
            }
            else
            {
                this.imageContainer.Clip = null;
            }
        }
    }
}

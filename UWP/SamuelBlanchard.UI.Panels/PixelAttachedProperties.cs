using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace SamBlanchard.UI.Panels
{
    [Bindable]
    public class Pixel : DependencyObject
    {
        private static PixelPanel GetParent(DependencyObject dpo)
        {
            while (dpo != null)
            {
                dpo = VisualTreeHelper.GetParent(dpo);

                if (dpo is PixelPanel)
                {
                    break;
                }
            }

            return dpo as PixelPanel;
        }

        /// <summary>
        /// X
        /// </summary>

        public static readonly DependencyProperty XProperty = DependencyProperty.RegisterAttached(
            "X", typeof(double), typeof(Pixel), new PropertyMetadata(0.0, OnXChange));

        private static void OnXChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as FrameworkElement;

            if (control != null)
            {
                var parent = GetParent(control);

                if (parent != null)
                {
                    var x = (double)e.NewValue;

                    parent.ArrangeChild(control,
                        parent.RenderSize,
                        parent.ImageWidth,
                        parent.ImageHeight,
                        parent.PixelWidth,
                        parent.PixelHeight,
                        childPixelX: x
                        );
                }
            }
        }

        public static void SetX(DependencyObject target, double value)
        {
            target.SetValue(XProperty, value);
        }

        public static double GetX(DependencyObject target)
        {
            return (double)target.GetValue(XProperty);
        }

        /// <summary>
        /// Y
        /// </summary>

        public static readonly DependencyProperty YProperty = DependencyProperty.RegisterAttached(
            "Y", typeof(double), typeof(Pixel), new PropertyMetadata(0.0, OnYChange));

        private static void OnYChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as FrameworkElement;

            if (control != null)
            {
                var parent = GetParent(control);

                if (parent != null)
                {
                    var y = (double)e.NewValue;

                    parent.ArrangeChild(control,
                        parent.RenderSize,
                        parent.ImageWidth,
                        parent.ImageHeight,
                        parent.PixelWidth,
                        parent.PixelHeight,
                        childPixelY: y
                        );
                }
            }
        }

        public static void SetY(DependencyObject target, double value)
        {
            target.SetValue(YProperty, value);
        }

        public static double GetY(DependencyObject target)
        {
            return (double)target.GetValue(YProperty);
        }

        /// <summary>
        /// Width
        /// </summary>

        public static readonly DependencyProperty WidthProperty = DependencyProperty.RegisterAttached(
            "Width", typeof(double), typeof(Pixel), new PropertyMetadata(double.NaN, OnWidthChange));

        private static void OnWidthChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as FrameworkElement;

            if (control != null)
            {
                var parent = GetParent(control);

                if (parent != null)
                {
                    var width = (double)e.NewValue;

                    //parent.MeasureChild(control,
                    //    parent.ImageWidth,
                    //    parent.ImageHeight,
                    //    parent.PixelWidth,
                    //    parent.PixelHeight,
                    //    childPixelWidth: width
                    //    );

                    parent.ArrangeChild(control,
                        parent.RenderSize,
                        parent.ImageWidth,
                        parent.ImageHeight,
                        parent.PixelWidth,
                        parent.PixelHeight,
                        childPixelWidth: width
                        );
                }
            }
        }

        public static void SetWidth(DependencyObject target, double value)
        {
            target.SetValue(WidthProperty, value);
        }

        public static double GetWidth(DependencyObject target)
        {
            return (double)target.GetValue(WidthProperty);
        }

        /// <summary>
        /// Height
        /// </summary>

        public static readonly DependencyProperty HeightProperty = DependencyProperty.RegisterAttached(
            "Height", typeof(double), typeof(Pixel), new PropertyMetadata(double.NaN, OnHeightChange));

        private static void OnHeightChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as FrameworkElement;

            if (control != null)
            {
                var parent = GetParent(control);

                if (parent != null)
                {
                    var height = (double)e.NewValue;

                    //parent.MeasureChild(control,
                    //    parent.ImageWidth,
                    //    parent.ImageHeight,
                    //    parent.PixelWidth,
                    //    parent.PixelHeight,
                    //    childPixelHeight: height
                    //    );

                    parent.ArrangeChild(control,
                        parent.RenderSize,
                        parent.ImageWidth,
                        parent.ImageHeight,
                        parent.PixelWidth,
                        parent.PixelHeight,
                        childPixelHeight: height
                        );
                }
            }
        }

        public static void SetHeight(DependencyObject target, double value)
        {
            target.SetValue(HeightProperty, value);
        }

        public static double GetHeight(DependencyObject target)
        {
            return (double)target.GetValue(HeightProperty);
        }

        /// <summary>
        /// Origin
        /// </summary>

        public static readonly DependencyProperty OriginProperty = DependencyProperty.RegisterAttached(
            "Origin", typeof(Point), typeof(Pixel), new PropertyMetadata(new Point(0,0), OnOriginChange));

        private static void OnOriginChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as FrameworkElement;

            if (control != null)
            {
                var parent = GetParent(control);

                if (parent != null)
                {
                    var origin = (Point)e.NewValue;

                    // seul la position change donc pas besoin d'appeller Measure
                    parent.ArrangeChild(control,
                        parent.RenderSize,
                        parent.ImageWidth,
                        parent.ImageHeight,
                        parent.PixelWidth,
                        parent.PixelHeight,

                        childOrigin:origin
                        );
                }
            }
        }

        public static void SetOrigin(DependencyObject target, Point value)
        {
            target.SetValue(OriginProperty, value);
        }

        public static Point GetOrigin(DependencyObject target)
        {
            return (Point)target.GetValue(OriginProperty);
        }
    }
}

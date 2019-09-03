#if UWP
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
#endif

#if WPF
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
#endif

namespace SamuelBlanchard.UI.Panels
{
    public class PixelPanel : Panel
    {
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

        public double ImageWidth
        {
            get;
            private set;
        } = -1;

        public double ImageHeight
        {
            get;
            private set;
        } = -1;

        public bool AutoApplyLayoutStretchToChild
        {
            get;
            set;
        } = false;

        public PixelPanel()
        {
            this.ClearSize();
        }

        /// <summary>
        /// Affecter les tailles des pixels
        /// </summary>
        /// <param name="pixelWidth"></param>
        /// <param name="pixelHeight"></param>

        public void SetPixelSize(int pixelWidth, int pixelHeight)
        {
            this.PixelWidth = pixelWidth;
            this.PixelHeight = pixelHeight;

            this.InvalidateMeasure();
        }

        /// <summary>
        /// Affecter la taille de l'image
        /// </summary>
        /// <param name="pixelWidth"></param>
        /// <param name="pixelHeight"></param>

        public void SetImageSize(double width, double height)
        {
            this.ImageWidth = width;
            this.ImageHeight = height;

            this.InvalidateMeasure();
        }

        public void ClearSize()
        {
            this.PixelWidth = -1;
            this.PixelHeight = -1;
        }

        public bool IsReadyForLayout
        {
            get
            {
                bool isReady = this.PixelWidth != -1 && this.PixelHeight != -1 && this.ImageWidth != -1 && this.ImageHeight != -1;

                Debug.WriteLine("IsReady=" + isReady);

                return isReady;
            }
        }

        public void MeasureChild(UIElement child, double imageWidth, double imageHeight, double imagePixelWidth, double imagePixelHeight, double childPixelWidth = double.NegativeInfinity, double childPixelHeight = double.NegativeInfinity)
        {
            UIElement elementChild = GetChild(child);

            if (double.IsNegativeInfinity(childPixelWidth))
            {
                childPixelWidth = Pixel.GetWidth(elementChild);
            }

            if (double.IsNegativeInfinity(childPixelHeight))
            {
                childPixelHeight = Pixel.GetHeight(elementChild);
            }

            var w = double.PositiveInfinity; // child.DesiredSize.Width;
            var h = double.PositiveInfinity; // child.DesiredSize.Height;

            if (double.IsNaN(childPixelWidth) == false)
            {
                w = (childPixelWidth * imageWidth) / imagePixelWidth;
            }

            if (double.IsNaN(childPixelHeight) == false)
            {
                h = (childPixelHeight * imageHeight) / imagePixelHeight;
            }

            if(AutoApplyLayoutStretchToChild == true)
            {
                var element = elementChild as FrameworkElement;
                element.HorizontalAlignment = HorizontalAlignment.Stretch;
                element.VerticalAlignment = VerticalAlignment.Stretch;
            }

            child.Measure(new Size(w, h));

            // dans le cas ou w ou h est en double.PositiveInfinity (mais pas les deux en même temps) la valeur dans DesiredSize n'est peut être pas bonne.
            // car elle est calculée par rapport à la valeur XAML et elle peut être deformé par la valeur Strech=Fill de l'image
            // le ratio width et Height de l'image n'est parfois pas uniforme à l'affichage (cas de Fill par exemple)
            // dans le cas ou w et h sont double.PositiveInfinity en même temps on considère que c'est la taille XAML qui est voulu

            //if (w == double.PositiveInfinity || h == double.PositiveInfinity)
            //{
            //    if (w != double.PositiveInfinity || h != double.PositiveInfinity)
            //    {
            //        // ici on sait que w ou h est double.PositiveInfinity (mais pas les deux en même temps).
            //        if (w == double.PositiveInfinity)
            //        {
            //            w = (child.DesiredSize.Width * imagePixelWidth) / imageWidth;
            //        }
            //        else
            //        {
            //            h = (child.DesiredSize.Height * imagePixelHeight) / imageHeight;
            //        }

            //        // Le child measure ne fonctionne pas !!!
            //        child.Measure(new Size(w, h));
            //    }
            //}
        }

        /// <summary>
        /// Obtenir une valeur en pixel a partir d'une valeur Xaml (position, taille)
        /// </summary>
        /// <param name="alignment"></param>
        /// <param name="xamlValue"></param>
        /// <returns></returns>

        public double ConvertToPixelLayout(CoordinateAlignment alignment, double xamlValue)
        {
            if (IsReadyForLayout)
            {
                if (alignment == CoordinateAlignment.Horizontal)
                {
                    return (xamlValue * this.PixelWidth) / this.ImageWidth;
                }

                return (xamlValue * this.PixelHeight) / this.ImageHeight;
            }

            return double.NaN;
        }

        /// <summary>
        /// Obtenir une valeur en XAML à partir d'une valeur en pixel (position, taille)
        /// </summary>
        /// <param name="alignment"></param>
        /// <param name="pixelValue"></param>
        /// <returns></returns>

        public double ConvertToXamlLayout(CoordinateAlignment alignment, double pixelValue)
        {
            if (IsReadyForLayout)
            {
                if (alignment == CoordinateAlignment.Horizontal)
                {
                    return (pixelValue * this.ImageWidth) / this.PixelWidth;
                }

                return (pixelValue * this.ImageHeight) / this.PixelHeight;
            }

            return double.NaN;
        }

        /// <summary>
        /// Measure
        /// </summary>
        /// <param name="availableSize"></param>
        /// <returns></returns>

        protected override Size MeasureOverride(Size availableSize)
        {
            var wp = availableSize.Width;
            var hp = availableSize.Height;

            if (IsReadyForLayout)
            {
                var imageWidth = this.ImageWidth;
                var imageHeight = this.ImageHeight;

                if (wp == double.PositiveInfinity)
                {
                    wp = this.ImageWidth;
                }

                if (hp == double.PositiveInfinity)
                {
                    hp = this.ImageHeight;
                }

                var imagePixelWidth = this.PixelWidth;
                var imagePixelHeight = this.PixelHeight;

                for (int i = 0; i < this.Children.Count; i++)
                {
                    UIElement child = this.Children[i];

                    this.MeasureChild(child,
                        imageWidth,
                        imageHeight,
                        imagePixelWidth,
                        imagePixelHeight
                        );

                    //Debug.WriteLine("Measure=" + size);
                }
            }
            else
            {
                // mise à zéro en taille
                for (int i = 0; i < this.Children.Count; i++)
                {
                    var child = this.Children[i];
                    child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                }

                if (wp == double.PositiveInfinity)
                {
                    wp = 0;
                }

                if (hp == double.PositiveInfinity)
                {
                    hp = 0;
                }
            }

            availableSize = new Size(wp, hp);

            return availableSize;
        }

        private UIElement GetChild(UIElement child)
        {
            UIElement elementChild;

            if (child is ContentPresenter)
            {
                elementChild = VisualTreeHelper.GetChild(child, 0) as UIElement;
            }
            else
            {
                elementChild = child;
            }

            return elementChild;
        }

        /// <summary>
        /// Arrange Child
        /// </summary>
        /// <param name="child"></param>
        /// <param name="childPixelX"></param>
        /// <param name="childPixelY"></param>
        /// <param name="childPixelWidth"></param>
        /// <param name="childPixelHeight"></param>
        /// <param name="childOrigin"></param>

        public void ArrangeChild(UIElement child, Size finalSize, double imageWidth, double imageHeight, double imagePixelWidth, double imagePixelHeight, double childPixelX = double.NegativeInfinity, double childPixelY = double.NegativeInfinity, double childPixelWidth = double.NegativeInfinity, double childPixelHeight = double.NegativeInfinity, Point? childOrigin = null)
        {
            UIElement elementChild = GetChild(child);

            if (double.IsNegativeInfinity(childPixelX))
            {
                childPixelX = Pixel.GetX(elementChild);
            }

            if (double.IsNegativeInfinity(childPixelY))
            {
                childPixelY = Pixel.GetY(elementChild);
            }

            if (double.IsNegativeInfinity(childPixelWidth))
            {
                childPixelWidth = Pixel.GetWidth(elementChild);
            }

            if (double.IsNegativeInfinity(childPixelHeight))
            {
                childPixelHeight = Pixel.GetHeight(elementChild);
            }

            Point childPixelOrigin;

            if (childOrigin == null)
            {
                childPixelOrigin = Pixel.GetOrigin(elementChild);
            }
            else
            {
                childPixelOrigin = childOrigin.Value;
            }

            var marginVertical = (finalSize.Height - imageHeight) / 2;
            var marginHorizontal = (finalSize.Width - imageWidth) / 2;

            //Debug.WriteLine("Margin H=" + marginHorizontal);

            var w = child.DesiredSize.Width;
            var h = child.DesiredSize.Height;

            double x = 0;
            double y = 0;

            x = (childPixelX * imageWidth) / imagePixelWidth;
            y = (childPixelY * imageHeight) / imagePixelHeight;

            // C'est important de garder cela car dans le cas d'un DesiredSize à 0,0 (le control se laisse retailler par son parent), il faut lui forcer la taille
            if (double.IsNaN(childPixelWidth) == false)
            {
                w = (childPixelWidth * imageWidth) / imagePixelWidth;
            }
            //else if (useStrechFill && double.IsNaN(childPixelHeight) == false)
            //{
            //    // par defaut si childPixelHeight existe mais childPixelWidth est à NaN, alors le DesiredSize.Width est calculer comme si l'axe Horiz et Vert avait le même ratio (ce qui n'est pas vrai en mode Fill mais ok pour les autres modes) 
            //    // on doit donc compenser
            //    w = (w * (ImageWidth / imagePixelWidth)) / (imageHeight / imagePixelHeight);
            //}

            if (double.IsNaN(childPixelHeight) == false)
            {
                h = (childPixelHeight * imageHeight) / imagePixelHeight;
            }
            //else if(useStrechFill && double.IsNaN(childPixelWidth) == false )
            //{
            //    // par defaut si childPixelWidth existe mais childPixelHeight est à NaN, alors le DesiredSize.Height est calculer comme si l'axe Horiz et Vert avait le même ratio (ce qui n'est pas vrai en mode Fill mais ok pour les autres modes) 
            //    // on doit donc compenser
            //    // TODO ne fonctionne plus width de l'image quand est très grand

            //    var ratioHeight = imageHeight / imagePixelHeight;
            //    var ratioWidth = imageWidth / imagePixelWidth;

            //    h = (h * ratioHeight) / ratioWidth;

            //    Debug.WriteLine("ratioH=" + ratioHeight + " ratioW=" + ratioWidth + "h=" + h + " DesiredW=" + DesiredSize.Width + " DesiredH=" + DesiredSize.Height);
            //}

            if (childPixelOrigin.X != 0)
            {
                x = x - (w * childPixelOrigin.X);
            }

            if (childPixelOrigin.Y != 0)
            {
                y = y - (h * childPixelOrigin.Y);
            }

            Point position = new Point(x + marginHorizontal, y + marginVertical);
            Size size = new Size(w, h);

            //Debug.WriteLine("Arrange=" + size);

            child.Arrange(new Rect(position, size));
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Debug.WriteLine("Arrange?");

            if (IsReadyForLayout)
            {
                Debug.WriteLine("Arrange Ready");

                var imageWidth = this.ImageWidth;
                var imageHeight = this.ImageHeight;

                var imagePixelWidth = this.PixelWidth;
                var imagePixelHeight = this.PixelHeight;

                for (int i = 0; i < this.Children.Count; i++)
                {
                    UIElement child = (UIElement)this.Children[i];

                    ArrangeChild(child,
                        finalSize,
                        imageWidth,
                        imageHeight,
                        imagePixelWidth,
                        imagePixelHeight
                        );
                }
            }
            else
            {
                base.ArrangeOverride(finalSize);
            }

            return finalSize; //OR, return a different Size, but that's rare        
        }
    }

    public enum CoordinateAlignment
    {
        Horizontal,
        Vertical
    }
}

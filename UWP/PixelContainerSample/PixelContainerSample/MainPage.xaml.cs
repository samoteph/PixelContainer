using PixelContainerSample.UserControls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PixelContainerSample
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Dictionary<Rectangle, Card> cardDictionnary = new Dictionary<Rectangle, Card>();

        public MainPage()
        {
            this.InitializeComponent();

            cardDictionnary.Add(this.RectMario, this.CardMario);
            cardDictionnary.Add(this.RectLuigi, this.CardLuigi);
            cardDictionnary.Add(this.RectPeach, this.CardPeach);
        }

        private void Rect_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            this.cardDictionnary[sender as Rectangle].Visibility = Visibility.Visible;
        }

        private void Rect_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            this.cardDictionnary[sender as Rectangle].Visibility = Visibility.Collapsed;
        }
    }
}

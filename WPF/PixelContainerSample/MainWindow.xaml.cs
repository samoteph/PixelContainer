using PixelContainerSample.UserControls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PixelContainerSample
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<Rectangle, Card> cardDictionnary = new Dictionary<Rectangle, Card>();

        public MainWindow()
        {
            this.InitializeComponent();

            cardDictionnary.Add(this.RectMario, this.CardMario);
            cardDictionnary.Add(this.RectLuigi, this.CardLuigi);
            cardDictionnary.Add(this.RectPeach, this.CardPeach);
        }

        private void RectMario_MouseEnter(object sender, MouseEventArgs e)
        {
            this.cardDictionnary[sender as Rectangle].Visibility = Visibility.Visible;
        }

        private void RectMario_MouseLeave(object sender, MouseEventArgs e)
        {
            this.cardDictionnary[sender as Rectangle].Visibility = Visibility.Collapsed;
        }
    }
}

#if UWP
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
#endif

#if WPF
using System.Windows;
using System.Windows.Controls;
#endif

// Pour en savoir plus sur le modèle d'élément Contrôle utilisateur, consultez la page https://go.microsoft.com/fwlink/?LinkId=234236


namespace PixelContainerSample.UserControls
{
    public sealed partial class Card : UserControl
    {
        public Card()
        {
            this.InitializeComponent();

            this.LayoutRoot.DataContext = this;
        }

        /// <summary>
        /// Name of the character
        /// </summary>

        public string CharacterName
        {
            get { return (string)GetValue(CharacterNameProperty); }
            set { SetValue(CharacterNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CharacterName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CharacterNameProperty =
            DependencyProperty.Register("CharacterName", typeof(string), typeof(Card), new PropertyMetadata(null));

        /// <summary>
        /// Strength of the character
        /// </summary>

        public string Strength
        {
            get { return (string)GetValue(StrengthProperty); }
            set { SetValue(StrengthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Strength.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrengthProperty =
            DependencyProperty.Register("Strength", typeof(string), typeof(Card), new PropertyMetadata(null));

        /// <summary>
        /// Is a good or Bad Guy
        /// </summary>

        public string GoodOrBadGuy
        {
            get { return (string)GetValue(GoodOrBadGuyProperty); }
            set { SetValue(GoodOrBadGuyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GoodOrBadGuy.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GoodOrBadGuyProperty =
            DependencyProperty.Register("GoodOrBadGuy", typeof(string), typeof(Card), new PropertyMetadata(null));
    }
}

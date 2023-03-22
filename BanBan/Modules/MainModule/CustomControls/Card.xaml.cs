using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using model = MainModule.Models;

namespace MainModule.CustomControls
{
    /// <summary>
    /// Interaction logic for Card.xaml
    /// </summary>
    public partial class Card : UserControl
    {
        public Card()
        {
            InitializeComponent();
        }
        private void updated(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(DataContext == null) return;

            model.Card card = DataContext as model.Card;
            tbx.Text = card.Content.Description;

        }
    }
}


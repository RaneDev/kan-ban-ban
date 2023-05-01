using System.Windows;
using System.Windows.Controls;
using model = KanBanModule.Models;

namespace KanBanModule.CustomControls
{
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


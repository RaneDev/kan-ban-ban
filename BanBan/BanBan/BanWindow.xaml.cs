using System.Windows;
using KanBanModule;

namespace BanBan
{
    public partial class BanWindow : Window
    {
        public BanWindow()
        {
            InitializeComponent();
            Container.Children.Add(new KanBanView());
        }
    }
}

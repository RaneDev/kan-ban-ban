using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MainModule.Models;

namespace MainPage
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public ObservableCollection<Group> CardsGroups { get; set; }

        public ObservableCollection<GroupAdder> CardsGroupAdder { get; set; }

        public MainView()
        {
            InitializeComponent();
            DataContext = this;

            CardsGroups = new()
            {
                new Group() { Name =  "1", Cards = new() { new Card() { Name = "Card1" }, new Card() { Name = "Card2" } } },
                new Group() { Name =  "2", Cards = new() { new Card() { Name = "Card1" }, new Card() { Name = "Card2" } } },
                new Group() { Name =  "3", Cards = new() { new Card() { Name = "Card1" }, new Card() { Name = "Card2" } } },
                new Group() { Name =  "4", Cards = new() { new SimpleCard() { Name = "CardSimple" }, new Card() { Name = "Card2" } } },
            };

            CardsGroupAdder = new()
            {
                new GroupAdder() {Text = "Adder"},
            };
        }


        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void FlowCanvas_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
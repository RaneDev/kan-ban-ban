using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using MainModule.Models;
using Cus = MainModule.CustomControls;

namespace MainPage
{
    public partial class MainView : UserControl, INotifyPropertyChanged
    {
        public ObservableCollection<IGroup> Groups { get; set; }
        public IGroup? DragGroup { get; set; }

        private Grid _blockNow = new();
        private Cus.Card _cardNow = new();
        private Point blockPoint;

        public MainView()
        {
            InitializeComponent();
            DataContext = this;

            Groups = new()
            {
                new Group() { ID = "1", Name =  "To Do", Cards = new() { new Card() { Name = "Planning 1", Content = new CardContent() { Description = "Do That" } }, new Card() { Name = "Planning 2" }, new CardAdder() } },
                new Group() { ID = "2", Name =  "In Progress", Cards = new() { new Card() { Name = "Doing 1" }, new Card() { Name = "Doing That" }, new CardAdder() } },
                new Group() { ID = "3", Name =  "Done", Cards = new() { new Card() { Name = "Done 1" }, new Card() { Name = "Done That" } , new CardAdder() } },
                new Group() { ID = "4", Name =  "Done2", Cards = new() { new Card() { Name = "Done 2" }, new Card() { Name = "Done That" } , new CardAdder() } },
                new GroupAdder() { Name = "Adder"},
            };
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Dragger.IsMouseCaptured)
                return;

            _blockNow = (Grid)sender;
            _blockNow.Opacity = 0.5;
            blockPoint = Mouse.GetPosition(_blockNow);

            DragGroup = Groups.First(g => g is Group grp && grp.ID == _blockNow.Tag?.ToString());
            OnPropertyChanged(nameof(DragGroup));
            Dragger.Visibility = Visibility.Visible;
            Dragger.CaptureMouse();
        }


        private void Card_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Dragger.IsMouseCaptured)
                return;

            _cardNow = (Cus.Card)sender;
            _cardNow.Opacity = 0.5;
            blockPoint = Mouse.GetPosition(_cardNow);

            DragGroup = new Group { Cards = new ObservableCollection<ICard> { (Card)_cardNow.DataContext } };
            OnPropertyChanged(nameof(DragGroup));
            Dragger.Visibility = Visibility.Visible;
            Dragger.CaptureMouse();
        }


        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragger.IsMouseCaptured)
            {
                var mousePosition = e.GetPosition((UIElement)VisualTreeHelper.GetParent(Dragger));
                Dragger.RenderTransform = new TranslateTransform(mousePosition.X - blockPoint.X, mousePosition.Y - blockPoint.Y);

                VisualTreeHelper.HitTest(MyGrid, null, result =>
                {
                    return TryAddShadow(result) ? HitTestResultBehavior.Stop : HitTestResultBehavior.Continue;
                }, new PointHitTestParameters(mousePosition));
            }
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            hasinsert = false;
            _blockNow.Opacity = 1;
            _cardNow.Opacity = 1;
            Dragger.Visibility = Visibility.Collapsed;
            Dragger.ReleaseMouseCapture();
        }

        private bool hasinsert = false;
        private bool TryAddShadow(HitTestResult result)
        {
            if (!string.IsNullOrWhiteSpace(DragGroup?.Name) && result?.VisualHit is FrameworkElement separator && separator.Tag?.ToString() == "Separator")
            {
                var grp = (Group)separator.DataContext;
                var dgrp = (Group)_blockNow.DataContext;

                Groups.Move(Groups.IndexOf(dgrp), Groups.IndexOf(grp));
                CollectionViewSource.GetDefaultView(Groups).Refresh();
                return true;
            }
            else if (string.IsNullOrWhiteSpace(DragGroup?.Name) && result?.VisualHit is FrameworkElement cardSeparator && cardSeparator.Tag?.ToString() == "CardSeparator")
            {
                if (hasinsert)
                    return false;

                var crd = (ICard)cardSeparator.DataContext;
                var crdnow = (Card)_cardNow.DataContext;

                if (crd != crdnow)
                {
                    Group gg = (Group)Groups.First(g => g is Group g1 && g1.Cards.Contains(crdnow));
                    Group ggg = (Group)Groups.First(g => g is Group g1 && g1.Cards.Contains(crd));
                    if (gg == ggg)
                    {
                        gg.Cards.Move(gg.Cards.IndexOf(crdnow), gg.Cards.IndexOf(crd));
                    }
                    else 
                    {
                        int ind = ggg.Cards.IndexOf(crd);
                        ggg.Cards.Insert(ind, crdnow);
                        _cardNow.DataContext = ggg.Cards[ind];
                        gg.Cards.Remove(crdnow);

                        //hasinsert = true;
                    }
                    CollectionViewSource.GetDefaultView(Groups).Refresh();
                }
            }
            return false;
        }


        StackPanel MyStackPanel = new();
        private void load(object sender, RoutedEventArgs e)
        {
            MyStackPanel = (StackPanel)sender;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using KanBanModule.Models;
using BanResources.Commands;
using CustomControl = KanBanModule.CustomControls;

namespace KanBanModule.ViewModels
{
    public class KanBanViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<IGroup> Groups { get; set; }
        public ICommand DragSetupCommand { get; set; }
        public ICommand DragStartCommand { get; set; }
        public ICommand DragMoveCommand { get; set; }
        public ICommand DragStopCommand { get; set; }
        public ICommand CardAddedCommand { get; set; }

        private IGroup? dragGroup;
        public IGroup? DragGroup
        {
            get { return dragGroup; }
            set
            {
                if (dragGroup != value)
                {
                    dragGroup = value;
                    OnPropertyChanged(nameof(DragGroup));
                }
            }
        }

        public static string GroupSeparatorTag => "GSr";
        public static string CardSeparatorTag => "CSr";

        private FrameworkElement _dragView = new();
        private FrameworkElement _selectedDragItem = new();
        private Style _draggingStyle = new();
        private Style _dragReleaseStyle = new();
        private Point _referencePoint;

        public KanBanViewModel()
        {
            DragSetupCommand = new RelayCommand<(dynamic, dynamic, dynamic)>(DragSetup);
            DragStartCommand = new RelayCommand<FrameworkElement>(DragStart);
            DragMoveCommand = new RelayCommand<FrameworkElement>(DragMove);
            DragStopCommand = new RelayCommand(DragStop);
            CardAddedCommand = new RelayCommand<object>(CardAdding);

            Groups = new()
            {
                new Group() { ID = "1", Name =  "To Do", Cards = new() { new Card() { Name = "Planning 1", GroupIndex = 0, Content = new CardContent() { Description = "Do That" } }, new Card() { Name = "Planning 2", GroupIndex = 0 }, new CardAdder() { GroupIndex = 0 } } },
                new Group() { ID = "2", Name =  "In Progress", Cards = new() { new Card() { Name = "Doing 1" }, new Card() { Name = "Doing That" }, new CardAdder() { GroupIndex = 1 } } },
                new Group() { ID = "3", Name =  "Done", Cards = new() { new Card() { Name = "Done 1" }, new Card() { Name = "Done That" } , new CardAdder() {  GroupIndex = 2 } } },
                new Group() { ID = "4", Name =  "Done2", Cards = new() { new Card() { Name = "Done 2" }, new Card() { Name = "Done That" } , new CardAdder() { GroupIndex = 3 } } },
                new GroupAdder() { Name = "Adder"},
            };
        }

        private void DragSetup((dynamic dragView, dynamic draggedStyle, dynamic dragReleaseStyle) sender)
        {
            _dragView =  sender.dragView;
            _draggingStyle = sender.draggedStyle;
            _dragReleaseStyle = sender.dragReleaseStyle;
        }

        private void DragStart(FrameworkElement dragItem)
        {
            if (_dragView.IsMouseCaptured) return;

            _selectedDragItem = dragItem;

            DragGroup = _selectedDragItem.DataContext switch
            {
                Group => Groups.First(groups => groups is Group group && group.ID == _selectedDragItem.Tag?.ToString()),
                Card => new Group { Cards = new ObservableCollection<ICard> { (Card)_selectedDragItem.DataContext } },
                _ => throw new System.NotImplementedException()
            };

            _selectedDragItem.Style = _draggingStyle;
            _referencePoint = Mouse.GetPosition(dragItem);
            _dragView.Visibility = Visibility.Visible;
            _dragView.CaptureMouse();
        }

        private void DragMove(FrameworkElement mainPanel)
        {
            if (!_dragView.IsMouseCaptured) return;

            var mousePosition = Mouse.GetPosition((UIElement)VisualTreeHelper.GetParent(_dragView));
            _dragView.RenderTransform = new TranslateTransform(mousePosition.X - _referencePoint.X, mousePosition.Y - _referencePoint.Y);

            VisualTreeHelper.HitTest(mainPanel, null, result =>
            {
                return TryInsertDragItem(result) ? HitTestResultBehavior.Stop : HitTestResultBehavior.Continue;
            }, new PointHitTestParameters(mousePosition));
        }

        private void DragStop()
        {
            _selectedDragItem.Style = _dragReleaseStyle;
            _dragView.Visibility = Visibility.Collapsed;
            _dragView.ReleaseMouseCapture();
        }

        private bool TryInsertDragItem(HitTestResult result)
        {
            if (result?.VisualHit is not FrameworkElement separator) return false;

            if (separator.Tag?.ToString() == GroupSeparatorTag && _selectedDragItem.DataContext is Group group)
            {
                Groups.Move(Groups.IndexOf(group), Groups.IndexOf((Group)separator.DataContext));

            }
            else if (separator.Tag?.ToString() == CardSeparatorTag && _selectedDragItem.DataContext is Card selectedCard)
            {
                var hitCard = (ICard)separator.DataContext;

                if (hitCard == selectedCard) return false;

                var hitGroup = (Group)Groups.First(groups => groups is Group group && group.Cards.Contains(hitCard));
                var selectedGroup = (Group)Groups.First(groups => groups is Group group && group.Cards.Contains(selectedCard));

                if (selectedGroup == hitGroup)
                {
                    selectedGroup.Cards.Move(selectedGroup.Cards.IndexOf(selectedCard), selectedGroup.Cards.IndexOf(hitCard));
                }
                else
                {
                    int hitCardIndex = hitGroup.Cards.IndexOf(hitCard);
                    hitGroup.Cards.Insert(hitCardIndex, selectedCard);
                    _selectedDragItem.DataContext = hitGroup.Cards[hitCardIndex];
                    selectedGroup.Cards.Remove(selectedCard);
                }
            }
            else
            {
                return false;
            }

            CollectionViewSource.GetDefaultView(Groups).Refresh();
            return true;
        }

        public void CardAdding(object sender)
        {
            int index = (int)((CustomControl.CardAdder)sender).Tag;
            var group = Groups[index] as Group;
            group?.Cards.Insert(group.Cards.Count - 1, new Card() { Name = "New Card", Content = new CardContent() { Description = "Cards" } });
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

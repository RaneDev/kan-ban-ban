using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BanResources;

namespace KanBanModule.CustomControls
{
    public partial class CardAdder : UserControl
    {
        public static DependencyProperty CardAddedDependency
       = DependencyProperty.Register("CardAddCommand", typeof(ICommand), typeof(CardAdder));

        public ICommand CardAddCommand
        {
            get { return (ICommand)GetValue(CardAddedDependency); }
            set { SetValue(CardAddedDependency, value); }
        }

        public CardAdder()
        {
            InitializeComponent();
        }

        private void CardAdder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CardDefineTextbox.ToggleVisibility();
            CardDefineTextbox.Focus();
        }

        private void KeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Hide_LostFocus(sender, e);
                CardAddCommand.Execute(this);
            }
        }

        private void Hide_LostFocus(object sender, RoutedEventArgs e)
        {
            CardDefineTextbox.Visibility = Visibility.Collapsed;
            CardDefineTextbox.Text = String.Empty;
        }
    }
}

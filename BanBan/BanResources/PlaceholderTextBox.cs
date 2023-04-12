using System;
using System.Windows;
using System.Windows.Controls;

namespace BanResources
{
    public class PlaceholderTextBox : TextBox
    {
        public static readonly DependencyProperty IsEmptyProperty =
        DependencyProperty.Register("IsEmpty", typeof(bool), typeof(PlaceholderTextBox),
            new PropertyMetadata(false));

        public bool IsEmpty
        {
            get { return (bool)GetValue(IsEmptyProperty); }
            private set { SetValue(IsEmptyProperty, value); }
        }

        protected override void OnInitialized(EventArgs e)
        {
            UpdateIsEmpty();
            base.OnInitialized(e);
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            UpdateIsEmpty();
            base.OnTextChanged(e);
        }

        private void UpdateIsEmpty()
        {
            IsEmpty = string.IsNullOrEmpty(Text);
        }

    }
}

using System;
using System.Windows;
using System.Windows.Controls;

namespace BanResources
{
    public class PlaceholderTextBox : TextBox
    {
        public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.Register("Placeholder", typeof(string), typeof(PlaceholderTextBox),
            new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty IsTextEmptyProperty =
        DependencyProperty.Register("IsTextEmpty", typeof(bool), typeof(PlaceholderTextBox),
            new PropertyMetadata(false));

        public bool IsTextEmpty
        {
            get { return (bool)GetValue(IsTextEmptyProperty); }
            private set { SetValue(IsTextEmptyProperty, value); }
        }

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }
        protected override void OnInitialized(EventArgs e)
        {
            CheckEmpty();
            base.OnInitialized(e);
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            CheckEmpty();
            base.OnTextChanged(e);
        }

        private void CheckEmpty()
        {
            IsTextEmpty = string.IsNullOrEmpty(Text);
        }

    }
}

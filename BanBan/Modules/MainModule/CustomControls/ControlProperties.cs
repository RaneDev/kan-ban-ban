using System.Windows;
using System.Windows.Controls;

namespace MainModule.CustomControls
{
    public static class TextBoxProperties
    {
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.RegisterAttached("Placeholder", typeof(string), typeof(TextBoxProperties), new PropertyMetadata(null, OnPlaceholderChanged));

        public static string GetPlaceholder(DependencyObject obj)
        {
            return (string)obj.GetValue(PlaceholderProperty);
        }

        public static void SetPlaceholder(DependencyObject obj, string value)
        {
            obj.SetValue(PlaceholderProperty, value);
        }

        private static void OnPlaceholderChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var textBox = obj as TextBox;

            if (textBox == null)
            {
                return;
            }

            var placeholderText = e.NewValue as string;

            if (placeholderText != null)
            {
                textBox.Loaded += (sender, args) =>
                {
                    textBox.Text = placeholderText;
                    //var template = textBox.Template;
                    //var watermark = template.FindName("PART_Watermark", textBox) as ContentControl;

                    //if (watermark != null)
                    //{
                    //    watermark.Content = placeholderText;
                    //}
                };
            }
        }
    }
}

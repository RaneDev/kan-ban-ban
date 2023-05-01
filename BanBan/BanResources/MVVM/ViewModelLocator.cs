using System;
using System.Reflection;
using System.Windows;

namespace BanResources
{
    public class ViewModelLocator
    {
        public static readonly DependencyProperty AutoConnectedViewModelProperty =
        DependencyProperty.RegisterAttached(
        "AutoConnectedViewModel",
        typeof(bool),
        typeof(ViewModelLocator),
        new PropertyMetadata(false, AutoConnectedViewModelChanged));

        public static bool GetAutoConnectedViewModelProperty(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoConnectedViewModelProperty);
        }

        public static void SetAutoConnectedViewModelProperty
        (DependencyObject obj, bool value)
        {
            obj.SetValue(AutoConnectedViewModelProperty, value);
        }

        private static void AutoConnectedViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewTypeName = d.GetType().FullName;
            var viewModelTypeName = viewTypeName?.Insert(viewTypeName.IndexOf('.'), ".ViewModels") + "Model";
            var viewModelType =  Assembly.GetAssembly(d.GetType())?.GetType(viewModelTypeName);
            var viewModel = Activator.CreateInstance(viewModelType!);
            ((FrameworkElement)d).DataContext = viewModel;
        }
    }
}

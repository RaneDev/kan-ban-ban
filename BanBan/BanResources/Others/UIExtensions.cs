﻿using System.Windows;

namespace BanResources
{
    public static class UIExtensions
    {
        public static void ToggleVisibility(this UIElement uIElement, bool hidden = false)
        {
            uIElement.Visibility = uIElement.Visibility == Visibility.Visible ?
                hidden ? Visibility.Hidden : Visibility.Collapsed
                : Visibility.Visible;
        }
    }
}
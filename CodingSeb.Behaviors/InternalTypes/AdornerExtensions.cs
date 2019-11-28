﻿using System.Windows;
using System.Windows.Documents;

namespace CodingSeb.Behaviors
{
    internal static class AdornerExtensions
    {
        public static void TryRemoveAdorners<T>(this UIElement elem) where T : Adorner
        {
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(elem);
            adornerLayer?.RemoveAdorners<T>(elem);
        }

        public static void RemoveAdorners<T>(this AdornerLayer adr, UIElement elem)
            where T : Adorner
        {
            Adorner[] adorners = adr.GetAdorners(elem);

            if (adorners == null) return;

            for (int i = adorners.Length - 1; i >= 0; i--)
            {
                if (adorners[i] is T)
                    adr.Remove(adorners[i]);
            }
        }

        public static void TryAddAdorner<T>(this UIElement elem, Adorner adorner)
            where T : Adorner
        {
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(elem);
            if (adornerLayer?.ContainsAdorner<T>(elem) == false)
            {
                adornerLayer.Add(adorner);
            }
        }

        public static bool ContainsAdorner<T>(this AdornerLayer adr, UIElement elem)
            where T : Adorner
        {
            Adorner[] adorners = adr.GetAdorners(elem);

            if (adorners == null) return false;

            for (int i = adorners.Length - 1; i >= 0; i--)
            {
                if (adorners[i] is T)
                    return true;
            }
            return false;
        }

        public static void RemoveAllAdorners(this AdornerLayer adr, UIElement elem)
        {
            Adorner[] adorners = adr.GetAdorners(elem);

            if (adorners == null) return;

            foreach (var toRemove in adorners)
                adr.Remove(toRemove);
        }
    }
}

using System;
using System.Collections.Generic;

namespace SpaceInvaders.Tools.Extensions
{
    public static class ObjectExtensions
    {
        public static void AssignTo<T>(this T value, ref T targetField, Action onChanged)
        {
            if (EqualityComparer<T>.Default.Equals(targetField, value))
            {
                return;
            }

            targetField = value;
            onChanged?.Invoke();
        }
    }
}
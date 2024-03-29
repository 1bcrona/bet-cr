﻿using System;
using System.Collections.Generic;

namespace BetCR.Library
{
    public static class OperationExtender
    {
        #region Public Methods

        public static bool AddDistinct<T>(this IList<T> list, T value, IEqualityComparer<T> comparer)
        {
            if (list.ContainsValue(value, comparer)) return false;

            list.Add(value);
            return true;
        }

        public static bool AddRangeDistinct<T>(this IList<T> list, IEnumerable<T> values)
        {
            return list.AddRangeDistinct(values, EqualityComparer<T>.Default);
        }

        public static bool AddRangeDistinct<T>(this IList<T> list, IEnumerable<T> values, IEqualityComparer<T> comparer)
        {
            var allAdded = true;
            foreach (var value in values)
                if (!list.AddDistinct(value, comparer))
                    allAdded = false;

            return allAdded;
        }

        public static bool ContainsValue<TSource>(this IEnumerable<TSource> source, TSource value, IEqualityComparer<TSource> comparer)
        {
            if (ReferenceEquals(source, null)) throw new ArgumentNullException("source");

            if (comparer == null) comparer = EqualityComparer<TSource>.Default;

            foreach (var local in source)
                if (comparer.Equals(local, value))
                    return true;

            return false;
        }

        #endregion Public Methods
    }
}
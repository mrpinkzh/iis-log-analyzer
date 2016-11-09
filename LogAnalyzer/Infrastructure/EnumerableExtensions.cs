using System;
using System.Collections.Generic;
using System.Linq;

namespace LogAnalyzer.Infrastructure
{
    public static class EnumerableExtensions
    {
        public static int IndexOfFirst<T>(this IEnumerable<T> enumerable, T item)
        {
            var list = enumerable?.ToList();
            if (list == null)
                return -1;
            for (int i = 0; i < list.Count; i++)
                if (Equals(item, list[i]))
                    return i;
            return -1;
        }

        public static IEnumerable<IEnumerable<T>> Sectionize<T>(this IEnumerable<T> enumerable, Func<T, bool> sectionHeaderIdentifier)
        {
            var list = enumerable.ToList();
            T firstSectionHeader = list.FirstOrDefault(sectionHeaderIdentifier);
            if (firstSectionHeader == null)
                return new List<IEnumerable<T>>();
            int indexOfFirstSectionHeader = list.IndexOfFirst(firstSectionHeader);

            T secondSectionHeader = list.Skip(indexOfFirstSectionHeader + 1).FirstOrDefault(sectionHeaderIdentifier);
            if (secondSectionHeader == null)
                return new List<IEnumerable<T>> {list.Skip(indexOfFirstSectionHeader)};

            int indexOfSecondSectionHeader = list.IndexOf(secondSectionHeader);
            IEnumerable<T> itemsInFirstSection = list.Skip(indexOfFirstSectionHeader).Take(indexOfSecondSectionHeader - indexOfFirstSectionHeader);
            var result = new List<IEnumerable<T>> { itemsInFirstSection };
            result.AddRange(list.Skip(indexOfSecondSectionHeader).Sectionize(sectionHeaderIdentifier));
            return result;
        }
    }
}
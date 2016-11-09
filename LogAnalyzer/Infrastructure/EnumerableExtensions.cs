using System;
using System.Collections.Generic;
using System.Linq;

namespace LogAnalyzer.Infrastructure
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Searches the index of the first occurance of the specified <paramref name="item"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="enumerable">The enumerable to search in.</param>
        /// <param name="item">The item to search the index for.</param>
        /// <returns>The index of the specified <paramref name="item"/>, -1 if it is not present.</returns>
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

        /// <summary>
        /// Searches the specified <paramref name="enumerable"/> for an item matching the <paramref name="sectionHeaderIdentifier"/> predicate.
        /// It then splits the enumerable in sections per found entry. The first element of each section will be the matched element.
        /// All elements prior to the first section header will not be in the result.
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="enumerable">The enumerable to sectionize.</param>
        /// <param name="sectionHeaderIdentifier">A predicate identfying a section header.</param>
        /// <returns>An <see cref="IReadOnlyList{T}"/> of IEnumerables starting with a section header.</returns>
        public static IReadOnlyList<IEnumerable<T>> Sectionize<T>(this IEnumerable<T> enumerable, Func<T, bool> sectionHeaderIdentifier)
        {
            var list = enumerable.ToList();
            T firstSectionHeader = list.FirstOrDefault(sectionHeaderIdentifier);
            if (firstSectionHeader == null)
                return new List<IEnumerable<T>>();
            int indexOfFirstSectionHeader = list.IndexOfFirst(firstSectionHeader);
            var firstSection = list.Skip(indexOfFirstSectionHeader).ToList();

            T secondSectionHeader = firstSection.Skip(1).FirstOrDefault(sectionHeaderIdentifier);
            if (secondSectionHeader == null)
                return new List<IEnumerable<T>> { firstSection };

            int indexOfSecondSectionHeader = firstSection.Skip(1).IndexOfFirst(secondSectionHeader) + 1;
            IEnumerable<T> itemsInFirstSection = firstSection.Take(indexOfSecondSectionHeader - indexOfFirstSectionHeader);
            var result = new List<IEnumerable<T>> { itemsInFirstSection };
            result.AddRange(list.Skip(indexOfSecondSectionHeader).Sectionize(sectionHeaderIdentifier));
            return result;
        }
    }
}
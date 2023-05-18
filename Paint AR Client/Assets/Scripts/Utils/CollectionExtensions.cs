using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ArPaint.Utils
{
    public static class CollectionExtensions
    {
        public static void Update<T>(this ObservableCollection<T> collection, T item)
        {
            var index = collection.IndexOf(item);
            if (index != -1) collection[index] = item;
        }

        public static int RemoveAll<T>(
            this ObservableCollection<T> coll, Func<T, bool> condition)
        {
            var itemsToRemove = coll.Where(condition).ToList();

            foreach (var itemToRemove in itemsToRemove) coll.Remove(itemToRemove);

            return itemsToRemove.Count;
        }
    }
}
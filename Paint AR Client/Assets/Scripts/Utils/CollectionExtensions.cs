using System.Collections.ObjectModel;

namespace Utils
{
    public static class CollectionExtensions
    {
        public static void Update<T>(this ObservableCollection<T> collection, T item)
        {
            var index = collection.IndexOf(item);
            if (index != -1)
            {
                collection[index] = item;
            }
        } 
    }
}
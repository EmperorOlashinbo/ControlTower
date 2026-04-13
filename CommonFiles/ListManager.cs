using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlTower.CommonFiles
{
    /// <summary>
    /// Simple, efficient list manager that implements IListManager{T}.
    /// </summary>
    /// <typeparam name="T">Item type.</typeparam>
    public class ListManager<T> : IListManager<T>
    {
        /// <summary>
        /// Internal list to store items. This is the core data structure for managing the collection of items in the ListManager class.
        /// </summary>
        private readonly List<T> items = new List<T>();
        /// <summary>
        /// Gets the number of items currently in the collection.
        /// </summary>
        public int Count => items.Count;
        /// <summary>
        /// Indexer to access items by their zero-based index. This allows for retrieving items from the ListManager using array-like syntax, 
        /// such as listManager[0] to get the first item. If an invalid index is provided, it will throw an exception, ensuring that only valid access is allowed.
        /// </summary>
        /// <param name="index">The zero-based index of the item to retrieve.</param>
        /// <returns>The item at the specified index.</returns>
        public T this[int index] => items[index];
        /// <summary>
        /// Adds an item to the end of the collection.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Add(T item)
        {
            items.Add(item);
        }
        /// <summary>
        /// Removes the item at the specified index from the collection. 
        /// Returns true if the item was successfully removed; otherwise, false if the index is invalid.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <returns>True if the item was successfully removed; otherwise, false.</returns>
        public bool RemoveAt(int index)
        {
            if (index < 0 || index >= items.Count)
                return false;

            items.RemoveAt(index);
            return true;
        }
        /// <summary>
        /// Removes all items from the collection, resetting it to an empty state.
        /// </summary>
        public void Clear()
        {
            items.Clear();
        }
        /// <summary>
        /// Returns an enumerator that iterates through the collection of items. 
        /// This allows for enumeration of the items in the ListManager using a foreach loop or other LINQ queries.
        /// </summary>
        /// <returns>An enumerator for the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }
        /// <summary>
        /// Returns an enumerator that iterates through the collection of items.
        /// </summary>
        /// <returns>An enumerator for the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

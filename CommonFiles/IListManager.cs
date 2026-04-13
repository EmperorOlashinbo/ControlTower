using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;


namespace ControlTower.CommonFiles
{
    /// <summary>
    /// Lightweight generic list manager interface used by the ControlTower project.
    /// Provides basic collection operations and enumeration.
    /// </summary>
    /// <typeparam name="T">Item type.</typeparam>
    public interface IListManager<T> : IEnumerable<T>
    {
        /// <summary>
        /// Adds an item to the collection.
        /// </summary>
        void Add(T item);

        /// <summary>
        /// Removes the item at the given index. Returns true if removed; false if index invalid.
        /// </summary>
        bool RemoveAt(int index);

        /// <summary>
        /// Clears the collection.
        /// </summary>
        void Clear();

        /// <summary>
        /// Gets the number of items.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Indexer to get item by index. Throws if index invalid.
        /// </summary>
        T this[int index] { get; }
    }
}

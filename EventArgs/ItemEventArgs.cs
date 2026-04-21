using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlTower.EventArgs
{
    /// <summary>
    /// Event arguments used by <see cref="ListManager{T}"/> when items are added/removed.
    /// </summary>
    /// <typeparam name="T">Item type.</typeparam>
    public class ItemEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Gets the stored item of type T.
        /// </summary>
        public T Item { get; }
        /// <summary>
        /// Initializes a new instance of the ItemEventArgs class with the specified item.
        /// </summary>
        /// <param name="item">The item associated with the event.</param>
        public ItemEventArgs(T item)
        {
            Item = item;
        }
    }
}

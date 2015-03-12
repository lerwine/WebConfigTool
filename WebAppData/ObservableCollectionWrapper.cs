using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppData
{
    /// <summary>
    /// Represents a read-only <see cref="System.Collections.ObjectModel.ObservableCollection&lt;TBaseType&gt;"/> from an observable collection of <typeparamref name="TItemType"/> objects.
    /// </summary>
    /// <typeparam name="TItemType">The type of elements in the source collection</typeparam>
    /// <typeparam name="TBaseType">The type which the elements are to be exposed as.</typeparam>
    [Serializable]
    public class ReadOnlyObservableCollectionWrapper<TItemType, TBaseType> : IList<TBaseType>, ICollection<TBaseType>, IList, ICollection, IReadOnlyList<TBaseType>, IReadOnlyCollection<TBaseType>, IEnumerable<TBaseType>, IEnumerable, INotifyCollectionChanged
        where TItemType : TBaseType 
    {
        private ObservableCollection<TItemType> _innerCollection;

        /// <summary>
        /// Initializes a new instance of a read-only collection that serves as a wrapper around an observable collection, exposing the elements as a base type.
        /// </summary>
        /// <param name="list">Items to be represented by this collection.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="list"/> is null.</exception>
        public ReadOnlyObservableCollectionWrapper(ObservableCollection<TItemType> list)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            this._innerCollection = list;
            this._innerCollection.CollectionChanged += innerCollection_CollectionChanged;
        }

        ~ReadOnlyObservableCollectionWrapper()
        {
            this._innerCollection.CollectionChanged -= innerCollection_CollectionChanged;
        }

        void innerCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (this.CollectionChanged != null)
                this.CollectionChanged(this, e);
        }

        #region IList<TBaseType>

        /// <summary>
        /// Searches for the specified object and returns the zero-based index of the first occurrence within the entire collection.
        /// </summary>
        /// <param name="item">The object to locate in the colleciton. The value can be null for reference types.</param>
        /// <returns>The zero-based index of the first occurrence of item within the entire collection, if found; otherwise, -1.</returns>
        public int IndexOf(TBaseType item)
        {
            if (item == null || item is TItemType)
                return this._innerCollection.IndexOf((TItemType)item);

            return -1;
        }

        void IList<TBaseType>.Insert(int index, TBaseType item)
        {
            throw new NotSupportedException();
        }

        void IList<TBaseType>.RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Gets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get.</param>
        /// <returns>The element at the specified index.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">index is less than zero.-or-index is equal to or greater than collection's count.</exception>
        public TBaseType this[int index]
        {
            get
            {
                return this._innerCollection[index];
            }
        }

        TBaseType IList<TBaseType>.this[int index]
        {
            set
            {
                throw new NotSupportedException();
            }
        }

        #endregion

        #region ICollection<TBaseType>

        void ICollection<TBaseType>.Add(TBaseType item)
        {
            throw new NotSupportedException();
        }

        void ICollection<TBaseType>.Clear()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Determines whether an element is in the collection.
        /// </summary>
        /// <param name="item">The object to locate in the collection. The value can be null for reference types.</param>
        /// <returns>true if value is found in the collection; otherwise, false.</returns>
        public bool Contains(TBaseType item)
        {
            if (item == null || item is TItemType)
                return this._innerCollection.Contains((TItemType)item);

            return false;
        }

        /// <summary>
        /// Copies the entire collection to a compatible one-dimensional array, starting at the specified index of the target array.
        /// </summary>
        /// <param name="array">The one-dimensional array that is the destination of the elements copied from collection. The array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <exception cref="System.ArgumentNullException">array is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">index is less than zero.</exception>
        /// <exception cref="System.ArgumentException">The number of elements in the source collection is greater than the available space from index to the end of the destination array.</exception>
        public void CopyTo(TBaseType[] array, int arrayIndex)
        {
            this._innerCollection.Cast<TBaseType>().ToArray().CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of elements contained in the collection instance.
        /// </summary>
        public int Count
        {
            get { return this._innerCollection.Count; }
        }

        /// <summary>
        /// Gets a value indicating whether the collection is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get { return true; }
        }

        bool ICollection<TBaseType>.Remove(TBaseType item)
        {
            throw new NotSupportedException();
        }

        #endregion

        #region IEnumerable<TBaseType>

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator for the collection.</returns>
        public IEnumerator<TBaseType> GetEnumerator()
        {
            return this._innerCollection.Cast<TBaseType>().GetEnumerator();
        }

        #endregion

        #region IEnumerator

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._innerCollection.Cast<TBaseType>().GetEnumerator();
        }

        #endregion

        #region ICollection

        void ICollection.CopyTo(Array array, int index)
        {
            this._innerCollection.ToArray().CopyTo(array, index);
        }

        /// <summary>
        /// Gets a value indicating whether access to the collection is synchronized (thread safe).
        /// </summary>
        public bool IsSynchronized
        {
            get { return false; }
        }

        private object _syncRoot = new object();

        /// <summary>
        /// Gets an object that can be used to synchronize access to the collection.
        /// </summary>
        public object SyncRoot { get { return this._syncRoot; } }

        #endregion

        #region IList

        int IList.Add(object value)
        {
            throw new NotSupportedException();
        }

        bool IList.Contains(object value)
        {
            if (value == null || value is TItemType)
                return this._innerCollection.Contains((TItemType)value);

            return false;
        }

        int IList.IndexOf(object value)
        {
            if (value == null || value is TItemType)
                return this._innerCollection.IndexOf((TItemType)value);

            return -1;
        }

        void IList.Insert(int index, object value)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Gets a value indicating whether the collection has a fixed size.
        /// </summary>
        public bool IsFixedSize
        {
            get { return false; }
        }
        
        void IList.Remove(object value)
        {
            throw new NotSupportedException();
        }

        object IList.this[int index]
        {
            get
            {
                return this._innerCollection[index];
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        #endregion

        /// <summary>
        /// Occurs when an item is added, removed, changed, moved, or the entire list is refreshed.
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}

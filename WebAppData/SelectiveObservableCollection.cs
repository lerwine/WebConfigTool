using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppData
{
    public class SelectiveObservableCollection<T> : ObservableCollection<SelectiveObservableCollection<T>.SelectableItem<T>>, IList<T>, ICollection<T>
    {
        public class SelectableItem<T>
        {
            public static IEnumerable<SelectableItem<T>> FromCollection(IEnumerable<T> collection, bool selectAll)
            {
                if (collection == null)
                    throw new ArgumentNullException("collection");

                return collection.Select(i => new SelectableItem<T>(i, selectAll));
            }

            public static IEnumerable<SelectableItem<T>> FromCollection(IEnumerable<T> selectedItems, params int[] selectedIndexes)
            {
                if (selectedItems == null)
                    throw new ArgumentNullException("");

                if (selectedIndexes == null || selectedIndexes.Length == 0)
                    return SelectableItem<T>.FromCollection(selectedItems, false);

                int index = 0;
                return selectedItems.Select(v =>
                {
                    SelectableItem<T> item = new SelectableItem<T>(v, selectedIndexes.Any(i => i == index));
                    index++;
                    return item;
                });
            }

            public bool IsSelected { get; set; }

            public T Value { get; set; }

            public SelectableItem(T value, bool isSelected)
            {
                this.Value = value;
                this.IsSelected = isSelected;
            }
        }

        /// <summary>
        /// Initializes a new selective observable collection
        /// </summary>
        public SelectiveObservableCollection() : base() { }

        /// <summary>
        /// Initializes a new selective observable collection that contains elements copied from the specified list.
        /// </summary>
        /// <param name="list">The list from which the elements are copied.</param>
        /// <exception cref="System.ArgumentNullException">The list parameter cannot be null.</exception>
        public SelectiveObservableCollection(List<T> list)
            : base(SelectiveObservableCollection<T>.SelectableItem<T>.FromCollection(list))
        {
            this.Initialize();
        }

        /// <summary>
        /// Initializes a new selective observable collection that contains elements copied from the specified collection.
        /// </summary>
        /// <param name="collection">The collection from which the elements are copied.</param>
        /// <exception cref="System.ArgumentNullException">The collection parameter cannot be null.</exception>
        public SelectiveObservableCollection(IEnumerable<T> collection)
            : base(SelectiveObservableCollection<T>.SelectableItem<T>.FromCollection(collection))
        {
            this.Initialize();
        }

        private ObservableCollection<T> _innerSelectedItems = new ObservableCollection<T>();
        private ObservableCollection<T> _innerUnselectedItems = new ObservableCollection<T>();
        private ReadOnlyObservableCollection<T> _selectedItems;
        private ReadOnlyObservableCollection<T> _unselectedItems;

        public ReadOnlyObservableCollection<T> SelectedItems { get { return this._selectedItems; } }
        public ReadOnlyObservableCollection<T> UnselectedItems { get { return this._unselectedItems; } }

        private void Initialize()
        {
            foreach (SelectableItem<T> item in base.Items)
            {
                if (item.IsSelected)
                    this._innerSelectedItems.Add(item.Value);
                else
                    this._innerUnselectedItems.Add(item.Value);
            }

            this._selectedItems = new ReadOnlyObservableCollection<T>(this._innerSelectedItems);
            this._unselectedItems = new ReadOnlyObservableCollection<T>(this._innerUnselectedItems);
        }

        public int IndexOf(T item)
        {
            return base.Items.Select(i => i.Value).ToList().IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            this.Insert(index, item, false);
        }

        public void Insert(int index, T item, bool isSelected)
        {
            base.Items.Insert(index, new SelectableItem<T>(item, isSelected));

            int altIndex = base.Items.Take(index).Count(i => i.IsSelected == isSelected);
            if (isSelected)
                this._innerSelectedItems.Insert(altIndex, item);
            else
                this._innerUnselectedItems.Insert(altIndex, item);
        }

        public new T this[int index]
        {
            get
            {
                return base[index].Value;
            }
            set
            {
                int altIndex = base.Items.Take(index).Count(i => i.IsSelected == base[index].IsSelected);
                if (base[index].IsSelected)
                    this._innerSelectedItems[altIndex] = value;
                else
                    this._innerUnselectedItems[altIndex] = value;
            }
        }

        public void Add(T item)
        {
            this.Add(item, false);
        }

        public void Add(T item, bool isSelected)
        {
            base.Add(new SelectableItem<T>(item, isSelected));
            if (isSelected)
                this._innerSelectedItems.Add(item);
            else
                this._innerSelectedItems.Add(item);
        }

        public bool Contains(T item)
        {
            return base.Items.Select(i => i.Value).ToList().Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            base.Items.Select(i => i.Value).ToArray().CopyTo(array, arrayIndex);
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            int index = this.IndexOf(item);
            if (index < 0)
                return false;

            SelectableItem<T> deletedItem = base[index];
            int altIndex = base.Items.Take(index).Count(i => i.IsSelected == deletedItem.IsSelected);
            base.RemoveAt(index);
            if (deletedItem.IsSelected)
                this._innerSelectedItems.RemoveAt(altIndex);
            else
                this._innerUnselectedItems.RemoveAt(altIndex);
            return true;
        }

        public new IEnumerator<T> GetEnumerator()
        {
            return base.Items.Select(i => i.Value).GetEnumerator();
        }
    }
}

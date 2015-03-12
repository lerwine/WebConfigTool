using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace WebAppData
{
    public interface IGenericAccessObservableCollection<TBaseType> : IList, ICollection, IEnumerable, INotifyCollectionChanged, INotifyPropertyChanged
    {
        ReadOnlyObservableCollection<TBaseType> ItemCollection { get; }
    }

    public interface IGenericAccessObservableCollection<TItem, TBaseType> : IGenericAccessObservableCollection<TBaseType>, IList<TItem>, ICollection<TItem>, IReadOnlyList<TItem>, IReadOnlyCollection<TItem>, IEnumerable<TItem>
        where TItem : TBaseType
    {
        ReadOnlyObservableCollection<TBaseType> ItemCollection { get; }
    }
}

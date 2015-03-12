using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using WebAppData;

namespace UnitTestProject1
{
    [TestClass]
    public class GenericAccessObservableCollectionUnitTest
    {
        public interface IExampleInterface
        {
            string Text { get; set; }
            bool IsSelected { get; set; }
        }

        public class ExampleDerrivedClass1 : ExampleBaseClass
        {
        }

        public class ExampleDerrivedClass2 : ExampleBaseClass
        {
        }

        public class ExampleBaseClass : IExampleInterface
        {
            public string Text { get; set; }
            public bool IsSelected { get; set; }
        }

        [TestMethod]
        public void ConstructorTestMethod1()
        {
            GenericAccessObservableCollection<ExampleBaseClass, IExampleInterface> target = new GenericAccessObservableCollection<ExampleBaseClass,IExampleInterface>();
            Assert.IsNotNull(target.ItemCollection);
            int expected = 0;
            int actual = target.Count;
            Assert.AreEqual(expected, actual);
            actual = target.ItemCollection.Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConstructorTestMethod2()
        {
            List<ExampleBaseClass> list = null;
            GenericAccessObservableCollection<ExampleBaseClass, IExampleInterface> target;
            try
            {
                target = new GenericAccessObservableCollection<ExampleBaseClass, IExampleInterface>(list);
            }
            catch (ArgumentNullException) { }

            list = new List<ExampleBaseClass>();
            target = new GenericAccessObservableCollection<ExampleBaseClass, IExampleInterface>(list);
            Assert.IsNotNull(target.ItemCollection);
            int expected = 0;
            int actual = target.Count;
            Assert.AreEqual(expected, actual);
            actual = target.ItemCollection.Count;
            Assert.AreEqual(expected, actual);

            list = new List<ExampleBaseClass>(new ExampleBaseClass[] { null });
            target = new GenericAccessObservableCollection<ExampleBaseClass, IExampleInterface>(list);
            Assert.IsNotNull(target.ItemCollection);
            expected = 1;
            actual = target.Count;
            Assert.AreEqual(expected, actual);
            Assert.IsNull(target[0]);
            actual = target.ItemCollection.Count;
            Assert.AreEqual(expected, actual);
            Assert.IsNull(target.ItemCollection[0]);
            
            ExampleBaseClass[] targetArray = new ExampleBaseClass[]
            {
                new ExampleBaseClass { Text = "Eins", IsSelected = true },
                new ExampleBaseClass { Text = "Dos", IsSelected = false },
                new ExampleDerrivedClass1 { Text = "Three", IsSelected = false },
                null,
                new ExampleDerrivedClass2 { Text = "Funf", IsSelected = true },
                new ExampleBaseClass { Text = "Cinco", IsSelected = true },
                new ExampleDerrivedClass1 { Text = "Six", IsSelected = false },
                new ExampleDerrivedClass2 { Text = null, IsSelected = false }
            };
            Tuple<string, Type>[] expectedValues = targetArray.Select(a => (a == null) ? new Tuple<string, Type>(null, null)  : new Tuple<string, Type>(a.Text, a.GetType())).ToArray();

            list = new List<ExampleBaseClass>(targetArray);
            target = new GenericAccessObservableCollection<ExampleBaseClass, IExampleInterface>(list);
            expected = expectedValues.Length;
            actual = target.Count;
            Assert.AreEqual(expected, actual);
            actual = target.ItemCollection.Count;
            Assert.AreEqual(expected, actual);

            for (int i = 0; i < expectedValues.Length; i++)
            {
                if (expectedValues[i].Item2 == null)
                {
                    Assert.IsNull(target[i]);
                    Assert.IsNull(target.ItemCollection[i]);
                }
                else
                {
                    Assert.IsNotNull(target[i]);
                    Assert.IsNotNull(target.ItemCollection[i]);
                    Assert.AreSame(target[i], target.ItemCollection[i]);
                    Assert.AreEqual(expectedValues[i].Item2, target[i].GetType());
                    if (expectedValues[i].Item1 == null)
                        Assert.IsNull(target[i].Text);
                    else
                    {
                        Assert.IsNotNull(target[i].Text);
                        Assert.AreEqual(expectedValues[i].Item1, target[i].Text);
                    }
                }
            }
        }

        [TestMethod]
        public void ConstructorTestMethod3()
        {
            IEnumerable<ExampleBaseClass> collection = null;
            GenericAccessObservableCollection<ExampleBaseClass, IExampleInterface> target;
            try
            {
                target = new GenericAccessObservableCollection<ExampleBaseClass, IExampleInterface>(collection);
            }
            catch (ArgumentNullException) { }

            collection = new ExampleBaseClass[0];
            target = new GenericAccessObservableCollection<ExampleBaseClass, IExampleInterface>(collection);
            Assert.IsNotNull(target.ItemCollection);
            int actual = target.Count;
            int expected = 0;
            Assert.AreEqual(expected, actual);
            actual = target.ItemCollection.Count;
            Assert.AreEqual(expected, actual);

            collection = new ExampleBaseClass[] { null };
            target = new GenericAccessObservableCollection<ExampleBaseClass, IExampleInterface>(collection);
            Assert.IsNotNull(target.ItemCollection);
            expected = 1;
            actual = target.Count;
            Assert.AreEqual(expected, actual);
            Assert.IsNull(target[0]);
            actual = target.ItemCollection.Count;
            Assert.AreEqual(expected, actual);
            Assert.IsNull(target.ItemCollection[0]);

            collection = new ExampleBaseClass[]
            {
                new ExampleBaseClass { Text = "Eins", IsSelected = true },
                new ExampleBaseClass { Text = "Dos", IsSelected = false },
                new ExampleDerrivedClass1 { Text = "Three", IsSelected = false },
                null,
                new ExampleDerrivedClass2 { Text = "Funf", IsSelected = true },
                new ExampleBaseClass { Text = "Cinco", IsSelected = true },
                new ExampleDerrivedClass1 { Text = "Six", IsSelected = false },
                new ExampleDerrivedClass2 { Text = null, IsSelected = false }
            };
            Tuple<string, Type>[] expectedValues = collection.Select(a => (a == null) ? new Tuple<string, Type>(null, null) : new Tuple<string, Type>(a.Text, a.GetType())).ToArray();

            target = new GenericAccessObservableCollection<ExampleBaseClass, IExampleInterface>(collection);
            expected = expectedValues.Length;
            actual = target.Count;
            Assert.AreEqual(expected, actual);
            actual = target.ItemCollection.Count;
            Assert.AreEqual(expected, actual);

            for (int i = 0; i < expectedValues.Length; i++)
            {
                if (expectedValues[i].Item2 == null)
                {
                    Assert.IsNull(target[i]);
                    Assert.IsNull(target.ItemCollection[i]);
                }
                else
                {
                    Assert.IsNotNull(target[i]);
                    Assert.IsNotNull(target.ItemCollection[i]);
                    Assert.AreSame(target[i], target.ItemCollection[i]);
                    Assert.AreEqual(expectedValues[i].Item2, target[i].GetType());
                    if (expectedValues[i].Item1 == null)
                        Assert.IsNull(target[i].Text);
                    else
                    {
                        Assert.IsNotNull(target[i].Text);
                        Assert.AreEqual(expectedValues[i].Item1, target[i].Text);
                    }
                }
            }
        }

        [TestMethod]
        public void AddTestMethod()
        {
            GenericAccessObservableCollection<ExampleBaseClass, IExampleInterface> target = new GenericAccessObservableCollection<ExampleBaseClass, IExampleInterface>();
            ExampleBaseClass[] items = new ExampleBaseClass[]
            {
                new ExampleDerrivedClass1 { Text = "Eins", IsSelected = true },
                new ExampleDerrivedClass2 { Text = "Two", IsSelected = true },
                null,
                new ExampleDerrivedClass2 { Text = "Tres", IsSelected = true },
                new ExampleBaseClass { Text = "Funf", IsSelected = true },
                new ExampleDerrivedClass1 { Text = "Five", IsSelected = true },
                new ExampleBaseClass { Text = "Seis", IsSelected = true },
                new ExampleDerrivedClass2 { Text = null, IsSelected = true }
            };
            Tuple<Type, string>[] values = items.Select(i => (i == null) ? new Tuple<Type, string>(null, null) : new Tuple<Type, string>(i.GetType(), i.Text)).ToArray();
            for (int i = 0; i < values.Length; i++)
            {
                target.Add(items[i]);
                int expectedCount = i + 1;
                int actualCount = target.Count;
                Assert.AreEqual(expectedCount, actualCount);
                actualCount = target.ItemCollection.Count;
                Assert.AreEqual(expectedCount, actualCount);
                for (int n = 0; n <= i; n++)
                {
                    if (values[n].Item1 == null)
                    {
                        Assert.IsNull(target[n]);
                        Assert.IsNull(target.ItemCollection[n]);
                    }
                    else
                    {
                        Assert.IsNotNull(target[n]);
                        Assert.IsNotNull(target.ItemCollection[n]);
                        Assert.AreSame(items[n], target[n]);
                        Assert.AreSame(items[n], target.ItemCollection[n]);
                        if (values[n].Item2 == null)
                            Assert.IsNull(target[n].Text);
                        else
                        {
                            Assert.IsNotNull(target[n].Text);
                            Assert.AreEqual(values[n].Item2, target[n].Text);
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void RemoveTestMethod()
        {
            ExampleBaseClass[] items = new ExampleBaseClass[]
            {
                new ExampleDerrivedClass1 { Text = "Eins", IsSelected = true },
                new ExampleDerrivedClass2 { Text = "Two", IsSelected = true },
                null,
                new ExampleDerrivedClass2 { Text = "Tres", IsSelected = true },
                new ExampleBaseClass { Text = "Funf", IsSelected = true },
                new ExampleDerrivedClass1 { Text = "Five", IsSelected = true },
                new ExampleBaseClass { Text = "Seis", IsSelected = true },
                new ExampleDerrivedClass2 { Text = null, IsSelected = true }
            };

            Tuple<Type, string>[] values = items.Select(i => (i == null) ? new Tuple<Type, string>(null, null) : new Tuple<Type, string>(i.GetType(), i.Text)).ToArray();

            GenericAccessObservableCollection<ExampleBaseClass, IExampleInterface> target = new GenericAccessObservableCollection<ExampleBaseClass, IExampleInterface>(items);

            int[] indexes = new int[] { 7, 5, 2, 3, 1, 2, 0, 0 };
            ExampleBaseClass[] expectedItems = items;
            for (int i = 0; i < values.Length; i++)
            {
                expectedItems = expectedItems.Take(indexes[i]).Concat(expectedItems.Skip(indexes[i] + 1)).ToArray();
                values = values.Take(indexes[i]).Concat(values.Skip(indexes[i] + 1)).ToArray();
                target.Remove(target[indexes[i]]);
                int expectedCount = expectedItems.Length;
                int actualCount = target.Count;
                Assert.AreEqual(expectedCount, actualCount);
                actualCount = target.ItemCollection.Count;
                Assert.AreEqual(expectedCount, actualCount);
                for (int n = 0; n < expectedItems.Length; n++)
                {
                    if (values[n].Item1 == null)
                    {
                        Assert.IsNull(target[n]);
                        Assert.IsNull(target.ItemCollection[n]);
                    }
                    else
                    {
                        Assert.IsNotNull(target[n]);
                        Assert.IsNotNull(target.ItemCollection[n]);
                        Assert.AreSame(expectedItems[n], target[n]);
                        Assert.AreSame(expectedItems[n], target.ItemCollection[n]);
                        if (values[n].Item2 == null)
                            Assert.IsNull(target[n].Text);
                        else
                        {
                            Assert.IsNotNull(target[n].Text);
                            Assert.AreEqual(values[n].Item2, target[n].Text);
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void RemoveAtTestMethod()
        {
            ExampleBaseClass[] items = new ExampleBaseClass[]
            {
                new ExampleDerrivedClass1 { Text = "Eins", IsSelected = true },
                new ExampleDerrivedClass2 { Text = "Two", IsSelected = true },
                null,
                new ExampleDerrivedClass2 { Text = "Tres", IsSelected = true },
                new ExampleBaseClass { Text = "Funf", IsSelected = true },
                new ExampleDerrivedClass1 { Text = "Five", IsSelected = true },
                new ExampleBaseClass { Text = "Seis", IsSelected = true },
                new ExampleDerrivedClass2 { Text = null, IsSelected = true }
            };

            Tuple<Type, string>[] values = items.Select(i => (i == null) ? new Tuple<Type, string>(null, null) : new Tuple<Type, string>(i.GetType(), i.Text)).ToArray();

            GenericAccessObservableCollection<ExampleBaseClass, IExampleInterface> target = new GenericAccessObservableCollection<ExampleBaseClass, IExampleInterface>(items);

            int[] indexes = new int[] { 7, 5, 2, 3, 1, 2, 0, 0 };
            ExampleBaseClass[] expectedItems = items;
            for (int i = 0; i < values.Length; i++)
            {
                expectedItems = expectedItems.Take(indexes[i]).Concat(expectedItems.Skip(indexes[i] + 1)).ToArray();
                values = values.Take(indexes[i]).Concat(values.Skip(indexes[i] + 1)).ToArray();
                target.RemoveAt(indexes[i]);
                int expectedCount = expectedItems.Length;
                int actualCount = target.Count;
                Assert.AreEqual(expectedCount, actualCount);
                actualCount = target.ItemCollection.Count;
                Assert.AreEqual(expectedCount, actualCount);
                for (int n = 0; n < expectedItems.Length; n++)
                {
                    if (values[n].Item1 == null)
                    {
                        Assert.IsNull(target[n]);
                        Assert.IsNull(target.ItemCollection[n]);
                    }
                    else
                    {
                        Assert.IsNotNull(target[n]);
                        Assert.IsNotNull(target.ItemCollection[n]);
                        Assert.AreSame(expectedItems[n], target[n]);
                        Assert.AreSame(expectedItems[n], target.ItemCollection[n]);
                        if (values[n].Item2 == null)
                            Assert.IsNull(target[n].Text);
                        else
                        {
                            Assert.IsNotNull(target[n].Text);
                            Assert.AreEqual(values[n].Item2, target[n].Text);
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void ClearTestMethod()
        {
            ExampleBaseClass[] items = new ExampleBaseClass[]
            {
                new ExampleDerrivedClass1 { Text = "Eins", IsSelected = true },
                new ExampleDerrivedClass2 { Text = "Two", IsSelected = true },
                null,
                new ExampleDerrivedClass2 { Text = "Tres", IsSelected = true },
                new ExampleBaseClass { Text = "Funf", IsSelected = true },
                new ExampleDerrivedClass1 { Text = "Five", IsSelected = true },
                new ExampleBaseClass { Text = "Seis", IsSelected = true },
                new ExampleDerrivedClass2 { Text = null, IsSelected = true }
            };

            Tuple<Type, string>[] values = items.Select(i => (i == null) ? new Tuple<Type, string>(null, null) : new Tuple<Type, string>(i.GetType(), i.Text)).ToArray();

            GenericAccessObservableCollection<ExampleBaseClass, IExampleInterface> target = new GenericAccessObservableCollection<ExampleBaseClass, IExampleInterface>(items);
            target.Clear();
            Assert.IsNotNull(target.ItemCollection);
            int expected = 0;
            int actual = target.Count;
            Assert.AreEqual(expected, actual);
            actual = target.ItemCollection.Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InsertTestMethod()
        {
            GenericAccessObservableCollection<ExampleBaseClass, IExampleInterface> target = new GenericAccessObservableCollection<ExampleBaseClass, IExampleInterface>();
            ExampleBaseClass[] items = new ExampleBaseClass[]
            {
                new ExampleDerrivedClass1 { Text = "Eins", IsSelected = true },
                new ExampleDerrivedClass2 { Text = "Two", IsSelected = true },
                null,
                new ExampleDerrivedClass2 { Text = "Tres", IsSelected = true },
                new ExampleBaseClass { Text = "Funf", IsSelected = true },
                new ExampleDerrivedClass1 { Text = "Five", IsSelected = true },
                new ExampleBaseClass { Text = "Seis", IsSelected = true },
                new ExampleDerrivedClass2 { Text = null, IsSelected = true }
            };
            Tuple<Type, string>[] values = items.Select(i => (i == null) ? new Tuple<Type, string>(null, null) : new Tuple<Type, string>(i.GetType(), i.Text)).ToArray();
            Tuple<Type, string>[] ev = new Tuple<Type, string>[0];
            ExampleBaseClass[] ei = new ExampleBaseClass[0];
            int[] indexes = new int[] { 0, 1, 0, 1, 3, 2, 5, 4 };
            for (int i = 0; i < values.Length; i++)
            {
                ev = ev.Take(indexes[i]).Concat(values.Skip(i).Take(1)).Concat(ev.Skip(indexes[i])).ToArray();
                ei = ei.Take(indexes[i]).Concat(items.Skip(i).Take(1)).Concat(ei.Skip(indexes[i])).ToArray();
                target.Insert(indexes[i], items[i]);
                int expectedCount = i + 1;
                int actualCount = target.Count;
                Assert.AreEqual(expectedCount, actualCount);
                actualCount = target.ItemCollection.Count;
                Assert.AreEqual(expectedCount, actualCount);
                for (int n = 0; n< ev.Length; n++)
                {
                    if (ev[n].Item1 == null)
                    {
                        Assert.IsNull(target[n]);
                        Assert.IsNull(target.ItemCollection[n]);
                    }
                    else
                    {
                        Assert.IsNotNull(target[n]);
                        Assert.IsNotNull(target.ItemCollection[n]);
                        Assert.AreSame(ei[n], target[n]);
                        Assert.AreSame(ei[n], target.ItemCollection[n]);
                        if (ev[n].Item2 == null)
                            Assert.IsNull(target[n].Text);
                        else
                        {
                            Assert.IsNotNull(target[n].Text);
                            Assert.AreEqual(ev[n].Item2, target[n].Text);
                        }
                    }
                }
            }
        }
    }
}

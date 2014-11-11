using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace log4netParser {
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>Based on an example from http://msdn.microsoft.com/en-us/library/aa480736.aspx</remarks>
    /// <example></example>
    public class SortableSearchableList<T> : BindingList<T> {
        /* *******************************************************************
         *  Properties
         * *******************************************************************/
        #region protected override PropertyDescriptor SortPropertyCore
        /// <summary>
        /// Gets the SortPropertyCore of the SortableSearchableList
        /// </summary>
        /// <value></value>
        protected override PropertyDescriptor SortPropertyCore {
            get { return _sortPropertyValue; }
        }
        PropertyDescriptor _sortPropertyValue;
        #endregion
        #region protected override ListSortDirection SortDirectionCore
        /// <summary>
        /// Gets the SortDirectionCore of the SortableSearchableList
        /// </summary>
        /// <value></value>
        protected override ListSortDirection SortDirectionCore {
            get { return _sortDirectionValue; }
        }
        ListSortDirection _sortDirectionValue;
        #endregion
        #region protected override bool SupportsSortingCore
        /// <summary>
        /// Gets the SupportsSortingCore of the SortableSearchableList
        /// </summary>
        /// <value></value>
        protected override bool SupportsSortingCore {
            get { return true; }
        }
        #endregion
        #region protected override bool SupportsSearchingCore
        /// <summary>
        /// Gets the SupportsSearchingCore of the SortableSearchableList
        /// </summary>
        /// <value></value>
        protected override bool SupportsSearchingCore {
            get { return true; }
        }
        #endregion
        #region protected override bool IsSortedCore
        /// <summary>
        /// Gets the IsSortedCore of the SortableSearchableList
        /// </summary>
        /// <value></value>
        protected override bool IsSortedCore {
            get { return _isSortedValue; }
        }
        bool _isSortedValue;
        #endregion
        private ArrayList _unsortedItems;
        private List<T> _hiddenItems;
        /* *******************************************************************
         *  Methods
         * *******************************************************************/
        #region protected override int FindCore(PropertyDescriptor prop, object key)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        protected override int FindCore(PropertyDescriptor prop, object key) {
            // Get the property info for the specified property.
            PropertyInfo propInfo = typeof(T).GetProperty(prop.Name);

            if (key != null) {
                // Loop through the items to see if the key
                // value matches the property value.
                for (int i = 0; i < Count; ++i) {
                    var item = Items[i];
                    if (propInfo.GetValue(item, null).Equals(key))
                        return i;
                }
            }
            return -1;
        }
        #endregion
        #region public int Find(string property, object key)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public int Find(string property, object key) {
            // Check the properties for a property with the specified name.
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            PropertyDescriptor prop = properties.Find(property, true);

            // If there is not a match, return -1 otherwise pass search to
            // FindCore method.
            if (prop == null)
                return -1;
            return FindCore(prop, key);
        }
        #endregion
        #region public SortableSearchableList(IEnumerable<T> logEntires)
        /// <summary>
        /// Initializes a new instance of the <b>SortableSearchableList&lt;T&gt;</b> class.
        /// </summary>
        /// <param name="logEntires"></param>
        public SortableSearchableList(IEnumerable<T> logEntires) {
            if (logEntires != null) {
                foreach (var logEntry in logEntires) {
                    Add(logEntry);
                }
            }
        }
        #endregion
        #region protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="direction"></param>
        /// <exception cref="NotSupportedException">If cannot sort by .</exception>
        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction) {
            var sortedList = new List<SortIndex<T>>();

            // Check to see if the property type we are sorting by implements
            // the IComparable interface.
            Type interfaceType = prop.PropertyType.GetInterface("IComparable");

            if (interfaceType != null) {
                // If so, set the SortPropertyValue and SortDirectionValue.
                _sortPropertyValue = prop;
                _sortDirectionValue = direction;

                _unsortedItems = new ArrayList(Count);

                // Loop through each item, adding it the the sortedItems ArrayList.
                foreach (var item in Items) {
                    sortedList.Add(new SortIndex<T>((IComparable)prop.GetValue(item), item));
                    _unsortedItems.Add(item);
                }
                // Call Sort on the ArrayList.
                sortedList.Sort();

                // Check the sort direction and then copy the sorted items
                // back into the list.
                if (direction == ListSortDirection.Descending)
                    sortedList.Reverse();

                for (var i = 0; i < Count; i++) {
                    var position = IndexOf(sortedList[i].Item);
                    if (position == i) continue;
                    var temp = this[i];
                    this[i] = this[position];
                    this[position] = temp;
                }

                _isSortedValue = true;

                // Raise the ListChanged event so bound controls refresh their
                // values.
                OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            } else
                // If the property type does not implement IComparable, let the user
                // know.
                throw new NotSupportedException("Cannot sort by " + prop.Name +
                                                ". This" + prop.PropertyType +
                                                " does not implement IComparable");
        }
        #endregion
        #region protected override void RemoveSortCore()
        /// <summary>
        /// 
        /// </summary>
        protected override void RemoveSortCore() {
            // Ensure the list has been sorted.
            if (_unsortedItems != null) {
                // Loop through the unsorted items and reorder the
                // list per the unsorted list.
                for (int i = 0; i < _unsortedItems.Count; ) {
                    int position = Find("LastName",
                        _unsortedItems[i].GetType().
                            GetProperty("LastName").GetValue(_unsortedItems[i], null));
                    if (position > 0 && position != i) {
                        object temp = this[i];
                        this[i] = this[position];
                        this[position] = (T)temp;
                        i++;
                    } else if (position == i)
                        i++;
                    else
                        // If an item in the unsorted list no longer exists,
                        // delete it.
                        _unsortedItems.RemoveAt(i);
                }
                _isSortedValue = false;
                OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
        }
        #endregion
        #region public void RemoveSort()
        /// <summary>
        /// 
        /// </summary>
        public void RemoveSort() {
            RemoveSortCore();
        }
        #endregion
        #region public override void EndNew(int itemIndex)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemIndex"></param>
        public override void EndNew(int itemIndex) {
            // Check to see if the item is added to the end of the list,
            // and if so, re-sort the list.
            if (_sortPropertyValue != null && itemIndex == Count - 1)
                ApplySortCore(_sortPropertyValue, _sortDirectionValue);

            base.EndNew(itemIndex);
        }
        #endregion

        public int Hide(Func<T, bool> predicate) {
            if (_hiddenItems == null) _hiddenItems = new List<T>();
            this.RaiseListChangedEvents = false;
            var removedItems = 0;
            for (var i = Count - 1; i >= 0; i--) {
                try {
                    if (predicate(this[i])) {
                        _hiddenItems.Add(this[i]);
                        RemoveAt(i);
                        removedItems++;
                    }
                } catch (Exception e) {
                    Debug.WriteLine("Error in predicate function " + e.Message);
                }
            }
            RaiseListChangedEvents = true;
            if (removedItems > 0) {
                OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
            return removedItems;    
        }

        public void ShowHiddenItems() {
            if (_hiddenItems == null || _hiddenItems.Count == 0) {
                return;
            }
            foreach (var hiddenItem in _hiddenItems) {
                Add(hiddenItem);
            }
            _hiddenItems = null;
        }

        /* *******************************************************************
         *  Inner classes
         * *******************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <remarks></remarks>
        /// <example></example>
        public class SortIndex<TItem> : IComparable {
            /* *******************************************************************
             *  Properties
             * *******************************************************************/
            #region public IComparable Key
            /// <summary>
            /// Get/Sets the Key of the SortIndex
            /// </summary>
            /// <value></value>
            public IComparable Key { get; set; }
            #endregion
            #region public T Item
            /// <summary>
            /// Get/Sets the Item of the SortIndex
            /// </summary>
            /// <value></value>
            public TItem Item { get; set; }
            #endregion
            /* *******************************************************************
             *  Constructors
             * *******************************************************************/
            #region public SortIndex(IComparable key, T item)
            /// <summary>
            /// Initializes a new instance of the <b>SortIndex&lt;T&gt;</b> class.
            /// </summary>
            /// <param name="key"></param>
            /// <param name="item"></param>
            public SortIndex(IComparable key, TItem item) {
                Key = key;
                Item = item;
            }
            #endregion
            /* *******************************************************************
             *  Methods
             * *******************************************************************/
            #region public int CompareTo(object obj)
            /// <summary>
            /// Compares the current instance with another object of the same type.
            /// </summary>
            /// <param name="obj">The <see cref="Object"/> to compare with this instance.
            /// </param>
            /// <returns>A 32-bit signed integer that indicates the relative order 
            /// of the objects being compared. The return value has these meanings: 
            /// <table>
            /// 		<tr><th>Value</th><th>Meaning</th></tr>
            /// 		<tr><td>Less than zero</td><td>This instance is less than <i>obj</i>.</td></tr>
            /// 		<tr><td>Zero</td><td>This instance is equal to <i>obj</i>.</td></tr>
            /// 		<tr><td>Greater than zero</td><td>This instance is greater than <i>obj</i>.</td></tr>
            /// 	</table>
            /// </returns>
            /// <exception cref="NotImplementedException"></exception>
            public int CompareTo(object obj) {
                var si = obj as SortIndex<TItem>;
                if (si == null) return 1;
                if (Key == null && si.Key == null) return 0;
                if (Key == null) return -1;
                return Key.CompareTo(si.Key);
            }
            #endregion
        }

    }

}
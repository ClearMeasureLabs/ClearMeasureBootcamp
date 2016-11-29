using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ClearMeasure.Bootcamp.Core.Model
{
    [Serializable]
    public abstract class ListItem : IComparable
    {
        private readonly string _displayName;
        private readonly int _value;

        protected ListItem()
        {
        }

        protected ListItem(int value, string displayName)
        {
            _value = value;
            _displayName = displayName;
        }

        public int Value
        {
            get { return _value; }
        }

        public string DisplayName
        {
            get { return _displayName; }
        }

        #region IComparable Members

        public virtual int CompareTo(object other)
        {
            return Value.CompareTo(((ListItem) other).Value);
        }

        #endregion

        public override string ToString()
        {
            return DisplayName;
        }

        public static IEnumerable<T> GetAll<T>() where T : ListItem, new()
        {
            Type type = typeof (T);
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            foreach (FieldInfo info in fields)
            {
                var instance = new T();
                var locatedValue = info.GetValue(instance) as T;

                if (locatedValue != null)
                {
                    yield return locatedValue;
                }
            }
        }

        public static IEnumerable<ListItem> GetAll(Type type)
        {
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            var listItems = new List<ListItem>();

            foreach (FieldInfo info in fields)
            {
                object instance = Activator.CreateInstance(type);
                listItems.Add((ListItem) info.GetValue(instance));
            }

            return listItems.ToArray();
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as ListItem;

            if (otherValue == null)
            {
                return false;
            }

            bool typeMatches = GetType().Equals(obj.GetType());
            bool valueMatches = _value.Equals(otherValue.Value);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static T FromValue<T>(int value) where T : ListItem, new()
        {
            T matchingItem = Parse<T, int>(value, "value", item => item.Value == value);
            return matchingItem;
        }

        public static T FromDisplayName<T>(string displayName) where T : ListItem, new()
        {
            T matchingItem = Parse<T, string>(displayName, "display name", item => item.DisplayName == displayName);
            return matchingItem;
        }

        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : ListItem, new()
        {
            T matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
            {
                string message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof (T));
                throw new ApplicationException(message);
            }

            return matchingItem;
        }

        public static ListItem FromValueOrDefault(Type listItemType, int listItemValue)
        {
            return GetAll(listItemType).SingleOrDefault(e => e.Value == listItemValue);
        }

        public static T FromValueOrDefault<T>(int listItemValue) where T : ListItem
        {
            return (T) GetAll(typeof (T)).SingleOrDefault(e => e.Value == listItemValue);
        }

        public static ListItem FromDisplayNameOrDefault(Type listItemType, string displayName)
        {
            return GetAll(listItemType).SingleOrDefault(e => e.DisplayName == displayName);
        }
    }
}
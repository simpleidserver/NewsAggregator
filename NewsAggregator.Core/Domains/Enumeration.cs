﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NewsAggregator.Core.Domains
{
    public class Enumeration : IComparable
    {
        public string Name { get; private set; }
        public int Id { get; private set; }

        public static IEnumerable<T> GetAll<T>() where T : Enumeration => typeof(T).GetFields(BindingFlags.Public |
                            BindingFlags.Static |
                            BindingFlags.DeclaredOnly)
                 .Select(f => f.GetValue(null))
                 .Cast<T>();

        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public static T FromId<T>(int id) where T : Enumeration
        {
            var values = GetAll<T>();
            return values.FirstOrDefault(e => e.Id == id);
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumeration
        {
            var matchingItem = Parse<T, string>(displayName, "display name", item => item.Name == displayName);
            return matchingItem;
        }

        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);
            if (matchingItem == null)
            {
                throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");
            }

            return matchingItem;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Input;
using DynamicData.Binding;

namespace Trader.Client.Infrastucture
{
    public class MenuItem : AbstractNotifyPropertyChanged, IComparable<MenuItem>, ICloneable
    {
        public MenuItem(
            string title,
            string description,
            Action action,
            IEnumerable<Link> link = null,
            object content = null)
        {
            Title = title;
            Description = description;
            Content = content;
            Category = MenuCategory.DynamicData;
            Link = link ?? Enumerable.Empty<Link>();
            Command = new Command(action);
        }

        public MenuItem(
            string title,
            string description,
            Action action,
            MenuCategory category,
            IEnumerable<Link> link = null,
            object content = null)
        {
            Title = title;
            Description = description;
            Category = category;
            Link = link ?? Enumerable.Empty<Link>();
            Command = new Command(action);
        }


        public string Title { get; }

        public ICommand Command { get; }

        private Action Action { get; }

        public IEnumerable<Link> Link { get; }

        public string Description { get; }

        public object Content { get; }

        public MenuCategory Category { get; }

        public object Clone()
        {
            return new MenuItem(
            Title,
            Description,
            Action,
            Category,
            Link,
            Command);
        }

        public int CompareTo([AllowNull] MenuItem other) => Title.Length >= other.Title.Length ? 1 : 0;
    }
}
﻿using Abp.VNext.Hello.XNetty;
using Microsoft.Extensions.Primitives;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Trader.Client.Infrastucture;
using Trader.Domain.Model;
using Trader.Domain.Services;

namespace Trader.Client.Views
{
    public class ChatViewer : ReactiveObject
    {
        private readonly Action<string> EnterPressEvent = async (text) =>
        {
            await CoreDispatcher.Dispatcher.Account.LoginAsync("abc", "123");
        };
        private ObservableCollection<ContactItem> _contacts;
        public ObservableCollection<ContactItem> Contacts
        {
            get => _contacts;
            set => this.RaiseAndSetIfChanged(ref _contacts, value);
        }

        private ObservableCollection<StringSegment> _names = new ObservableCollection<StringSegment>();
        public ObservableCollection<StringSegment> Names
        {
            get => _names;
            set => this.RaiseAndSetIfChanged(ref _names, value);
        }

        private ObservableCollection<Point3D> _points = new ObservableCollection<Point3D>(new List<Point3D> {
            Point3D.Origin,
            Point3D.Max,
            Point3D.Min
        });
        public ObservableCollection<Point3D> Points
        {
            get => _points;
            set => this.RaiseAndSetIfChanged(ref _points, value);
        }

        private string input;
        public string Input
        {
            get => input;
            set => this.RaiseAndSetIfChanged(ref input, value);
        }

        private string output;
        public string Output
        {
            get => output;
            set => this.RaiseAndSetIfChanged(ref output, value);
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => this.RaiseAndSetIfChanged(ref _searchText, value);
        }

        public ICommand EnterKeyCommand { get; set; }

        private IContactService ContactService { get; set; }
        public ChatViewer(IContactService contactService)
        {
            ContactService = contactService;
            CoreDispatcher.Dispatcher.Account.MessageEventHandler += Account_MessageEventHandler;
            EnterKeyCommand = new Command(EnterKeyAction);
            HelloCommand = new Command(async () =>
            {
                SearchText = DateTime.Now.ToString();
                List<ContactItem> items = await ContactService.GetItemsAsync("/hello/api/hello/angkor/country");
                Contacts = new ObservableCollection<ContactItem>(items);
            });

        }

        private void Account_MessageEventHandler(string json, Tuple<int, int> type)
        {
            Output = Output + "\n" + json;
        }

        public Command HelloCommand { get; set; }

        public void EnterKeyAction()
        {
            string input = Input ?? "";
            Output = Output + "\n" + input;
            EnterPressEvent(input);
            Input = "";
        }



    }
}

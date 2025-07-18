﻿using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Maui.Views;
using HelloMAUI.Models;
using HelloMAUI.ViewModels;
using HelloMAUI.Views;
using Microsoft.Maui.Layouts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace HelloMAUI.Pages
{
    public class ListPage : BaseContentPage<ListViewModel>
    {
        private readonly SearchBar _searchBar;

        public ListPage(ListViewModel listViewModel) : base(listViewModel)
        {
            

            Content = new RefreshView()
            {
                Content = new CollectionView()
                {
                    Header = new SearchBar()
                    .Placeholder("Search titles")
                    .Center()
                    .TextCenter()
                    .Behaviors(new UserStoppedTypingBehavior()
                    {
                        StoppedTypingTimeThreshold = 1000,
                        Command = new Command(() => UserStoppedTyping())
                    })
                    .TapGesture(async () =>
                    {
                        // This is just to show that we can use TapGesture on SearchBar
                        await Toast.Make("Search Bar Tapped").Show();
                    }, 2)
                    .Assign(out _searchBar),

                    Footer = new Label()
                    .Text(".NET MAUI: From Zero to Hero")
                    .Paddings(left: 8)
                    .Font(size: 32)
                    .Center()
                    .TextCenter(),

                    SelectionMode = SelectionMode.Single,


                }.ItemsSource(MauiLibraries) // where we are grabing the data from. ItemSource is an extention method that is used to pass data into the CollectionView
                .ItemTemplate(new MauiLibrariesDataTemlate())// item template is used to to predefine a single template that the CollectionView will use to display the collection of items that will bw displayed
                .Invoke(collectionView => collectionView.SelectionChanged += HandleSelectionChanged)// Invoke is used to invoke an action 
            }.Invoke(refreshView => refreshView.Refreshing += HandleRefreshing)
            .Margins(12, 24, 12, 0);
        }



        private void UserStoppedTyping()
        {
            MauiLibraries.Clear();
            var searchText = _searchBar.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(searchText))
            {
                foreach (var library in Libraries())
                {
                    MauiLibraries.Add(library);
                }
            }
            else
            {
                foreach(var library in Libraries().Where(x => x.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase)))
                {
                    MauiLibraries.Add(library);
                }
            }
        }

        private async void HandleRefreshing(object? sender, EventArgs e)
        {
            ArgumentNullException.ThrowIfNull(sender);


            _searchBar.IsEnabled = false;

            var refreshView = (RefreshView)sender;

            await Task.Delay(TimeSpan.FromSeconds(2));

            MauiLibraries.Add(new()
            {
                Title = "Sharpnado.Tabs",
                Description =
                "Pure MAUI and Xamarin.Forms Tabs, including fixed tabs, scrollable tabs, bottom tabs, badge, segmented control, custom tabs, button tabs, bendable tabs",
                ImageSource = "https://api.nuget.org/v3-flatcontainer/sharpnado.tabs/2.2.0/icon"
            });

            refreshView.IsRefreshing = false;

            _searchBar.IsEnabled = true;
        }

        private async void HandleSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            ArgumentNullException.ThrowIfNull(sender);

            var collectionView = (CollectionView)sender;

            if(e.CurrentSelection.FirstOrDefault() is LibraryModel model)
            {
                await Shell.Current.GoToAsync(AppShell.GetRoute<DetailsPage>(), new Dictionary<string, object>
                {
                    {DetailsViewModel.LibraryModelKey, model }
                });
            }

            collectionView.SelectedItem = null;
        }

        ObservableCollection<LibraryModel> MauiLibraries { get; } = new(Libraries());



        static List<LibraryModel> Libraries() => new(){
            new()
            {
                Title = "Microsoft.Maui",
                Description = ".NET Multi-platform App UI is a framework for building native device applications spanning mobile, tablet, and desktop",
                ImageSource = "https://api.nuget.org/v3-flatcontainer/microsoft.maui.controls/8.0.3/icon"
            },
            new()
            {
                Title = "CommunityToolkit.Maui",
                Description = "The .NET MAUI Community Toolkit is a community-created library that contains .NET MAUI Extensions, Advanced UI/UX Controls, and Behaviors to help make your life as a .NET MAUI developer easier",
                ImageSource = "https://api.nuget.org/v3-flatcontainer/communitytoolkit.maui/5.2.0/icon"
            },
            new()
            {
                Title = "CommunityToolkit.Maui.Markup",
                Description = "The .NET MAUI Markup Community Toolkit is a community-created library that contains Fluent C# Extension Methods to easily create your User Interface in C#",
                ImageSource = "https://api.nuget.org/v3-flatcontainer/communitytoolkit.maui.markup/3.2.0/icon"
            },
            new()
            {
                Title = "CommunityToolkit.MVVM",
                Description = "This package includes a .NET MVVM library with helpers such as ObservableObject, ObservableRecipient, ObservableValidator, RelayCommand, AsyncRelayCommand, WeakReferenceMessenger, StrongReferenceMessenger and IoC",
                ImageSource = "https://api.nuget.org/v3-flatcontainer/communitytoolkit.mvvm/8.2.0/icon"
            },
            new()
            {
                Title = "Sentry.Maui",
                Description = "Bad software is everywhere, and we're tired of it. Sentry is on a mission to help developers write better software faster, so we can get back to enjoying technology",
                ImageSource = "https://api.nuget.org/v3-flatcontainer/sentry.maui/3.33.1/icon"
            },
            new()
            {
                Title = "Esri.ArcGISRuntime.Maui",
                Description = "Contains APIs and UI controls for building native mobile and desktop apps with the .NET Multi-platform App UI (.NET MAUI) cross-platform framework",
                ImageSource = "https://api.nuget.org/v3-flatcontainer/esri.arcgisruntime.maui/100.14.1-preview3/icon"
            },
            new()
            {
                Title = "Syncfusion.Maui.Core",
                Description = "This package contains .NET MAUI Avatar View, .NET MAUI Badge View, .NET MAUI Busy Indicator, .NET MAUI Effects View, and .NET MAUI Text Input Layout components for .NET MAUI application",
                ImageSource = "https://api.nuget.org/v3-flatcontainer/syncfusion.maui.core/21.2.10/icon"
            },
            new()
            {
                Title = "DotNet.Meteor",
                Description = "A VSCode extension that can run and debug .NET apps (based on Clancey VSCode.Comet)",
                ImageSource = "https://nromanov.gallerycdn.vsassets.io/extensions/nromanov/dotnet-meteor/3.0.3/1686392945636/Microsoft.VisualStudio.Services.Icons.Default"
            },

        };


    }
}

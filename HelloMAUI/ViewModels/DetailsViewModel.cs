using CommunityToolkit.Mvvm.Input;
using HelloMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HelloMAUI.ViewModels
{
    public class DetailsViewModel : BaseViewModel, IQueryAttributable
    {
        
        public const string LibraryModelKey = nameof(LibraryModelKey); //key that is needed to be passed for IquAtt to be able to locate the the view being navigated to 
        private ImageSource? _libraryImageSource;
        string? _libraryTitle;
        string? _libraryDescription;

        public ICommand BackButtonCommand { get; }

        public DetailsViewModel()
        {
            BackButtonCommand = new AsyncRelayCommand(()=> HandleBackButton());
        }


        public ImageSource? LibraryImageSource
        {
            get => _libraryImageSource;
            set => SetProperty(ref _libraryImageSource, value);
        }

        public string? LibraryTitle
        {
            get {
                try
                {
                    return _libraryTitle;
                }catch(Exception ex)
                {
                    return ex.Message;
                }
            }
            set => SetProperty(ref _libraryTitle, value);
        }

        public string? LibraryDescription
        {
            get => _libraryDescription;
            set => SetProperty(ref _libraryDescription, value);
        }

        private async Task HandleBackButton()
        {
            await Shell.Current.GoToAsync("..", true);
        }

        //gets the data passed from the navigation to the view's logic
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var libraryModel = (LibraryModel)query[LibraryModelKey];

            LibraryImageSource = libraryModel.ImageSource;
            LibraryDescription = libraryModel.Description;
            LibraryTitle = libraryModel.Title;


        }
    }
}

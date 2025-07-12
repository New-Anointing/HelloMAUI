using CommunityToolkit.Maui.Markup;
using HelloMAUI.ViewModels;

namespace HelloMAUI.Pages
{
    public class DetailsPage : BaseContentPage<DetailsViewModel>
    {

        public DetailsPage(DetailsViewModel detailsViewModel) : base(detailsViewModel)
        {
            this.Bind(TitleProperty,
                getter: (DetailsViewModel vm) => vm.LibraryTitle);

            Shell.SetBackButtonBehavior(this, new()
            {
#if IOS
                TextOverride = "List"
#endif
            });
            Content = new VerticalStackLayout()
            {
                Spacing = 12,
                Children =
                {
                    new Image()
                        .Center()
                        .Size(250)
                        .Bind(Image.SourceProperty,
                            getter: (DetailsViewModel vm) => vm.LibraryImageSource),

                    new Label()
                        .TextCenter()
                        .Center()
                        .Bind(Label.TextProperty,
                            getter: (DetailsViewModel vm) => vm.LibraryTitle),

                    new Label()
                        .TextCenter()
                        .Center()
                        .Font(size: 13)
                        .Bind(Label.TextProperty,
                            getter: (DetailsViewModel vm) => vm.LibraryDescription),

                    new Button()
                        .Text("Back")
                        .Bind(Button.CommandProperty, 
                            getter:(DetailsViewModel vm) => vm.BackButtonCommand)
                }
            }.Center()
            .Padding(12);
        }
    }
}

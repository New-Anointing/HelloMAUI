using CommunityToolkit.Maui.Markup;
using HelloMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace HelloMAUI.Views
{

    //This DataTemplate is the template that is used by the CollectionView to display a list of items
    public class MauiLibrariesDataTemlate : DataTemplate
    {
        const int imageRadius = 25;
        const int imagePadding = 8;

        public MauiLibrariesDataTemlate() : base(() => CreateGridTemplate())
        {
            
        }

        static Grid CreateGridTemplate() => new()
        {
            RowDefinitions = Rows.Define(
                (Row.Title, 22),
                (Row.Description, 44),
                (Row.BottomPadding, 8)),

            ColumnDefinitions = Columns.Define(
                (Column.Icon, imageRadius * 2 + imagePadding * 2),
                (Column.Text, Star)),

            RowSpacing = 4,

            Children =
            {
                new Image()
                    .Row(Row.Title).RowSpan(2)
                    .Column(Column.Icon)
                    .Center()
                    .Aspect(Aspect.AspectFit)
                    .Size(imageRadius * 2)
                    .Bind(Image.SourceProperty, getter: (LibraryModel model) => model.ImageSource,
                        mode: BindingMode.OneWay),

                new Label()
                    {
                        Style = AppStyles.GetResource<Style>("LargeFontLabel")
                    }
                    .Row(Row.Title)
                    .Column(Column.Text)
                    .Font(bold:true)
                    .TextTop()
                    .TextStart()
                    .Bind(Label.TextProperty, getter: (LibraryModel model) => model.Title,
                        mode: BindingMode.OneWay),

                new Label{ MaxLines = 2, LineBreakMode = LineBreakMode.WordWrap }
                    .Row(Row.Description)
                    .Column(Column.Text)
                    .Font(size:18)
                    .TextColor(Color.FromArgb("#595959"))
                    .TextTop()
                    .TextStart()
                    .Bind(Label.TextProperty, getter: (LibraryModel model) => model.Description,
                        mode: BindingMode.OneWay),


            }
        };


        enum Column { Icon, Text}
        enum Row { Title, Description, BottomPadding }
    }
}

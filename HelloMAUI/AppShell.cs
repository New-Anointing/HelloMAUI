using HelloMAUI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloMAUI
{
    public class AppShell : Shell
    {
        public AppShell(ListPage listPage)
        {
            Items.Add(listPage);

            CreatRoutes();
        }

        public static string GetRoute<T>() where T : ContentPage
        {
            string route = typeof(T) switch
            {
                Type t when t == typeof(ListPage) => $"//{nameof(ListPage)}",
                Type t when t == typeof(DetailsPage) => $"//{nameof(ListPage)}/{nameof(DetailsPage)}",
                Type t => throw new NotImplementedException($"{t.FullName} Not Found in routing table")
            };

            return route;
        }

        static void CreatRoutes()
        {
            Routing.RegisterRoute(GetRoute<ListPage>(), typeof(ListPage));
            Routing.RegisterRoute(GetRoute<DetailsPage>(), typeof(DetailsPage));
        }
    }
}

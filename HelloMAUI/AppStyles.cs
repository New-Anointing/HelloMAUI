using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloMAUI
{
    public class AppStyles
    {
        public static T GetResource<T>(string resourceName)
        {
            if (Application.Current?.Resources.TryGetValue(resourceName, out var resource) is true)
            {
                return (T)resource;
            }

            throw new KeyNotFoundException($"Resourse {resourceName} Not Found");
        }
    }
}

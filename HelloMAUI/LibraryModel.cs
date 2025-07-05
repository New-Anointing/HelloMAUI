using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloMAUI
{
    public class LibraryModel
    {
        public required string Title { get; init; }
        public required string Description { get; init; }
        public required ImageSource ImageSource { get; init; }
    }
}

using System;
using System.Collections.Generic;

namespace BookShop
{
    public partial class Book
    {
        public Book()
        {
            Avails = new HashSet<Avail>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Author { get; set; } = null!;
        public int? Price { get; set; }

        public virtual ICollection<Avail> Avails { get; set; }
    }
}

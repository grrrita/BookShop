using System;
using System.Collections.Generic;

namespace BookShop
{
    public partial class Shop
    {
        public Shop()
        {
            Avails = new HashSet<Avail>();
        }

        public int Id { get; set; }
        public string Address { get; set; } = null!;
        public string Director { get; set; } = null!;

        public virtual ICollection<Avail> Avails { get; set; }
    }
}

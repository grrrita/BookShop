using System;
using System.Collections.Generic;

namespace BookShop
{
    public partial class Avail
    {
        public int IdShop { get; set; }
        public int IdBook { get; set; }
        public int? Count { get; set; }

        public virtual Book IdBookNavigation { get; set; } = null!;
        public virtual Shop IdShopNavigation { get; set; } = null!;
    }
}

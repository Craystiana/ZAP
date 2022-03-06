using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ZAP.DataAccess.Entities
{
    public partial class CarBrand
    {
        public CarBrand()
        {
            Cars = new HashSet<Car>();
        }

        public int CarBrandId { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}

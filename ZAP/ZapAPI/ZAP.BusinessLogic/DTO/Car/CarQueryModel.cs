using System.Collections.Generic;

namespace ZAP.BusinessLogic.DTO.Car
{
    public class CarQueryModel
    {
        public IEnumerable<int> CarTypeIds { get; set; }

        public IEnumerable<int> CarClassIds { get; set; }
        
        public IEnumerable<int>  CarBrandIds { get; set; }

        public int? SortById { get; set; }

        public string SearchTerm { get; set; }
    }
}

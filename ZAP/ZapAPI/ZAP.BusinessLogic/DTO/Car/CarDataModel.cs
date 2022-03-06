using System.Collections.Generic;
using ZAP.BusinessLogic.DTO.Generic;

namespace ZAP.BusinessLogic.DTO.Car
{
    public class CarDataModel
    {
        public IEnumerable<GenericModel> CarTypes {get; set;}

        public IEnumerable<GenericModel> CarClasses { get; set; }

        public IEnumerable<GenericModel> CarBrands { get; set; }
    }
}

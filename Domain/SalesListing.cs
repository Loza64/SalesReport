using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SalesListing
    {
        public int Order_Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Customer { get; set; }
        public string Products { get; set; }
        public double TotalAmount { get; set; }
    }
}

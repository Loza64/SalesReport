using System;

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

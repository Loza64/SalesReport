using DataAcces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SalesReport
    {
        //Attributes-Properties
        public DateTime reportDate { get; private set; }
        public DateTime startDate { get; private set; }
        public DateTime endDate { get; private set; }
        public List<SalesListing> salesListing { get; private set; }
        public List<NetSalesByPeriod> netSalesByPeriod { get; private set; }
        public double totalNetSales { get; private set; }
        //Methods
        public void createSalesOrderReport(DateTime fromDate, DateTime toDate)
        {
            //implement dates
            reportDate = DateTime.Now;
            startDate = fromDate;
            endDate = toDate;
            //create sales listing
            OrderDAO orderDao = new OrderDAO();
            DataTable result = orderDao.GetSalesOrders(fromDate, toDate);
            salesListing = new List<SalesListing>();
            foreach (DataRow rows in result.Rows)
            {
                SalesListing salesModel = new SalesListing()
                {
                    Order_Id = Convert.ToInt32(rows[0]),
                    OrderDate = Convert.ToDateTime(rows[1]),
                    Customer = Convert.ToString(rows[2]),
                    Products = Convert.ToString(rows[3]),
                    TotalAmount = Convert.ToDouble(rows[4])
                };
                salesListing.Add(salesModel);
                //calculate total net sales
                totalNetSales += Convert.ToDouble(rows[4]);
            }
            //create net sales by period
            ////create temp list net sales by date
            var listSalesByDate = (from sales in salesListing
                                   group sales by sales.OrderDate
                                   into listSales
                                   select new
                                   {
                                       date = listSales.Key,
                                       amount = listSales.Sum(item => item.TotalAmount)
                                   }).AsEnumerable();
            ////get number of days
            int totalDays = Convert.ToInt32((toDate - fromDate).Days);
            ////group period by days
            if (totalDays <= 7)
            {
                netSalesByPeriod = (from sales in listSalesByDate
                                    group sales by sales.date.ToString("dd-MMM-yyyy")
                                    into listSales
                                    select new NetSalesByPeriod
                                    {
                                        Period = listSales.Key,
                                        NetSales = listSales.Sum(item => item.amount)
                                    }).ToList();
            }
            ////group period by weeks
            else if (totalDays <= 30)
            {
                netSalesByPeriod = (from sales in listSalesByDate
                                    group sales by
                                    CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(sales.date, CalendarWeekRule.FirstDay, DayOfWeek.Monday)
                                    into listSales
                                    select new NetSalesByPeriod
                                    {
                                        Period = "Week " + listSales.Key.ToString(),
                                        NetSales = listSales.Sum(item => item.amount)
                                    }).ToList();
            }
            ////group period by months
            else if (totalDays <= 365)
            {
                netSalesByPeriod = (from sales in listSalesByDate
                                    group sales by sales.date.ToString("MMM-yyyy")
                                    into listSales
                                    select new NetSalesByPeriod
                                    {
                                        Period = listSales.Key,
                                        NetSales = listSales.Sum(item => item.amount)
                                    }).ToList();
            }
            ////group period by years
            else
            {
                netSalesByPeriod = (from sales in listSalesByDate
                                    group sales by sales.date.ToString("yyyy")
                                    into listSales
                                    select new NetSalesByPeriod
                                    {
                                        Period = listSales.Key,
                                        NetSales = listSales.Sum(item => item.amount)
                                    }).ToList();
            }
        }
    }
}

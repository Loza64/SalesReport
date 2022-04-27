using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }
        private void getSalesReport(DateTime starDate, DateTime endDate)
        {
            SalesReport salesReport = new SalesReport();
            salesReport.createSalesOrderReport(starDate, endDate);
            SalesReportBindingSource.DataSource = salesReport;
            SalesListingBindingSource.DataSource = salesReport.salesListing;
            NetSalesByPeriodBindingSource.DataSource = salesReport.netSalesByPeriod;
            this.reportViewer1.RefreshReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fromdate = DateTime.Today;
            var todate = DateTime.Now;
            getSalesReport(fromdate, todate);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var fromdate = DateTime.Today.AddDays(-7);
            var todate = DateTime.Now;
            getSalesReport(fromdate, todate);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var fromdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var todate = DateTime.Now;
            getSalesReport(fromdate, todate);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var fromdate = DateTime.Today.AddDays(-30);
            var todate = DateTime.Now;
            getSalesReport(fromdate, todate);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var fromdate = new DateTime(DateTime.Now.Year, 1, 1);
            var todate = DateTime.Now;
            getSalesReport(fromdate, todate);
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(dtmfromdate.Value < dtmtodate.Value)
            {
                getSalesReport(dtmfromdate.Value, new DateTime(dtmtodate.Value.Year, dtmtodate.Value.Month, dtmtodate.Value.Day, 23, 59, 59));
            }
            else
            {
                this.reportViewer1.RefreshReport();
            }
        }
    }
}

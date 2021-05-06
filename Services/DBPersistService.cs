using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data; 
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using billMgr.Services;
using Microsoft.Extensions.Configuration;


namespace billMgr.Services
{
    public class DBPersistSerive : IDBPersistService
    {
        private IConfiguration Configuration;
        private string CNString;

        public DBPersistSerive(IConfiguration _configuration)
        {
            Configuration = _configuration;
            CNString = this.Configuration.GetConnectionString("DefaultConnection");
        }

        public List<BillManager> GetBillsAsync()
        {
            // SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString);
            SqlConnection cn = new SqlConnection(CNString);
            string command = "select * from BILLS";
            cn.Open();
            SqlDataAdapter da = new SqlDataAdapter(command, cn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            // List<BillManager>
            var billmgr = new List<BillManager>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                billmgr.Add(new BillManager
                {
                    Bill = (string)dr["BILL"],
                    BillDate = (DateTime)dr["billDate"],
                    TotalBill = (decimal)dr["billtotal"]
                });
            }
            return billmgr;

        }

        public List<BillDetail> GetBillsByFriendAsync()
        {
            // List<BillDetail>
            var billmgr = new List<BillDetail>();
            try
            {
                // SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString);
                SqlConnection cn = new SqlConnection(CNString);
                string command = "select bill, friend, billdate, payamount  from BILLDETAIL";
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(command, cn);
                DataSet ds = new DataSet();
                da.Fill(ds);


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    billmgr.Add(new BillDetail
                    {

                        bill = (string)dr["BILL"],
                        friend = (string)dr["friend"],
                        billdate = (DateTime)dr["billDate"],
                        billPortion = (decimal)dr["payamount"]
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message, e.InnerException);
            }
            return billmgr;
        }

        public List<BillDetail> GetFriendsByBillsAsync()
        {
            List<BillDetail> billmgr = new List<BillDetail>();
            try
            {
                // SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString);
                SqlConnection cn = new SqlConnection(CNString);
                string command = "select friend, bill, billdate, payamount  from BILLDETAIL";
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(command, cn);
                DataSet ds = new DataSet();
                da.Fill(ds);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    billmgr.Add(new BillDetail
                    {
                        friend = (string)dr["friend"],
                        bill = (string)dr["BILL"],
                        billdate = (DateTime)dr["billDate"],
                        billPortion = (decimal)dr["payamount"]
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message, e.InnerException);
            }
            return billmgr;
        }

        public List<BillDetail> AddNewBillAsync(newBills newBills)
        {
            try
            {
                DataTable tbl = new DataTable();
                tbl.Columns.Add(new DataColumn("friend", typeof(string)));
                tbl.Columns.Add(new DataColumn("bill", typeof(string)));
                tbl.Columns.Add(new DataColumn("billDate", typeof(DateTime)));
                tbl.Columns.Add(new DataColumn("billPortion", typeof(decimal)));

                decimal billprtn = newBills.TotalBill / newBills.friends.Count(); 

                foreach (string frnd in newBills.friends)
                {
                    DataRow dr = tbl.NewRow();
                    dr["friend"] = frnd;
                    dr["bill"] = newBills.BillDate;
                    dr["billDate"] = newBills.BillDate;
                    dr["billPortionl"] = billprtn;

                    tbl.Rows.Add(dr);
                }
                /*
                    public string friend { get; set; }
                    public string bill { get; set; }
                    public DateTime billdate { get; set; }
                    public decimal billPortion { get; set; }
                */
    //    string connection = "Data Source=192.168.1.1;Initial Catalog=XXXXXXX;User Id = abc123; Password = xxxxxxxx";
                SqlConnection con = new SqlConnection(CNString);
                //create object of SqlBulkCopy which help to insert  
                SqlBulkCopy objbulk = new SqlBulkCopy(con);

                //assign Destination table name  
                objbulk.DestinationTableName = "billdetail";


                objbulk.ColumnMappings.Add("bill", "bill");
                objbulk.ColumnMappings.Add("billdate", "billdate");
                objbulk.ColumnMappings.Add("friend", "friend");
                objbulk.ColumnMappings.Add("billPortion", "billportion");

                con.Open();
                //insert bulk Records into DataBase.  
                objbulk.WriteToServer(tbl);
                con.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
    }
}
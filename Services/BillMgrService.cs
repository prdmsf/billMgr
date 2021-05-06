using System;
using System.Collections.Generic;
using System.Collections.Concurrent; 
using System.Linq;
using System.Threading.Tasks;
using billMgr;
using System.IO;
using Newtonsoft.Json.Linq;
using billMgr.Services;

namespace billMgr.Services
{
    public class BillMgrService : IBillMgrService
    {
       static private ConcurrentDictionary<string, Dictionary<string, decimal>> billsDict =
        new ConcurrentDictionary<string, Dictionary<string, decimal>>(StringComparer.OrdinalIgnoreCase);
        static private Dictionary<string, decimal> pymtDict = new Dictionary<string, decimal>();

        private readonly IDBPersistService _dbPersistService;
        public BillMgrService(IDBPersistService dbPersistService)
        {
            _dbPersistService = dbPersistService;
        }
        async Task<List<BillManager>> IBillMgrService.GetBillsListAsync()
        {
            //List<BillDetail> bills = new List<BillDetail>();

            var bills = _dbPersistService.GetBillsAsync();
            
            return (bills);
        }

       async  Task<List<BillDetail>> IBillMgrService.GetBillsByFriendsListAsync()
        {
          //  List<BillDetail> bills = new List<BillDetail>();
            var bills = _dbPersistService.GetBillsByFriendAsync();

            return bills ;
        }

       async Task<List<BillDetail>> IBillMgrService.GetFriendsByBillsListAsync()
        {
            //Task<List<BillDetail>> bills = new List<Billdetail>();
            var bills =  _dbPersistService.GetFriendsByBillsAsync();
            return bills; 
        }

        async Task<BillDetail> IBillMgrService.AddNewBillAsync(newBills newBill)
        {
            BillDetail NewBillDetail = new BillDetail();

            /*
                    public string Bill { get; set; }
                    public DateTime BillDate { get; set; }
                    public decimal TotalBill { get; set; }
                    public string []  friends { get; set; }
             */
            
           

            //Task<List<BillDetail>> bills = new List<Billdetail>();
            var bills = _dbPersistService.AddNewBillAsync(newBill);
            return null; 
        }


        /*        Task<BillManager> IBillMgrService.GetFriendBillsAsync(string guest)
                { 
                    return null; 
                }
                Task<BillManager> IBillMgrService.GetFriendListAsync()
                {
                    return null;
                }
        */
    }
}

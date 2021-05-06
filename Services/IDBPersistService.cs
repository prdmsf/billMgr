using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace billMgr.Services
{
    public interface IDBPersistService
    {
        List<BillManager> GetBillsAsync();
        List<BillDetail> GetBillsByFriendAsync();
        List<BillDetail> GetFriendsByBillsAsync();
        List<BillDetail> AddNewBillAsync(newBills newBills);
    }
}

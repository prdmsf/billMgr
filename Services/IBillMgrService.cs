using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace billMgr.Services
{
    public interface IBillMgrService
    {
        Task<List<BillManager>>  GetBillsListAsync();
        Task<List<BillDetail>> GetBillsByFriendsListAsync();
        Task<List<BillDetail>> GetFriendsByBillsListAsync();

        Task<BillDetail> AddNewBillAsync(newBills newBill);
    }
}

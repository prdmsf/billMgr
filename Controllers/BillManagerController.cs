using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using billMgr.Services;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json;

namespace billMgr.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BillManagerController : ControllerBase
    {
        private readonly ILogger<BillManagerController> _logger;
        private readonly IBillMgrService _billMgrService;

        public BillManagerController(ILogger<BillManagerController> logger, Services.IBillMgrService billMgrService)
        {
            _logger = logger;
            _billMgrService = billMgrService;
        }


        //[HttpGet("{id}/{first}/{second}")]
        //[HttpGet("{id}/{backendOnly}")]
        [HttpGet("{lst}")]
        public async Task<IActionResult> Get(string lst)
        {
            Task<List<BillDetail>> byList = null;
            switch (lst)
            {

                case "byfriends":
                    var byfriendList = _billMgrService.GetFriendsByBillsListAsync();
                    byList = byfriendList;
                    break;
                case "bybills":
                    var bybillList = _billMgrService.GetBillsByFriendsListAsync();
                    byList = bybillList;
                    break;
            }
            ///   Task<List<BillManager>> allbills =  _billMgrService.GetBillsListAsync();
            return Ok(byList);
        }

        /*       [HttpGet ("BILL/BYFRIEND")]
               public string GetBILLBYFRIEND(string friend )
               {
                   Task<List<BillManager>> allbills = _billMgrService.GetBillsListAsync();

                   return "member only";    
               }

               [HttpGet("FRIEND/BYBILL")]
               public string GetFRIENDBYBILL()
               {
                   return "member only";
               }
        */
        /*        [HttpGet("bill/member/{string")]
                public string Get(string bill, string member)
                {
                    return "member only";
                }

                // PUT api/values/5
                [HttpPut("{string}")]
                public string Put(int id, [FromBody] string value)
                {
                    return " in PUT ";
                }
        */
        [HttpPost]
        [Route ("{toadd}/{sub}")]
        public void Post([FromBody] newBills jsonBills)
        {
           Console.WriteLine(jsonBills);
           var addedBIlls =  IBillMgrService.AddNewBillAsync(newBill)

        }

        public string getListofAllbills()
        {

            return "bills";
        }

        public string getmembersAtbill(string bill)
        {
            return "member at bill";
        }

        public string getmemberAtbill(string bill, string member)
        {
            return "member at bill";
        }

        public string getbillsMemberpresent(string member)
        {
            return "member at bill";
        }
    }
}


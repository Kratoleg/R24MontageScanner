using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R24MontageScannerSqlAccess.Models;

public class MontageLieferscheinModel : LieferscheinModel
{
   
        public DateTime MontageTS { get; }
        

        public MontageLieferscheinModel()
        {
            MontageTS = DateTime.Now;
        }
    
}

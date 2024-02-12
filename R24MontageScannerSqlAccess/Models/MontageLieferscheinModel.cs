using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace R24MontageScannerSqlAccess.Models;

public class MontageLieferscheinModel : LieferscheinModel, IDisposable
{

    public DateTime MontageTS { get; }



    public MontageLieferscheinModel(string lieferschein)
    {
        MontageTS = DateTime.Now;
        Lieferschein = lieferschein;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }


}



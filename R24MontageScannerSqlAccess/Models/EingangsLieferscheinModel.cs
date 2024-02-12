using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R24MontageScannerSqlAccess.Models;

public class EingangsLieferscheinModel : LieferscheinModel
{
    public DateTime EingangsTS { get;}

    public EingangsLieferscheinModel()
    {
        EingangsTS = DateTime.Now;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontageScannerLibrary
{
    public class DisplayedModel
    {
        public string Lieferschein { get; set; }
        public string Nachname { get; set; }
        public DateTime TimeStamp { get; set; }

        public string StringEingangsTS
        { get { return TimeStamp.ToString("dd.MM.yyyy HH:mm:ss"); } }
    }
}

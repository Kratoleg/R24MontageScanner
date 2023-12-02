using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontageScanLib
{
    public class LieferscheinStatus
    {
        public enum LsStatus
        {
            kommissionierung,
            montage,
            vorbVersand,
            versand
        }
    }
}

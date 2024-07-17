using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Settings.Sage300CRE
{
    public class Sage300CREPageData
    {
        public int Section { get; set; }
        public int Character1 { get; set; }
        public int Character2 { get; set; }
        public int Character3 { get; set; }
        public string Connection1 { get; set; }
        public string Connection2 { get; set; }
        public string Connection3 { get; set; }

        public  Sage300CREPageData()
        {
            Section = 0;
            Character1 = 0;
            Character2 = 0;
            Character3 = 0;
            Connection1 = string.Empty;
            Connection2 = string.Empty;
            Connection3 = string.Empty;
        }
    }
}

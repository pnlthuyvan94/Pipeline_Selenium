using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Settings.Builder.Lot
{
    public class LotSettingData
    {
        public string LotStatus;
        public string NewLotStatus;
        public LotSettingData()
        {
            LotStatus = string.Empty;
            NewLotStatus = string.Empty;
        }
    }
}

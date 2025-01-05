using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PumpTypes
{
    public class CompactPump : Pump
    {
        public CompactPump(string SerialID, Room Location, Type type) : base(SerialID, Location)
        {
            Wifi = false;
            Metric = "190 x 100 x 120 mm";
        }
        private CompactPump()
        {

        } //Deszerializálás miatt szükség van egy üres konstruktorra
    }
}

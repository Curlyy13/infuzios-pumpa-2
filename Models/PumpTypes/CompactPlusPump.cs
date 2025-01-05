using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PumpTypes
{
    public class CompactPlusPump : Pump
    {
        public CompactPlusPump(string SerialID, Room Location, Type type) : base(SerialID, Location)
        {
            Wifi = false;
            Metric = type == Type.Volumetric ? "229 x 98 x 220 mm" : "290 x 98 x 220 mm";
        }
        private CompactPlusPump()
        {

        } //Deszerializálás miatt szükség van egy üres konstruktorra
    }
}

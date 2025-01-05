using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PumpTypes
{
    public class SpacePump : Pump
    {
        public SpacePump(string SerialID, Room Location, Type type) : base(SerialID, Location)
        {
            Wifi = false;
            Metric = type == Type.Volumetric ? "214 x 68 x 124 mm" : "249 x 68 x 152 mm";
        }
        private SpacePump()
        {
            
        } //Deszerializálás miatt szükség van egy üres konstruktorra
    }
}

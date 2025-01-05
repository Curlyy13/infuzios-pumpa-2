using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PumpTypes
{
    public class SpacePlusPump : Pump
    {
        public SpacePlusPump(string SerialID, Room Location, Type type) : base(SerialID, Location)
        {
            Wifi = true;
            Metric = type == Type.Volumetric ? "215 x 70 x 170 mm" : "255 x 70 x 170 mm";
            PremiumFunctions.Add(PremiumFeatures.AP);
            if (type == Type.Syringe) { PremiumFunctions.Add(PremiumFeatures.TOM); }
        }
        private SpacePlusPump()
        {

        } //Deszerializálás miatt szükség van egy üres konstruktorra
    }
}

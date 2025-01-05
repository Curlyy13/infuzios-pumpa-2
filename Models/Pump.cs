using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public enum PremiumFeatures
    {
        AP,
        TOM
    }
    public enum Room
    {
        Room201,
        Room202, 
        Room203,
    }
    public enum Type
    {
        Volumetric,
        Syringe
    }
    public abstract class Pump
    {
        [Key]
        public string SerialID { get; protected set; }
        public bool Wifi { get; protected set; }
        public string Metric { get; protected set; }
        public List<PremiumFeatures> PremiumFunctions { get; protected set; }
        public Room Location { get; set; }

        public Pump(string SerialID, Room Location)
        {
            this.SerialID = SerialID;
            this.Location = Location;
            PremiumFunctions = new List<PremiumFeatures>();
        }
        protected Pump()
        {

        } //Deszerializálás miatt szükség van egy üres konstruktorra
        public override string ToString()
        {
            string premiumfeatures = PremiumFunctions.Any() ? string.Join(", ", PremiumFunctions) : "None";
            return $"SerialID: {SerialID}, Wifi: {Wifi}, Metric: {Metric}, Location: {Location}, Premium Features: {premiumfeatures}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Pump t)
            {
                return t.SerialID == SerialID ;
            }
            return false ;
        }

        public override int GetHashCode()
        {
            return SerialID.GetHashCode();
        }
    }
}

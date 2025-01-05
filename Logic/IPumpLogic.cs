using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IPumpLogic
    {
        void Create(Pump entity);
        Pump Read(string id);
        void Delete(string id);
        IQueryable<Pump> ReadAll();
        void ChangePumpLocation(string serialNumber, Room newRoom);
    }
}

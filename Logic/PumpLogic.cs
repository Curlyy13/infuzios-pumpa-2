using Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class PumpLogic : IPumpLogic
    {
        IRepository<Pump> repo;

        public PumpLogic(IRepository<Pump> repo)
        {
            this.repo = repo;
        }
        public void ChangePumpLocation(string serialNumber, Room newRoom)
        {
            var pumpToUpdate = repo.Read(serialNumber);
            if(pumpToUpdate == null)
            {
                throw new ArgumentException($"Couldn't find a pump with the given serialID: {serialNumber}");
            }
            pumpToUpdate.Location = newRoom;
            repo.Update(pumpToUpdate);
        }

        public void Create(Pump entity)
        {
            if(repo.ReadAll().Any(t => t.SerialID == entity.SerialID))
            {
                throw new ArgumentException($"A pump with the given serialID already exists: {entity.SerialID}");
            }
            repo.Create(entity);
        }

        public void Delete(string id)
        {
            var pump = repo.Read(id);
            if(pump == null)
            {
                throw new ArgumentException($"Couldn't find a pump with the given serialID: {id}");
            }
            repo.Delete(id);
        }

        public Pump Read(string id)
        {
            var pump = repo.Read(id);
            if(pump == null)
            {
                throw new ArgumentException($"Couldn't find a pump with the given serialID: {id}");
            }
            return repo.Read(id);
        }

        public IQueryable<Pump> ReadAll()
        {
            return repo.ReadAll();
        }
    }
}

using Models;
using Repository.GenericRepository;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ModelRepositories
{
    public class PumpRepository : Repository<Pump>, IRepository<Pump>
    {
        public PumpRepository(PumpDbContext ctx) : base(ctx) {}

        public override Pump Read(string id)
        {
            return this.ctx.pumps.FirstOrDefault(t => t.SerialID == id);
        }

        public override void Update(Pump entity)
        {
            var old = Read(entity.SerialID);
            foreach (var prop in old.GetType().GetProperties())
            {
                if(prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(entity));
                }
            }
            ctx.SaveChanges();
        }
    }
}

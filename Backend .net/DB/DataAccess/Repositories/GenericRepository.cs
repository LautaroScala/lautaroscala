using Microsoft.EntityFrameworkCore;
using DB.DataAccess.Context;
using DB.DataAccess.RepoInterfaces;
using DB.Entities;

namespace DB.DataAccess.Repositories
{
    public class GenericRepository<TEnt> : IGenericRepository<TEnt>
        where TEnt : BaseEntity
    {
        protected ThingsContext context;
        internal DbSet<TEnt> dbSet;
        public GenericRepository(ThingsContext context)
        {
            this.context = context;
            dbSet = context.Set<TEnt>();
        }

        public TEnt Add(TEnt entity)
        {
            return dbSet.Add(entity).Entity;
        }

        public bool Delete(int id)
        {
            var entity = GetEntById(id);
            if (entity == null)
            {
                return false;
            }
            dbSet.Remove(entity);
            return true;
        }

        public List<TEnt> GetAll()
        {
            return dbSet.ToList();
        }

        public TEnt GetEntById(int id)
        {
            return dbSet.FirstOrDefault(x => x.Id == id);
        }

        public TEnt Update(TEnt entity)
        {
            return dbSet.Update(entity).Entity;
        }
    }
}

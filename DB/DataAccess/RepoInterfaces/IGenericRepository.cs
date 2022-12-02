using DB.Entities;

namespace DB.DataAccess.RepoInterfaces
{
    public interface IGenericRepository<TEnt>
        where TEnt : BaseEntity
    {
        TEnt Add(TEnt entity);
        bool Delete(int id);
        TEnt Update(TEnt entity);
        TEnt GetEntById(int id);
        List<TEnt> GetAll();
    }
}
using System.Collections.Generic;
using System.Configuration;


namespace ProjetVideoGameV2.Model.DAO
{
    public abstract class DAO<T> 
    {
        protected string connectionString = null;
        public DAO()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["VideoGamesDB"].ConnectionString;
        }
        public abstract bool Create(T obj);
        public abstract bool Delete(int id);
        public abstract bool Update(T obj);
        public abstract T Find(int id);
        public abstract List<T> FindAll();

    }
}

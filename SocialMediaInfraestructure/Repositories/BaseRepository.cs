using Microsoft.EntityFrameworkCore;
using SocialMediaCore.Entidades;
using SocialMediaCore.Interfaces;
using SocialMediaInfraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaInfraestructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly SocialMediaContext db;
        protected readonly DbSet<T> entities;

        public BaseRepository(SocialMediaContext _db)
        {
            db = _db;
            entities = db.Set<T>();
        }

        public async Task Add(T entity)
        {
            await entities.AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            T entity = await GetById(id);
            entities.Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public async Task<T> GetById(int id)
        {
            return await entities.FindAsync(id);
        }

        public void Update(T entity)
        {
            entities.Update(entity);
        }
    }
}

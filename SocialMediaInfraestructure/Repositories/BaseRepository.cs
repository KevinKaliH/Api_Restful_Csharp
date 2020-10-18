using Microsoft.EntityFrameworkCore;
using SocialMediaCore.Entidades;
using SocialMediaCore.Interfaces;
using SocialMediaInfraestructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaInfraestructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly SocialMediaContext db;
        private readonly DbSet<T> entities;

        public BaseRepository(SocialMediaContext _db)
        {
            db = _db;
            entities = db.Set<T>();
        }

        public async Task Add(T entity)
        {
            entities.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            T entity = await GetById(id);
            entities.Remove(entity);

            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await entities.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await entities.FindAsync(id);
        }

        public async Task Update(T entity)
        {
            entities.Update(entity);
            await db.SaveChangesAsync();
        }
    }
}

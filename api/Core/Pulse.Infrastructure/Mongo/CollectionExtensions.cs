using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Pulse.Domain.Interfaces;

namespace Pulse.Infrastructure.Mongo
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IFindFluent<T, T> Where<T>(this IMongoCollection<T> collection, Expression<Func<T, bool>> predicate) where T : IEntity
        {
            return collection
                .Find(Builders<T>.Filter.Where(predicate));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static IFindFluent<T, T> FindAll<T>(this IMongoCollection<T> collection) where T : IEntity
        {
            return collection
                .Find(Builders<T>.Filter.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<T> GetOneAsync<T>(this IMongoCollection<T> collection, Guid id) where T : IEntity
        {
            return await collection
                .Find(Builders<T>.Filter.Eq(x => x.Id, id))
                .SingleOrDefaultAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task<Guid> AddOrUpdate<T>(this IMongoCollection<T> collection, T entity) where T : IEntity
        {
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }

            var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
            var options = new UpdateOptions { IsUpsert = true };

            await collection.ReplaceOneAsync(filter, entity, options);

            return entity.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task DeleteOneByIdAsync<T>(this IMongoCollection<T> collection, Guid id) where T : IEntity
        {
            await collection.DeleteOneAsync(Builders<T>.Filter.Eq(x => x.Id, id));
        }
    }
}
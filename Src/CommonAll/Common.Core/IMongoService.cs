namespace Common.Core
{
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IMongoService
    {
        /// <summary>
        /// If provide null instead of databasename it will provide default database
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        IMongoDatabase GetDatabase(string databaseName);

        /// <summary>
        /// Convenstion that followed -> Collection name would be pluralize from its type name. ex: UserRole -> UserRoles
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        IMongoCollection<T> GetCollection<T>();

        IMongoCollection<T> GetCollection<T>(string collectionName, string databaseName);

    }
}

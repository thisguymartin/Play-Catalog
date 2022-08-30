using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Play.Catalog.Service.Entities;
using MongoDB.Driver;
using Play.Catalog.Service.Configuration.MongoDbSettings;



namespace Play.Catalog.Service.Repository
{
    public static class Extensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            services.AddSingleton(serviceProvider =>
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                var mongoSetting = configuration.GetSection(MongoDbSettings.Setting).Get<MongoDbSettings>();
                var mongoClient = new MongoClient(mongoSetting.ConnectionString);
                return mongoClient.GetDatabase("Catalog");
            });

            return services;
        }

        public static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string collectionName)
         where T : IEntity
        {
            services.AddSingleton<IRepository<T>>(serviceProvider =>
            {
                var database = serviceProvider.GetService<IMongoDatabase>();
                return new MongoRepository<T>(database, collectionName);

            });

            return services;
        }
    }
}
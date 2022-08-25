namespace Play.Catalog.Service.Configuration.MongoDbSettings
{
    class MongoDbSettings
    {
        public const string Setting = "MongoDbSettings";

        public string Host { get; set; } = String.Empty;
        public int Port { get; set; }

        public string ConnectionString => $"mongodb://{Host}:{Port}";

    }
}
namespace Jaroszek.ProofOfConcept.MongoDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Jaroszek.ProofOfConcept.MongoDb.Model;
    using MongoDB.Driver;

    class Program
    {
        private static IMongoCollection<Contractor> collection;

        static void Main(string[] args)
        {
            var client = new MongoClient("mongodb://localhost:27017");

            var database = client.GetDatabase("Cancer");

            collection = database.GetCollection<Contractor>("CancerForm");

            Contractor items = new Contractor();
            items.Name = "xxxx";
            items.Surname = "xxxx";
            items.Address = "xxxx";

            List<Contractor> item = new List<Contractor>();

            item.Add(items);

            AddNew(item);


            var contractorList = GetAll();

            foreach (var result in contractorList)
            {
                Console.WriteLine($"-GetAll- {result.Name}, {result.Surname}, {result.Address}");
            }


            var res = GetByName("Pawxxxel");

            foreach (var result in res)
            {
                Console.WriteLine($"-GetByName- {result.Name}, {result.Surname}, {result.Address}");
            }


            Console.ReadKey();

            Console.WriteLine($"-- update !!!!");

            var itemUpdate = new Contractor();
            itemUpdate.Id = res.First().Id;
            itemUpdate.Name = "xxx";
            itemUpdate.Surname = "xxxx";
            itemUpdate.Address = "xxxxx";


            Update(itemUpdate);

            Console.ReadKey();

        }

        public static void AddNew(List<Contractor> items)
        {
            collection.InsertMany(items);
        }

        public static List<Contractor> GetAll()
        {
            return collection.Find(contractor => true).ToList();
        }

        public static List<Contractor> GetByName(string name)
        {

            var filter = Builders<Contractor>.Filter.Eq(nameof(Contractor.Name), name);

            return collection.Find(filter).ToList();
        }

        public static void Update(Guid id, string surname)
        {

            var filter = Builders<Contractor>.Filter.Eq(nameof(Contractor.Id), id);
            var update = Builders<Contractor>.Update.Set(nameof(Contractor.Surname), surname);

            collection.UpdateOne(filter, update);
        }

        public static void DeleteItem(string name)
        {
            var filter = Builders<Contractor>.Filter.Eq(nameof(Contractor.Name), name);
            collection.DeleteOne(filter);

        }

        public static void Update(Contractor item)
        {

            var filter = Builders<Contractor>.Filter.Eq(nameof(item.Id), item.Id);
            var update = Builders<Contractor>.Update
                .Set(nameof(Contractor.Address), item.Address)
                .Set(nameof(Contractor.Name), item.Name)
                .Set(nameof(Contractor.Surname), item.Surname);

            collection.UpdateOne(filter, update);
        }

    }
}

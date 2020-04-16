namespace Jaroszek.ProofOfConcept.MongoDb
{
    using System;
    using System.Collections.Generic;
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
            items.Name = "Gambol";
            items.Surname = "Jaroszek";
            items.Address = "Netowisko";

            List<Contractor> item = new List<Contractor>();

            item.Add(items);

            AddNew(item);


            var contractorList = GetAll();

            foreach (var result in contractorList)
            {
                Console.WriteLine($"-GetAll- {result.Name}, {result.Surname}, {result.Address}");
            }


            var res = GetByName("Pawel");

            foreach (var result in res)
            {
                Console.WriteLine($"-GetByName- {result.Name}, {result.Surname}, {result.Address}");
            }


            Console.ReadKey();

            //    Update("Pawel", "");

            DeleteItem("Gambol");

            Console.ReadKey();

        }


        //5e949b4d43ed503e6c7b08e8

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

        public static void Update(string name, string surname)
        {

            var filter = Builders<Contractor>.Filter.Eq(nameof(Contractor.Name), name);
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

            //    var filter = Builders<Contractor>.Filter.Eq(nameof(item.Id), item.Id);
            //   collection.UpdateMany()



            //    UpdateDefinition<Contractor> update = item;

            //   collection.UpdateOne(filter, update);
        }


    }

}

using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi_challengedev.Servicos;

namespace WebApi_challengedev.Data
{
    public class MotoristasContext
    {
        //Atributos do context
        private IConfiguration configuration;
        private MongoClient client;
        private IMongoDatabase db;
        private IMongoCollection<Motoristas> collection;

        //Construtor
        public MotoristasContext(IConfiguration config)
        {
            configuration = config;
            client = new MongoClient(
                configuration.GetConnectionString("MongoConnection"));

            db = client.GetDatabase("DBWappa");

            collection = db.GetCollection<Motoristas>("Motoristas");

        }

        //Obtendo um motorista 
        public Motoristas ObterMotorista(ObjectId _id)
        {
            var filter = Builders<Motoristas>.Filter.Eq("_id", _id);

            return db.GetCollection<Motoristas>("Motoristas")
                .Find(filter).FirstOrDefault();
        }

        public Motoristas ObterMotorista(string nome, string sobrenome)
        {
            var filter = Builders<Motoristas>.Filter.Eq(x => x.Nome, nome.ToUpper())
                & Builders<Motoristas>.Filter.Eq(x => x.SobreNome, sobrenome);

            Motoristas nMoto = collection.Find(filter).FirstOrDefault();
            return nMoto;
        }

        public Motoristas ObterMotorista(string nome)
        {
            var filter = Builders<Motoristas>.Filter.Eq("Nome", nome);
            return collection.Find(filter).FirstOrDefault();
        }

        //Listar Motoristas
        public List<Motoristas> ObterListaMotorista()
        {

            List<Motoristas> motoList = new List<Motoristas>();
            motoList = collection.Find(new BsonDocument()).SortByDescending(c => c.Nome).ToList();


            return motoList;
        }

        //Deletar Motorista
        public string DeletarMotorista(string name, string sobrenome=null)
        {
            string result = string.Empty;
            var filter = Builders<Motoristas>.Filter.Eq(x => x.Nome, name);

            if (sobrenome != null)
                filter = Builders<Motoristas>.Filter.Eq(x => x.Nome.ToUpper(), name) 
                    & Builders<Motoristas>.Filter.Eq(x => x.SobreNome, sobrenome);

            

            try
            {
                var rr = collection.DeleteMany(filter);
                result = $"Sucesso Numero de deletes: {rr.DeletedCount}";

            }
            catch (Exception e)
            {
                result = $"Erro Delete: {e.Message}";
            }


            return result;
        }

        //Editar motorista
        public string UpadateMotorista(Motoristas moto)
        {

            string result = string.Empty;

            try
            {
                var filter = Builders<Motoristas>.Filter.Eq(s => s._id, moto._id);
                var rr = collection.ReplaceOne(filter, moto);
                result = $"Sucesso: {rr.ModifiedCount}";

            }
            catch (Exception e)
            {
                result = $"ERRO: {e.Message}";
            }

            return result;

        }

        //Criar Motorista
        public string CriarMotorista(Motoristas moto)
        {
            string result = string.Empty;


            if (moto.DadosEndereco.Lat == null || moto.DadosEndereco.Log == null)
            {
                GeoAPi geoAPi = new GeoAPi(configuration);
                Motoristas newMoto = new Motoristas();
                geoAPi.ResquestGeoApi(moto);
                result = geoAPi.SetGeoMotorista(moto, geoAPi._googleGeo, out newMoto);
                if (!result.Contains("ERRO"))
                {
                    try
                    {
                        collection.InsertOne(newMoto);
                        result += "##Sucesso Insert Mongo##";
                    }
                    catch (Exception e)
                    {
                        result += $"ERRO MONGO: {e.Message}";
                    }
                }
            }



            return result;
        }




    }
}

using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Wappa.Domain.Entities;
using Wappa.Domain.Abstract;

namespace Wappa.Domain.Concrete
{
    public class EnderecoRepository: IEnderecoRepository {

        DbWappa db = null;

        public EnderecoRepository() {
            db = new DbWappa();
        }

        public List<Endereco> get() {
            return db.Enderecos.ToList();
        }

        public Endereco get(int id) {
            return db.Enderecos
                .FirstOrDefault(i => i.ID == id);
            
        }

        public void add(Endereco endereco) {
            endereco.ID = 0;
			db.Enderecos.Add(endereco);

            try {
                db.SaveChanges();
            }
            catch (Exception ex) {
                throw ex;
            }
            

        }

        public void update(Endereco endereco) {
            Endereco enderecoEdit = db.Enderecos.FirstOrDefault(i => i.ID == endereco.ID);

        
			enderecoEdit.CEP = endereco.CEP;
			enderecoEdit.Logradouro = endereco.Logradouro;
			enderecoEdit.Numero = endereco.Numero;
			enderecoEdit.Complemento = endereco.Complemento;
			enderecoEdit.Bairro = endereco.Bairro;
			enderecoEdit.Cidade = endereco.Cidade;
			enderecoEdit.Estado = endereco.Estado;
			enderecoEdit.Latitude = endereco.Latitude;
			enderecoEdit.Longitude = endereco.Longitude;     

            db.SaveChanges();

        }

        public void delete(int id) {
            Endereco endereco = db.Enderecos.FirstOrDefault(i => i.ID == id);
            db.Enderecos.Remove(endereco);
            db.SaveChanges();
			

        }

    }    
}

			// https://developers.google.com/maps/documentation/geocoding/intro
			// https://maps.googleapis.com/maps/api/geocode/json?address=1600+Amphitheatre+Parkway,+Mountain+View,+CA&key=YOUR_API_KEY
			
			//dotnet add package Newtonsoft.Json
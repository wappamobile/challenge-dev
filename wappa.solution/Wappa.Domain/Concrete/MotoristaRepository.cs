using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Wappa.Domain.Entities;
using Wappa.Domain.Abstract;

namespace Wappa.Domain.Concrete
{
    public class MotoristaRepository: IMotoristaRepository {

        DbWappa db = null;

        public MotoristaRepository() {
            db = new DbWappa();
        }

        public List<Motorista> get() {
            return db.Motoristas
            .Include(i => i.Endereco)
            .Include(i => i.Carro)
            .Include(i => i.Carro.Modelo)
            .Include(i => i.Carro.Modelo.Marca)
            .ToList();
        }

        public Motorista get(int id) {
            return db.Motoristas
                .Include(i => i.Endereco)
                .Include(i => i.Carro)
                .Include(i => i.Carro.Modelo)
                .Include(i => i.Carro.Modelo.Marca)            
                .FirstOrDefault(i => i.ID == id);
            
        }

        public void add(Motorista motorista) {
            motorista.ID = 0;
            Carro carro = db.Carros.FirstOrDefault(i => i.ID == motorista.Carro.ID);
            motorista.Carro = carro;

            Endereco endereco = db.Enderecos.FirstOrDefault(i => i.ID == motorista.Endereco.ID);
            motorista.Endereco = endereco;

			db.Motoristas.Add(motorista);

            try {
                db.SaveChanges();
            }
            catch (Exception ex) {

                throw ex;
            }
            

        }

        public void update(Motorista motorista) {
            Motorista motoristaEdit = db.Motoristas
            .Include(i => i.Carro)
            .Include(i => i.Endereco)
            .FirstOrDefault(i => i.ID == motorista.ID);

            Carro carro = db.Carros.FirstOrDefault(i => i.ID == motorista.Carro.ID);
            motoristaEdit.Carro = carro;

            Endereco endereco = db.Enderecos.FirstOrDefault(i => i.ID == motorista.Endereco.ID);
            motoristaEdit.Endereco = endereco;

			motoristaEdit.PrimeiroNome = motorista.PrimeiroNome;
			motoristaEdit.UltimoNome = motorista.UltimoNome;

            db.SaveChanges();

        }

        public void delete(int id) {
            Motorista motorista = db.Motoristas.FirstOrDefault(i => i.ID == id);
            db.Motoristas.Remove(motorista);
            db.SaveChanges();
			

        }

    }    
}
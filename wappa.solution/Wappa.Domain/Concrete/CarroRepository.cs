using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Wappa.Domain.Entities;
using Wappa.Domain.Abstract;

namespace Wappa.Domain.Concrete
{
    public class CarroRepository: ICarroRepository {

        DbWappa db = null;

        public CarroRepository() {
            db = new DbWappa();
        }

        public List<Carro> get() {
            return db.Carros
            .Include(i => i.Modelo)
            .Include(i => i.Modelo.Marca).ToList();
        }

        public List<Carro> get(Marca marca) {
            return db.Carros
            .Include(i => i.Modelo)
            .Include(i => i.Modelo.Marca)
            .Where(i => i.Modelo.Marca.ID == marca.ID)
            .ToList();
        }

        public List<Carro> get(Modelo modelo) {
            return db.Carros
            .Include(i => i.Modelo)
            .Where(i => i.Modelo.ID == modelo.ID)
            .ToList();
        }

        public Carro get(int id) {
            return db.Carros
                .Include(i => i.Modelo)
                .Include(i => i.Modelo.Marca)            
                .FirstOrDefault(i => i.ID == id);
            
        }

        public Carro get(string placa) {
            return db.Carros
                .Include(i => i.Modelo)
                .Include(i => i.Modelo.Marca)            
                .FirstOrDefault(i => i.Placa.Equals(placa));
            
        }

        public void add(Carro carro) {
            carro.ID = 0;
            Modelo modelo = db.Modelos.FirstOrDefault(i => i.ID == carro.Modelo.ID);
            carro.Modelo = modelo;

            db.Carros.Add(carro);

            db.SaveChanges();

        }

        public void update(Carro carro) {
            Carro carroEdit = db.Carros
            .Include(i => i.Modelo)
            .FirstOrDefault(i => i.ID == carro.ID);
            Modelo modelo = db.Modelos.FirstOrDefault(i => i.ID == carro.Modelo.ID);
            carroEdit.Modelo = modelo;
            carroEdit.Placa = carro.Placa;

            db.SaveChanges();

            carro.Modelo = carroEdit.Modelo;
        }

        public void delete(int id) {
            Carro carro = db.Carros.FirstOrDefault(i => i.ID == id);
            db.Carros.Remove(carro);
            db.SaveChanges();
        }

    }    
}

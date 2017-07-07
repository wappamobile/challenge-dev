using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Wappa.Domain.Entities;
using Wappa.Domain.Abstract;

namespace Wappa.Domain.Concrete
{
    public class ModeloRepository: IModeloRepository {

        DbWappa db = null;

        public ModeloRepository() {
            db = new DbWappa();
        }

        public List<Modelo> get() {
            return db.Modelos
            .Include(i => i.Marca)
            .ToList();
        }

        public List<Modelo> get(String descricao) {
            return db.Modelos
            .Include(i => i.Marca)
            .Where(i => i.Descricao.Contains(descricao))
            .ToList();
        }

        public List<Modelo> get(Marca marca) {
            return db.Modelos
            .Include(i => i.Marca)
            .Where(i => i.Marca.ID == marca.ID)
            .ToList();
        }

        public Modelo get(int id) {
            return db.Modelos
                .Include(i => i.Marca)
                .FirstOrDefault(i => i.ID == id);
            
        }

        public void add(Modelo modelo) {
            modelo.ID = 0;
            Marca marca = db.Marcas.FirstOrDefault(i => i.ID == modelo.Marca.ID);
            modelo.Marca = marca;
            db.Modelos.Add(modelo);

            db.SaveChanges();

        }

        public void update(Modelo modelo) {
            Marca marca = db.Marcas.FirstOrDefault(i => i.ID == modelo.Marca.ID);
            modelo.Marca = marca;
            Modelo modeloEdit = db.Modelos.FirstOrDefault(i => i.ID == modelo.ID);
            modeloEdit.Descricao = modelo.Descricao;
            modeloEdit.Marca = marca;
            
            db.SaveChanges();
        }

        public void delete(int id) {
            Modelo modelo = db.Modelos.FirstOrDefault(i => i.ID == id);
            db.Modelos.Remove(modelo);
            db.SaveChanges();
        }

    }    
}

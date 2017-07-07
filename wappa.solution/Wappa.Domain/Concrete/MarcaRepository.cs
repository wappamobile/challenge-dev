using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Wappa.Domain.Entities;
using Wappa.Domain.Abstract;

namespace Wappa.Domain.Concrete
{
    public class MarcaRepository: IMarcaRepository {

        DbWappa db = null;

        public MarcaRepository() {
            db = new DbWappa();
        }

        public List<Marca> get() {
            return db.Marcas.ToList();
        }

        public List<Marca> get(String descricao) {
            return db.Marcas
            .Where(i => i.Descricao.Contains(descricao))
            .ToList();
        }

        public Marca get(int id) {
            return db.Marcas
                .FirstOrDefault(i => i.ID == id);
            
        }

        public void add(Marca marca) {
            marca.ID = 0;
            db.Marcas.Add(marca);

            db.SaveChanges();

        }

        public void update(Marca marca) {
            Marca marcaEdit = db.Marcas.FirstOrDefault(i => i.ID == marca.ID);
            marcaEdit.Descricao = marca.Descricao;

            db.SaveChanges();
        }

        public void delete(int id) {
            Marca marca = db.Marcas.FirstOrDefault(i => i.ID == id);
            db.Marcas.Remove(marca);
            db.SaveChanges();
        }

    }    
}

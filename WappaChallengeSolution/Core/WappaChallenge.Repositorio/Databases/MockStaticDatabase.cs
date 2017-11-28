using System;
using System.Collections.Generic;
using System.Linq;
using WappaChallenge.Dominio.Entidades;
using WappaChallenge.Dominio.Interfaces.Repositorio;

namespace WappaChallenge.Repositorio.Databases
{
    public class MockStaticDatabase : IDatabase
    {
        public T Cadastrar<T>(T entidade) where T : BaseDominio
        {
            entidade.Id = CriadorDatabase<T>.ObterId();
            CriadorDatabase<T>.Database.Add(entidade);
            return entidade;
        }

        public T Atualizar<T>(T entidade) where T : BaseDominio
        {
            this.Excluir<T>(entidade.Id);
            CriadorDatabase<T>.Database.Add(entidade);

            return entidade;
        }

        public void Excluir<T>(int entidadeId) where T : BaseDominio
        {
            T entidadeDb = CriadorDatabase<T>.Database.FirstOrDefault(c => c.Id == entidadeId);
            CriadorDatabase<T>.Database.Remove(entidadeDb);
        }

        public IEnumerable<T> ObterTodos<T>() where T : BaseDominio
        {
            return CriadorDatabase<T>.Database;
        }

        public IEnumerable<T> Buscar<T>(Func<T, bool> query) where T : BaseDominio
        {
            return CriadorDatabase<T>.Database.Where(query).ToList();
        }

        public T BuscarPorId<T>(int entidadeId) where T : BaseDominio
        {
            return CriadorDatabase<T>.Database.FirstOrDefault(c => c.Id == entidadeId);
        }

        public void Dispose()
        {
            
        }
    }

    public static class CriadorDatabase<T> where T : BaseDominio
    {
        public static ICollection<T> Database { get; set; } = new List<T>();
        private static int contadorId;

        public static int ObterId()
        {
            return ++contadorId;
        }
    }
}

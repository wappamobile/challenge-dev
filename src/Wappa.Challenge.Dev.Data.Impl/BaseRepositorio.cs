using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Wappa.Challenge.Dev.Models;

namespace Wappa.Challenge.Dev.Data.Impl
{
    public class BaseRepositorio<T> where T : Entidade
    {
        private static string NomeEntidade;
        private static string BaseDirectory;
        private static int UltimoID;

        static BaseRepositorio()
        {
            NomeEntidade = typeof(T).Name;
            BaseDirectory = Path.Combine(AppContext.BaseDirectory, "Entidades", NomeEntidade);
            Directory.CreateDirectory(BaseDirectory);

            UltimoID = ObterUltimoId();
        }

        private string ObterCaminhoArquivo(int id)
        {
            return Path.Combine(BaseDirectory, $"{NomeEntidade}.{id}.json");
        }

        protected int Gravar(T entidade)
        {
            if (entidade.ID == 0)
            {
                entidade.ID = ++UltimoID;
            }

            var caminhoArquivo = ObterCaminhoArquivo(entidade.ID);
            if (File.Exists(caminhoArquivo))
            {
                File.Move(caminhoArquivo, $"{caminhoArquivo}.UPD.{Guid.NewGuid()}.bkp");
            }

            File.WriteAllText(caminhoArquivo, JsonConvert.SerializeObject(entidade), Encoding.UTF8);

            return 1;
        }

        protected int Excluir(int id)
        {
            var caminhoArquivo = ObterCaminhoArquivo(id);
            if (File.Exists(caminhoArquivo))
            {
                File.Move(caminhoArquivo, $"{caminhoArquivo}.DEL.{Guid.NewGuid()}.bkp");

                return 1;
            }

            return 0;
        }

        protected IEnumerable<T> Listar()
        {
            foreach (var arquivo in EnumerarArquivos())
            {
                yield return JsonConvert.DeserializeObject<T>(File.ReadAllText(arquivo, Encoding.UTF8));
            }
        }

        private static int ObterUltimoId()
        {
            var arquivos = EnumerarArquivos().ToArray();
            if (!arquivos.Any())
            {
                return 1;
            }

            return arquivos
                .Max(arquivo => int.Parse(Path.GetFileNameWithoutExtension(arquivo).Split('.').Last()));
        }

        private static IEnumerable<string> EnumerarArquivos()
        {
            return Directory.EnumerateFiles(BaseDirectory, $"{NomeEntidade}.*.json");
        }
    }
}
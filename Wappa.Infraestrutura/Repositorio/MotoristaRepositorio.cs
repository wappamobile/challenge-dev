using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Wappa.Dominio.Entidade;
using Wappa.Dominio.Repositorio;
using Wappa.Infraestrutura.Contexto;
using Wappa.ViewModel.Motorista;

namespace Wappa.Infraestrutura.Repositorio
{
    public class MotoristaRepositorio : Repositorio<Motorista>, IMotoristaRepositorio
    {
        private WappaContexto _contexto;

        public MotoristaRepositorio(WappaContexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public List<MotoristaVM> ObterTodos()
        {
            return Query()
                    .Include(i => i.ListaCarro)
                    .Include(i => i.ListaEndereco)
                    .Select(s => new MotoristaVM
                    {
                        Id = s.Id,
                        Nome = s.Nome,
                        SobreNome = s.Sobrenome,
                        CarroLista = s.ListaCarro
                                     .Select(c => new CarroVM
                                     {
                                         Id = c.Id,
                                         Cor = c.Cor,
                                         Lugar = c.Lugar,
                                         Mala = c.Mala,
                                         Marca = c.Marca,
                                         Modelo = c.Modelo,
                                         Placa = c.Placa
                                     }).ToList(),
                        EnderecoLista = s.ListaEndereco
                                        .Select(e => new EnderecoVM
                                        {
                                            Id = e.Id,
                                            Cep = e.Cep,
                                            Complemento = e.Complemento,
                                            Logradouro = e.Logradouro,
                                            Numero = e.Numero,
                                        }).ToList()
                    })
                    .OrderBy(o => o.Nome)
                    .AsNoTracking()
                    .ToList();
        }

        public Motorista Obter(Expression<Func<Motorista, bool>> condicao)
        {
            return Query()
                 .Include(i => i.ListaCarro)
                 .Include(i => i.ListaEndereco)
                 .FirstOrDefault(condicao);
        }
    }
}
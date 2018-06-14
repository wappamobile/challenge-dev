using System;
using System.Collections.Generic;
using System.Text;
using Teste.Implementacao.Inteface;
using Teste.Servicos.Externos;

namespace Teste.Implementacao
{
    public abstract class Cadastro<T> : ICadastro
    {
        protected Exception exception;
        
        public abstract void Cadastrar(IDTO dadosCadastro);

        public abstract IEnumerable<IDTO> Consultar(IFiltro ordenarPor);

        public abstract void Editar(IDTO dadosCadastro);

        public abstract void Excluir(int id);

        protected virtual T Enriquecer(T dadosCadastro)
        {
            return dadosCadastro;
        }

        protected virtual IEnumerable<T> OrdenarPor(IEnumerable<T> dadosCadastro, IFiltro filtroConsulta)
        {
            return dadosCadastro;
        }

        protected virtual bool Validar(T dadosCadastro)
        {
            return true;
        }

        protected virtual bool ValidarChave(long id)
        {
            return true;
        }

        protected IServicoGoogleMaps servicoGoogleMaps;
        public IServicoGoogleMaps ServicoGoogleMaps
        {
            get => servicoGoogleMaps ?? new ServicoGoogleMaps();
            set => servicoGoogleMaps = value;
        }
    }
}

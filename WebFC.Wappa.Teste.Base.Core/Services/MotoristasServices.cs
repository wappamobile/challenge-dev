using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebFC.Wappa.Teste.Base.Core.Interface;
using WebFC.Wappa.Teste.Base.Core.Models;
using WebFC.Wappa.Teste.Base.Core.Repositorios;

namespace WebFC.Wappa.Teste.Base.Core.Services
{
    public class MotoristasServices : IMotoristasServices 
    {

        private readonly IMotoristasRepositorio _MotoristasRepositorio;
        private readonly ICarrosRepositorio _CarrosRepositorio;


        public MotoristasServices(IMotoristasRepositorio MotoristasRepositorio, ICarrosRepositorio CarrosRepositorio)
        {

            _MotoristasRepositorio = MotoristasRepositorio;
            _CarrosRepositorio = CarrosRepositorio; 
        }

        public IEnumerable<Motoristas> GetAllFilter(Expression<Func<Motoristas, bool>> where, string orderBy)
        {

            try
            {
                return _MotoristasRepositorio.GetAllFilter(where, orderBy);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public Motoristas Add(Motoristas Motorista)
        {

            try
            {
                Motorista.DataCadastro = DateTime.Now;
                _MotoristasRepositorio.Add(Motorista);
                return Motorista; 

            }catch(Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public Motoristas Remove(Motoristas Motorista)
        {

            try
            {
                _MotoristasRepositorio.Remove(Motorista);
                return Motorista; 

            }catch(Exception ex) {

                throw new Exception(ex.Message); 

            }
            
            

        }

        public Motoristas Update(Motoristas editarMotorista)
        {

            try
            {

                Motoristas motorista = GetAllFilter(x => x.IdMotorista == editarMotorista.IdMotorista, "IdMotorista").FirstOrDefault();

                Carros carroAtual = motorista.Carro.FirstOrDefault(x => x.Ativo == 1);

                motorista.Nome = editarMotorista.Nome;
                motorista.SegundoNome = editarMotorista.SegundoNome;
                motorista.Endereco = editarMotorista.Endereco;
                motorista.Lat = editarMotorista.Lat;
                motorista.Long = editarMotorista.Long; 
                motorista.DataCadastro = DateTime.Now;

                Carros carroNovo = editarMotorista.Carro.FirstOrDefault(); 

                if (carroNovo.IdCarro.ToString() == "00000000-0000-0000-0000-000000000000")
                {
                    if(carroAtual != null) {  
                        carroAtual.Ativo = 0;
                        _CarrosRepositorio.Update(carroAtual);

                    }
                    carroNovo.Ativo = 1;
                    carroNovo.IdMotorista = editarMotorista.IdMotorista;
                    carroNovo.IdCarro = Guid.NewGuid(); 
                    _CarrosRepositorio.Add(carroNovo);
                }

                _MotoristasRepositorio.Update(motorista);
                return editarMotorista; 
            }
            catch(Exception e)
            {

                throw new Exception(e.Message); 
            }
        }
    }
}

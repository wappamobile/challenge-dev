using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teste.Repositorio.Entidade;
using Teste.Repositorio.Interface;
using Teste.Repositorio.RepositorioBase;

namespace Teste.Repositorio.Repositorio
{
    public class MotoristaRepositorio : Repositorio<Motorista>
    {
        public override IEnumerable<Motorista> Consultar()
        {
            using (var conexao = base.ConexaoSqlServer)
            {
                return Dapper.SqlMapper.Query<Motorista, Endereco, Carro, Modelo, Marca, Motorista>(conexao,
                    @"SELECT * FROM MOTORISTA M 
                      INNER JOIN ENDERECO E ON E.IDMOTORISTA = M.ID
                      INNER JOIN CARRO C ON C.IDMOTORISTA = M.ID
                      INNER JOIN MODELO MD ON MD.ID = C.IDMODELO
                      INNER JOIN MARCA MA ON MA.ID = MD.IDMARCA",
                     map: (motorista, endereco, carro, modelo, marca) =>
                     {
                         motorista.Endereco = endereco;
                         modelo.Marca = marca;
                         carro.Modelo = modelo;
                         motorista.Carro = carro;
                         return motorista;
                     });
            }

        }

        public override void Editar(Motorista dados)
        {
            using (var conexao = base.ConexaoSqlServer)
            {
                conexao.Open();
                var trans = conexao.BeginTransaction();
                try
                {
                    conexao.Query(" UPDATE MOTORISTA SET NOME = @Nome, SOBRENOME = @Sobrenome WHERE ID = @id",
                                  new { id = dados.ID, Nome = dados.Nome, Sobrenome = dados.Sobrenome }, trans);

                    conexao.Query("UPDATE CARRO SET IDMOTORISTA = @IdMotorista, IDMODELO = @IdModelo, PLACA = @Placa WHERE ID = @Id ",
                                  new { Id = dados.Carro.Id, IdMotorista = dados.ID, IdModelo = dados.Carro.Modelo.Id, Placa = dados.Carro.Placa }, trans);

                    conexao.Query("UPDATE ENDERECO SET IDMOTORISTA = @IdMotorista, LOGRADOURO = @Logradouro, NUMERO = @Numero, COMPLEMENTO = @Complemento, BAIRRO = @Bairro, CIDADE = @Cidade, ESTADO = @Estado, CEP = @CEP, LATITUDE = @Latitude, LONGITUDE = @Longitude WHERE ID = @Id",
                                  new { Id = dados.Endereco.Id, IdMotorista = dados.ID, Logradouro = dados.Endereco.Logradouro, Numero = dados.Endereco.Numero, Complemento = dados.Endereco.Complemento, Bairro = dados.Endereco.Bairro, Cidade = dados.Endereco.Cidade, Estado = dados.Endereco.Estado, CEP = dados.Endereco.CEP, Latitude = dados.Endereco.Latitude, Longitude = dados.Endereco.Longitude }, trans);

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }
                finally
                {
                    conexao.Close();
                }

            }
        }

        public override void Excluir(int id)
        {
            using (var conexao = base.ConexaoSqlServer)
            {
                conexao.Open();
                var trans = conexao.BeginTransaction();
                try
                {
                    conexao.Query("DELETE FROM CARRO WHERE IdMotorista = @IdMotorista",
                                  new { IdMotorista = id }, trans);

                    conexao.Query("DELETE FROM ENDERECO WHERE IdMotorista = @IdMotorista",
                                  new { IdMotorista = id }, trans);

                    conexao.Query(" DELETE FROM MOTORISTA WHERE Id = @IdMotorista",
                                  new { IdMotorista = id }, trans);

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }
                finally
                {
                    conexao.Close();
                }

            }
        }

        public override void Inserir(Motorista dados)
        {
            using (var conexao = base.ConexaoSqlServer)
            {
                conexao.Open();
                var trans = conexao.BeginTransaction();
                try
                {
                    var motorista = conexao.Query("INSERT INTO MOTORISTA (NOME, SOBRENOME) VALUES (@Nome, @Sobrenome) select cast(scope_identity() as int) as id",
                                  new { Nome = dados.Nome, Sobrenome = dados.Sobrenome }, trans).First();

                    conexao.Query("INSERT INTO CARRO (IDMOTORISTA, IDMODELO, PLACA) VALUES (@IdMotorista, @IdModelo, @Placa)",
                                  new { IdMotorista = motorista.id, IdModelo = dados.Carro.Modelo.Id, Placa = dados.Carro.Placa }, trans);

                    conexao.Query("INSERT INTO [dbo].[ENDERECO](idMotorista,logradouro,numero,complemento,bairro,cidade,estado,cep,latitude,longitude) VALUES(@IdMotorista,@Logradouro,@Numero,@Complemento,@Bairro,@Cidade,@Estado,@CEP,@Latitude,@Longitude)",
                                  new
                                  {
                                      IdMotorista = motorista.id,
                                      Logradouro = dados.Endereco.Logradouro,
                                      Numero = dados.Endereco.Numero,
                                      Complemento = dados.Endereco.Complemento,
                                      Bairro = dados.Endereco.Bairro,
                                      Cidade = dados.Endereco.Cidade,
                                      Estado = dados.Endereco.Estado,
                                      CEP = dados.Endereco.CEP,
                                      Latitude = dados.Endereco.Latitude,
                                      Longitude = dados.Endereco.Longitude
                                  }, trans);

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }
                finally
                {
                    conexao.Close();
                }

            }
        }
    }
}

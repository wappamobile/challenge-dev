using DriverMgr.DataAccess.Factory;
using DriverMgr.DataAccessMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DriverMgr.Api.App_Start
{
    public class DataFactoryProvider
    {
        public static DataFactoryProvider Singleton { get; set; } = new DataFactoryProvider();

        public virtual DataFactory GetDataFactory()
        {
            // Em um cenário real esse código chamaria uma classe com
            // implementações para acessar uma base de dados. Para simplificar
            // o exercício, tal classe não foi implementada, portanto aqui vai
            // uma chamada direta a uma classe de Mock.

            // Importante resaltar que a classe DataFactoryMock deve existir
            // para ser consumida pelos testes, mesmo quando a implementação
            // real de acesso a dados for adicionada ao projeto.

            return new DataFactoryMock();
        }
    }
}
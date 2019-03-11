using Driver.Application.Data.Repositories.Common;
using Driver.Application.Enums;
using System.Data.SqlClient;

namespace Driver.Application.Migration
{
    /// <summary>
    /// Estrutura para recriar a base sempre que a aplicação for rodada
    /// </summary>
    public class CreateDataBaseMigration
    {
        /// <summary>
        /// Nome da base
        /// </summary>
        private readonly static string DataBaseName;

        static CreateDataBaseMigration()
        {
            var builder = new SqlConnectionStringBuilder(ConnectionManager.Connections[ConnectionEnum.DefaultConnection]);

            DataBaseName = builder.InitialCatalog;
        }

        /// <summary>
        /// Executa os comandos
        /// </summary>
        public static void Run()
        {
            using (SqlConnectionProvider provider = new SqlConnectionProvider(ConnectionEnum.MasterConnection))
            {
                using (var cmd = provider.Connection.CreateCommand())
                {
                    cmd.CommandText = $@"IF EXISTS (SELECT TOP 1 1 FROM sys.databases WHERE name = N'{DataBaseName}')
	BEGIN
		DECLARE @kill varchar(8000) = '';  
		SELECT @kill = @kill + 'kill ' + CONVERT(varchar(5), session_id) + ';' FROM sys.dm_exec_sessions WHERE database_id  = db_id('{DataBaseName}');

        EXEC(@kill);

        DROP DATABASE {DataBaseName};
    END";

                    cmd.ExecuteNonQuery();

                    cmd.CommandText = $@"CREATE DATABASE {DataBaseName}";

                    cmd.ExecuteNonQuery();

                    cmd.CommandText = $@"create table {DataBaseName}.dbo.Driver
(
	DriverId int not null identity(1,1),
	FirstName varchar(100) not null,
	LastName varchar(100) not null,
	CarModel varchar(100) not null,
	CarBrand varchar(100) not null,
	CarLicensePlate varchar(20) not null,
	AddressNumber varchar(20) null,
	[Address] varchar(100) not null,
	AddressDistrict varchar(100) null,
	AddressCity varchar(100) not null,
	AddressState varchar(2) not null,
	AddressZipCode varchar(20) null,
	AddressLatitude float null,
	AddressLongitude float null,
	CreatedDate datetime not null,
	UpdatedDate datetime null,
	[Enabled] bit not null,
	constraint PK_Driver primary key(DriverId)
)";

                    cmd.ExecuteNonQuery();

                    cmd.CommandText = $@"
SET IDENTITY_INSERT {DataBaseName}.dbo.Driver ON;

INSERT INTO {DataBaseName}.dbo.Driver ([DriverId], [FirstName], [LastName], [CarModel], [CarBrand], [CarLicensePlate], [AddressNumber], [Address], [AddressDistrict], [AddressCity], [AddressState], [AddressLatitude], [AddressLongitude], [CreatedDate], [UpdatedDate], [Enabled], [AddressZipCode])
VALUES (1, 'Usuário', 'Teste', 'Gol', 'Volkswagen', 'AAA1111', '1', 'Rua Teste', 'Vila Teste', 'Cidade do Teste', 'TE', 0.015, 0.015, GETDATE(), GETDATE(), 1, '01234567'),
(2, 'Usuário', 'Deleted Teste', 'Gol', 'Volkswagen', 'AAA1111', '1', 'Rua Teste', 'Vila Teste', 'Cidade do Teste', 'TE', 0.015, 0.015, GETDATE(), GETDATE(), 1, '01234567');

SET IDENTITY_INSERT {DataBaseName}.dbo.Driver OFF;";

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
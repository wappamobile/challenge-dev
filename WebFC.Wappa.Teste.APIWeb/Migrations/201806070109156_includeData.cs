namespace WebFC.Wappa.Teste.APIWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class includeData : DbMigration
    {
        public override void Up()
        {

            CreateTable(
"dbo.Motoristas",
c => new
{
    IdMotorista = c.Guid(nullable: false),
    Nome = c.String(),
    SegundoNome = c.String(),
    Lat = c.String(),
    Long = c.String(),
    Endereco = c.String(),
    Carro_IdCarro = c.Guid(),
})
.PrimaryKey(t => t.IdMotorista);

            CreateTable(
                "dbo.Carros",
                c => new
                {
                    IdCarro = c.Guid(nullable: false),
                    Marca = c.String(),
                    Modelo = c.String(),
                    Placa = c.String(),
                    Ano = c.String(),
                })
                .PrimaryKey(t => t.IdCarro);
        }
        
        public override void Down()
        {
        }
    }
}

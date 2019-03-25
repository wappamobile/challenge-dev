using DriverLib.Domain;
using DriverLib.Domain.Enums;
using System;
using System.Linq;

namespace DriverLib.Repository
{
    public  class Seeder
    {

        private readonly ApplicationDbContext _context;

        // Teste123@
        private const string PASSWORD_HASH = "cWrRhnwyLmSOv3FIn7abuRevvV/GkGc1E/c66s02ujQ=";
        private const string PASSWORD_SALT = "xP+CoqfrCbbfIU9HPCd4rA==";


        public Seeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            _context.Database.EnsureCreated();

            if ( !_context.Users.Any() )
            {

                var adm = new User()
                {
                    Name = "Vagner",
                    LastName = "Administrador",
                    Email = "vagner@driverlib.com",
                    Linkedin = "linkedin.com/vagner",
                    Profile = Profile.Administrator,
                    Password = PASSWORD_HASH,
                    PasswordSalt = PASSWORD_SALT,
                    CreationDate = DateTime.Now,
                    Address = new Address()
                    {
                        Street = "Rua teste",
                        Number = "2",
                        Complement = "apto 2",
                        Neighborhood = "Bairro teste",
                        PostalCode = "11111-111",
                        City = "São Paulo",
                        State = "SP",
                        Country = "Brasil"
                    }
                };

                var driver1 = new User()
                {
                    Name = "João",
                    LastName = "Motorista",
                    Email = "walter@driverlib.com",
                    Linkedin = "linkedin.com/walter.cardoso",
                    Password = PASSWORD_HASH,
                    PasswordSalt = PASSWORD_SALT,
                    CreationDate = DateTime.Now,
                    Address = new Address()
                    {
                        Street = "Rua teste",
                        Number = "1",
                        Complement = "apto 1",
                        Neighborhood = "Bairro teste",
                        PostalCode = "11111-111",
                        City = "São Paulo",
                        State = "SP",
                        Country = "Brasil"
                    },
                    Car = new Car()
                    {
                        Brand = "Chevrolet",
                        Model = "Blazer",
                        LicensePlate = "AAA 1111-1111"
                    }
                };

                var driver2 = new User()
                {
                    Name = "Mário",
                    LastName = "Motorista",
                    Email = "rodrigo@driverlib.com",
                    Linkedin = "linkedin.com/rodrigo",
                    Password = PASSWORD_HASH,
                    PasswordSalt = PASSWORD_SALT,
                    CreationDate = DateTime.Now,
                    Address = new Address()
                    {
                        Street = "Rua teste",
                        Number = "3",
                        Complement = "apto 3",
                        Neighborhood = "Bairro teste",
                        PostalCode = "11111-111",
                        City = "São Paulo",
                        State = "SP",
                        Country = "Brasil"
                    },
                    Car = new Car()
                    {
                        Brand = "Chevrolet",
                        Model = "S10",
                        LicensePlate = "BBB 2222-2222"
                    }
                };

                var driver3 = new User()
                {
                    Name = "Fernando",
                    LastName = "Motorista",
                    Email = "cussa@driverlib.com",
                    Linkedin = "linkedin.com/cussa",
                    Profile = Profile.Administrator,
                    Password = PASSWORD_HASH,
                    PasswordSalt = PASSWORD_SALT,
                    CreationDate = DateTime.Now,
                    Address = new Address()
                    {
                        Street = "Rua teste",
                        Number = "4",
                        Complement = "apto 4",
                        Neighborhood = "Bairro teste",
                        PostalCode = "11111-111",
                        City = "São Paulo",
                        State = "SP",
                        Country = "Brasil"
                    },
                    Car = new Car()
                    {
                        Brand = "Fiat",
                        Model = "Palio",
                        LicensePlate = "CCC 3333-3333"
                    }
                };

                _context.Users.AddRange(adm, driver1, driver2, driver3);

                _context.SaveChanges();
            }

        }
        
    }
}

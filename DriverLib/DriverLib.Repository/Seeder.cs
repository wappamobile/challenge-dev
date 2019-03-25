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
                var grantee = new User()
                {
                    Name = "Walter Vinicius Lopes Cardoso",
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
                    }
                };

                var @operator = new User()
                {
                    Name = "Vagner",
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

                var donor = new User()
                {
                    Name = "Rodrigo",
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
                    }
                };

                var facilitator = new User()
                {
                    Name = "Cussa",
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
                    }
                };

                _context.Users.AddRange(grantee, @operator);

                _context.SaveChanges();
            }

        }
        
    }
}

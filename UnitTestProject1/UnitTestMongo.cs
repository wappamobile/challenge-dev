using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi_challengedev.Data;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTestMongo
    {
        private readonly MotoristasContext _context;

        public UnitTestMongo(MotoristasContext context)
        {
            _context = context;
        }

        [TestMethod]
        public void TestMethodMongoFind()
        {
            
            
           List<Motoristas> motoTotal = _context.ObterListaMotorista();
            List<Motoristas> motoTotal1 = _context.ObterListaMotorista();

            Assert.AreEqual(motoTotal[0], motoTotal1[0]);
        }   

        [TestMethod]
        public void TestMethodMongoDel()
        {
        }

        [TestMethod]
        public void TestMethodMongoIns()
        {
        }

        [TestMethod]
        public void TestMethodMongoUP()
        {
        }
    }
}

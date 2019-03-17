using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace WappaMobile.ChallengeDev.Models
{
    [TestClass]
    public class NomeTest
    {
        [TestMethod]
        public void AceitaNomeComposto()
        {
            var nome = new Nome("Paulo", "Duarte");
            Assert.IsTrue("Paulo Duarte".Equals(nome));
        }

        [TestMethod]
        public void AceitaNomeCompleto()
        {
            var nome = new Nome("Paulo Henrique", "Fernandes Duarte");
            Assert.IsTrue("Paulo Henrique Fernandes Duarte" == nome);
        }

        [TestMethod]
        public void SeparaPrimeiroNome()
        {
            var nome = new Nome("Paulo", "Duarte");
            Assert.AreEqual("Paulo", nome.Primeiro);

            var nomecompleto = new Nome("Paulo Henrique Fernandes Duarte");
            Assert.AreEqual("Paulo Henrique", nomecompleto.Primeiro);
        }

        [TestMethod]
        public void SeparaUltimoNome()
        {
            var nome = new Nome("Paulo", "Duarte");
            Assert.AreEqual("Duarte", nome.Ultimo);

            var nomecompleto = new Nome("Paulo Henrique Fernandes Duarte");
            Assert.AreEqual("Fernandes Duarte", nomecompleto.Ultimo);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NaoAceitaPrimeiroNomeVazio()
        {
            var nome = new Nome("", "Duarte");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NaoAceitaUltimoNomeVazio()
        {
            var nome = new Nome("Paulo", "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NaoAceitaNumero()
        {
            var nome = new Nome("Paulo123");
        }
    }
}

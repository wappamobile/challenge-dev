using System;

namespace Wappa.Challenge.Util.Auditoria
{
    internal class AuditoriaDadosBLL
    {
        #region Variáveis

        private AuditoriaDados db;

        #endregion

        #region Construtores

        public AuditoriaDadosBLL()
        {
            db = new AuditoriaDados();
        }

        #endregion

        public void Gravar(Auditoria pAcao)
        {
            try
            {
                db.Auditoria.Add(pAcao);
                db.SaveChanges();
            }
            //catch { }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using Microsoft.Extensions.Configuration;

namespace DriverRegistration.InfraStructure.Repository
{
    public class RepositoryBase
    {
        #region Constructors
        public RepositoryBase(IConfiguration configuration)
        {
            _Configuration = configuration;
            ConnectionString = _Configuration["AppConfiguration:Connection"];
        }
        #endregion

        #region Attributes
        protected readonly string ConnectionString;
        private readonly IConfiguration _Configuration;
        #endregion

        #region Properties

        #endregion

        #region Methods

        #endregion
    }
}

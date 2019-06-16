using System;
using System.Runtime.Serialization;

namespace Motoristas.Core.Data
{
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityName, string entityId) : base($"Entidade do tipo {entityName} com ID {entityId ?? "NULL"} não encontrada")
        {
            EntityName = entityName;
            EntityId = entityId;
        }

        protected EntityNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

        public string EntityName { get; }
        public string EntityId { get; }
    }
}
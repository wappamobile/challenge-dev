using Newtonsoft.Json;
using DriverLib.Domain.Common;
using System;

namespace DriverLib.Api.ViewModels
{
    public abstract class BaseViewModel : IIdProperty
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}

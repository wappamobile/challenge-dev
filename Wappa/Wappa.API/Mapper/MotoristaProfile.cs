using AutoMapper;
using Wappa.API.ViewModel;
using Wappa.Entities;

namespace Wappa.API.Mapper
{
    public class MotoristaProfile : Profile
    {
        public MotoristaProfile()
        {
            CreateMap<UpdateMotoristaViewModel, Motorista>();
        }
    }
}

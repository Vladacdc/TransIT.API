using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Mappings
{
    public class TransitionProfile : Profile
    {
        public TransitionProfile()
        {
            CreateMap<TransitionDTO, Transition>()
            .ForMember(i => i.UpdatedById, opt => opt.Ignore())
            .ForMember(i => i.CreatedById, opt => opt.Ignore())
            .ForMember(i => i.Mod, opt => opt.Ignore())
            .ForMember(i => i.Create, opt => opt.Ignore())
            .ForMember(i => i.ActionType, opt => opt.Ignore())
            .ForMember(i => i.ActionTypeId, opt => opt.MapFrom(x => x.ActionType.Id))
            .ForMember(i => i.FromState, opt => opt.Ignore())
            .ForMember(i => i.FromStateId, opt => opt.MapFrom(x => x.FromState.Id))
            .ForMember(i => i.ToState, opt => opt.Ignore())
            .ForMember(i => i.ToStateId, opt => opt.MapFrom(x => x.ToState.Id))
            .ForMember(i => i.CreatedDate, opt => opt.Ignore())
            .ForMember(i => i.UpdatedDate, opt => opt.Ignore());

            CreateMap<Transition, TransitionDTO>();
        }
    }
}

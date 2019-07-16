using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Mappings
{
    public class StateProfile : Profile
    {
        public StateProfile()
        {
            CreateMap<StateDTO, State>()
                .ForMember(s => s.Issue, opt => opt.Ignore())
                .ForMember(s => s.IssueLogNewState, opt => opt.Ignore())
                .ForMember(s => s.IssueLogOldState, opt => opt.Ignore());
            CreateMap<State, StateDTO>();
        }
    }
}

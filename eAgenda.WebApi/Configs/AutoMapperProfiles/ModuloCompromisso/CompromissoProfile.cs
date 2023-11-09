using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.WebApi.ViewModels.ModuloCompromisso;

namespace eAgenda.WebApi.Configs.AutoMapperProfiles.ModuloCompromisso
{
    public class CompromissoProfile : Profile
    {
        public CompromissoProfile()
        {
            CreateMap<Compromisso, ListarCompromissoViewModel>()
                .ForMember(des => des.Data, opt => opt.MapFrom(origem => origem.Data.ToShortDateString()))
                .ForMember(des => des.HoraInicio, opt => opt.MapFrom(origem => origem.HoraInicio.ToString(@"hh\:mm")))
                .ForMember(des => des.HoraTermino, opt => opt.MapFrom(origem => origem.HoraTermino.ToString(@"hh\:mm")));

            CreateMap<Compromisso, VisualizarCompromissoViewModel>()
                .ForMember(des => des.Data, opt => opt.MapFrom(origem => origem.Data.ToShortDateString()))
                .ForMember(des => des.HoraInicio, opt => opt.MapFrom(origem => origem.HoraInicio.ToString(@"hh\:mm")))
                .ForMember(des => des.HoraTermino, opt => opt.MapFrom(origem => origem.HoraTermino.ToString(@"hh\:mm")));

            CreateMap<Compromisso, FormCompromissoViewModel>()                
                .ForMember(des => des.Data, opt => opt.MapFrom(origem => origem.Data.ToShortDateString()))
                .ForMember(des => des.HoraInicio, opt => opt.MapFrom(origem => origem.HoraInicio.ToString(@"hh\:mm")))
                .ForMember(des => des.HoraTermino, opt => opt.MapFrom(origem => origem.HoraTermino.ToString(@"hh\:mm")));

            CreateMap<FormCompromissoViewModel, Compromisso>();
        }
    }
}

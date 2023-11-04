using eAgenda.Dominio.ModuloDespesa;
using eAgenda.WebApi.ViewModels.ModuloDespesa;

namespace eAgenda.WebApi.Configs.AutoMapperProfiles.ModuloDespesas
{
    public class DespesaProfile : Profile
    {
        public DespesaProfile()
        {
            CreateMap<Despesa, FormDespesaViewModel>()
                .ForMember(des => des.Data, opt => opt.MapFrom(origem => origem.Data.ToShortDateString()));
            CreateMap<Despesa, ListarDespesaViewModel>()
                .ForMember(des => des.Data, opt => opt.MapFrom(origem => origem.Data.ToShortDateString()));
            CreateMap<Despesa, VisualizarDespesaViewModel>()
                .ForMember(des => des.Data, opt => opt.MapFrom(origem => origem.Data.ToShortDateString()));
            CreateMap<FormDespesaViewModel, Despesa>();
        }
    }
}

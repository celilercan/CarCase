using AutoMapper;
using CarCaseTest.Business.Search.IndexModels;
using CarCaseTest.Domain.Entities;
using CarCaseTest.Domain.Enums;
using CarCaseTest.Domain.Extensions;
using CarCaseTest.Domain.Models.Adverts;

namespace CarCaseTest.Business.Mapping
{
    public class ServiceMapping : Profile
    {
        public ServiceMapping()
        {
            CreateMap<Advert, AdvertDetailModel>().ReverseMap();
            CreateMap<AdvertListIndex, AdvertListModel>().ReverseMap();
            CreateMap<Advert, AdvertListIndex>()
                .ForMember(x => x.FuelId, x => x.MapFrom(s => s.Fuel.ToEnumId()))
                .ForMember(x => x.GearId, x => x.MapFrom(s => s.Gear.ToEnumId()))
                .ForMember(x => x.ColorId, x => x.MapFrom(s => s.Color.ToEnumId()))
                .ReverseMap();

            CreateMap<AdvertListIndex, AdvertListModel>()
                .ForMember(x => x.Color, x => x.MapFrom(s => ((ColorType)s.ColorId).GetDisplayName()))
                .ForMember(x => x.Fuel, x => x.MapFrom(s => ((FuelType)s.FuelId).GetDisplayName()))
                .ForMember(x => x.Gear, x => x.MapFrom(s => ((GearType)s.GearId).GetDisplayName()))
                .ReverseMap();

            CreateMap<AdvertSearchResponse, AdvertListResultModel>()
                .ForMember(x => x.Adverts, x => x.MapFrom(s => s.Documents))
                .ReverseMap();
            
        }
    }
}

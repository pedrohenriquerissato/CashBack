using AutoMapper;
using CashBack.Api.Resources;
using CashBack.Domain;
using CashBack.Domain.Models;

namespace CashBack.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Purchase, PurchaseResource>();
            CreateMap<Retailer, RetailerResource>();
            CreateMap<CashbackPurchaseDTO, CashbachPurchaseResource>();
            CreateMap<object, LoginResource>();

            CreateMap<PurchaseResource, Purchase>();
            CreateMap<RetailerResource, Retailer>();
            CreateMap<LoginResource, object>();

            CreateMap<SaveRetailerResource, Retailer>();
            CreateMap<SavePurchaseResource, Purchase>();
        }
    }
}

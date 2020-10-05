using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CashBack.Api.Resources;
using CashBack.Domain.Models;

namespace CashBack.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Purchase, PurchaseResource>();
            CreateMap<Retailer, RetailerResource>();
            CreateMap<object, LoginResource>();

            CreateMap<PurchaseResource, Purchase>();
            CreateMap<RetailerResource, Retailer>();
            CreateMap<LoginResource, object>();

            CreateMap<SaveRetailerResource, Retailer>();
            CreateMap<SavePurchaseResource, Purchase>();
        }
    }
}

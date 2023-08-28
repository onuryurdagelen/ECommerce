using AutoMapper;
using ECommerce.API.Entities;
using ECommerce.Data.Dtos;
using ECommerce.Data.Helpers;
using ECommerce.Entity.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Mappings
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

            CreateMap<Address, AddressDto>()
                .ReverseMap();
                //.ForMember(d => d.LastName, o => o.MapFrom(s => s.LastName))
                //.ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName))
                //.ForMember(d => d.State, o => o.MapFrom(s => s.State))
                //.ForMember(d => d.Street, o => o.MapFrom(s => s.Street))
                //.ForMember(d => d.City, o => o.MapFrom(s => s.City))
                //.ForMember(d => d.ZipCode, o => o.MapFrom(s => s.ZipCode));

            //Custom value resolver
        }
    }
}

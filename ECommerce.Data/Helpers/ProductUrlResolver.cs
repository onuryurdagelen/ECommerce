using AutoMapper;
using ECommerce.API.Entities;
using ECommerce.Data.Dtos;
using ECommerce.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
               return AppSettingsExtension.GetSectionValue("ApiUrl") + source.PictureUrl;
            
            return null;
        }
    }
}

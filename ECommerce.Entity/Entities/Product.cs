using ECommerce.Entity.Entities;

namespace ECommerce.API.Entities
{
    public class Product: BaseEntity
    {

        #region properties

        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int ProductTypeId { get; set; }
        public int ProductBrandId { get; set; }

        #endregion

        #region navigations
        public virtual ProductType ProductType { get; set; }
        
        public virtual ProductBrand ProductBrand { get; set; }
        #endregion

    }
}

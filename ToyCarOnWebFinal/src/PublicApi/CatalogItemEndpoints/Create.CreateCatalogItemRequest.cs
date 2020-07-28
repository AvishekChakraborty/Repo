﻿namespace Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints
{
    public class CreateCatalogItemRequest : BaseRequest 
    {
        public int CatalogBrandId { get; set; }
        public int CatalogTypeId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string PictureUri { get; set; }
        public double Price { get; set; }
    }

}

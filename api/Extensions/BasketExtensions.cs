using System.Linq;
using api.DTOs;
using api.Entities;

namespace api.Extensions
{
    public static class BasketExtensions
    {
        public static BasketDTO MapBasketToDto(this Basket basket)
        {
            return new BasketDTO
            {
                ID = basket.ID,
                BuyerID = basket.BuyerID,
                Items = basket.Items.Select(item => new BasketItemDto
                {
                    ProductID = item.ProductID,
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                    PictureURL = item.Product.PictureURL,
                    Type = item.Product.Type,
                    Brand = item.Product.Brand,
                    Quantity = item.Quantity
                }).ToList()
            };
        }
    }
}
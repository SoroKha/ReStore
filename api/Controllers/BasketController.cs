using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using api.Data;
using api.DTOs;
using api.Entities;
using api.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace api.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly StoreContext _context;
        public BasketController(StoreContext context)
        {
            _context = context;
            
        }

        [HttpGet(Name = "GetBasket")]
        public async Task<ActionResult<BasketDTO>> GetBasket()
        {
            var basket = await RetrieveBasket(GetBuyerId());

            if (basket == null) return NotFound();
            return basket.MapBasketToDto();
        }

        [HttpPost]
        public async Task<ActionResult<BasketDTO>> AddItemToBasket(int productID, int quantity)
        {
            // get basket || create basket
            var basket = await RetrieveBasket(GetBuyerId());
            if (basket == null) basket = CreateBasket();

            // get product
            var product = await _context.Products.FindAsync(productID);
            if (product == null) return BadRequest(new ProblemDetails{Title = "Product Not Found"});

            // add item
            basket.AddItem(product, quantity);
            var result = await _context.SaveChangesAsync() > 0;

            // save changes
            if (result) return CreatedAtRoute("GetBasket", basket.MapBasketToDto());

            return BadRequest(new ProblemDetails{Title = "Problem saving item to basket"});
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveBasketItem(int productID, int quantity)
        {
            // get basket
            var basket = await RetrieveBasket(GetBuyerId());

            if (basket == null) return NotFound();

            // remove item or reduce quantity
            basket.RemoveItem(productID, quantity);
            var result = await _context.SaveChangesAsync() > 0;

            // save changes
            if (result) return Ok();
            return BadRequest(new ProblemDetails{Title = "Problem deleting item from basket"});
        }

        private async Task<Basket> RetrieveBasket(string buyerID)
        {
            if (string.IsNullOrEmpty(buyerID))
            {
                Response.Cookies.Delete("buyerID");
                return null;
            }
            return await _context.Baskets
                .Include(i => i.Items)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(x => x.BuyerID == buyerID);
        }

        private string GetBuyerId()
        {
            return User.Identity?.Name ?? Request.Cookies["buyerID"];
        }

        private Basket CreateBasket()
        {
            var buyerID = User.Identity?.Name;
            if (string.IsNullOrEmpty(buyerID))
            {
                buyerID = Guid.NewGuid().ToString();
                var cookieOptions = new CookieOptions{IsEssential = true, Expires = DateTime.Now.AddDays(30)};
                Response.Cookies.Append("buyerID", buyerID, cookieOptions);
            }
            
            var basket = new Basket{BuyerID = buyerID};
            _context.Baskets.Add(basket);
            return basket;
        }
    }
}
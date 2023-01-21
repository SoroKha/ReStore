using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs
{
    public class BasketDTO
    {
        public int ID { get; set; }

        public string BuyerID { get; set; }
        public List<BasketItemDto> Items { get; set; }
    }
}
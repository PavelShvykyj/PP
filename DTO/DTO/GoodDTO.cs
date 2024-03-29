﻿using System.ComponentModel.DataAnnotations;

namespace DTO.DTO
{
    public class GoodDTO
    {
        public ushort Id { get; set; }
        public string Name { get; set; }
        public ushort Rest { get; set; }
        [Range(0,System.Double.MaxValue)]
        public decimal Price { get; set; }
    }
}

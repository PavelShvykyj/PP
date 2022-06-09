﻿using System.ComponentModel.DataAnnotations;

namespace PP.EF.models
{
    public class Customers
    {
        public ushort id { get; set; }
        public string name { get; set; } = null!;
        [EmailAddress]
        public string email { get; set; } = null!;
    }
}

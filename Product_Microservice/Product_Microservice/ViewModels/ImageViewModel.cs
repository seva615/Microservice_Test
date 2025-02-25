﻿using System.ComponentModel.DataAnnotations;

namespace Product.Api.ViewModels
{
    public class ImageViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public byte[] Base64 { get; set; }
        
        public Guid ProductId { get; set; }
    }
}

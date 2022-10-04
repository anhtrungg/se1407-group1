using System;
using System.ComponentModel.DataAnnotations;

namespace VoiceAPI.Categories
{
    public class CategoryVM
    {
        [MaxLength(100), Required]
        public string Name { get; set; }
    }
}

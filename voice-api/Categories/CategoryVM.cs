using System;

namespace VoiceAPI.Categories
{
    public class CategoryVM
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
    }

    public class Category : CategoryVM
    {
        //public Guid Id { get; set; }
    }
}

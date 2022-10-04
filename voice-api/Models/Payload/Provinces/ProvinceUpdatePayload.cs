using System;
using System.ComponentModel.DataAnnotations;

namespace VoiceAPI.Models.Payload.Provinces
{
    public class ProvinceUpdatePayload
    {
        [Required]
        public Guid Code  { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

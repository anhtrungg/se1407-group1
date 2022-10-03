using System;

namespace VoiceAPI.Provinces
{
    public class ProvinceVM
    {
        public string name { get; set; }
        public string code { get; set; }
        public Guid id { get; internal set; }
    }
    public class ProvicesVM : ProvinceVM
    {
        public int id { get; set; }
    }
}

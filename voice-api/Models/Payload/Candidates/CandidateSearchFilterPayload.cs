using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities.Enums;

namespace VoiceAPI.Models.Payload.Candidates
{
    public class CandidateSearchFilterPayload
    {
        public string SearchText { get; set; }

        public GenderEnum? Gender { get; set; }
        public AccentEnum? Accent { get; set; }
        public Guid? SubCategoryId { get; set; }

        public Guid? CategoryId { get; set; }

        public int? MinAge { get; set; }

        public int? MaxAge { get; set; }
    }
}

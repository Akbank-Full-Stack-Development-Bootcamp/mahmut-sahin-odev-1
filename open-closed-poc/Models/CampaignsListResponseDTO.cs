using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace open_closed_poc.Models
{
    public class CampaignsListResponseDTO
    {
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ImageUrl { get; set; }
        //public string? Conditions { get; set; }
    }
}

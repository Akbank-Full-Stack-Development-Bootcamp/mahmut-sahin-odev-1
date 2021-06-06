using open_closed_poc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace open_closed_poc.Services
{
    public static class CampaignRepository
    {
        public static List<Campaign> InitRepo()
        {
            List<Campaign> campaigns = new()
            {
                new Campaign
                {
                    Id= 1,
                    Name= "Kodluyoruz'dan gelenlere %3 faiz!",
                    StartTime= new DateTime(2021,5,15),
                    EndTime = new DateTime(2021,6,15),
                    Desc="Lorem",
                    Conditions = "Kodluyoruz üyeleri katılabilir.",
                    IsActive = true,
                    ImageUrl = "https://www.kodluyoruz.org",
                    CountLimit = 250
                },
                new Campaign
                {
                    Id= 2,
                    Name= "Kripto para ile ödemelere 0 komisyon!",
                    StartTime= new DateTime(2021,6,19),
                    EndTime = new DateTime(2021,8,1),
                    Desc="Ipsum",
                    Conditions = null,
                    IsActive = false,
                    ImageUrl = "https://www.akbank.com",
                    CountLimit = 10000
                }
            };
            return campaigns;
        }
    }
}

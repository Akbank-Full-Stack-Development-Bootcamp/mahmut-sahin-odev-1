using open_closed_poc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace open_closed_poc.Services
{
    public class CampaignService
    {
        public static List<CampaignsListResponseDTO> GetCampaignsListDTO()
        {
            // Correct!
            List<Campaign> campaignsRepo = CampaignRepository.InitRepo();
            List<CampaignsListResponseDTO> dtoList = new();
            Campaign campaign = new();
            CampaignsListResponseDTO campaignsListResponseDTO = new();
            var sourceProps = campaign.GetType().GetProperties();
            var dtoProps = campaignsListResponseDTO.GetType().GetProperties();
            foreach (var obj in campaignsRepo)
            {
                CampaignsListResponseDTO dtoCampaign = new();
                foreach (var sourceProp in sourceProps)
                {
                    foreach (var dtoProp in dtoProps)
                    {
                        if (sourceProp.Name == dtoProp.Name && sourceProp.PropertyType == dtoProp.PropertyType)
                        {
                            dtoProp.SetValue(dtoCampaign, sourceProp.GetValue(obj));
                        }
                    }
                }
                dtoList.Add(dtoCampaign);
            }
            return dtoList;
        }

            // False!
            //campaignsRepo.ForEach(c =>
            //{
            //    var campaignListObj = new CampaignsListResponseDTO
            //    {
            //        Name = c.Name,
            //        StartTime = c.StartTime,
            //        EndTime = c.EndTime,
            //        ImageUrl = c.ImageUrl,
            //        Conditions = c.Conditions // Ekledik.
            //    };
            //    dtoList.Add(campaignListObj);
            //});
            //return dtoList;
    }
}

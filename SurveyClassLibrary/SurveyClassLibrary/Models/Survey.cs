using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyClassLibrary.Models
{
    [Table("Surveys")]
    public class Survey
    {
        public Guid GuidLink { get; set; }
        public int SurveyId { get; set; }
        public string SurveyName { get; set; }
        public string SurveyDescription { get; set; }
        public DateTime DateCreated { get; set; }
        public string SurveyOwnerId { get; set; }
        public ApplicationUser SurveyOwner { get; set; }
        public DateTime AvailableFromDate { get; set; }
        public DateTime ClosesOnDate { get; set; }
        public string ModifiedByUserId { get; set; }
        public ApplicationUser ModifiedByUser { get; set; }
        public List<SurveyQuestion> QuestionsList { get; set; }
    }
}

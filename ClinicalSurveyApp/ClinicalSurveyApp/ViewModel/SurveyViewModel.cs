using Microsoft.AspNetCore.Identity;
using SurveyClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicalSurveyApp.ViewModel
{

    public class SurveyViewModel
    {
        [Key]
        public int SurveyId { get; set; }
        [Display(Name = "Title")]
        [Required]
        public string SurveyName { get; set; }
        [Display(Name = "Description")]
        [Required]
        [MaxLength(128)]
        public string SurveyDescription { get; set; }
        public Guid GuidLink { get; set; }
        [Display(Name ="Created on date")]
        public DateTime DateCreated { get; set; }
        public string SurveyOwnerId { get; set; }
        public ApplicationUser SurveyOwner { get; set; }
        [Display(Name = "Survey available from")]
        public DateTime AvailableFromDate { get; set; }
        [Display(Name = "Survey closes on")]
        public DateTime ClosesOnDate { get; set; }
        public IdentityUser ModifiedByUser { get; set; }
        public List<SurveyQuestionViewModel> QuestionsList { get; set; }

    }
}

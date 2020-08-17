using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyClassLibrary.Models
{
    [Table("UserSurveyResponse")]
    public class UserSurveyResponse
    {
        public int UserSurveyResponseId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
        public List<UserQuestionAnswer> UserQuestionAnswersList { get; set; }
        public DateTime CompletedOnDate { get; set; }

    }
}

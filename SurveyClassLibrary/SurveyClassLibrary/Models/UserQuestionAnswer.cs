using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyClassLibrary.Models
{
    [Table("UserQuestionAnswers")]
    public class UserQuestionAnswer
    {
        public int UserQuestionAnswerId { get; set; }
        public int SurveyQuestionId { get; set; }
        public SurveyQuestion SurveyQuestion { get; set; }
        public int UserSurveyResponseId { get; set; }
        public List<UserAnswers> UserQuestionResponse { get; set; }



    }
}

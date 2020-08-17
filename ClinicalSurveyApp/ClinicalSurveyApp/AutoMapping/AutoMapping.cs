using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ClinicalSurveyApp.ViewModel;
using SurveyClassLibrary.Models;

namespace ClinicalSurveyApp.AutoMapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Survey, SurveyViewModel>();
            CreateMap<SurveyViewModel, Survey>();
            CreateMap<SurveyQuestion, SurveyQuestionViewModel>();
            CreateMap<SurveyQuestionViewModel, SurveyQuestion>();
            CreateMap<SurveyPossibleAnswer, SurveyPossibleAnswerViewModel>();
            CreateMap<SurveyPossibleAnswerViewModel, SurveyPossibleAnswer>();
            CreateMap<UserSurveyResponse, UserSurveyResponseViewModel>();
            CreateMap<UserSurveyResponseViewModel, UserSurveyResponse>();
            CreateMap<UserQuestionAnswer, UserQuestionAnswerViewModel>();
            CreateMap<UserQuestionAnswerViewModel, UserQuestionAnswer>();
            CreateMap<UserAnswers, UserAnswersViewModel>();
            CreateMap<UserAnswersViewModel, UserAnswers>();
            CreateMap<UserDetailsViewModel, ApplicationUser>();
            CreateMap<ApplicationUser, UserDetailsViewModel>();
        }

    }
}

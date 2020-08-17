using System;
using System.Collections.Generic;
using System.Text;
using SurveyClassLibrary.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SurveyAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<SurveyPossibleAnswer> SurveyPossibleAnswer { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestion { get; set; }
        public DbSet<Survey> Survey { get; set; }
        public DbSet<UserQuestionAnswer> UserQuestionAnswer { get; set; }
        public DbSet<UserSurveyResponse> UserSurveyResponse { get; set; }
        public DbSet<UserAnswers> UserAnswers { get; set; }
       
    }
}

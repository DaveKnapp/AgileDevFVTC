﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace T5.Brothership.PL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using T5.Brothership.Entities.Models;
    
    public partial class brothershipEntities : DbContext
    {
        public brothershipEntities()
            : base("name=brothershipEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<GameCategory> GameCategories { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<IntegrationType> IntegrationTypes { get; set; }
        public virtual DbSet<Nationality> Nationalities { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<SocialMediaType> SocialMediaTypes { get; set; }
        public virtual DbSet<UserIntegration> UserIntegrations { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<UserRating> UserRatings { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserSocialJunc> UserSocialJuncs { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
    }
}

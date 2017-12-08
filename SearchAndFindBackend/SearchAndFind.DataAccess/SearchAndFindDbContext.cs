using SearchAndFind.Entities;
using System.Runtime.Remoting.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SearchAndFind.DataAccess
{
    public class SearchAndFindDbContext: DbContext
    {
        public SearchAndFindDbContext() : base("name=SearchAndFindDb") { }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Saler> Salers { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Query> Queries { get; set; }
        public virtual DbSet<Tender> Tenders { get; set; }
        public virtual DbSet<TenderImage> TenderImages { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            this.Configuration.LazyLoadingEnabled = false;
            ConfigureCategoryTable(modelBuilder);
            ConfigureTenderImageTable(modelBuilder);
            ConfigureReviewTable(modelBuilder);
            ConfigureQueryTable(modelBuilder);
            ConfigureTenderTable(modelBuilder);
            ConfigClientTable(modelBuilder);
            ConfigSalerTable(modelBuilder);
        }

        

        private void ConfigureTenderImageTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TenderImage>().
               HasKey(image => new { image.Id, image.TenderId });
        }

        private void ConfigureReviewTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>().
                HasKey(review => review.Id);
            modelBuilder.Entity<Review>().
                Property(review => review.ReviewDate).HasColumnType("datetime2");
        }

        private void ConfigureQueryTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Query>().
                Property(query => query.QueryDate).HasColumnType("datetime2");
            modelBuilder.Entity<Query>().
                Property(query => query.State).HasMaxLength(1);

        }

        private void ConfigureTenderTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tender>().
                Property(tender => tender.TenderDate).HasColumnType("datetime2");
            modelBuilder.Entity<Tender>().
                Property(tender => tender.State).HasMaxLength(1);
        }

        private void ConfigureCategoryTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Property(c => c.Name).HasColumnType("varchar").HasMaxLength(50)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("CategoryNameIndex") { IsUnique = true }));
        }

        private void ConfigClientTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasKey(client => client.Id).
                 Property(client => client.MailAddress).HasColumnType("varchar").HasMaxLength(50).
                     HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("ClientMailAddressIndex") { IsUnique = true }));
            modelBuilder.Entity<User>().HasMany<Category>(client => client.Categories)
                      .WithMany(category => category.Users)
                      .Map(cs =>
                      {
                          cs.MapLeftKey("UserId");
                          cs.MapRightKey("CategoryId");
                          cs.ToTable("UserCategories");
                      });
            
        }

        private void ConfigSalerTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Saler>().HasKey(saler => saler.Id).
                 Property(saler => saler.MailAddress).HasColumnType("varchar").HasMaxLength(50).
                     HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("SalerMailAddressIndex") { IsUnique = true }));
            


        }
    }
}
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace _5204_Passion_Project_n01442368_v2.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class PassionProjectv2DbContext : IdentityDbContext<ApplicationUser>
    {
        public PassionProjectv2DbContext()
            : base("PassionProjectv2DataContextwAuth", throwIfV1Schema: false)
        {
        }

        public static PassionProjectv2DbContext Create()
        {
            return new PassionProjectv2DbContext();
        }

        //Set the models as tables in the database.
        public DbSet<Film> Films { get; set; }
        public DbSet<Lens> Lenses { get; set; }
        public DbSet<Photo> Photos { get; set; }

        //Turn off cascade delete
        //https://www.learnentityframeworkcore.com/configuration/one-to-many-relationship-configuration
        //https://www.entityframeworktutorial.net/code-first/cascade-delete-in-code-first.aspx
        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Photo>()
                .HasRequired(p => p.Film)
                .WithMany(f => f.Photos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Photo>()
                .HasRequired(p => p.Lens)
                .WithMany(l => l.Photos)
                .WillCascadeOnDelete(false);
        }*/

    }
}
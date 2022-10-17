using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Mappings
{
    public class UserRoleMap : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            // Primary key
            builder.HasKey(r => new { r.UserId, r.RoleId });

            // Maps to the AspNetUserRoles table
            builder.ToTable("AspNetUserRoles");

            builder.HasData(new AppUserRole
            {
                UserId = Guid.Parse("ED101E7F-B43D-489C-B124-2EFA21108FAC"),
                RoleId = Guid.Parse("F758FAE1-935B-4FFB-A82F-DD6FD4AD7219"),
            },
            new AppUserRole
            {
                UserId = Guid.Parse("CFE8C4D2 - 9077 - 48D8 - 81D7 - 707CD32D083F"),
                RoleId = Guid.Parse("2E884348-1406-4409-972C-EA82F186A2CF"),
            });


        }
    }
}

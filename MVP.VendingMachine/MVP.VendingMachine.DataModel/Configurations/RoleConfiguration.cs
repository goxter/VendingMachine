using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP.VendingMachine.DataModel.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
        new IdentityRole
        {
            Id = "44eda852-f157-4826-86bf-1f44e447d654",
            ConcurrencyStamp = "08252f36-1387-4027-b31c-01b1c8ada3ae",
            Name = "Seller",
            NormalizedName = "SELLER"
        },
        new IdentityRole
        {
            Id = "866765b1-91d9-47ae-96b2-85dacd72bad2",
            ConcurrencyStamp = "99de036f-6a3e-446e-83a3-7cd6440e32d8",
            Name = "Buyer",
            NormalizedName = "BUYER"
        });
    }
}


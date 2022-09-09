using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVP.VendingMachine.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP.VendingMachine.DataModel.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        // Primary Key
        builder.HasKey(t => t.Id);

        // Table & Column Mappings
        builder.ToTable("User");
    }
}


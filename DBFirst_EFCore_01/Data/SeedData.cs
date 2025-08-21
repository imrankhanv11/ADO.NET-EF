using DBFirst_EFCore_01.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFirst_EFCore_01.Data
{
    public class Benefitconfiguration : IEntityTypeConfiguration<Benefit>
    {

        public void Configure(EntityTypeBuilder<Benefit> builder)
        {
            builder.HasData(
                new Benefit
                {
                    Id = 1, BenefitName = "Health"
                },
                new Benefit
                {
                    Id= 2, BenefitName = "Travel"
                }
                );
        }
    }
}

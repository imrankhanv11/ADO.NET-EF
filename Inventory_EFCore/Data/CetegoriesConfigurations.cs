using EFCore_DBFirstApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_DBFirstApp.Data
{
    public class CetegoriesConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category
                {
                    CategoryId = -1, CategoryName = "Default 01"
                },
                new Category
                {
                    CategoryId = -2, CategoryName = "Default 02"
                },
                new Category
                {
                    CategoryId = -3,
                    CategoryName = "Default 03"
                },
                new Category
                {
                    CategoryId = -4,
                    CategoryName = "Default 04"
                }

                );
        }
    }
}

﻿using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebAPI.Models;

namespace WebAPI.Data
{
    internal class DbInitializer
    {
        // https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-7.0
        internal static void Initialize(SignatureContext context)
        {
            ArgumentNullException.ThrowIfNull(context, nameof(context));
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Students.Any())
            {
                return;   // DB has been seeded
            }

            var promotions = new Promotion[]
            {
                new Promotion{ Name="MS2D" },
                new Promotion{ Name="ERIS" },
                new Promotion{ Name="I" }
            };
            foreach (Promotion promotion in promotions)
            {
                context.Promotions.Add(promotion);
            }
            context.SaveChanges();

            var groups = new Group[]
            {
                new Group{ Name="FA" },
                new Group{ Name="FE" }
            };
            foreach (Group group in groups)
            {
                context.Groups.Add(group);
            }
            context.SaveChanges();

            var students = new Student[]
            {
                new Student{ Firstname="Carson", Lastname="Alexander", IsActive=true, MacAdress="82A70095380B", GroupID=1, PromotionID=1 },
                new Student{ Firstname="Meredith", Lastname="Alonso", IsActive=false, MacAdress="82A70095380B", GroupID=1, PromotionID=2 },
                new Student{ Firstname="Arturo", Lastname="Anand", IsActive=false, MacAdress="82A70095380B", GroupID=2, PromotionID=1 },
                new Student{ Firstname="Gytis", Lastname="Barzdukas", GroupID=2, PromotionID=1 },
                new Student{ Firstname="Yan", Lastname="Li", GroupID=2, PromotionID=3 }
            };
            foreach (Student student in students)
            {
                context.Students.Add(student);
            }
            context.SaveChanges();

            var signatures = new Signature[]
            {
                new Signature{ CreatedAt=DateTime.Parse("2022-09-01"), IsPresent=false, StudentID=1},
                new Signature{ CreatedAt=DateTime.Parse("2022-09-02"), IsPresent=true, StudentID=1},
                new Signature{ CreatedAt=DateTime.Parse("2022-09-03"), IsPresent=true, StudentID=1},
                new Signature{ CreatedAt=DateTime.Parse("2022-09-04"), IsPresent=true, StudentID=1},
                new Signature{ CreatedAt=DateTime.Parse("2005-02-02"), IsPresent=false, StudentID=2},
                new Signature{ CreatedAt=DateTime.Parse("2005-02-03"), IsPresent=false, StudentID=2},
                new Signature{ CreatedAt=DateTime.Parse("2021-01-01"), IsPresent=true, StudentID=3},
                new Signature{ CreatedAt=DateTime.Parse("2020-07-10"), IsPresent=false, StudentID=4},
                new Signature{ CreatedAt=DateTime.Parse("2020-07-11"), IsPresent=true, StudentID=4},
                new Signature{ CreatedAt=DateTime.Parse("2018-05-18"), IsPresent=false, StudentID=5},
            };
            foreach (Signature signature in signatures)
            {
                context.Signatures.Add(signature);
            }
            context.SaveChanges();
        }
    }
}
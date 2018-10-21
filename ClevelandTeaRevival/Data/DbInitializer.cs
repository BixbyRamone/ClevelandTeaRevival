using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClevelandTeaRevival.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

// var context = services.GetRequiredService<ApplicationDbContext>();

namespace ClevelandTeaRevival.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider services, ApplicationDbContext _context)
        {
            _context.Database.EnsureCreated();
            //==========================
            var roleManager = services
                .GetRequiredService<RoleManager<IdentityRole>>();
            await EnsureRolesAsync(roleManager);
            //==========================
            var userManager = services
                .GetRequiredService<UserManager<IdentityUser>>();
            await EnsureTestAdminAsync(userManager);
            //==========================


            //Look for any customers
            if (_context.Teas.Any())
            {
                return; //customers found, db has already been seeded
            }

            var administrators = new Administrator[]
            {
                new Administrator{FirstName="Amber", LastName="A"},
                new Administrator{FirstName="Mike", LastName="M"}
            };

            foreach (Administrator a in administrators)
            {
                _context.Admin.Add(a);
            }
            _context.SaveChanges();

            var customers = new Customer[]
            {
            new Customer{FirstName="Carson",LastName="Alexander",MailingAddress="123 Fake Street, Cleveland, OH, 44108, U.S.A."},
            new Customer{FirstName="Meredith",LastName="Alonso",MailingAddress="666 Satan Lane, Hot Town, FL, Luciferia"},
            new Customer{FirstName="Arturo",LastName="Anand",MailingAddress="999 Rancid Road, , Los Angeles, CA, U.S.A." },
            new Customer{FirstName="Gytis",LastName="Barzdukas",MailingAddress="filler address"},
            new Customer{FirstName="Yan",LastName="Li",MailingAddress="filler address"},
            new Customer{FirstName="Peggy",LastName="Justice",MailingAddress="filler address"},
            new Customer{FirstName="Laura",LastName="Norman",MailingAddress="filler address"},
            new Customer{FirstName="Nino",LastName="Olivetto",MailingAddress="filler address"}
            };
            foreach (Customer c in customers)
            {
                _context.Customers.Add(c);
            }
            _context.SaveChanges();

            var blogPosts = new BlogPost[]
            {
                new BlogPost{Title="Yes Kombucha", Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris"},
                new BlogPost{Title="No Kombucha", Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris"},
                new BlogPost{Title="Yes Kombucha", Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris"},
                new BlogPost{Title="No Kombucha", Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris"},
                new BlogPost{Title="Yes Kombucha", Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris"}
            };
            foreach (BlogPost bp in blogPosts)
            {
                _context.BlogPosts.Add(bp);
            }
            _context.SaveChanges();

            var events = new Event[]
            {
                new Event{EventName="Kombucha Class", EventDate=DateTime.Parse("2018-09-27")},
                new Event{EventName="Kombucha Class", EventDate=DateTime.Parse("2018-10-20")},
            };
            foreach (Event e in events)
            {
                _context.Events.Add(e);
            }
            _context.SaveChanges();

            var foodItems = new FoodItem[]
            {
                new FoodItem{Name="Toast", Price=5.55m},
                new FoodItem{Name="Salad Bowl", Price=8}
            };
            foreach (FoodItem fi in foodItems)
            {
                _context.FoodItems.Add(fi);
            }
            _context.SaveChanges();

            var teas = new Tea[]
            {
                new Tea{Name="Silver Needles", Category ="White Teas", PricePerCup =4.5m, PricePerPot =5.5m, PricePerOz =8, Description ="China – light honey, floral umami finish"},
                new Tea{Name="Peony", Category="White Teas", PricePerCup=3, PricePerPot=5, PricePerOz=6, Description="China – cooling nectarine flavor, mild hay"},
                new Tea{Name="Jasmine Pearl", Category="Green Teas", PricePerCup=4.5m, PricePerPot=5.5m, PricePerOz=11, Description="China – jasmine infused green tea pearls"},
                new Tea{Name="Dragonwell", Category="Green Teas", PricePerCup=4, PricePerPot=5.5m, PricePerOz=9, Description="China – pan cooked, bright, sweet & savory finish"},
                new Tea{Name="Gunpowder", Category="Green Teas", PricePerCup=3, PricePerPot=5, PricePerOz=6, Description="China – medium roast, charcoal, honey"},
                new Tea{Name="Jade Cloud", Category="Green Teas", PricePerCup=3.5m, PricePerPot=5, PricePerOz=6, Description="China – sweet cream, grassy"},
                new Tea{Name="Gaba Green", Category="Green Teas", PricePerCup=5.5m, PricePerPot=6.5m, PricePerOz=12, Description="Taiwan – velvety, herbaceous"},
                new Tea{Name="Genmaicha", Category="Green Teas", PricePerCup=3.5m, PricePerPot=5, PricePerOz=6, Description="Japan – steamed with brown rice, toasted, oceanic"},
                new Tea{Name="Genmai Matcha", Category="Green Teas", PricePerCup=3, PricePerPot=5, PricePerOz=6, Description="Japan – steamed with brown rice, tossed in matcha, bright vegetal"},
                new Tea{Name="Gyokuro", Category="Green Teas", PricePerCup=5.5m, PricePerPot=6, PricePerOz=12, Description="Japan – umami, oceanic, spring"},
                new Tea{Name="Kukicha", Category="Green Teas", PricePerCup=4, PricePerPot=5, PricePerOz=6, Description="Japan – bright, sweet flavor"},
                new Tea{Name="Sencha", Category="Green Teas", PricePerCup=3.5m, PricePerPot=5, PricePerOz=7, Description="Japan – grassy, vegetal"},
                new Tea{Name="Sencha Infused With Matcha", Category="Green Teas", PricePerCup=4.5m, PricePerPot=5.5m, PricePerOz=7.5m, Description="Japan – grassy, tossed in matcha"},
                new Tea{Name="Matcha", Category="Green Teas", PricePerCup=3.5m, PricePerPot=5.5m, PricePerOz=16, PricePerLb=30, Description="Japan"}

            };
            foreach (Tea tea in teas)
            {
                _context.Teas.Add(tea);
            }
            _context.SaveChanges();

        }


        private static async Task EnsureRolesAsync(
    RoleManager<IdentityRole> roleManager)
        {
            var alreadyExists = await roleManager
                .RoleExistsAsync(Constants.AdministratorRole);

            if (alreadyExists) return;

            await roleManager.CreateAsync(
                new IdentityRole(Constants.AdministratorRole));
        }

        private static async Task EnsureTestAdminAsync(
    UserManager<IdentityUser> userManager)
        {
            var testAdmin = await userManager.Users
                .Where(x => x.UserName == "amber@tootmail.com")
                .SingleOrDefaultAsync();

            if (testAdmin != null) return;

            testAdmin = new IdentityUser
            {
                UserName = "amber@tootmail.com",
                Email = "amber@tootmail.com"
            };
            await userManager.CreateAsync(
                testAdmin, "R@m0n@");
            await userManager.AddToRoleAsync(
                testAdmin, Constants.AdministratorRole);
        }

    }


}

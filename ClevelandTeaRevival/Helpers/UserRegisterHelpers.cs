using ClevelandTeaRevival.Data;
using ClevelandTeaRevival.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClevelandTeaRevival.Helpers
{
    public class UserRegisterHelpers
    {
        private readonly ApplicationDbContext _context;

        public UserRegisterHelpers(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> RegisterCustomer(IdentityUser currentUser)
        {
            var customer = await _context.Customers
                                .Where(c => c.AspNetUserId == currentUser.Id)
                                .ToArrayAsync();
                if (customer.Length < 1)
            {
                Customer customerToAdd = new Customer();
                customerToAdd.AspNetUserId = currentUser.Id;

                _context.Add(customerToAdd);

                await _context.SaveChangesAsync();
                
            }
           

            return null;
        }
    }
}

﻿using DataAccess.Abstract;
using DataAccess.EFCore;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly RepositoryContext _context;

        public UserRoleRepository(RepositoryContext context)
        {
            _context = context;
        }

        public List<UserRole> GetAll(Guid UserId)
        {
            return _context.UserRoles
                .Include(x => x.User)
                .Include(x => x.Role)
                .ToList();
        }
    }
}

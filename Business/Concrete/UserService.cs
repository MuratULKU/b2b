using Business.Abstract;
using DataAccess.EFCore;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly RepositoryContext
        public List<UserAcount> GetAllUser()
        {
            throw new NotImplementedException();
        }
    }
}

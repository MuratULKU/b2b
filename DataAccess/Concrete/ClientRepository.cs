using DataAccess.Abstract;
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
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(RepositoryContext context) : base(context)
        {
        }
    }
}

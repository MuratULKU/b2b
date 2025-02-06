using DataAccess.Abstract;
using DataAccess.EFCore;
using Entity;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Concrete
{
    public class DocumentNoRepository : Repository<DocumentNo>, IDocumentNoRepository
    {
        public DocumentNoRepository(RepositoryContext context) : base(context)
        {
        }

    }
}

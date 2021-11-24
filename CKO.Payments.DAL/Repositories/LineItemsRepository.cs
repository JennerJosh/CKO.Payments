using CKO.Payments.DAL.Repositories.Interfaces;
using CKO.Payments.Data;
using CKO.Payments.Data.DTO;

namespace CKO.Payments.DAL.Repositories
{
    internal class LineItemsRepository : BaseRepository<LineItem>, ILineItemsRepository
    {
        public LineItemsRepository(CkoContext context) : base(context)
        {

        }
    }
}

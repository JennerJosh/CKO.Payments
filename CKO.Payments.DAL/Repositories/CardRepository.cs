using CKO.Payments.DAL.Repositories.Interfaces;
using CKO.Payments.Data;
using CKO.Payments.Data.DTO;

namespace CKO.Payments.DAL.Repositories
{
    internal class CardRepository : BaseRepository<Card>, ICardRepository
    {
        public CardRepository(CkoContext context) : base(context)
        {

        }
    }
}

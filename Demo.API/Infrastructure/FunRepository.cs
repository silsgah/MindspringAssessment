using Entity;
using Entity.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class FunRepository : IFunTranslateRepository
    {
        private readonly BackContext _backContext;
        public FunRepository(BackContext context)
        {
            _backContext = context;
        }
        public async Task<IReadOnlyList<TblResponse>> GetFunAsync()
        {
            return await _backContext.TblResponses.OrderByDescending(response => response.Id)
                            .ToListAsync();
        }
    }
}

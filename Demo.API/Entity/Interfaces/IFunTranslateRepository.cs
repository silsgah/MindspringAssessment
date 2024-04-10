

namespace Entity.Interfaces
{
    public interface IFunTranslateRepository
    {
        Task<IReadOnlyList<TblResponse>> GetFunAsync();
    }
}

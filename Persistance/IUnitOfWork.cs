using System.Threading.Tasks;

namespace vega_be.Persistance
{
    public interface IUnitOfWork
    {
        Task Complete();
    }
}
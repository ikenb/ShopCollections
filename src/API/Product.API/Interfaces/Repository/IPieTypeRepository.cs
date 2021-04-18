using System.Collections.Generic;

namespace Product.API.Interfaces.Repository
{
    public interface IPieTypeRepository
    {
        ICollection<IPieTypeRepository> GetAllPieTypes { get; }
    }
}

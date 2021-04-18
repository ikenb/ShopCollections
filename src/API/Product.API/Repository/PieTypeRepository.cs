using Product.API.Interfaces.Repository;
using System;
using System.Collections.Generic;

namespace Product.API.Repository
{
    public class PieTypeRepository : IPieTypeRepository
    {
        public ICollection<IPieTypeRepository> GetAllPieTypes => throw new NotImplementedException();
    }
}

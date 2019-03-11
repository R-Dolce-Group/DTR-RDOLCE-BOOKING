using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDolce.Interfaces
{
    public interface IUnitFieldsDataProvider
    {
        Task<IEnumerable<UnitFields>> GetUnitFields();

        Task<UnitFields> GetUnitFields(int unitId);

        Task AddUnitFields(UnitFields unitFields);

        Task UpdateUnitFields(UnitFields unitFields);

        Task DeleteUnitFields(int unitId);
    }
}

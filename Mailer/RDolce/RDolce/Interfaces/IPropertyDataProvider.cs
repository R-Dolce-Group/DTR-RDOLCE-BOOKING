using RDolce.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RDolce.Interfaces
{
   public interface IPropertyDataProvider
    {
      
            Task<IEnumerable<RDolce.Property.PropertFields>> GetPropertyFields();

            Task<RDolce.Property.PropertFields> GetPropertyFields(string propertyId);

            Task AddPropertyFields(RDolce.Property.PropertFields propertyFields);

            Task UpdatePropertyFields(RDolce.Property.PropertFields propertyFields);

            Task DeletePropertyFields(int propertyId);
        
    }
}

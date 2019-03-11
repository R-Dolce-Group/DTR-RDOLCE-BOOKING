using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDolce.Interfaces
{
    public interface IReservationFieldsDataProvider
    {
        Task<IEnumerable<ReservationFields>> GetReservationFields();

        Task<ReservationFields> GetReservationFields(int reservationId);

        Task<List<ReservationFields>> GetUnProcessed();

        Task AddReservationFields(ReservationFields reservationFields);

        Task UpdateReservationFields(ReservationFields reservationFields);

        Task DeleteReservationFields(int reservationId);

        Task SetProcessed(string reservationId);
    }
}

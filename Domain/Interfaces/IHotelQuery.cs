using Domain.DTOs;

namespace Domain
{
    public interface IHotelQuery
    {
        IEnumerable<HotelDTO> SearchHotels(SearchCriteria dto);
    }
}
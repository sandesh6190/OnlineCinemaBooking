using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleAuth.Constants;
using SimpleAuth.Models;

namespace SimpleAuth.ViewModels.ShowSeats;
public class ShowSeatIndexVm
{
    public List<ShowSeat> ShowSeats;
    public string SeatName { get; set; }
    public string SearchByShowSeatStatus { get; set; }
    //for selectlist
    public SelectList SearchByShowSeatStatusSelectList()
    {
        return new SelectList(
            SeatStatusConstants.SeatStatusList,
            // nameof(MovieStatusConstants.StatusList),
            // nameof(MovieStatusConstants.StatusList),
            SearchByShowSeatStatus
        );
    }
}

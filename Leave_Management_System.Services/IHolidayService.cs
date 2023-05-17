using Leave_Management_System.Data.Models;
namespace Leave_Management_System.Services
{
    public interface IHolidayService
    {
        IEnumerable<Holiday> GetHolidays();
        Holiday GetHolidayById(int id);
        void AddHoliday(Holiday holiday);
        void UpdateHoliday(Holiday holiday);
        void DeleteHoliday(int id);
    }
}

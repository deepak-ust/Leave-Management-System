using Leave_Management_System.Data.Models;
namespace Leave_Management_System.Repositories
{
    public interface IHolidayRepository
    {
        IEnumerable<Holiday> GetAllHolidays();
        Holiday GetHolidayById(int id);
        void CreateHoliday(Holiday holiday);
        void UpdateHoliday(Holiday holiday);
        void DeleteHoliday(Holiday holiday);
        void SaveChanges();
    }
}

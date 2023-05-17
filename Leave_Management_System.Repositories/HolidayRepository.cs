using Leave_Management_System.Data;
using Leave_Management_System.Data.Models;
namespace Leave_Management_System.Repositories
{
    public class HolidayRepository : IHolidayRepository
    {
        private readonly LeaveManagementDbContext _context;
        public HolidayRepository(LeaveManagementDbContext context)
        {
            _context = context;
        }
        public void CreateHoliday(Holiday holiday)
        {
            _context.Holidays.Add(holiday);
        }
        public void DeleteHoliday(Holiday holiday)
        {
            _context.Holidays.Remove(holiday);
        }
        public IEnumerable<Holiday> GetAllHolidays()
        {
            return _context.Holidays.ToList();
        }
        public Holiday GetHolidayById(int id)
        {
            return _context.Holidays.FirstOrDefault(c => c.Id == id);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void UpdateHoliday(Holiday holiday)
        {
            _context.Holidays.Update(holiday);
        }
    }
}

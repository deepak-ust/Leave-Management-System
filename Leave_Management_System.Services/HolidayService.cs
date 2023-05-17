using Leave_Management_System.Data.Models;
using Leave_Management_System.Repositories;
namespace Leave_Management_System.Services
{
    public class HolidayService : IHolidayService
    {
        private readonly IHolidayRepository _holidayRepository;
        public HolidayService(IHolidayRepository holidayRepository)
        {
            _holidayRepository = holidayRepository;
        }
        public void AddHoliday(Holiday holidayObj)
        {
            var holiday = new Holiday
            {
                Id = holidayObj.Id,
                Name = holidayObj.Name,
                Date = holidayObj.Date,
            };
            _holidayRepository.CreateHoliday(holiday);
            _holidayRepository.SaveChanges();
        }
        public void DeleteHoliday(int id)
        {
            var calendar = _holidayRepository.GetHolidayById(id);
            if (calendar != null)
            {
                _holidayRepository.DeleteHoliday(calendar);
                _holidayRepository.SaveChanges();
            }
        }
        public Holiday GetHolidayById(int id)
        {
            var holiday = _holidayRepository.GetHolidayById(id);
            if (holiday == null)
            {
                return null;
            }
            return new Holiday
            {
                Id = holiday.Id,
                Name = holiday.Name,
                Date = holiday.Date,
            };
        }
        public IEnumerable<Holiday> GetHolidays()
        {
            var holidays = _holidayRepository.GetAllHolidays();
            return holidays.Select(c => new Holiday
            {
                Id = c.Id,
                Name = c.Name,
                Date = c.Date
            });
        }
        public void UpdateHoliday(Holiday holidayObj)
        {
            var holiday = _holidayRepository.GetHolidayById(holidayObj.Id);
            if (holiday != null)
            {
                holiday.Id = holidayObj.Id;
                holiday.Name = holidayObj.Name;
                holiday.Date = holidayObj.Date;
                _holidayRepository.UpdateHoliday(holiday);
                _holidayRepository.SaveChanges();
            }
        }
    }
}

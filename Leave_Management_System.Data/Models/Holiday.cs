using System.ComponentModel.DataAnnotations;
namespace Leave_Management_System.Data.Models
{
    public class Holiday
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
    }
}

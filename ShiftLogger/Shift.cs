using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftLogger
{
    public class Shift
    {
        public int shiftId { get; set; }
        [Required]
        public int employeeId { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }

        public TimeSpan Duration { get;  set; }


    }
}

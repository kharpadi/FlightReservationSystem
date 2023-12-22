using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightReservationSystem.Models
{
	public class Reservation
	{
		[Key]
		public int Id { get; set; }
        [ForeignKey("User")]
		[Required]
		public int UserId { get; set; }
        [ForeignKey("Schedule")]
		[Required]
		public int ScheduleId { get; set; }
        [Required]
		public User User { get; set; }
		[Required]
		public Schedule Schedule { get; set; }
		[Required]
		public string SeatNumber { get; set; }
	}
}

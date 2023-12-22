using System.ComponentModel.DataAnnotations;

namespace FlightReservationSystem.Models
{
	public class Route
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage ="Kalkış Yeri belirleyiniz!!")]
		public string DepartureCity { get; set; }
		[Required(ErrorMessage ="Varış Yeri belirleyiniz!!")]
		public string ArrivalCity { get; set; }

	}
}

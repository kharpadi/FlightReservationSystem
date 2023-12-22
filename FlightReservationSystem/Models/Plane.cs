using System.ComponentModel.DataAnnotations;

namespace FlightReservationSystem.Models
{
	public class Plane
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage ="Isim alanı boş bırakılamaz")]
		public string Name { get; set; }
		[Required(ErrorMessage ="Uçak tipi seçiniz")]
		public string Type { get; set; }
		public int TotalSeat { get; set; }
	}
}

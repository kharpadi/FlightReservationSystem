using System.ComponentModel.DataAnnotations;

namespace FlightReservationSystem.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage ="İsim alanı boş bırakılamaz")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Soyisim alanı boş bırakılamaz")]
		public string Surname { get; set; }
        [Required(ErrorMessage = "Email alanı boş bırakılamaz")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz")]
		public string Password { get; set; }
		[Required(ErrorMessage = "rol alanı boş bırakılamaz")]
		public string role { get; set; }
		public List<Reservation>? Reservations { get; set; }
	}	
}

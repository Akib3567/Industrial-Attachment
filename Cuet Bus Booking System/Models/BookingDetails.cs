namespace Cuet_Bus_Booking_System.Models
{
    public class BookingDetails
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int BusId { get; set; }
        public string BusName { get; set; }
        public int SeatNumber { get; set; }
        public string ScheduleTime { get; set; }
        public DateTime BookingDate { get; set; }
    }
}

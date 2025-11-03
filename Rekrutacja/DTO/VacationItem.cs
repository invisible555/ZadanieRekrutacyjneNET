namespace Rekrutacja.DTO
{
    public class VacationItem
    {
        public DateTime DateSince { get; set; }
        public DateTime DateUntil { get; set; }
        public int Duration => (DateUntil - DateSince).Days + 1;
    }
}

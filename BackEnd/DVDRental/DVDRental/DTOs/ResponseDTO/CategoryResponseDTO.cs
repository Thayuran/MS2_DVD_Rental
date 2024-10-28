using DVDRental.Entities;

namespace DVDRental.DTOs.ResponseDTO
{
    public class CategoryResponseDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<MovieDvd> DVDs { get; set; } = new List<MovieDvd>();
    }
}

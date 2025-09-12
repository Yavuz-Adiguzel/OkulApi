using OkulApi.Models;

namespace OkulApi.Dtos
{
    public class OgrenciDto
    {
        public int Id { get; set; }
        public string AdiSoyadi { get; set; }

        public List<DersDto> Dersler { get; set; }
    }
}

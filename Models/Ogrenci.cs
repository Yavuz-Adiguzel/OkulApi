namespace OkulApi.Models
{
    public class Ogrenci
    {
        public int Id { get; set; }
        public string AdiSoyadi { get; set; }

        public ICollection<OgrenciDers> OgrenciDersler { get; set; }
    }
}

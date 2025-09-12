namespace OkulApi.Models
{
    public class Ders
    {
        public int DersId { get; set; }
        public string DersAdi { get; set; }
        public ICollection<OgrenciDers> OgrenciDersler { get; set; }
    }
}

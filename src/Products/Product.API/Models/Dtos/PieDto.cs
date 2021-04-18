namespace Product.API.Models.Dtos
{
    public class PieDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public byte[] Image { get; set; }
        public int TypeId { get; set; }
        public PieType Type { get; set; }
    }
}

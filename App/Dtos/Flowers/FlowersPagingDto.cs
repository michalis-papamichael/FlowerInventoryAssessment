namespace App.Dtos.Flowers
{
    public class FlowersPagingDto
    {
        public FlowersPagingDto()
        {
            Flowers = new List<FlowerDto>();
        }
        public List<FlowerDto> Flowers { get; set; }
        public int TotalFlowers { get; set; }
    }
}

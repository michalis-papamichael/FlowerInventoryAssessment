using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceDtos.Flowers
{
    public class SFlowersPagingDto
    {
        public SFlowersPagingDto()
        {
            Flowers = new List<SFlowerDto>();
        }
        public List<SFlowerDto> Flowers { get; set; }
        public int TotalFlowers { get; set; }
    }
}

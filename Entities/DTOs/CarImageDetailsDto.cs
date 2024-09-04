using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CarImageDetailsDto : IDto
    {
        public int ImageId { get; set; }
        public int CarId { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int brandId { get; set; }


    }
}

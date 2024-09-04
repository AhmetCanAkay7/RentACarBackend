using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImageController : ControllerBase
    {
        ICarImageService _carImageService;
        public CarImageController(ICarImageService carImageService)
        {
            _carImageService = carImageService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _carImageService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyimageid")]
        public IActionResult GetByImageId(int imageId)
        {
            var result = _carImageService.GetByImageID(imageId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbycarid")]

        public IActionResult GetByCarId(int carId)
        {
            var result = _carImageService.GetImagesByCarId(carId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getcarimagesdetail")]

        public IActionResult GetCarImagesDetails()
        {
            var result = _carImageService.GetCarImagesDetail();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]

        public IActionResult AddImage([FromForm] IFormFile file, [FromForm] CarImage carImage)
        {
            var result = _carImageService.Insert(file, carImage);

            if (result.IsSuccess) return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult UpdateImage([FromForm] IFormFile file, [FromForm] CarImage carImage)
        {
            var result = _carImageService.Update(file, carImage);

            if (result.IsSuccess) return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("delete/{id}")]
        public IActionResult DeleteImage(int carImageId)
        {
            var carDeleteImage = _carImageService.GetByImageID(carImageId).Data;
            var result = _carImageService.Delete(carDeleteImage);
            if (result.IsSuccess) return Ok(result);
            return BadRequest(result);
        }

    }
}

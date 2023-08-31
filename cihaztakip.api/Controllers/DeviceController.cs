using cihaztakip.api.DTO;
using cihaztakip.business.Abstract;
using cihaztakip.entity;
using Microsoft.AspNetCore.Mvc;

namespace cihaztakip.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceController : ControllerBase
    {
        private IDeviceService _deviceService;
        public DeviceController(IDeviceService _deviceService)
        {
            _deviceService = _deviceService;
        }
        [HttpGet]
        public async Task<IActionResult> GetDevices()
        {
            var products = await _deviceService.GetAll();
            var productDTO = new List<DeviceDTO>();
            foreach (var p in products)
            {
                productDTO.Add(DeviceToDTO(p));
            }
            return Ok(productDTO);//200
        }

        private static DeviceDTO DeviceToDTO(Device d)
        {
            return new DeviceDTO
            {
                DeviceId = d.DeviceId,
                Name = d.Name,
      
            };
        }
    }
}

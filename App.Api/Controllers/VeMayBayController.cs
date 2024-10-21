using App.Data.Entities;
using App.Data;
using App.Data.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeMayBayController : ControllerBase
    {
        private readonly IVeMayBayRepo _veMayBayRepo;
        private readonly IValidator<VeMayBay> _validator;
        private readonly AppDbContext _context;

        public VeMayBayController(IVeMayBayRepo veMayBayRepo, IValidator<VeMayBay> validator, AppDbContext context)
        {
            _veMayBayRepo = veMayBayRepo;
            _validator = validator;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var veMayBays = await _veMayBayRepo.GetAllAsync();
            return Ok(veMayBays);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var veMayBay = await _veMayBayRepo.GetByIdAsync(id);
            if (veMayBay == null)
            {
                return NotFound();
            }
            return Ok(veMayBay);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(VeMayBay veMayBay)
        {
            var validatorResult = await _validator.ValidateAsync(veMayBay);
            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage,
                }));
            }

            await _veMayBayRepo.AddAsync(veMayBay);
            return Ok(veMayBay);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, VeMayBay veMayBay)
        {
            if (id != veMayBay.Id)
            {
                return BadRequest("Id không khớp");
            }

            var validatorResult = await _validator.ValidateAsync(veMayBay);
            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage,
                }));
            }

            await _veMayBayRepo.UpdateAsync(veMayBay);
            return Ok(veMayBay);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _veMayBayRepo.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("tinh-tong-gia-ve")]
        public IActionResult TinhTongGiaVe([FromBody] TinhTongGiaVe request)
        {
            if (request.SoLuongHanhKhach <= 0 || request.GiaVeMoiNguoi <= 0)
            {
                return BadRequest("Số lượng hành khách và giá vé mỗi người phải lớn hơn 0.");
            }

            decimal tongGiaVe = request.SoLuongHanhKhach * request.GiaVeMoiNguoi;
            return Ok(new { TongGiaVe = tongGiaVe });
        }

    }
}

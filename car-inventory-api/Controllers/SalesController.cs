using AutoMapper;
using car_inventory_api.DTOs;
using car_inventory_api.Models;
using car_inventory_api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace car_inventory_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IMapper _mapper;

        public SalesController(ISalesRepository salesRepository, IMapper mapper)
        {
            _salesRepository = salesRepository;
            _mapper = mapper;
        }

        // GET: /api/sales/GetAllSales
        [HttpGet("GetAllSales")]
        public async Task<IActionResult> GetAllSales()
        {
            var sales = await _salesRepository.GetAllSalesAsync();
            var salesDto = _mapper.Map<IEnumerable<SaleDTO>>(sales);
            return Ok(salesDto);
        }

        // GET: /api/sales/Details/{SaleID}
        [HttpGet("Details/{SaleID}")]
        public async Task<IActionResult> GetSaleDetails(string SaleID)
        {
            var sale = await _salesRepository.GetSaleDetailsAsync(SaleID);
            if (sale == null)
                return NotFound($"Sale with ID {SaleID} not found.");

            var saleDto = _mapper.Map<SaleDTO>(sale);
            return Ok(saleDto);
        }
        
        // POST: /api/sales/Create
        [HttpPost("Create")]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleDTO createSaleDTO)
        {
            if (createSaleDTO == null)
                return BadRequest("Invalid sale data.");

            var sale = _mapper.Map<Sales>(createSaleDTO);
            await _salesRepository.CreateSaleAsync(sale);
            return CreatedAtAction(nameof(GetSaleDetails), new { SaleID = sale.SaleID }, sale);
        }

        // PUT: /api/sales/Update/{SaleID}
        [HttpPut("Update/{SaleID}")]
        public async Task<IActionResult> UpdateSale(string SaleID, [FromBody] UpdateSaleDTO updateSaleDTO)
        {
            if (updateSaleDTO == null)
                return BadRequest("Invalid sale update data.");

            var sale = await _salesRepository.GetSaleDetailsAsync(SaleID);
            if (sale == null)
                return NotFound($"Sale with ID {SaleID} not found.");

            _mapper.Map(updateSaleDTO, sale);
            await _salesRepository.UpdateSaleAsync(sale);
            return Ok($"Sale with ID {SaleID} updated successfully.");
        }

        // DELETE: /api/sales/Delete/{SaleID}
        [HttpDelete("Delete/{SaleID}")]
        public async Task<IActionResult> DeleteSale(string SaleID)
        {
            var sale = await _salesRepository.GetSaleDetailsAsync(SaleID);
            if (sale == null)
                return NotFound($"Sale with ID {SaleID} not found.");

            await _salesRepository.DeleteSaleAsync(SaleID);
            return Ok($"Sale with ID {SaleID} deleted successfully.");
        }
    }
}


using AutoMapper;
using Customer_Balance_Paltform.DTOS;
using Customer_Balance_Paltform.Models;
using Customer_Balance_Paltform.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Customer_Balance_Paltform.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class CFPContrller : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICustomerRepo _repo;
    public CFPContrller(IMapper mapper, ICustomerRepo repo) 
    {
        _mapper = mapper;
        _repo = repo;
    }
    [HttpPost]
    public async Task<IActionResult> CreateCustomer(CustomerDto customerDto)
    {
        var customer = _mapper.Map<TCustomer>(customerDto);
        var createdCustomer = await _repo.CreateCustomerAsync(customer);
        var createdCustomerDto = _mapper.Map<CustomerDto>(createdCustomer);
        return CreatedAtAction(nameof(GetCustomerById), new { id = createdCustomerDto.CustomerID }, createdCustomerDto);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(int id)
    {
        var customer = await _repo.GetCustomerByIdAsync(id);
        if (customer == null)
        {
            return NotFound();
        }
        var customerDto = _mapper.Map<CustomerDto>(customer);
        return Ok(customerDto);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(int id, CustomerDto customerDto)
    {
        if (id != customerDto.CustomerID)
        {
            return BadRequest();
        }

        var customer = _mapper.Map<TCustomer>(customerDto);
        await _repo.UpdateCustomerAsync(customer);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        await _repo.DeleteCustomerAsync(id);
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCustomers()
    {
        var customers = await _repo.GetAllCustomersAsync();
        var customerDtos = _mapper.Map<IEnumerable<CustomerDto>>(customers);
        return Ok(customerDtos);
    }
}
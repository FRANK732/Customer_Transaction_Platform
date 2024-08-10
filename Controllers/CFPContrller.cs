using AutoMapper;
using Customer_Balance_Paltform.DTOS;
using Customer_Balance_Paltform.Models;
using Customer_Balance_Paltform.Models.RequestModel;
using Customer_Balance_Paltform.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Customer_Balance_Paltform.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class CFPContrller : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICustomerRepo _repo;
    private readonly ITransactionRepo _transactionRepo;

    public CFPContrller(IMapper mapper, ICustomerRepo repo, ITransactionRepo transactionRepo) 
    {
        _mapper = mapper;
        _repo = repo;
        _transactionRepo = transactionRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCustomers()
    {
        var customers = await _repo.GetAllCustomersAsync();
        var customerDtos = _mapper.Map<IEnumerable<CustomerDto>>(customers);
        return Ok(customerDtos);
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


    [HttpGet]
    public async Task<IActionResult> GetTransactionById(string transactionID)
    {
        var trans = await _transactionRepo.GetTransactionByIdAsync(transactionID);
        return Ok(trans);
    }
    
    [HttpGet]
    public async Task<IActionResult> GenerateTransactionReportAsync(int customerId, DateTime? startDate, DateTime? endDate)
    {
        var trans = await _transactionRepo.GenerateTransactionReportAsync(customerId,startDate,endDate);
        return Ok(trans);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCustomer(Customer rCustomer)
    {
        var customer = _mapper.Map<TCustomer>(rCustomer);
        var createdCustomer = await _repo.CreateCustomerAsync(customer);
        var createdCustomerDto = _mapper.Map<CustomerDto>(createdCustomer);
        return CreatedAtAction(nameof(GetCustomerById), new { id = createdCustomerDto.CustomerID }, createdCustomerDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction([FromBody] Transaction rTransaction)
    {
        try
        {
            var transaction = _mapper.Map<TTransactions>(rTransaction);

            var createTrans =await _transactionRepo.CreateTransactionAsync(transaction);
            var transactionDetail = _mapper.Map<TTransactions>(createTrans);
            return CreatedAtAction(nameof(GetTransactionById), new { transactionID = transactionDetail.UniqueNumber },transactionDetail);
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(int id, Customer rCustomer)
    {
        var customer = _mapper.Map<TCustomer>(rCustomer);
        var res = await _repo.UpdateCustomerAsync(customer);
        return res? Ok(res) : NotFound(res);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var res=await _repo.DeleteCustomerAsync(id);
        return res? Ok(res) : NotFound(res);
    }

   
}
using AutoMapper;
using Customer_Balance_Paltform.DTOS;
using Customer_Balance_Paltform.Models;

namespace Customer_Balance_Paltform.Mappers;

public class CFPMapper : Profile
{
    public CFPMapper()
    {
        // Customer mapping
        CreateMap<TCustomer, CustomerDto>();
        CreateMap<CustomerDto, TCustomer>();

        // Contact Info mapping
        CreateMap<TContactInfo, ContactInfoDto>();
        CreateMap<ContactInfoDto, TContactInfo>();

        // Transaction mapping
        CreateMap<TTransactions, TransactionDto>();
        CreateMap<TransactionDto, TTransactions>();
    }
}
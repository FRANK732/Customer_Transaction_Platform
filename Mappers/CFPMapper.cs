using AutoMapper;
using Customer_Balance_Paltform.DTOS;
using Customer_Balance_Paltform.Models;
using Customer_Balance_Paltform.Models.RequestModel;

namespace Customer_Balance_Paltform.Mappers;

public class CFPMapper : Profile
{
    public CFPMapper()
    {
        // Customer mapping
        CreateMap<TCustomer, CustomerDto>().ReverseMap();
        CreateMap<Customer, TCustomer>().ReverseMap();

        // Contact Info mapping
        CreateMap<TContactInfo, ContactInfoDto>().ReverseMap();
        CreateMap<ContactInfo, TContactInfo>().ReverseMap();

        // Transaction mapping
        CreateMap<TTransactions, TransactionDto>()
            .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.TransactionType.ToString()));
        CreateMap<Transaction, TTransactions>().ReverseMap();

       
    }
}
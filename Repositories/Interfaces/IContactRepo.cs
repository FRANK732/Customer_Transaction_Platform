using Customer_Balance_Paltform.DTOS;

namespace Customer_Balance_Paltform.Repositories;

public interface IContactRepo
{
    Task<ContactInfoDto> CreateContactInfoAsync(ContactInfoDto contactInfo);
    Task<ContactInfoDto> GetContactInfoByIdAsync(int id);
    Task UpdateContactInfoAsync(ContactInfoDto contactInfo);
    Task DeleteContactInfoAsync(int id);
}
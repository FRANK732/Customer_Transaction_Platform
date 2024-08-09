using Customer_Balance_Paltform.Mappers;

namespace Customer_Balance_Paltform.Services;

public static class CFPService
{
    public static IServiceCollection AddCFPServices(this IServiceCollection service)
    {

        service.AddAutoMapper(typeof(CFPMapper));

        return service;
    }
}
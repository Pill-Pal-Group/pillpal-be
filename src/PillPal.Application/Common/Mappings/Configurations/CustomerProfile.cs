using PillPal.Application.Features.Customers;

namespace PillPal.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void CustomerProfile()
    {
        CreateMap<Customer, CustomerDto>()
            .ForMember(c => c.ApplicationUser, dest => dest.MapFrom(src => src.IdentityUser))
            .ForMember(c => c.MealTimeOffset, dest => dest.MapFrom(src => src.MealTimeOffset.TotalMinutes.ToString()))
            .ForMember(c => c.BreakfastTime, dest => dest.MapFrom(src => src.BreakfastTime.ToString("HH:mm")))
            .ForMember(c => c.LunchTime, dest => dest.MapFrom(src => src.LunchTime.ToString("HH:mm")))
            .ForMember(c => c.AfternoonTime, dest => dest.MapFrom(src => src.AfternoonTime.ToString("HH:mm")))
            .ForMember(c => c.DinnerTime, dest => dest.MapFrom(src => src.DinnerTime.ToString("HH:mm")))
            .ReverseMap();

        CreateMap<Customer, UpdateCustomerDto>().ReverseMap();

        CreateMap<Customer, CustomerMealTimeDto>()
            .ForMember(c => c.MealTimeOffset, dest => dest.MapFrom(src => src.MealTimeOffset.TotalMinutes.ToString()))
            .ForMember(c => c.BreakfastTime, dest => dest.MapFrom(src => src.BreakfastTime.ToString("HH:mm")))
            .ForMember(c => c.LunchTime, dest => dest.MapFrom(src => src.LunchTime.ToString("HH:mm")))
            .ForMember(c => c.AfternoonTime, dest => dest.MapFrom(src => src.AfternoonTime.ToString("HH:mm")))
            .ForMember(c => c.DinnerTime, dest => dest.MapFrom(src => src.DinnerTime.ToString("HH:mm")))
            .ReverseMap();

        CreateMap<UpdateCustomerMealTimeDto, Customer>()
            .ForMember(c => c.MealTimeOffset, dest => dest.MapFrom(src => TimeSpan.Parse(src.MealTimeOffset!)))
            .ForMember(c => c.BreakfastTime, dest => dest.MapFrom(src => TimeOnly.Parse(src.BreakfastTime!)))
            .ForMember(c => c.LunchTime, dest => dest.MapFrom(src => TimeOnly.Parse(src.LunchTime!)))
            .ForMember(c => c.AfternoonTime, dest => dest.MapFrom(src => TimeOnly.Parse(src.AfternoonTime!)))
            .ForMember(c => c.DinnerTime, dest => dest.MapFrom(src => TimeOnly.Parse(src.DinnerTime!)));

        CreateMap<CustomerDeviceTokenDto, Customer>().ReverseMap();
    }
}
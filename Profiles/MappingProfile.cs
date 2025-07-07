using AutoMapper;
using realstate.DAL.Models;
using realstate.Services.DTOs;

namespace realstate.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User mappings
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<User, UserDto>();

            // Property mappings
            CreateMap<CreatePropertyDto, Property>();
            CreateMap<UpdatePropertyDto, Property>();
            CreateMap<Property, PropertyDto>();

            // Booking mappings
            CreateMap<CreateBookingDto, Booking>();
            CreateMap<Booking, BookingDto>();

            // Payment mappings
            CreateMap<CreatePaymentDto, Payment>();
            CreateMap<Payment, PaymentDto>();

            // Notification mappings
            CreateMap<CreateNotificationDto, Notification>();
            CreateMap<Notification, NotificationDto>();
        }
    }
}


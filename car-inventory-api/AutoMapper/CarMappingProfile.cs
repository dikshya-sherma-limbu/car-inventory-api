
using car_inventory_api.DTOs;
using car_inventory_api.Models;
using AutoMapper;

namespace car_inventory_api.AutoMapper
{
    public class CarMappingProfile : Profile
    {
        public CarMappingProfile()
        {
            // Mapping from CreateCarDTO to Cars for creating a car
            CreateMap<CreateCarDTO, Cars>();

            // Mapping from Cars to ListCarDTO for listing cars
            CreateMap<Cars, ListCarDTO>();

            // Mapping from DeleteCarDTO to Cars for delete operation (input)
            CreateMap<DeleteCarDTO, Cars>();  // This is where you map user input (DTO) to model
            

            CreateMap<Sales, SaleDTO>();

            // CreateSaleDTO -> Sales
            CreateMap<CreateSaleDTO, Sales>();

            // UpdateSaleDTO -> Sales (for updating sale properties)
            CreateMap<UpdateSaleDTO, Sales>();

        }


    }
}

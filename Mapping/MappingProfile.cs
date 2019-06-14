using System.Runtime.InteropServices.ComTypes;
using System.Linq;
using AutoMapper;
using vega_be.DTOs;
using vega_be.Models;
using System.Collections.Generic;

namespace vega_be.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Domain to DTO
            CreateMap<Make, KeyValuePairDto>();
            CreateMap<CarModel, KeyValuePairDto>();
            CreateMap<Feature, KeyValuePairDto>();
            CreateMap<Vehicle, VehicleDto>()
                .ForMember(vr => vr.Make, opt => opt.MapFrom(v => v.Model.Make))
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactDto { Name = v.ContactName, Number = v.ContactNumber }))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(f => new KeyValuePairDto { Id = f.Feature.Id, Name = f.Feature.Name })));

            //DTO to Domain
            CreateMap<SaveVehicleDto, Vehicle>()
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForMember(v => v.ContactNumber, opt => opt.MapFrom(vr => vr.Contact.Number))
                .ForMember(v => v.Features, opt => opt.Ignore())
                .AfterMap((vr, v) =>
                {
                    //Add the new features
                    var addedFeatures = vr.Features.Where(id => !v.Features.Any(f => f.FeatureId == id)).ToList();
                    addedFeatures.ForEach(id => v.Features.Add(new VehicleFeature { FeatureId = id }));

                    //Remove the deleted features
                    var deletedFeatures = v.Features.Where(feature => !vr.Features.Any(id => id == feature.FeatureId)).ToList();
                    deletedFeatures.ForEach(feature => v.Features.Remove(feature));
                });
        }
    }
}
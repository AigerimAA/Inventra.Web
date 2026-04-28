using AutoMapper;
using Inventra.Application.DTOs;
using Inventra.Domain.Entities;
using Microsoft.AspNetCore.Routing.Constraints;

namespace Inventra.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Inventory, InventoryDto>()
                .ForMember(dest => dest.OwnerName,
                    opt => opt.MapFrom(src => src.Owner != null
                        ? src.Owner.UserName : null))
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category != null
                        ? src.Category.Name : null))
                .ForMember(dest => dest.Tags,
                    opt => opt.MapFrom(src => src.InventoryTags
                        .Select(it => it.Tag.Name)
                        .ToList()))
                .ForMember(dest => dest.ItemsCount,
                    opt => opt.MapFrom(src => src.Items.Count));

            CreateMap<InventoryDto, Inventory>()
            .ForMember(dest => dest.Owner, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.InventoryTags, opt => opt.Ignore())
            .ForMember(dest => dest.Items, opt => opt.Ignore())
            .ForMember(dest => dest.AccessList, opt => opt.Ignore())
            .ForMember(dest => dest.Comments, opt => opt.Ignore())
            .ForMember(dest => dest.CustomIdFormat, opt => opt.Ignore())
            .ForMember(dest => dest.Sequence, opt => opt.Ignore());

            CreateMap<Item, ItemDto>()
            .ForMember(dest => dest.CreatedByName,
                opt => opt.MapFrom(src => src.CreatedBy != null
                    ? src.CreatedBy.UserName
                    : null))
            .ForMember(dest => dest.LikesCount,
                opt => opt.MapFrom(src => src.Likes.Count))
            .ForMember(dest => dest.IsLikedByCurrentUser,
                opt => opt.Ignore());

            CreateMap<ItemDto, Item>()
                .ForMember(dest => dest.Inventory, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Likes, opt => opt.Ignore());
        }
    }
}

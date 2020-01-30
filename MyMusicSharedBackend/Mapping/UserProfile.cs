using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMusicSharedBackend.Mapping
{
    /// <summary>
    /// AutoMapper User Profile
    /// </summary>
    public class UserProfile : Profile
    {
        /// <summary>
        /// Constructor that contains the Mapper rules
        /// </summary>
        public UserProfile()
        {
            CreateMap<Models.User, Core.Dto.UserDto>();
            CreateMap<Core.Dto.UserDto, Models.User>();
        }
    }
}
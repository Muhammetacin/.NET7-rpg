using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _NET7_rpg
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterResponseDTO>();
            CreateMap<AddCharacterRequestDTO, Character>();
            CreateMap<UpdateCharacterRequestDTO, Character>();
        }
    }
}
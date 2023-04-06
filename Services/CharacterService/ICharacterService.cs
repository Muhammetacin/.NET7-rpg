using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterResponseDTO>>> GetAllCharacters();
        Task<ServiceResponse<GetCharacterResponseDTO>> GetCharacter(int id);
        Task<ServiceResponse<List<GetCharacterResponseDTO>>> AddNewCharacter(AddCharacterRequestDTO newCharacter);
        Task<ServiceResponse<GetCharacterResponseDTO>> UpdateCharacter(UpdateCharacterRequestDTO updatedCharacter);
    }
}
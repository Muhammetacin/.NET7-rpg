using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        public static List<Character> characters = new List<Character> 
        {
            new Character(),
            new Character {
                Id = 1,
                Name = "Sam"
            }
        };

        public async Task<ServiceResponse<List<GetCharacterResponseDTO>>> AddNewCharacter(AddCharacterRequestDTO newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDTO>>();
            characters.Add(newCharacter);
            serviceResponse.Data = characters;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDTO>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDTO>>();
            serviceResponse.Data = characters;
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterResponseDTO>> GetCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResponseDTO>();
            var character = characters.FirstOrDefault(c => c.Id == id);
            serviceResponse.Data = character;
            
            return serviceResponse;
        }
    }
}
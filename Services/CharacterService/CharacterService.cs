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
        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDTO>>> AddNewCharacter(AddCharacterRequestDTO newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDTO>>();
            var character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(c => c.Id) + 1;
            characters.Add(character);
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterResponseDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDTO>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDTO>>();
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterResponseDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterResponseDTO>> GetCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResponseDTO>();
            var character = characters.FirstOrDefault(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterResponseDTO>(character);
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterResponseDTO>> UpdateCharacter(UpdateCharacterRequestDTO updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResponseDTO>();
            
            try 
            {
                var character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);

                if(character is null) {
                    throw new Exception($"Character with Id '{updatedCharacter.Id}' not found.");
                }

                // Using mapper to map object
                // _mapper.Map<Character>(updatedCharacter);
                _mapper.Map(updatedCharacter, character);

                // character.Name = updatedCharacter.Name;
                // character.HitPoints = updatedCharacter.HitPoints;
                // character.Strength = updatedCharacter.Strength;
                // character.Defence = updatedCharacter.Defence;
                // character.Intelligence = updatedCharacter.Intelligence;
                // character.Class = updatedCharacter.Class;

                serviceResponse.Data = _mapper.Map<GetCharacterResponseDTO>(character);
            } 
            catch(Exception ex) 
            {
                serviceResponse.IsSuccessful = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDTO>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDTO>>();

            try 
            {
                var character = characters.First(c => c.Id == id);

                if(character is null) {
                    throw new Exception($"Character with Id '{id}' not found.");
                }

                characters.Remove(character);
                serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterResponseDTO>(c)).ToList();
            }
            catch(Exception ex) 
            {
                serviceResponse.IsSuccessful = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
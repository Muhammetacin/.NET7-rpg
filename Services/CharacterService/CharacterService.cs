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
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDTO>>> AddNewCharacter(AddCharacterRequestDTO newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDTO>>();
            var dbCharacter = _mapper.Map<Character>(newCharacter);
            
            _context.Characters.Add(dbCharacter);
            await _context.SaveChangesAsync();

            serviceResponse.Data = _context.Characters.Select(c => _mapper.Map<GetCharacterResponseDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDTO>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDTO>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterResponseDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterResponseDTO>> GetCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResponseDTO>();
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterResponseDTO>(dbCharacter);
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterResponseDTO>> UpdateCharacter(UpdateCharacterRequestDTO updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResponseDTO>();
            
            try 
            {
                var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);

                if(dbCharacter is null) {
                    throw new Exception($"Character with Id '{updatedCharacter.Id}' not found.");
                }

                // Using mapper to map object
                // _mapper.Map<Character>(updatedCharacter);
                _mapper.Map(updatedCharacter, dbCharacter);

                await _context.SaveChangesAsync();
                // character.Name = updatedCharacter.Name;
                // character.HitPoints = updatedCharacter.HitPoints;
                // character.Strength = updatedCharacter.Strength;
                // character.Defence = updatedCharacter.Defence;
                // character.Intelligence = updatedCharacter.Intelligence;
                // character.Class = updatedCharacter.Class;

                serviceResponse.Data = _mapper.Map<GetCharacterResponseDTO>(dbCharacter);
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
                var dbCharacter = await _context.Characters.FirstAsync(c => c.Id == id);

                if(dbCharacter is null) {
                    throw new Exception($"Character with Id '{id}' not found.");
                }

                _context.Characters.Remove(dbCharacter);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterResponseDTO>(c)).ToListAsync();
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
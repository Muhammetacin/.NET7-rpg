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

        public async Task<List<Character>> AddNewCharacter(Character newCharacter)
        {
            characters.Add(newCharacter);
            return characters;
        }

        public async Task<List<Character>> GetAllCharacters()
        {
            return characters;
        }

        public async Task<Character> GetCharacter(int id)
        {
            var character = characters.FirstOrDefault(c => c.Id == id);
            
            if(character != null) {
                return character;
            }

            throw new Exception("Character not found");
        }
    }
}
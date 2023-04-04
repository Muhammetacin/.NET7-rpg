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
                Name = "Sam"
            }
        };

        public List<Character> AddNewCharacter(Character newCharacter)
        {
            characters.Add(newCharacter);
            return characters;
        }

        public List<Character> GetAllCharacters()
        {
            return characters;
        }

        public Character GetCharacter(int id)
        {
            return characters.FirstOrDefault(c => c.Id == id);
        }
    }
}
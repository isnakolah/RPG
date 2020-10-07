using RPG.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character { Id = 1,  Name = "Sam" }
        };
        private readonly ICharacterService characterService;

        public async Task<ServiceResponse<List<Character>>> AddCharacter(Character newCharacter)
        {
            ServiceResponse<List<Character>> serviceResponse = new ServiceResponse<List<Character>>
            {
                Data = characters
            };
            serviceResponse.Data = characters;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Character>>> GetAllCharacters()
        {
            ServiceResponse<List<Character>> serviceResponse = new ServiceResponse<List<Character>>
            {
                Data = characters
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<Character>> GetCharacterById(int id)
        {
            ServiceResponse<Character> serviceResponse = new ServiceResponse<Character>()
            {
                Data = characters.FirstOrDefault(c => c.Id == id)
            };
            return serviceResponse;
        }
    }
}

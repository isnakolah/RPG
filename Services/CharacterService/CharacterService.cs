using AutoMapper;
using RPG.DTOs.Characters;
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
        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter)
        {
            ServiceResponse<List<GetCharacterDTO>> serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            characters.Add(_mapper.Map<Character>(newCharacter));
            serviceResponse.Data = (characters.Select(c => _mapper.Map<GetCharacterDTO>(c))).ToList();
            return serviceResponse;
        } 

        public async Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters()
        {
            ServiceResponse<List<GetCharacterDTO>> serviceResponse = new ServiceResponse<List<GetCharacterDTO>>
            {
                Data = (characters.Select(c => _mapper.Map<GetCharacterDTO>(c))).ToList()
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> GetCharacterById(int id)
        {
            ServiceResponse<GetCharacterDTO> serviceResponse = new ServiceResponse<GetCharacterDTO>()
            {
                Data = _mapper.Map<GetCharacterDTO>(characters.FirstOrDefault(c => c.Id == id))
            };
            return serviceResponse;
        }
    }
}

using AutoMapper;
using RPG.DTOs.Characters;
using RPG.Models;
using System;
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

        //private IEnumerable<Character> validCharacters(Character characters)
        //{
        //    IEnumerable<Character> validCharacters =
        //        from character in characters
        //        where character.Deleted == false
        //        select character;
        //    return validCharacters;
        //}

        public async Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter)
        {
            ServiceResponse<List<GetCharacterDTO>> serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            Character character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(c => c.Id) + 1;
            characters.Add(character);
            serviceResponse.Data = (characters.Select(c => _mapper.Map<GetCharacterDTO>(c))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDTO>> serviceResponce = new ServiceResponse<List<GetCharacterDTO>>();
            try
            {
                Character character = characters.First(c => c.Id == id);
                if (character.Deleted == true)
                {
                    serviceResponce.Success = false;
                    serviceResponce.Message = "You cannot delete an already deleted character";
                }
                else
                {
                    // Hard deleting a character
                    // characters.Remove(character);

                    // Soft deleting a character
                    character.Deleted = true;

                    serviceResponce.Data = (characters.Select(c => _mapper.Map<GetCharacterDTO>(c))).ToList();
                }
            }
            catch (Exception ex)
            {
                serviceResponce.Success = false;
                serviceResponce.Message = ex.Message;
            }

            return serviceResponce;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters()
        {
            ServiceResponse<List<GetCharacterDTO>> serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();

            IEnumerable<Character> validCharacters =
                from character in characters
                where character.Deleted == false
                select character;

            serviceResponse.Data = validCharacters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
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

        public async Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO updatedCharacter)
        {
            ServiceResponse<GetCharacterDTO> serviceResponce = new ServiceResponse<GetCharacterDTO>();
            try { 
                Character character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);
                character.Name = updatedCharacter.Name;
                character.Class = updatedCharacter.Class;
                character.Defence = updatedCharacter.Defence;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Strength = updatedCharacter.Strength;

                serviceResponce.Data = _mapper.Map<GetCharacterDTO>(character);
            }
            catch (Exception ex) 
            {
                serviceResponce.Success = false;
                serviceResponce.Message = ex.Message;
            }

            return serviceResponce;
        }
    }
}

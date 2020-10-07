using RPG.Models;
using System.Collections.Generic;

namespace RPG.Services.CharacterService
{
    interface ICharacterService
    {
        List<Character> GetAllCharacters();
        Character GetCharacterById(int id);
        List<Character> AddCharacter(Character newCharacter);
    }
}

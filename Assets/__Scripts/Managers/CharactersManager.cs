using UnityEngine;
using System.Collections;

public class CharactersManager : MonoBehaviour
{
    [SerializeField]
    public CharacterSO[] availableCharacters;

    public CharacterSO GetDefaultInitialCharacter()
    {
        return this.availableCharacters[0];
    }
}

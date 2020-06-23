using UnityEngine;
using System.Collections;

public class GeneralUtils : MonoBehaviour
{
    public static GeneralUtils Instance;

    [SerializeField]
    public CharacterSO[] availableCharacters;

    public CharacterSO GetDefaultInitialCharacter()
    {
        return this.availableCharacters[0];
    }
}

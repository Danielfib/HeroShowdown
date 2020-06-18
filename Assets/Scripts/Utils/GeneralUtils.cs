using UnityEngine;
using System.Collections;

public static class GeneralUtils
{
    public static Character[] availableCharacters =
    {
        new Character("warrior", "Sprites/Character_Sprite_Sheet"),
        new Character("mage", "Sprites/PirateCaracter")
    };

    public static Character DefaultInitialCharacter = availableCharacters[0];
}

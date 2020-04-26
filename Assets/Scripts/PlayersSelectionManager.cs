using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayersSelectionManager : MonoBehaviour
{
    private MenuInputActions menuInputActions;

    public static Character[] availableCharacters =
    {
        new Character("warrior", "Sprites/Character_Sprite_Sheet"),
        new Character("mage", "Sprites/SpikeBall")
    };

    // Start is called before the first frame update
    void Start()
    {
        menuInputActions = new MenuInputActions();
        menuInputActions.Enable();
    }
}

public class Character
{
    public string name;
    public string sprite;

    public Character(string name, string sprite)
    {
        this.name = name;
        this.sprite = sprite;
    }
}

public class PlayerSelectionSettings
{

}

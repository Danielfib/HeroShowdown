using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMenuHandler : MonoBehaviour
{
    private Character SelectedCharacter;
    private Sprite CharacterSprite;
    private Sprite CharacterNameSprite;
    private int currentCharacterIndex;

    private void Start()
    {
        SetupOnStart();
    }

    private void SetupOnStart()
    {
        currentCharacterIndex = 0;
        SelectedCharacter = PlayersSelectionManager.availableCharacters[0];
    }

    private void UpdateCharUIInfo()
    {
        CharacterSprite = Resources.Load<Sprite>(SelectedCharacter.sprite);
        //CharacterNameSprite = Resources.Load<Sprite>(SelectedCharacter.name);

        this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = CharacterSprite;
        //this.transform.GetChild(1).GetComponent<SpriteRenderer>() = CharacterNameSprite;
    }

    public void Progress(CallbackContext context)
    {
        //TODO: start scene with chosen character
        SceneManager.LoadScene("Playground");
    }

    private void ChangedHeroBackwards()
    {
        if(currentCharacterIndex <= 0)
        {
            currentCharacterIndex = PlayersSelectionManager.availableCharacters.Length - 1;
        }
        else
        {
            currentCharacterIndex--;
        }
    }

    private void ChangedheroForward()
    {
        if(currentCharacterIndex >= PlayersSelectionManager.availableCharacters.Length - 1)
        {
            currentCharacterIndex = 0;
        } else
        {
            currentCharacterIndex++;
        }
    }

    private void SelectHeroOnIndex(int index)
    {
        SelectedCharacter = PlayersSelectionManager.availableCharacters[index];
    }

    public void ChangeHero(CallbackContext context)
    {
        float inputValue = context.ReadValue<float>();

        if (inputValue == 0)
            return;

        if (inputValue < 0)
            ChangedHeroBackwards();
        else
            ChangedheroForward();

        SelectHeroOnIndex(currentCharacterIndex);
        UpdateCharUIInfo();
    }

    public void Back(CallbackContext context)
    {

    }
}
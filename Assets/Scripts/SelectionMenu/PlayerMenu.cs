using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMenu : MonoBehaviour
{
    public int PlayerIndex;

    private bool _IsReady = false;
    public bool IsReady
    {
        get { return _IsReady; }
        set
        {
            this._IsReady = value;
            this.ReadySprite.SetActive(this._IsReady);
        }
    }

    public GameObject ReadySprite;

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
        SelectedCharacter = GeneralUtils.DefaultInitialCharacter;
        UpdateCharUIInfo();
    }

    private void UpdateCharUIInfo()
    {
        CharacterSprite = Resources.Load<Sprite>(SelectedCharacter.spritePath);
        //CharacterNameSprite = Resources.Load<Sprite>(SelectedCharacter.name);

        this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = CharacterSprite;
        //this.transform.GetChild(1).GetComponent<SpriteRenderer>() = CharacterNameSprite;
    }

    public void Progress(CallbackContext context)
    {
        //prevents event being called twice
        if (!context.performed)
            return;

        if (!this.IsReady)
        {
            PlayersSettings.PlayerDataList.Find(x => x.playerIndex == this.PlayerIndex).character = SelectedCharacter;
            this.IsReady = true;
        } else
        {
            if (PlayersSelectionManager.IsEveryoneReady)
            {
                SceneManager.LoadScene("Playground");
            }
            else
            {
                //TODO: somebody not ready feedback
            }
        }
    }

    private void ChangedHeroBackwards()
    {
        if(currentCharacterIndex <= 0)
        {
            currentCharacterIndex = GeneralUtils.availableCharacters.Length - 1;
        }
        else
        {
            currentCharacterIndex--;
        }
    }

    private void ChangedheroForward()
    {
        if(currentCharacterIndex >= GeneralUtils.availableCharacters.Length - 1)
        {
            currentCharacterIndex = 0;
        } else
        {
            currentCharacterIndex++;
        }
    }

    private void SelectHeroOnIndex(int charIndex)
    {
        SelectedCharacter = GeneralUtils.availableCharacters[charIndex];
    }

    public void ChangeHero(CallbackContext context)
    {
        if (!context.performed || this.IsReady)
            return;

        float inputValue = context.ReadValue<float>();
        Debug.Log(inputValue);
        if (inputValue != 1 && inputValue != -1)
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
        //prevents event being called twice
        if (!context.performed)
            return;

        if (this.IsReady)
        {
            this.IsReady = false;
        }
        else
        {
            //Leave player?
        }
    }
}
using System;
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

    private CharacterSO SelectedCharacter;
    private Sprite CharacterSprite;
    private Sprite CharacterNameSprite;
    private int currentCharacterIndex;

    public CharactersManager charactersManager;

    private SpriteRenderer[] spriteRenderers;

    private TeamIDEnum _Team;
    public TeamIDEnum Team
    {
        get { return _Team; }
        set
        {
            this._Team = value;
            ChangedTeam();
        }
    }

    private void Start()
    {
        SetupOnStart();
    }

    private void Awake()
    {
        charactersManager = GameObject.Find("CharactersManager").GetComponent<CharactersManager>();
    }

    private void SetupOnStart()
    {
        currentCharacterIndex = 0;
        SelectedCharacter = this.charactersManager.GetDefaultInitialCharacter();
        UpdateCharUIInfo();
        ChooseDefaultTeam();
        this.IsReady = false;
        this.spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        SetupSpriteMaterials();
    }

    private void UpdateCharUIInfo()
    {
        CharacterSprite = SelectedCharacter.sprite;

        this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = CharacterSprite;
    }

    public void Progress(CallbackContext context)
    {
        //prevents event being called twice
        if (!context.performed)
            return;

        if (!this.IsReady)
        {
            PlayerData playerData = PlayersSettings.PlayerDataList.Find(x => x.playerIndex == this.PlayerIndex);
            playerData.character = SelectedCharacter;
            playerData.team = this._Team;
            this.IsReady = true;
        } else
        {
            if (PlayersSelectionManager.IsEveryoneReady)
            {
                GameObject.FindObjectOfType<PlayersSelectionManager>().LoadMatchLevel();
            }
            else
            {
                //TODO: somebody not ready feedback
            }
        }
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
    
    #region [HeroSelection]
    public void ChangedHeroBackwards(CallbackContext context)
    {
        if (!context.performed || this.IsReady || charactersManager == null)
            return;

        if (currentCharacterIndex <= 0)
        {
            currentCharacterIndex = this.charactersManager.availableCharacters.Length - 1;
        }
        else
        {
            currentCharacterIndex--;
        }

        SelectHeroOnIndex(currentCharacterIndex);
        UpdateCharUIInfo();
    }

    public void ChangedHeroForward(CallbackContext context)
    {
        if (!context.performed || this.IsReady || charactersManager == null)
            return;

        if (currentCharacterIndex >= this.charactersManager.availableCharacters.Length - 1)
        {
            currentCharacterIndex = 0;
        } else
        {
            currentCharacterIndex++;
        }

        SelectHeroOnIndex(currentCharacterIndex);
        UpdateCharUIInfo();
    }

    private void SelectHeroOnIndex(int charIndex)
    {
        SelectedCharacter = this.charactersManager.availableCharacters[charIndex];
    }
    #endregion

    #region [Team]
    private void ChooseDefaultTeam()
    {
        this.Team = TeamIDEnum.RED;
    }

    public void ChangeTeamLeft(CallbackContext context)
    {
        if (!context.performed || this.IsReady)
            return;

        int currentTeam = (int)this._Team;

        int newTeam;
        if(currentTeam == 0)
            newTeam = Enum.GetNames(typeof(TeamIDEnum)).Length - 1;
        else
            newTeam = currentTeam - 1;

        this.Team = (TeamIDEnum)newTeam;
    }

    public void ChangeTeamRight(CallbackContext context)
    {
        if (!context.performed || this.IsReady)
            return;

        int currentTeam = (int)this._Team;
        
        int newTeam;
        if (currentTeam == Enum.GetNames(typeof(TeamIDEnum)).Length - 1)
            newTeam = 0;
        else
            newTeam = currentTeam + 1;

        this.Team = (TeamIDEnum)newTeam;
    }

    private void ChangedTeam()
    {
        SpriteRenderer renderer = this.GetComponentInChildren<SpriteRenderer>();

        switch (this._Team)
        {
            case TeamIDEnum.RED:
                renderer.color = ColorUtils.Red;
                break;
            //case TeamIDEnum.PURPLE:
            //    renderer.color = ColorUtils.Purple;
            //    break;
            //case TeamIDEnum.GREEN:
            //    renderer.color = ColorUtils.Green;
            //    break;
            //case TeamIDEnum.BROWN:
            //    renderer.color = ColorUtils.Brown;
            //    break;
            case TeamIDEnum.BLUE:
                renderer.color = ColorUtils.Blue;
                break;
        }

        SetupSpriteMaterials();
    }

    private void SetupSpriteMaterials()
    {
        Color color = ColorUtils.TeamIdEnumToColor(this._Team);

        if (this.spriteRenderers != null)
        {
            foreach (var spriteRenderer in spriteRenderers)
            {
                spriteRenderer.material.SetColor("_NewColor", color);
            }
        }
    }
    #endregion
}
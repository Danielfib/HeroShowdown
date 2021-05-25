using System;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMenu : NetworkRoomPlayer
{
    public int PlayerIndex;

    [SyncVar(hook = nameof(HandleSelectedCharacterChanged))]
    private int currentCharacterIndex;

    [SyncVar(hook = nameof(HandleTeamChanged))]
    public TeamIDEnum Team;

    [SyncVar(hook = nameof(HandleIsServerPlayerChanged))]
    public bool IsServerPlayer = false;

    public GameObject ReadySprite, IsLeaderSprite;
    [SerializeField]
    private TextMeshProUGUI charName, charDescription, abilityTitle;
    [SerializeField]
    private Image abilityIcon;
    public CharacterSO SelectedCharacter;
    private Sprite CharacterSprite;

    private SwitchColorToTeamColor materialColorSwitcher;

    private CharactersManager characterManager;

    [SerializeField]
    private Image charImage;

    [SerializeField]
    private InputActionAsset menuInputActionAsset;

    private void Awake()
    {
        characterManager = GameObject.Find("CharactersManager").GetComponent<CharactersManager>();
    }

    [Command]
    private void CmdSetupOnStart()
    {
        RpcSetupOnStart();
    }

    [ClientRpc]
    private void RpcSetupOnStart()
    {
        currentCharacterIndex = 0;
        SelectedCharacter = this.characterManager.availableCharacters[0];
        IsLeaderSprite.SetActive(IsServerPlayer);
        ChooseDefaultTeam();
        ChangedTeam();
        UpdateCharUIInfo();

        if(hasAuthority) GetComponent<PlayerInput>().actions = menuInputActionAsset;
    }

    private void InitializeColorSwitcher()
    {
        this.materialColorSwitcher = this.transform.GetComponentInChildren<SwitchColorToTeamColor>();
    }

    private void UpdateCharUIInfo()
    {
        CharacterSprite = SelectedCharacter.sprite;
        charName.text = SelectedCharacter.hero.ToString();
        charDescription.text = SelectedCharacter.description;
        abilityIcon.sprite = SelectedCharacter.charAttributes.abilityIcon;
        abilityTitle.text = SelectedCharacter.charAttributes.abilityTitle;

        charImage.sprite = CharacterSprite;
    }

    #region [NetworkLifecyle]
    public override void OnStartClient()
    {
        CmdSetupOnStart();
    }

    public override void OnStartLocalPlayer()
    {
        //CmdSetupOnStart();

        if(!IsServerPlayer)
        {
            IsServerPlayer = (isServer && isLocalPlayer);
        }
    }

    public void HandleIsServerPlayerChanged(bool oldValue, bool newValue)
    {
        IsLeaderSprite.SetActive(newValue);
    }

    public override void OnStopClient()
    {
        //Lobby.LobbyPlayers.Remove(this);
    }
    #endregion

    #region [Progress]
    public void Progress(CallbackContext context)
    {
        //prevents event being called twice
        if (!context.performed || !hasAuthority)
            return;

        CmdProgress();
    }

    [Command]
    public void CmdProgress()
    {
        if (!readyToBegin)
        {
            ReadySprite.SetActive(true);
            CmdChangeReadyState(true);
        }
        else
        {
            CmdTryStartGame();
        }
    }

    [Command]
    private void CmdTryStartGame()
    {
        GameObject.FindObjectOfType<NetworkManagerRoom>().StartGame();
    }

    public void Back(CallbackContext context)
    {
        //prevents event being called twice
        if (!context.performed || !hasAuthority)
            return;

        CmdBack();
    }

    [Command]
    public void CmdBack() 
    {
        if (readyToBegin)
        {
            ReadySprite.SetActive(false);
            CmdChangeReadyState(true);
        }
        else
        {
            //Leave player?
        }
    }
    #endregion

    #region [HeroSelection]
    public void ChangedHeroBackwards(CallbackContext context)
    {
        if (!context.performed || readyToBegin || characterManager == null || !hasAuthority)
            return;

        CmdChangedHeroBackward();
    }

    public void ChangedHeroForward(CallbackContext context)
    {
        if (!context.performed || readyToBegin || characterManager == null || !hasAuthority)
            return;

        CmdChangedHeroForward();
    }

    [Command]
    private void CmdChangedHeroForward()
    {
        if (currentCharacterIndex >= this.characterManager.availableCharacters.Length - 1)
        {
            currentCharacterIndex = 0;
        }
        else
        {
            currentCharacterIndex++;
        }
    }

    [Command]
    private void CmdChangedHeroBackward()
    {
        if (currentCharacterIndex <= 0)
        {
            currentCharacterIndex = this.characterManager.availableCharacters.Length - 1;
        }
        else
        {
            currentCharacterIndex--;
        }
    }

    private void HandleSelectedCharacterChanged(int oldValue, int newValue)
    {
        SelectedCharacter = this.characterManager.availableCharacters[newValue];
        Debug.Log("Changed character to: " + SelectedCharacter.hero.ToString());
        UpdateCharUIInfo();
    }
    #endregion

    #region [Team]
    [Command]
    private void ChooseDefaultTeam()
    {
        this.Team = TeamIDEnum.RED;
    }

    public void ChangeTeamLeft(CallbackContext context)
    {
        if (!context.performed || readyToBegin || characterManager == null || !hasAuthority)
            return;

        CmdChangedTeamLeft();
    }

    public void ChangeTeamRight(CallbackContext context)
    {
        if (!context.performed || readyToBegin || characterManager == null || !hasAuthority)
            return;

        CmdChangedTeamRight();
    }

    private void ChangedTeam()
    {
        if (!materialColorSwitcher)
            InitializeColorSwitcher();

        materialColorSwitcher.SetupImageMaterials(this.Team);
    }

    [Command]
    private void CmdChangedTeamRight()
    {
        int currentTeam = (int)this.Team;

        int newTeam;
        if (currentTeam == Enum.GetNames(typeof(TeamIDEnum)).Length - 1)
            newTeam = 0;
        else
            newTeam = currentTeam + 1;

        this.Team = (TeamIDEnum)newTeam;
    }

    [Command]
    private void CmdChangedTeamLeft()
    {
        int currentTeam = (int)this.Team;

        int newTeam;
        if (currentTeam == 0)
            newTeam = Enum.GetNames(typeof(TeamIDEnum)).Length - 1;
        else
            newTeam = currentTeam - 1;

        this.Team = (TeamIDEnum)newTeam;
    }

    private void HandleTeamChanged(TeamIDEnum oldValue, TeamIDEnum newValue)
    {
        ChangedTeam();
    }
    #endregion
}
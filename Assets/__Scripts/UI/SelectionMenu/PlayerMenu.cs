using System;
using System.Linq;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMenu : NetworkBehaviour
{
    public int PlayerIndex;

    private NetworkManagerLobby lobby;
    private NetworkManagerLobby Lobby
    {
        get
        {
            if (lobby != null) { return lobby; }
            return lobby = NetworkManager.singleton as NetworkManagerLobby;
        }
    }

    [SyncVar(hook = nameof(HandleReadyStatusChanged))]
    public bool IsReady = false;

    public GameObject ReadySprite;
    [SerializeField]
    private TextMeshProUGUI charName, charDescription, abilityTitle;
    [SerializeField]
    private Image abilityIcon;

    private CharacterSO SelectedCharacter;
    private Sprite CharacterSprite;
    private int currentCharacterIndex;
    private SwitchColorToTeamColor materialColorSwitcher;

    private CharactersManager characterManager;

    [SerializeField]
    private Image charImage;

    [SerializeField]
    private InputActionAsset menuInputActionAsset;

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
        characterManager = GameObject.Find("CharactersManager").GetComponent<CharactersManager>();
    }

    private void SetupOnStart()
    {
        currentCharacterIndex = 0;
        SelectedCharacter = this.characterManager.GetDefaultInitialCharacter();
        UpdateCharUIInfo();
        ChooseDefaultTeam();
        this.IsReady = false;
        InitializeColorSwitcher();

        if(isLocalPlayer) GetComponent<PlayerInput>().actions = menuInputActionAsset;
    }

    private void InitializeColorSwitcher()
    {
        this.materialColorSwitcher = this.transform.GetComponentInChildren<SwitchColorToTeamColor>();
    }

    private void UpdateCharUIInfo()
    {
        CharacterSprite = SelectedCharacter.sprite;
        charName.text = SelectedCharacter.name;
        charDescription.text = SelectedCharacter.description;
        abilityIcon.sprite = SelectedCharacter.charAttributes.abilityIcon;
        abilityTitle.text = SelectedCharacter.charAttributes.abilityTitle;

        charImage.sprite = CharacterSprite;
    }

    #region [NetworkLifecyle]
    public override void OnStartClient()
    {
        Lobby.LobbyPlayers.Add(this);
    }

    public override void OnStopClient()
    {
        Lobby.LobbyPlayers.Remove(this);
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
        if (!this.IsReady)
        {
            // PlayerData playerData = PlayersSettings.PlayerDataList.Find(x => x.playerIndex == this.PlayerIndex);
            // playerData.character = SelectedCharacter;
            // playerData.team = this._Team;
            this.IsReady = true;
        }
        else
        {
            if (Lobby.IsEveryoneReadyToStart())
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
        if (!context.performed || !hasAuthority)
            return;

        CmdBack();
    }

    [Command]
    public void CmdBack() 
    {
        if (this.IsReady)
        {
            this.IsReady = false;
        }
        else
        {
            //Leave player?
        }
    }

    private void HandleReadyStatusChanged(bool oldValue, bool newValue) 
    {
        this.ReadySprite.SetActive(newValue);
    }
    #endregion

    #region [HeroSelection]
    public void ChangedHeroBackwards(CallbackContext context)
    {
        if (!context.performed || this.IsReady || characterManager == null || !hasAuthority)
            return;

        CmdChangedHeroBackward();
    }

    public void ChangedHeroForward(CallbackContext context)
    {
        if (!context.performed || this.IsReady || characterManager == null || !hasAuthority)
            return;

        CmdChangedHeroForward();
    }

    private void SelectHeroOnIndex(int charIndex)
    {
        SelectedCharacter = this.characterManager.availableCharacters[charIndex];
    }

    [Command]
    private void CmdChangedHeroForward()
    {
        //TODO: Validate logic here
        RpcChangeHeroForward();
    }

    [ClientRpc]
    private void RpcChangeHeroForward()
    {
        if (currentCharacterIndex >= this.characterManager.availableCharacters.Length - 1)
        {
            currentCharacterIndex = 0;
        }
        else
        {
            currentCharacterIndex++;
        }

        SelectHeroOnIndex(currentCharacterIndex);
        UpdateCharUIInfo();
    }

    [Command]
    private void CmdChangedHeroBackward()
    {
        //TODO: Validate logic here
        RpcChangeHeroBackward();
    }

    [ClientRpc]
    private void RpcChangeHeroBackward()
    {
        if (currentCharacterIndex <= 0)
        {
            currentCharacterIndex = this.characterManager.availableCharacters.Length - 1;
        }
        else
        {
            currentCharacterIndex--;
        }

        SelectHeroOnIndex(currentCharacterIndex);
        UpdateCharUIInfo();
    }
    #endregion

    #region [Team]
    private void ChooseDefaultTeam()
    {
        this.Team = TeamIDEnum.RED;
    }

    public void ChangeTeamLeft(CallbackContext context)
    {
        if (!context.performed || this.IsReady || !hasAuthority)
            return;

        CmdChangedTeamLeft();
    }

    public void ChangeTeamRight(CallbackContext context)
    {
        if (!context.performed || this.IsReady || !hasAuthority)
            return;

        CmdChangedTeamRight();
    }

    private void ChangedTeam()
    {
        if (!materialColorSwitcher)
            InitializeColorSwitcher();

        materialColorSwitcher.SetupImageMaterials(this._Team);
    }

    [Command]
    private void CmdChangedTeamRight()
    {
        RpcChangedTeamRight();
    }

    [ClientRpc]
    private void RpcChangedTeamRight()
    {
        int currentTeam = (int)this._Team;

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
        RpcChangedTeamLeft();
    }

    [ClientRpc]
    private void RpcChangedTeamLeft()
    {
        int currentTeam = (int)this._Team;

        int newTeam;
        if (currentTeam == 0)
            newTeam = Enum.GetNames(typeof(TeamIDEnum)).Length - 1;
        else
            newTeam = currentTeam - 1;

        this.Team = (TeamIDEnum)newTeam;
    }
    #endregion
}
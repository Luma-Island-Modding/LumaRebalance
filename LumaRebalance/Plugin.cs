using BepInEx;
using LumaLibrary.Manager;
using Steamworks;

namespace LumaRebalance;

[BepInPlugin(ModGUID, ModName, Version)]
[BepInDependency(LumaLibrary.Plugin.ModGUID)]
public class Plugin : BaseUnityPlugin
{
    public const string ModGUID = "etc.shikaru.lumarebalance";
    public const string ModName = "LumaRebalance";
    public const string Version = "0.0.2";
        
    private void Awake()
    {
        Logger.LogInfo($"Plugin {ModGUID} is loaded!");

        ShopManager.Instance.OnShopBenchPlaced += OnShopPlaced;
        ShopManager.Instance.OnShopInteract += OnShopInteract;

        GameDataManager.Instance.OnGameDataInitialized += OnGameDataInitialized;

        NetworkManager.Instance.OnStartHost += OnHostInitialized;
        NetworkManager.Instance.OnStartClient += OnClientInitialized;

        NetworkManager.Instance.OnNetworkInitialze += OnNetworkInitialized;

        LumaSteamManager.Instance.OnSteamInitialized += OnSteamInitialized;
    }

    private void OnHostInitialized()
    {
        LumaLibrary.Logger.LogInfo("HOST INITIALIZED");
    }

    private void OnSteamInitialized()
    {
        LumaLibrary.Logger.LogInfo("Steam initialized!");

        LumaSteamLobbyManager.Instance.OnLobbyCreated += OnLobbyCreated;
        LumaSteamLobbyManager.Instance.OnLobbyBeforeEntered += OnLobbyBeforeEntered;

        LumaSteamLobbyManager.Instance.OnLobbyEntered += OnLobbyEntered;
        LumaSteamLobbyManager.Instance.OnLobbyInviteReceived += OnLobbyInviteReceived;

        LumaSteamLobbyManager.Instance.OnFailedToJoinGame += OnFailedToJoinGame;
    }

    private void OnFailedToJoinGame(GameJoinFailReason reason)
    {
        LumaLibrary.Logger.LogInfo($"Lobby game join failed: {reason.ToString()}");
    }

    private void OnLobbyInviteReceived(LobbyInvite_t invite)
    {
        LumaLibrary.Logger.LogInfo($"Lobby invite: {invite.m_ulSteamIDUser} | {invite.m_ulGameID} | {invite.m_ulSteamIDLobby}");
    }

    private void OnLobbyCreated(LobbyCreated_t lobby)
    {
        LumaLibrary.Logger.LogInfo($"Lobby created: {lobby.m_ulSteamIDLobby}");
    }

    private void OnLobbyBeforeEntered(LobbyEnter_t lobby)
    {
        LumaLibrary.Logger.LogInfo($"Lobby before enter: {lobby.m_ulSteamIDLobby}");
    }

    private void OnLobbyEntered(LobbyEnter_t lobby)
    {
        LumaLibrary.Logger.LogInfo($"Lobby enter: {lobby.m_ulSteamIDLobby}");
    }

    private void OnClientInitialized()
    {
        LumaLibrary.Logger.LogInfo("CLIENT INITIALIZED");
    }

    private void OnClientDisconnectedFromHost()
    {
        LumaLibrary.Logger.LogInfo("CLIENT DISCONNECTED FROM HOST");
    }

    private void OnPlayerJoinedGame(string machineId)
    {
        LumaLibrary.Logger.LogInfo($"CLIENT CONNECTED: {machineId}");

        if(Network.IsClient || Network.Instance.PlayerID == machineId)
        {
            return;
        }

        var player = PlayersManager.Instance.GetActivePlayerWithID(machineId);
        if (player != null)
        {
            LumaLibrary.Logger.LogInfo($"CLIENT CONNECTED: {machineId}");
        }
    }

    private void OnPlayerLeftGame(string machineId)
    {
        LumaLibrary.Logger.LogInfo($"Player Left Game: {machineId}");
    }

    private void OnNetworkInitialized()
    {
        LumaLibrary.Logger.LogInfo("NETWORK INITIALIZED");

        NetworkManager.Instance.ListenClientEvent(LumaLibrary.Enum.NetworkClientEvent.OnDisconnectedFromHost, OnClientDisconnectedFromHost);
        NetworkManager.Instance.ListenClientEvent(LumaLibrary.Enum.NetworkClientEvent.OnConnectedToServer, OnClientConnectedToServer);

        NetworkManager.Instance.ListenHostEvent(LumaLibrary.Enum.NetworkHostEvent.OnPlayerJoinedGame, OnPlayerJoinedGame);
        NetworkManager.Instance.ListenHostEvent(LumaLibrary.Enum.NetworkHostEvent.OnPlayerLeftGame, OnPlayerLeftGame);
    }

    private void OnClientConnectedToServer()
    {
        LumaLibrary.Logger.LogInfo("CLIENT CONNECTED TO SERVER");
    }

    private void OnGameDataInitialized(bool val)
    {
        Logger.LogInfo("Game data initialized!");
        RebalanceBuyValue();
        RebalanceSellValue();
    }

    private void RebalanceBuyValue()
    {
        ItemManager.Instance.UpdateBuyValue(LumaLibrary.Constant.GameItems.Gravel, 5);
    }

    private void RebalanceSellValue()
    {
        ItemManager.Instance.UpdateSellValue(LumaLibrary.Constant.GameItems.Gravel, 4);
    }

    private void OnShopPlaced(Shop instance)
    {

    }

    private void OnShopInteract(Shop instance) 
    {
        // ...
    }
}

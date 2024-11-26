using BepInEx;
using BepInEx.Logging;
using LumaLibrary.Manager;

namespace LumaRebalance;

[BepInPlugin(ModGUID, ModName, Version)]
[BepInDependency(LumaLibrary.Plugin.ModGUID)]
public class Plugin : BaseUnityPlugin
{
    public const string ModGUID = "etc.shikaru.lumarebalance";
    public const string ModName = "LumaRebalance";
    public const string Version = "0.0.1";
    internal static new ManualLogSource Logger;
        
    private void Awake()
    {
        // Plugin startup logic
        Logger = base.Logger;
        Logger.LogInfo($"Plugin {ModGUID} is loaded!");

        ShopManager.Instance.OnShopPlaced += OnShopPlaced;
    }

    private void OnShopPlaced(Shop instance)
    {
        Logger.LogInfo($"Shop placed event: {instance.name}");
    }
}

﻿using BepInEx;
using LumaLibrary.Manager;
using System.ArrayExtensions;
using System.Linq;

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

        GameDataManager.Instance.OnGameInitialized += OnGameInitialized;
    }

    private void OnGameInitialized(bool val)
    {
        Logger.LogInfo("Game initialized!");
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

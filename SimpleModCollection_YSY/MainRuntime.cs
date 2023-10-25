using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using SimpleModCollection_YSY.Patchers;

namespace SimpleModCollection_YSY;

[BepInPlugin("zender.SimpleModCollection_YSY.YSY.main", "SimpleModCollection_YSY", "0.0.0.3")]
public class MainRuntime : BaseUnityPlugin
{
    public static readonly Harmony HarmonyInstance = new("zender.SimpleModCollection_YSY.YSY.main");
    public static ConfigEntry<bool> AddPower = null!;
    public static ConfigEntry<bool> InfFoodIngredients = null!;
    public static ConfigEntry<bool> SimpleAlchemyOnFree = null!;
    public static ConfigEntry<bool> SuperAutoFood = null!;
    public static ConfigEntry<bool> SuperAutoFoodMakeWindow = null!;

    static MainRuntime()
    {
        HarmonyInstance.PatchAll(typeof(HotelAttrPatcher));
        HarmonyInstance.PatchAll(typeof(ShopPatcher));
        HarmonyInstance.PatchAll(typeof(RestaurantPatcher));
        HarmonyInstance.PatchAll(typeof(AlchemyPatcher));
    }

    private void Awake()
    {
        AddPower = Config.Bind("作弊功能", "zender.hack.AddPower", false, "将基础体力增大一倍");
        InfFoodIngredients = Config.Bind("作弊功能", "zender.hack.InfFoodIngredients", false, "购买食材不减少商店存量");
        SimpleAlchemyOnFree = Config.Bind("作弊功能", "zender.hack.SimpleAlchemyOnFree", false,
            "自由炼金时，如果产物品质小于紫则再roll两次，总共三次中取品质最高");

        SuperAutoFood = Config.Bind("非作弊功能", "zender.noHack.SuperAutoFood", true, "餐厅补货时自动购买缺少的食材并自动做菜");
        SuperAutoFoodMakeWindow = Config.Bind("非作弊功能", "zender.noHack.SuperAutoFoodMakeWindow", true,"自动做菜是否弹窗,true则不弹窗");
    }
}
using System;
using System.Linq;
using AlchemySystem;
using HarmonyLib;

namespace SimpleModCollection_YSY.Patchers;

[HarmonyPatch]
public static class AlchemyPatcher
{
    [HarmonyReversePatch, HarmonyPatch(typeof(AlchemySystemManager), nameof(GetAlchemyItemByTotalAlchemyNum))]
    public static int GetAlchemyItemByTotalAlchemyNum(this AlchemySystemManager instance, int totalAlchemyNum)
    {
        throw new NotImplementedException("HarmonyReversePatch");
    }

    [HarmonyPostfix, HarmonyPatch(typeof(AlchemySystemManager), nameof(GetAlchemyItemByTotalAlchemyNum))]
    public static void GetAlchemyItemByTotalAlchemyNumPost(AlchemySystemManager __instance, int totalAlchemyNum,
        ref int __result)
    {
        if (!MainRuntime.SimpleAlchemyOnFree.Value) return;
        var item = ItemManager.Instance.GetItem(__result);
        if (item.Rarity >= 3) return;
        var result1 = __instance.GetAlchemyItemByTotalAlchemyNum(totalAlchemyNum);
        var result2 = __instance.GetAlchemyItemByTotalAlchemyNum(totalAlchemyNum);
        __result = (from id in new[] {__result, result1, result2}
            let itemRarity = ItemManager.Instance.GetItem(id).Rarity
            orderby itemRarity descending
            select id).First();
    }
}
using Common;
using HarmonyLib;
using HotelModule;

namespace SimpleModCollection_YSY.Patchers;

[HarmonyPatch]
public static class HotelAttrPatcher
{
    [HarmonyPrefix, HarmonyPatch(typeof(HotelAttributes), nameof(HotelAttributes.ResetActionPoint))]
    public static bool Post_ResetActionPoint(HotelAttributes __instance)
    {
        if (!MainRuntime.AddPower.Value) return true;
        var item = AttributesManager.Instance.GetItem(921);
        __instance.attributeDic[921].value = (int) (item.StartValue * 0.002f);
        EventManager.FireEvent(14, 921, 0, 0L);
        return false;
    }
}
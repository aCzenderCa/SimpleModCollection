using BagSystem;
using Common;
using Example;
using Foundation.UI;
using HarmonyLib;
using HotelModule;

namespace SimpleModCollection_YSY.Patchers;

[HarmonyPatch]
public static class ShopPatcher
{
    [HarmonyPrefix, HarmonyPatch(typeof(HotelShopManager), nameof(HotelShopManager.PurchaseItem))]
    public static bool PurchaseItem(HotelShopManager __instance, SellItem sellItem, int num)
    {
        if (!MainRuntime.InfFoodIngredients.Value) return true;
        if (!__instance.CheckCanBuyItem(sellItem, num))
            return false;
        var itemId = sellItem.ItemId;

        ItemBurstManager.Instance.AddRarityItemBurst(new ItemBurstData
        {
            itemId = itemId,
            callback = AddRarityItemBurst_Callback
        });
        return false;

        void AddRarityItemBurst_Callback()
        {
            BagManager.Instance.AddItem(itemId, num);
            int shopPrice = __instance.GetShopPrice(sellItem.Price, num);
            if (sellItem.UseMoney)
            {
                HotelAttributes.Instance.ModifyAttriBute(AttributesECurrencyEnum.E_Money,
                    HotelBuffEBuffEffect.E_ExtraReduce, shopPrice);
                SettlementManager.Instance.TotalShopSpend += shopPrice;
            }
            else
                BagManager.Instance.CostItem(10013, shopPrice);

            sellItem.OnSale(sellItem.Num > 100 ? 0 : num);

            if (itemId is >= 11003 and <= 11009) ++sellItem.CurrentIdx;
            UIManager.Instance.GetUIByName<ShopWindow>("UI_Shop")?.RefreshCurrentPage();
            EventManager.FireEvent(38);
        }
    }
}
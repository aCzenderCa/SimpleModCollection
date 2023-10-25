using System;
using BagSystem;
using Common;
using Example;
using Foundation.UI;
using HotelModule;

namespace SimpleModCollection_YSY.Helper;

public static class BuyHelper
{
    public static void FastBuy(this HotelShopManager shopManager, SellItem sellItem, int count)
    {
        if (!shopManager.CheckCanBuyItem(sellItem, count)) return;
        var itemId = sellItem.ItemId;
        BagManager.Instance.AddItem(itemId, count);
        var shopPrice = shopManager.GetShopPrice(sellItem.Price, count);
        if (sellItem.UseMoney)
        {
            HotelAttributes.Instance.ModifyAttriBute(AttributesECurrencyEnum.E_Money,
                HotelBuffEBuffEffect.E_ExtraReduce, shopPrice);
            SettlementManager.Instance.TotalShopSpend += shopPrice;
        }
        else
            BagManager.Instance.CostItem(10013, shopPrice);

        sellItem.OnSale(sellItem.Num > 100 && MainRuntime.InfFoodIngredients.Value ? 0 : count);
        if (itemId is >= 11003 and <= 11009)
            ++sellItem.CurrentIdx;
        UIManager.Instance.GetUIByName<ShopWindow>("UI_Shop")?.RefreshCurrentPage();
        EventManager.FireEvent(38);
    }

    public static void BuyFood(this HotelShopManager shopManager, int id, int count)
    {
        if (shopManager.GetFoodSellItemByItemID(id) is not SellItem sellItem) return;
        while (count > 0)
        {
            var rawNum = sellItem.Num;
            shopManager.FastBuy(sellItem, Math.Min(count, rawNum));
            count -= rawNum;
            if (sellItem.Num == 0)
            {
                return;
            }
        }
    }
}
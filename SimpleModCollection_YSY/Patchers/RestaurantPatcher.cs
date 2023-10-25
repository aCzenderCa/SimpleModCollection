using System;
using System.Collections.Generic;
using System.Linq;
using BagSystem;
using Foundation;
using HarmonyLib;
using HotelModule;
using SimpleModCollection_YSY.Helper;

namespace SimpleModCollection_YSY.Patchers;

[HarmonyPatch]
public static class RestaurantPatcher
{
    public static void FastCooking(this RestaurantSystemManager restaurantSystemManager, int recipeId, int num)
    {
        if (num <= 0) return;

        if (!restaurantSystemManager.RestaurantRecipeItemDic.TryGetValue(recipeId, out var restaurantRecipeItem))
            return;

        if (!restaurantSystemManager.CheckEnough(restaurantRecipeItem.Data.Dosage, restaurantRecipeItem.Data.DosageNum))
        {
            GameAPI.ShowTips(GameAPI.GetLanguageStr(13101118));
            return;
        }

        for (var i = 0; i < num; i++)
        {
            for (var j = 0; j < restaurantRecipeItem.Data.Dosage.Count; j++)
            {
                BagManager.Instance.CostItem(restaurantRecipeItem.Data.Dosage[j],
                    restaurantRecipeItem.Data.DosageNum[j]);
            }
        }

        restaurantSystemManager.CreateNewFood(restaurantRecipeItem.GetFoodItemId(), num, false);
        StatisticsManager.Instance.AddCookingCounter(restaurantRecipeItem.foodRecipeId, num);
    }

    [HarmonyReversePatch, HarmonyPatch(typeof(RestaurantSystemManager), nameof(CheckEnough))]
    public static bool CheckEnough(this RestaurantSystemManager __instance, List<int> itemList, List<int> numList)
    {
        throw new NotImplementedException("HarmonyReversePatch");
    }

    [HarmonyReversePatch, HarmonyPatch(typeof(RestaurantSystemManager), nameof(GetRecipeItemByFoodItem))]
    public static RestaurantRecipeItem GetRecipeItemByFoodItem(this RestaurantSystemManager __instance, int foodItem)
    {
        throw new NotImplementedException("HarmonyReversePatch");
    }

    [HarmonyPrefix, HarmonyPatch(typeof(RestaurantSystemManager), nameof(RestaurantSystemManager.CheckAutoAddFood))]
    public static void SuperCheckAutoAddFood(RestaurantSystemManager __instance)
    {
        if (!MainRuntime.SuperAutoFood.Value) return;
        foreach (var restaurantMenuItem in __instance.RestaurantMenuItemDic.Values)
        {
            if (!restaurantMenuItem.IsAutoSetFood) continue;
            if (!__instance.RestaurantFoodItemDic.ContainsKey(restaurantMenuItem.foodItemId) ||
                __instance.RestaurantFoodItemDic[restaurantMenuItem.foodItemId] == null) continue;

            var count = __instance.RestaurantFoodItemDic[restaurantMenuItem.foodItemId].Count;
            var num = restaurantMenuItem.Cap - restaurantMenuItem.FoodNum;
            if (num + 1 <= count) continue;
            var reqCook = num - count + 1;
            var recipe = __instance.GetRecipeItemByFoodItem(restaurantMenuItem.foodItemId);
            recipe.Data.Dosage.Select((itemId, i) => (itemId,
                    Math.Max(0, reqCook * recipe.Data.DosageNum[i] - BagManager.Instance.GetItemCount(itemId, 0))))
                .Do(((int id, int count) tuple) =>
                {
                    var (id, i) = tuple;
                    if (i == 0) return;
                    HotelShopManager.Instance.BuyFood(id, i);
                });
            if (MainRuntime.SuperAutoFoodMakeWindow.Value)
            {
                __instance.FastCooking(recipe.foodRecipeId, reqCook);
            }
            else
            {
                __instance.Cooking(recipe.foodRecipeId, reqCook);
            }
        }
    }
}
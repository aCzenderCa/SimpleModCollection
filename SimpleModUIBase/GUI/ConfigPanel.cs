using BepInEx.Configuration;
using UnityEngine;

namespace SimpleModUIBase.GUI;

public class ConfigPanel : PanelBase
{
}

public abstract class ConfigCellBase : MonoBehaviour
{
}

public class BoolConfigCell : ConfigCellBase
{
    public ConfigEntry<bool>? ConfigEntry;

    public void Init(ConfigEntry<bool> configEntry)
    {
        ConfigEntry = configEntry;
    }

    public void Click(bool stat)
    {
        if(ConfigEntry==null)return;
        ConfigEntry.Value = stat;
    }
}
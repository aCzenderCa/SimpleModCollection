using UnityEngine;
using UnityEngine.UI;
using UniverseLib.UI;
using UniverseLib.UI.Panels;

namespace SimpleModCollection_YSY.GUI;

public class ConfigUI : PanelBase
{
    public GameObject PanelConfig = null!;
    public GameObject PanelConfigHolder = null!;
    public GameObject PanelAbout = null!;
    public GameObject PanelAboutHolder = null!;

    public ConfigUI(UIBase owner, string name, int minWidth = 0, int minHeight = 0, Vector2? defaultAnchorMin = null,
        Vector2? defaultAnchorMax = null) : base(owner)
    {
        Name = name;
        MinWidth = minWidth;
        MinHeight = minHeight;
        DefaultAnchorMin = defaultAnchorMin ?? new Vector2(0.5f, 0.5f);
        DefaultAnchorMax = defaultAnchorMax ?? new Vector2(0.5f, 0.5f);
    }

    protected override void ConstructPanelContent()
    {
        var _root = UIFactory.CreateVerticalGroup(ContentRoot, "_root", true, true, false, false);
        var select = UIFactory.CreateHorizontalGroup(_root, "select", true, false, false, true);
        PanelConfig = UIFactory.CreatePanel("ConfigPanel", _root, out PanelConfigHolder);
        PanelAbout = UIFactory.CreatePanel("AboutPanel", _root, out PanelAboutHolder);
        
        UIFactory.CreateScrollView(PanelConfigHolder, "config_root", out var config_root, out var autoScrollbar);
        config_root = UIFactory.CreateVerticalGroup(config_root, "config_root", false, false, true, true);
        
        UIFactory.CreateScrollView(PanelConfigHolder, "about_root", out var about_root, out var aboutAutoScrollbar);
        about_root = UIFactory.CreateVerticalGroup(about_root, "about_root", false, false, true, true);

        var _horizontal = UIFactory.CreateHorizontalGroup(config_root, "_", true, false, true, true);
        UIFactory.CreateLabel(_horizontal, MainRuntime.AddPower.Definition.Key,
            MainRuntime.AddPower.Description.Description);
        UIFactory.CreateToggle(_horizontal, MainRuntime.AddPower.Definition.Key + "_Toggle", out var toggle,
            out var text);
        text.text = "是否开启";
        toggle.onValueChanged.AddListener(stat => { MainRuntime.AddPower.Value = stat; });
        
        _horizontal = UIFactory.CreateHorizontalGroup(config_root, "_", true, false, true, true);
        UIFactory.CreateLabel(_horizontal, MainRuntime.InfFoodIngredients.Definition.Key,
            MainRuntime.InfFoodIngredients.Description.Description);
        UIFactory.CreateToggle(_horizontal, MainRuntime.InfFoodIngredients.Definition.Key + "_Toggle", out toggle,
            out text);
        text.text = "是否开启";
        toggle.onValueChanged.AddListener(stat => { MainRuntime.InfFoodIngredients.Value = stat; });
        
        _horizontal = UIFactory.CreateHorizontalGroup(config_root, "_", true, false, true, true);
        UIFactory.CreateLabel(_horizontal, MainRuntime.SimpleAlchemyOnFree.Definition.Key,
            MainRuntime.SimpleAlchemyOnFree.Description.Description);
        UIFactory.CreateToggle(_horizontal, MainRuntime.SimpleAlchemyOnFree.Definition.Key + "_Toggle", out toggle,
            out text);
        text.text = "是否开启";
        toggle.onValueChanged.AddListener(stat => { MainRuntime.SimpleAlchemyOnFree.Value = stat; });
        
        _horizontal = UIFactory.CreateHorizontalGroup(config_root, "_", true, false, true, true);
        UIFactory.CreateLabel(_horizontal, MainRuntime.SuperAutoFood.Definition.Key,
            MainRuntime.SuperAutoFood.Description.Description);
        UIFactory.CreateToggle(_horizontal, MainRuntime.SuperAutoFood.Definition.Key + "_Toggle", out toggle,
            out text);
        text.text = "是否开启";
        toggle.onValueChanged.AddListener(stat => { MainRuntime.SuperAutoFood.Value = stat; });
        
        _horizontal = UIFactory.CreateHorizontalGroup(config_root, "_", true, false, true, true);
        UIFactory.CreateLabel(_horizontal, MainRuntime.SuperAutoFood.Definition.Key,
            MainRuntime.SuperAutoFood.Description.Description);
        UIFactory.CreateToggle(_horizontal, MainRuntime.SuperAutoFood.Definition.Key + "_Toggle", out toggle,
            out text);
        text.text = "是否开启";
        toggle.onValueChanged.AddListener(stat => { MainRuntime.SuperAutoFood.Value = stat; });

        UIFactory.CreateLabel(about_root, "aboutInfo", @"本模组作者为zender
作者qq号：3101376153（没事别乱加）
作者GitHub主页https://github.com/aCzenderCa
");
    }

    public override string Name { get; }
    public override int MinWidth { get; }
    public override int MinHeight { get; }
    public override Vector2 DefaultAnchorMin { get; }
    public override Vector2 DefaultAnchorMax { get; }
}
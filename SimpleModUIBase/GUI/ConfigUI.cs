using System.Collections.Generic;
using UnityEngine;

namespace SimpleModUIBase.GUI;

public class ConfigUI : MonoBehaviour
{
    public List<PanelBase> AllPanel = new();
    public BoolConfigCell PrefabBoolConfigCell = null!;

    public void SetActivePanel(string id)
    {
        AllPanel.ForEach(panel => { panel.SetActive(panel.id == id); });
    }
}
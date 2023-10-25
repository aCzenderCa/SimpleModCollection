using UnityEngine;

namespace SimpleModUIBase.GUI;

public abstract class PanelBase:MonoBehaviour
{
    public string id = "";
    public ConfigUI? ConfigUI;
    
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public string message;

    void OnMouseEnter()
    {
        TooltipSingleton._instance.ShowTooltip(GetInstanceID(), message);
    }
    //this may cause wonkyness when moving fast
    void OnMouseExit()
    {
        TooltipSingleton._instance.HideTooltip(GetInstanceID());
    }

    void OnDestroy()
    {
        TooltipSingleton._instance.HideTooltip(GetInstanceID());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public string message;
    public PlantBehavior p;

    void OnMouseEnter()
    {
        SequencePlant plnt = p.p as SequencePlant;
        TooltipSingleton._instance.ShowTooltip(GetInstanceID(), message, new List<Season>(plnt.neededSeasons), new List<Season>(plnt.forbiddenSeasons));
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

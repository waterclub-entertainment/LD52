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

        if (plnt == null)
        {
            TooltipSingleton._instance.ShowTooltip(GetInstanceID(), message, new List<Season>(), Season.None);
        }
        else
        {
            var neededSeasons = new List<Season>(plnt.neededSeasons);
            if ((neededSeasons.Count - plnt.getStage()) > 0)
                neededSeasons = neededSeasons.GetRange(plnt.getStage(), neededSeasons.Count - plnt.getStage());
            else if ((neededSeasons.Count - plnt.getStage()) == 0)
                neededSeasons = new List<Season>();
            TooltipSingleton._instance.ShowTooltip(GetInstanceID(), message, neededSeasons, plnt.forbiddenSeasons);
        }
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    [HideInInspector]
    public int x, y; //source coordinates

    public List<Season> seasons;
    public float stageThreshold;

    public bool shouldApply(Season s, float stage)
    {
        return seasons.Contains(s) && stageThreshold <= stage;
    }

    public abstract void applyEffect(PlotContext ctx);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect
{
    protected int x, y; //source coordinates

    public Effect(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public abstract void applyEffect(PlotContext ctx);
}

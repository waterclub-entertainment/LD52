using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Effect
{
    public void applyEffect(PlotContext ctx, int x, int y);
}

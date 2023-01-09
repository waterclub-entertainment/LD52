using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SeasonHandler", menuName = "ScriptableObjects/SeasonHandler", order = 1)]
public class SeasonHandler : ScriptableObject
{
    public Animator seasonChangeAnimator;

    public interface SeasonChangeListener
    {
        public void onSeasonChange(Season s);
    }

    public List<SeasonChangeListener> listeners = new List<SeasonChangeListener>();

    public void TriggerChange(Season s)
    {
        Debug.Log("Changing Season To " + s.ToString());
        foreach(SeasonChangeListener listener in listeners)
            listener.onSeasonChange(s);
    }
}

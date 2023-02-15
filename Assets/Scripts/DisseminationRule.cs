using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Plant/Disemination Rule")]
public class DisseminationRule : ScriptableObject
{
    [Serializable]
    public class PlantRule
    {
        public Plant p;
        public bool whitelist;
        public int count;
    }
    
    public List<PlantRule> parents;
    public Plant child;
    public int priority;
    public int probability;
    public int tier;

    public bool isApplicable(List<Plant> plants)
    {
        if (parents.Count > plants.Count)
            return false;

        var prob = UnityEngine.Random.Range(0, 100);

        foreach (var p in parents)
        {
            var cnt = plants.Count(item => item.title == p.p.title);
            if (p.whitelist)
            {
                if (p.count > cnt)
                {
                    //Debug.Log("Dropping Rule. Not enough of instance " + p.p.title);
                    return false; //not enough of one plant
                }
            }
            else
            {
                Debug.Log("Dropping Rule. Too many of instance");
                return !(cnt > 0);
            }
        }
        return probability > prob;
    }
    
}

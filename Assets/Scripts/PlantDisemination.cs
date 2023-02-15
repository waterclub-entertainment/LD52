using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlantDisemination : MonoBehaviour
{
    public static PlantDisemination _instance;

    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    /// <summary>
    /// Get all instances of scriptable objects with given type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static List<T> GetAllInstances<T>() where T : ScriptableObject
    {
        return AssetDatabase.FindAssets($"t: {typeof(T).Name}").ToList()
                    .Select(AssetDatabase.GUIDToAssetPath)
                    .Select(AssetDatabase.LoadAssetAtPath<T>)
                    .ToList();
    }
    public static List<DisseminationRule> GetRuleInstances() { return GetAllInstances<DisseminationRule>(); }

    public List<Plant> computePossiblePlants(List<Plant> neighbors)
    {
        var rules = GetRuleInstances();
        var res = rules.Where(c => c.isApplicable(neighbors)).OrderBy(c => -c.tier).ThenBy(c => -c.priority).Select(c => c.child).ToList();
        foreach (Plant p in res)
        {
            Debug.Log("Rule to spawn " + p.title + "applies");
        }
        return res;
    }
}

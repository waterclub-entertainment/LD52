using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonHandlerProxy : MonoBehaviour
{
    public SeasonHandler handler;
    public Season season;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        handler.TriggerChange(season);
    }
}

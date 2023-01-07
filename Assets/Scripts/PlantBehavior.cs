using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBehavior : MonoBehaviour
{
    public Plant p;

    public void setPlant(Plant p)
    {
        this.p = p; //move
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.localScale = new Vector3(0.4f, p.getStage(), 0.4f);
    }
}

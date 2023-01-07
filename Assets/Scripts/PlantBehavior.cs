using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBehavior : MonoBehaviour
{
    public SequencePlant p;

    public void setPlant(Plant p, int x, int y)
    {
        this.p = p as SequencePlant; //move
        this.p.OnPlant(x, y);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMouseDown()
    {
        p.HarvestReward();
        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(0.4f, p.getStage(), 0.4f);
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    Nullable<Season> actualSeason; //this keeps track of the base state. Relevant to apply multiple mutations on tick
    int x, y;
    int initialized = 0;

    //TODO add mutations for effects.
    public Nullable<Season> season; //does this need to be a collection of seassons due to plants?
    public bool guarded;
    public int tickSize;

    GameObject plant;

    //TODO, REMOVE
    public GameObject plantPrefab;

    public void setup(int x, int y)
    {
        this.x = x;
        this.y = y;
        if ((initialized & 1) != 0)
            transform.localPosition = new Vector3(x, 0, y);
        initialized |= 2;
        reset();
    }
    public void reset()
    {
        season = null;

        //reset mutations
        actualSeason = null;
        tickSize = 1;
        guarded = false;
    }

    //update pipeline calls
    public void setSeasonState(Season season, bool isDefault = false)
    {
        this.season = season;
        
        if (isDefault)
            actualSeason = season;
    }
    public void computeEffects(ref List<Effect> effects)
    {
        if (plant != null)
            plant.GetComponent<PlantBehavior>().p.getEffects(actualSeason.Value, ref effects);
    }
    public void apply()
    {
        if (plant != null)
        {
            var res = plant.GetComponent<PlantBehavior>().p.Progress(season.Value);
            if (!res) //plant ded
            {
                Destroy(plant);
                plant = null;
            }
        }
        else
        {
            //handle dissemination and spawning of new plants as mutations of neighbors?
        }
    }

    public bool setPlant(Plant p)
    {
        //maybe this should be moved down the line?
        if (plant)
            return false;
        plant = Instantiate(plantPrefab) as GameObject;
        plant.transform.SetParent(this.transform, false);
        PlantBehavior beh = plant.GetComponent<PlantBehavior>();

        beh.setPlant(p, x, y);
        return true;
    }
    public GameObject getPlant()
    {
        return plant;
    }

    public void removePlant()
    {
        if (plant)
            Destroy(plant);
        plant = null;
    }

    void Start()
    {
        if ((initialized & 2) != 0)
            transform.localPosition = new Vector3(x, 0, y);
        initialized |= 1;
    }

    void Update()
    {

    }

    void OnMouseEnter()
    {
        transform.position += new Vector3(0f,0.1f,0f);
    }
    void OnMouseExit()
    {
        transform.position -= new Vector3(0f, 0.1f, 0f);
    }
}

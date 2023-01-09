using System;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    Season actualSeason; //this keeps track of the base state. Relevant to apply multiple mutations on tick
    public Plant fallowPlant;
    int x, y;
    int initialized = 0;

    //TODO add mutations for effects.
    [HideInInspector]
    public Season season; //does this need to be a collection of seassons due to plants?
    [HideInInspector]
    public bool guarded; //TODO

    [HideInInspector]
    public bool shouldKill; //Implemented

    [HideInInspector]
    public bool spawnsFallow;

    public class ReplacePlant
    {
        public ReplacePlant(Plant p, bool replace) { this.p = p; this.replace = replace; }
        public Plant p;
        public bool replace;
    }
    [HideInInspector]
    public ReplacePlant spawnPlant; //Implemented

    public int tickSize; //TODO

    [HideInInspector]
    public bool hovering = false;

    GameObject plant;

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
        season = Season.None;

        //reset mutations
        actualSeason = Season.None;
        tickSize = 1;
        guarded = false;
        shouldKill = false;
        spawnsFallow = true;
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
        {
            PlantBehavior beh = plant.GetComponent<PlantBehavior>();
            Plant p = beh.p;
            p.getEffects(actualSeason, ref effects);
        }
    }
    public void apply()
    {
        if (plant != null && (spawnPlant == null || !spawnPlant.replace))
        {
            var res = plant.GetComponent<PlantBehavior>().p.Progress(season, actualSeason);
            plant.GetComponent<PlantBehavior>().UpdateStage();
            if (!res || shouldKill) //plant ded
            {
                Debug.Log("Plant Died (Killed: " + shouldKill.ToString() + ")");

                removePlant();
                if (spawnsFallow)
                {
                    setPlant(fallowPlant);
                }
            }
            else
            {
                Debug.Log("Plant Grew");
            }
        }
        else if (spawnPlant != null)
        {
            if (plant != null && spawnPlant.replace)
                removePlant();

            setPlant(spawnPlant.p);
        }
    }

    public bool setPlantNoSound(Plant p)
    {
        //maybe this should be moved down the line?
        if (plant != null)
            return false;
        plant = Instantiate(p.prefab) as GameObject;
        plant.GetComponent<AudioSource>().enabled = false; // no sound
        plant.transform.SetParent(this.transform, false);
        PlantBehavior beh = plant.GetComponent<PlantBehavior>();

        beh.setPlant(p, x, y);
        return true;
    }

    public bool setPlant(Plant p)
    {
        //maybe this should be moved down the line?
        if (plant != null)
            return false;

        // Prevent corpseflower from being planted in a position where it would die
        if (p.title.Equals("CorpseFlower")) {
            FieldController fieldController = transform.parent.GetComponent<FieldController>();
            bool hasFallow = false;
            foreach (GameObject prop in fieldController.ctx.getNeighbors(x, y)) {
                Plot plot = prop.GetComponent<Plot>();
                GameObject pl = plot.getPlant();
                if (pl == null)
                    continue;
                Plant plnt = pl.GetComponent<PlantBehavior>().p;
                if (plnt.title == "Fallow")
                    hasFallow |= true;
            }
            if (!hasFallow) {
                return false;
            }
        }

        plant = Instantiate(p.prefab) as GameObject;
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
        {
            HarvestStack harvestStack = GameObject.FindObjectOfType<HarvestStack>();
            PlantBehavior beh = plant.GetComponent<PlantBehavior>();
            Plant p = beh.p;
            harvestStack.AddHidden(p.HarvestReward()); //this is unsafe
            Destroy(plant);
        }
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
        if (hovering) {
            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        } else {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}

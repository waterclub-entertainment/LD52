using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour
{
    public int X = 4;
    public int Y = 5;
    public GameObject plotPrefab;
    public Plant plant;

    PlotContext ctx;

    // Start is called before the first frame update
    void Start()
    {
        ctx = new PlotContext(X, Y, () => { return Instantiate(plotPrefab, transform) as GameObject; });
        ctx.applyToAllEx((Plot p, PlotContext ctx, int x, int y) => { p.setup(x, y); });
    }

    void Step(Season season)
    {
        //reset plots for steps
        ctx.applyToAll((Plot plt) => { plt.reset(); plt.setSeasonState(season, true); });

        List<Effect> effects = new List<Effect>();

        //iterate plant effects over season
        ctx.applyToAll((Plot plt) => { plt.computeEffects(ref effects); });

        //TODO: order effects
        foreach (Effect e in effects)
        {
            e.applyEffect(ctx);
        }

        //update plants
        ctx.applyToAll((Plot plt) => { plt.apply(); });
    }

    void Harvest(PlantBehavior beh)
    {
        beh.p.HarvestReward();//spawn new plant datastructure to be linked to behavior
        ctx.getPlot(beh.p.x.Value, beh.p.y.Value).GetComponent<Plot>().removePlant();
    }

    // Update is called once per frame
    void Update()
    {
        //get clicked plots
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                GameObject obj = hit.collider.gameObject;
                var plt = obj.GetComponent<Plot>();
                var plnt = obj.GetComponent<PlantBehavior>();
                if (plt != null) {
                    HandController handController = GameObject.FindObjectOfType<HandController>();
                    HandCard card = handController.GetSelected();
                    if (card != null) {
                        if (plt.setPlant(Instantiate(card.card.plant)))
                            handController.PlayCard(card);
                    }
                    else
                    {
                        if (plt.getPlant() != null)
                        {
                            var pnt_beh = plt.getPlant().GetComponent<PlantBehavior>();
                            Harvest(pnt_beh);
                        }
                    }
                }
                else if (plnt != null) //HARVESTING
                {
                    Harvest(plnt);
                }
            }
        }
        // TODO: handle animations i guess. maybe poll UI event?
    }
}

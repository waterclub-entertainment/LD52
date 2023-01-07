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

    Season[] seasons = {Season.Spring, Season.Summer, Season.Autumn, Season.Winter };
    int lastSeason = 0;

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
                if(plt != null) {
                    HandController handController = GameObject.FindObjectOfType<HandController>();
                    HandCard card = handController.GetSelected();
                    if (card != null) {
                        handController.PlayCard(card);
                        plt.setPlant(Instantiate(card.card.plant));
                    }
                }
            }
        }

        // TODO: handle animations i guess. maybe poll UI event?
    }
}

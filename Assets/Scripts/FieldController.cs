using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour, SeasonHandler.SeasonChangeListener
{
    public int X = 4;
    public int Y = 5;
    public GameObject plotPrefab;

    public SeasonHandler handler;

    PlotContext ctx;

    private Plot lastHoveredPlot;

    // Start is called before the first frame update
    void Start()
    {
        //TODO Clean
        ctx = new PlotContext(X, Y, () => { return Instantiate(plotPrefab, transform) as GameObject; });
        ctx.applyToAllEx((Plot p, PlotContext ctx, int x, int y) => { p.setup(x, y); });
        handler.listeners.Add(this);
    }

    public void onSeasonChange(Season season)
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
        HarvestStack harvestStack = GameObject.FindObjectOfType<HarvestStack>();
        Card reward = beh.p.HarvestReward();
        if (reward != null) {
            harvestStack.Add(reward);
        }
        // TODO: Animation
        Plot plot = beh.transform.parent.GetComponent<Plot>();
        plot.removePlant();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Plot plot in GetComponentsInChildren<Plot>()) {
            plot.hovering = false;
        }
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {
            GameObject obj = hit.collider.gameObject;
            var plt = obj.GetComponent<Plot>();
            var plnt = obj.GetComponent<PlantBehavior>();
            if (plnt != null) {
                plt = obj.transform.parent.GetComponent<Plot>();
            }
            if (plt != null) {
                plt.hovering = true;
                if (Input.GetMouseButtonDown(0)) {
                    HandController handController = GameObject.FindObjectOfType<HandController>();
                    HandCard card = handController.GetSelected();
                    if (card != null) {
                        if (plt.setPlant(Instantiate(card.card.plant)))
                            handController.PlayCard(card);
                        else
                            handController.GetSelected().selected = false;
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
            }
        }
    }
}

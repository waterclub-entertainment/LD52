using UnityEngine;

public class PlantBehavior : MonoBehaviour
{
    public Plant p;
    public AudioSource src;
    public GameObject winterNextSeasonEffect;
    public GameObject springNextSeasonEffect;
    public GameObject summerNextSeasonEffect;
    public GameObject autumnNextSeasonEffect;

    private GameObject nextSeasonEffect = null;

    public void setPlant(Plant p, int x, int y)
    {
        this.p = p; //move
        foreach (Effect e in GetComponents<Effect>())
        {
            e.x = x;
            e.y = y;
            this.p.RegisterEffect(e);
        }
        
        UpdateStage();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (src != null) {
            src.Play();
        }
    }

    // Update is called once per frame
    public void UpdateStage()
    {
        float scale = p.GrowthStage() * 0.5f + 0.5f;
        transform.localScale = new Vector3(scale, scale, scale);
        // Destroy old effect
        if (nextSeasonEffect != null) {
            Destroy(nextSeasonEffect);
        }
        Season? nextSeason = p.NextSeasonEffect();
        if (nextSeason != null) {
            switch (nextSeason) {
                case Season.Winter:
                    if (winterNextSeasonEffect != null)
                        nextSeasonEffect = Instantiate(winterNextSeasonEffect, transform);
                    break;
                case Season.Spring:
                    if (springNextSeasonEffect != null)
                        nextSeasonEffect = Instantiate(springNextSeasonEffect, transform);
                    break;
                case Season.Summer:
                    if (summerNextSeasonEffect != null)
                        nextSeasonEffect = Instantiate(summerNextSeasonEffect, transform);
                    break;
                case Season.Autumn:
                    if (autumnNextSeasonEffect != null)
                        nextSeasonEffect = Instantiate(autumnNextSeasonEffect, transform);
                    break;
            }
        }
    }
}

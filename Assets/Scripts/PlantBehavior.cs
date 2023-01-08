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

    public void setPlant(Plant p)
    {
        this.p = p; //move
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
                    nextSeasonEffect = Instantiate(winterNextSeasonEffect, transform);
                    break;
                case Season.Spring:
                    nextSeasonEffect = Instantiate(springNextSeasonEffect, transform);
                    break;
                case Season.Summer:
                    nextSeasonEffect = Instantiate(summerNextSeasonEffect, transform);
                    break;
                case Season.Autumn:
                    nextSeasonEffect = Instantiate(autumnNextSeasonEffect, transform);
                    break;
            }
        }
    }
}

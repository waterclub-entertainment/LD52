using UnityEngine;

public class PlantBehavior : MonoBehaviour
{
    public Plant p;
    public AudioSource src;
    public GameObject winterNextSeasonEffect;
    public GameObject springNextSeasonEffect;
    public GameObject summerNextSeasonEffect;
    public GameObject autumnNextSeasonEffect;
    public Animator animator;

    private GameObject nextSeasonEffect = null;
    private float plantingTime = 0;

    private float lastStage;

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
        if (p.title == "Fallow") {
            plantingTime = 1.2f;
        } else {
            transform.localScale = Vector3.zero;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (src != null) {
            src.Play();
        }
        lastStage = 0;
    }

    void Update() {
        // Planting animation
        if (plantingTime < 1.2f) {
            plantingTime += Time.deltaTime;
            float scale = Mathf.Max(0, plantingTime - 0.8f) / 0.4f * 0.5f;
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    // Update is called once per frame
    public void UpdateStage()
    {
        float scale = p.GrowthStage() * 0.5f + 0.5f;
        transform.localScale = new Vector3(scale, scale, scale);

        if (animator != null)
            if (scale != lastStage)
            {
                animator.SetTrigger("HasGrown");
                if (scale == 1)
                    animator.SetTrigger("FinishedGrowth");
            }
        lastStage = scale;

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

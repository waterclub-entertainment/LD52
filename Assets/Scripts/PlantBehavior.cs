using UnityEngine;

public class PlantBehavior : MonoBehaviour
{
    public Plant p;
    public AudioSource src;

    public void setPlant(Plant p)
    {
        this.p = p; //move
    }

    // Start is called before the first frame update
    void Start()
    {
        if (src != null) {
            src.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float scale = p.GrowthStage() * 0.5f + 0.5f;
        transform.localScale = new Vector3(scale, scale, scale);
    }
}

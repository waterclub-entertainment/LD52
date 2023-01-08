using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteIconographer : MonoBehaviour
{
    public List<Season> iconSeasons;

    [Serializable]
    public class SeasonIcon
    {
        public Season season;
        public Sprite sprite;
    }
    public List<SeasonIcon> seasonMap;
    public Sprite FallowOverlay;
    public GameObject iconPrefab;
    // Start is called before the first frame update
    void Start()
    {
        if (iconSeasons != null)
        {
            float i = 0.0f;
            foreach (Season s in iconSeasons)
            {
                GameObject obj = Instantiate(iconPrefab) as GameObject;
                obj.transform.SetParent(transform, false);
                obj.transform.Translate(new Vector3(i, 0.0f, 0.0f));
                i += 1.0f;
                SpriteRenderer renderer = obj.GetComponent<SpriteRenderer> ();
                renderer.sprite = seasonMap.Find(x => x.season == s).sprite;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

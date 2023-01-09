using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteIconographer : MonoBehaviour
{
    [HideInInspector]
    public List<Season> iconSeasons;
    [HideInInspector]
    public Season fallowIconSeasons;

    [Serializable]
    public class SeasonIcon
    {
        public Season season;
        public Sprite sprite;
    }
    public List<SeasonIcon> seasonMap;
    public Sprite FallowOverlay;
    public GameObject iconPrefab;

    public int horizontalOffsetFactor;
    // Start is called before the first frame update
    void Start()
    {
        UpdateSequence();
    }

    public void UpdateSequence()
    {
        foreach (Image child in GetComponentsInChildren<Image>())
        {
            if (child.gameObject != this.gameObject)
                Destroy(child.gameObject);
        }
        if (iconSeasons != null)
        {
            int actualIncrements = 0;
            for (int i = 0; i < iconSeasons.Count; i++)
            {
                SeasonIcon icon = seasonMap.Find(x => x.season == iconSeasons[i]);
                if (icon == null)
                    continue;
                GameObject obj = Instantiate(iconPrefab) as GameObject;
                obj.transform.SetParent(transform, false);
                obj.transform.Translate(actualIncrements * horizontalOffsetFactor, 0, 0);
                Image renderer = obj.GetComponent<Image>();
                renderer.sprite = icon.sprite;
                actualIncrements += 1;
            }
        }

        int actualIncrements1 = 0;
        for (int i = 0; i < 4; i++)
        {
            int seasonID = (1 << i) & (int)fallowIconSeasons;
            if (seasonID == 0)
                continue;
            Season s = (Season)(seasonID);
            SeasonIcon icon = seasonMap.Find(x => x.season == s);
            if (icon == null)
                continue;

            GameObject obj = Instantiate(iconPrefab) as GameObject;
            obj.transform.SetParent(transform, false);
            obj.transform.Translate(actualIncrements1 * horizontalOffsetFactor, 0, 0);
            Image renderer = obj.GetComponent<Image>();
            renderer.sprite = seasonMap.Find(x => x.season == s).sprite;

            obj = Instantiate(iconPrefab) as GameObject;
            obj.transform.SetParent(transform, false);
            obj.transform.Translate(actualIncrements1 * horizontalOffsetFactor, 0, 1);
            renderer = obj.GetComponent<Image>();
            renderer.sprite = FallowOverlay;
            actualIncrements1 += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

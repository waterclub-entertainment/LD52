using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            for (int i = 0; i < iconSeasons.Count; i++)
            {
                GameObject obj = Instantiate(iconPrefab) as GameObject;
                obj.transform.SetParent(transform, false);
                obj.transform.Translate(i * horizontalOffsetFactor, 0, 0);
                Image renderer = obj.GetComponent<Image>();
                renderer.sprite = seasonMap.Find(x => x.season == iconSeasons[i]).sprite;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipSingleton : MonoBehaviour
{
    public static TooltipSingleton _instance;
    public TextMeshProUGUI text;

    private int currentOwner;

    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        gameObject.SetActive(false);
    }

    public void ShowTooltip(int id, string text)
    {
        gameObject.SetActive(true);
        this.text.text = text;
        currentOwner = id;
    }
    public void HideTooltip(int id)
    {
        if (currentOwner != id)
            return;
        gameObject.SetActive(false);
        this.text.text = string.Empty;
        currentOwner = -1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
    }
}

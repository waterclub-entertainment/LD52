using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogAnimator : MonoBehaviour, SeasonHandler.SeasonChangeListener
{
    public float BaseRotation = 360.0f;
    public float PointerRotation = 22.5f;
    public CogAnimation Pointer;
    public List<CogAnimation> cogs;
    public SeasonHandler handler;
    public Animator animatior;

    private float goalOffset = 220.0f;
    private float offset = 220.0f;

    private float elapsedTime;

    private int windDown = 0;

    [Serializable]
    public class CogAnimation
    {
        public GameObject obj;
        public float secondsPerRotation;
    }


    // Start is called before the first frame update
    void Start()
    {
        handler.listeners.Add(this);
        elapsedTime = 0.0f;
        offset = goalOffset;

        windDown = 0;
    }

    public void onSeasonChange(Season s)
    {
        animatior.SetTrigger("WindupTrigger");
        if (s == Season.Spring)
        {
            goalOffset = 220.0f;
        }
        else if (s == Season.Summer)
        {
            goalOffset = 140.0f;
        }
        else if (s == Season.Autumn)
        {
            goalOffset = 45.0f;
        }
        else if (s == Season.Winter)
        {
            goalOffset = 320.0f;
        }
        windDown = 1;
    }

    public void onWindDown()
    {
        windDown = 2;
    }
    public void onWindDownEnd()
    {
        windDown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (CogAnimation cog in cogs)
        {
            cog.obj.transform.Rotate(new Vector3(0.0f, 0.0f, Time.deltaTime * BaseRotation / cog.secondsPerRotation));
        }

        //Improve
        elapsedTime += Time.deltaTime;
        Pointer.obj.transform.localEulerAngles = new Vector3(0.0f, 0.0f, ((float)Math.Sin(Math.PI * 0.5f * elapsedTime / Pointer.secondsPerRotation) * PointerRotation) + 360.0f + offset);

        if ((windDown == 1) || ((windDown != 1) && (Math.Abs(offset - goalOffset) > 1.0f)))
        {
            offset += Time.deltaTime * BaseRotation / Pointer.secondsPerRotation;
        }
        if (offset > 360.0f)
        {
            offset -= 360.0f;
        }
    }
}

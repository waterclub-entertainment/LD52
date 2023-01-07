using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogAnimator : MonoBehaviour
{
    public float BaseRotation = 360.0f;
    public List<CogAnimation> cogs;

    [Serializable]
    public class CogAnimation
    {
        public GameObject obj;
        public float secondsPerRotation;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (CogAnimation cog in cogs)
        {
            cog.obj.transform.Rotate(new Vector3(0.0f, 0.0f, Time.deltaTime * BaseRotation / cog.secondsPerRotation));
        }
    }
}

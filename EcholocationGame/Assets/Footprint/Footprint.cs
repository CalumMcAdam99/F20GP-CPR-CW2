using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footprint : MonoBehaviour
{
    public float Lifetime = 20.0f;

    private float mark;
    private Vector3 OrigSize;
    // Start is called before the first frame update
    void Start()
    {
        mark = Time.time;
        OrigSize = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float ElapsedTime = Time.time - mark;
        if (ElapsedTime != 0)
        {
            float PercentTimeLeft = (Lifetime - ElapsedTime) / Lifetime;
            this.transform.localScale = new Vector3(OrigSize.x * PercentTimeLeft, OrigSize.y * PercentTimeLeft, OrigSize.z * PercentTimeLeft);
            if(ElapsedTime > Lifetime)
            {
                Destroy(this.gameObject);
            }
        }
    }
}

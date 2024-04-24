using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAniScr : MonoBehaviour
{
    private void Awake()
    {
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    private void Update()
    {
        transform.localScale += new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
        if (transform.localScale.x >= 1f)
        {
            Destroy(gameObject);
        }
    }
}

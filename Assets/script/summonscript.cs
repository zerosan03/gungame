using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class summonscript : MonoBehaviour
{
    public GameObject safety;
    public GameObject ironPrefab;
    public float safetydistance;
    private float summonInterval = 50;
    // Start is called before the first frame update
    private void Start()
    {
        safety = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        safetydistance = Vector3.Distance(safety.transform.position, transform.position);
        if (safetydistance < 50)
        {
            summonInterval -= 1;
            if (summonInterval < 0)
            {
                Instantiate(ironPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
                summonInterval = 500;
            }
        }
    }
}

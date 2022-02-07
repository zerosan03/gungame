using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookatplayerscript : MonoBehaviour
{
    public GameObject targetObject;
    private void Start()
    {
        targetObject = GameObject.Find("Player");
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(targetObject.transform);
    }
}

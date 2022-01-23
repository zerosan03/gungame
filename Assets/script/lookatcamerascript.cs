using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookatcamerascript : MonoBehaviour
{
    public GameObject targetObject; 

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(targetObject.transform);
    }
}

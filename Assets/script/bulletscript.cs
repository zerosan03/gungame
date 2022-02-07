using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletscript : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if ((other.collider.tag != "turret") && (other.collider.tag != "iron") && (other.collider.tag != "summon"))
        {
            Destroy(gameObject);
        }
    }
}

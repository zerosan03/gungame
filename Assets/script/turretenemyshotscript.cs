using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretenemyshotscript : MonoBehaviour
{
    public GameObject safety;
    public GameObject bulletPrefab;
    public float safetydistance;
    public float shotSpeed;
    private float shotInterval = 0;
    private void Start()
    {
        safety = GameObject.Find("Player");
    }
    // Update is called once per frame
    void LateUpdate()
    {
        safetydistance = Vector3.Distance(safety.transform.position, transform.position);
        if (safetydistance < 50)
        {
            shotInterval -= 1;
            if (shotInterval < 0)
            {
                GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                bulletRb.AddForce(transform.forward * shotSpeed);
                shotInterval = 50;
            }
        }
    }
}

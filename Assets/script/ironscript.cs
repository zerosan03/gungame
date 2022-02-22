using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ironscript : MonoBehaviour
{
    [SerializeField] Image HPgauge;
    public GameObject targetObject;
    public GameObject Lvtext;
    public GameObject clearflag;
    public Rigidbody myRigidbody;
    public Vector3 myscale;
    public float velocityZ = 0;
    public float myrandom;
    public float random;
    public float myLv = 1;
    public float Lv;
    public float Lv1;
    public float enemyHP = 5;
    public float firstenemyHP;
    public float maxenemyHP;
    public float counter = 1;
    private void Start()
    {
        myrandom = Random.Range(0.1f, 9999.9f);
        targetObject = GameObject.Find("Player");
        clearflag = GameObject.Find("clearflag");
        clearflag.GetComponent<gameclearscript>().enemycounter(counter);
        maxenemyHP = enemyHP;
        firstenemyHP = maxenemyHP;
    }
    // Update is called once per frame
    void Update()
    {
        Transform trans = transform;
        transform.position = trans.position;
        this.transform.LookAt(targetObject.transform);
        myRigidbody.velocity = trans.TransformDirection(new Vector3(0, 0, velocityZ));
        if (velocityZ < 5)
        {
            velocityZ += 1;
        }
        else
        {
            velocityZ = 5;
        }
        myscale.x = 0.5f + (myLv * 0.1f);
        myscale.y = 0.5f + (myLv * 0.1f);
        myscale.z = 0.5f + (myLv * 0.1f);
        gameObject.transform.localScale = myscale;
        HPgauge.fillAmount = enemyHP / maxenemyHP;
        this.Lvtext.GetComponent<Text>().text = "Lv" + myLv;

    }
    public void Lvdelete(float getrandom,float getLv)
    {
        random = getrandom;
        Lv = getLv;
        if (myrandom < random)
        {
            Destroy(this.gameObject);
            clearflag.GetComponent<gameclearscript>().enemycounter(-counter);
        }
        else if (myrandom != random)
        {
            myLv = myLv + Lv;
            maxenemyHP = firstenemyHP + myLv * 2 - 2;
            enemyHP = maxenemyHP;
        }
    }
    public void ReceveDamage(int damageScore)
    {
        enemyHP -= damageScore;
        //オーバーキルでHPが0を飛び越えても対処できるよう、判定を0以下にしておく
        if (enemyHP <= 0)
        {
            //このスクリプトがアタッチされているオブジェクトを消す
            Destroy(this.gameObject);
            clearflag.GetComponent<gameclearscript>().enemycounter(-counter);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            velocityZ = -20;
            other.collider.GetComponent<playercontrol>().irondamage(myLv);
        }
        if (other.collider.tag == "iron")
        {
            other.collider.GetComponent<ironscript>().Lvdelete(myrandom,myLv);
        }
    }
}

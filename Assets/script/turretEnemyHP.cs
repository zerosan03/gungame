using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class turretEnemyHP : MonoBehaviour
{
    [SerializeField] Image HPgauge;
    float enemyHP = 10;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HPgauge.fillAmount = enemyHP / 10;
    }
    //被ダメージ処理
    public void ReceveDamage(int damageScore)
    {
        enemyHP -= damageScore;
        //オーバーキルでHPが0を飛び越えても対処できるよう、判定を0以下にしておく
        if (enemyHP <= 0)
        {
            //このスクリプトがアタッチされているオブジェクトを消す
            Destroy(this.gameObject);
        }
    }
}
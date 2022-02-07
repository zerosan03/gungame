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
    //��_���[�W����
    public void ReceveDamage(int damageScore)
    {
        enemyHP -= damageScore;
        //�I�[�o�[�L����HP��0���щz���Ă��Ώ��ł���悤�A�����0�ȉ��ɂ��Ă���
        if (enemyHP <= 0)
        {
            //���̃X�N���v�g���A�^�b�`����Ă���I�u�W�F�N�g������
            Destroy(this.gameObject);
        }
    }
}
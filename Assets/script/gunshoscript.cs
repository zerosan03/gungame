using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gunshoscript : MonoBehaviour
{
    //Image�N���X�ϐ���錾�BSerializeField��Inspector�ォ���`
    [SerializeField] Image sight;
    [SerializeField] Image hitaction;
    //ray�̒���
    public float rayLength = 20000;
    //ray�����������I�u�W�F�N�g�̏����擾����ׂ̕ϐ�
    RaycastHit hit;
    //�G�ɗ^����_���[�W�̒l
    public int damageScore = 1;
    public int gunbullet = 6;
    public int fastgunbullet;
    public GameObject gunbulletText;
    public int hittime = 0;
    public int reloadtime = 100;
    public int reloadtimecounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        this.gunbulletText = GameObject.Find("gunbullet");
        fastgunbullet = gunbullet;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gunbullet > 0)
        {
            gunbullet -= 1;
        }
        //�J�����̌��_���烌�C���΂��ƃv���C���[���g�̃^�O���擾���Ă��܂��ׁAZ�����ɃY����
        Ray ray = new Ray(transform.position + transform.rotation * new Vector3(0, 0, 0.01f), transform.forward);
        //ray�̉���
        //ray��ɃR���C�_�[�����݂���ꍇ�ARayCastHit�^�ϐ��ɏ����i�[����Bout�C���q��t���āA�߂�l���擾�����Ɋ֐����ŕϐ��̒l�𑀍삷��
        if (Physics.Raycast(ray, out hit, rayLength))
        {
            //hit�Ɏ擾�������̓��A�^�O�����擾
            string hitTagName = hit.transform.gameObject.tag;
            //�^�O����Enemy�������ꍇ
            if (hitTagName == "Enemy")
            {
                //Enemy�^�O�̃I�u�W�F�N�g��ray�������������̏Ə��̐F
                sight.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
                //�}�E�X�̍��{�^�����N���b�N���ꂽ��
                if (Input.GetMouseButtonDown(0) && gunbullet > 0)
                {
                    //Enemy�I�u�W�F�N�g�ɕt���Ă���EnemyHP��ReceveDamage�֐����Ăяo��
                    hit.collider.GetComponent<EnemyHP>().ReceveDamage(damageScore);
                    hittime = 5;
                }
            }
            else
            {
                //Enemy�^�O�ȊO�̃I�u�W�F�N�g��ray�������������̏Ə��̐F
                sight.color = new Color(1.0f, 1.0f, 1.0f, 0.8f);
            }
        }
        else
        {
            //ray���ǂ̃I�u�W�F�N�g�ɂ��������Ă��Ȃ���(��������Ă��鎞)�̏Ə��̐F
            sight.color = new Color(1.0f, 1.0f, 1.0f, 0.8f);
        }
        if (hittime >= 0)
        {
            hitaction.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            hittime -= 1;
        }
        else
        {
            hitaction.color = new Color(1.0f, 0.0f, 0.0f, 0.0f);
        }
        //reload
        if (Input.GetButtonDown("Reload"))
        {
            reloadtimecounter = 1;
        }
        if (reloadtime == -1)
        {
            reloadtime = 100;
            reloadtimecounter = 0;
            gunbullet = fastgunbullet;
        }
        reloadtime -= reloadtimecounter;
        this.gunbulletText.GetComponent<Text>().text = fastgunbullet + "/" + gunbullet;
    }
}

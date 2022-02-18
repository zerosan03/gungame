using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gunshoscript : MonoBehaviour
{
    //Image�N���X�ϐ���錾�BSerializeField��Inspector�ォ���`
    [SerializeField] Image sight;
    [SerializeField] Image hitaction;
    [SerializeField] Image reloading;
    //ray�̒���
    public float rayLength = 20000;
    //ray�����������I�u�W�F�N�g�̏����擾����ׂ̕ϐ�
    RaycastHit hit;
    //�G�ɗ^����_���[�W�̒l
    public int damageScore = 1;
    public int gunbullet = 30;
    public int fastgunbullet;
    public GameObject gunbulletText;
    public GameObject muzzlePrefab;
    public GameObject muzzlePosition;
    public int hittime = 0;
    public float fastreloadtime;
    public float reloadtime = 100;
    public int reloadtimecounter = 0;
    public int shotcounter = 0;
    public bool gemestop;
    public AudioClip gunshotsound;
    public AudioClip reloadsound;
    AudioSource gunshot;
    AudioSource reload;
    // Start is called before the first frame update
    void Start()
    {
        this.gunbulletText = GameObject.Find("gunbullet");
        gunshot = gameObject.GetComponent<AudioSource>();
        reload = gameObject.GetComponent<AudioSource>();
        fastgunbullet = gunbullet;
        fastreloadtime = reloadtime;
    }

    // Update is called once per frame
    void Update()
    {
        if (gemestop == false)
        {
            //�J�����̌��_���烌�C���΂��ƃv���C���[���g�̃^�O���擾���Ă��܂��ׁAZ�����ɃY����
            Ray ray = new Ray(transform.position + transform.rotation * new Vector3(0, 0, 0.01f), transform.forward);
            //ray�̉���
            //ray��ɃR���C�_�[�����݂���ꍇ�ARayCastHit�^�ϐ��ɏ����i�[����Bout�C���q��t���āA�߂�l���擾�����Ɋ֐����ŕϐ��̒l�𑀍삷��
            if (Physics.Raycast(ray, out hit, rayLength))
            {
                //hit�Ɏ擾�������̓��A�^�O�����擾
                string hitTagName = hit.transform.gameObject.tag;
                //�^�O����Enemy�������ꍇ
                if (hitTagName == "turret")
                {
                    //Enemy�^�O�̃I�u�W�F�N�g��ray�������������̏Ə��̐F
                    sight.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
                    //�}�E�X�̍��{�^�����N���b�N���ꂽ��
                    if (Input.GetMouseButton(0) && gunbullet > 0 && shotcounter <= 0)
                    {
                        //Enemy�I�u�W�F�N�g�ɕt���Ă���EnemyHP��ReceveDamage�֐����Ăяo��
                        hit.collider.GetComponent<turretEnemyHP>().ReceveDamage(damageScore);
                        hittime = 5;
                    }
                }
                else if (hitTagName == "iron")
                {
                    //Enemy�^�O�̃I�u�W�F�N�g��ray�������������̏Ə��̐F
                    sight.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
                    //�}�E�X�̍��{�^�����N���b�N���ꂽ��
                    if (Input.GetMouseButton(0) && gunbullet > 0 && shotcounter <= 0)
                    {
                        //Enemy�I�u�W�F�N�g�ɕt���Ă���EnemyHP��ReceveDamage�֐����Ăяo��
                        hit.collider.GetComponent<ironscript>().ReceveDamage(damageScore);
                        hittime = 5;
                    }
                }
                else if (hitTagName == "summon")
                {
                    //Enemy�^�O�̃I�u�W�F�N�g��ray�������������̏Ə��̐F
                    sight.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
                    //�}�E�X�̍��{�^�����N���b�N���ꂽ��
                    if (Input.GetMouseButton(0) && gunbullet > 0 && shotcounter <= 0)
                    {
                        //Enemy�I�u�W�F�N�g�ɕt���Ă���EnemyHP��ReceveDamage�֐����Ăяo��
                        hit.collider.GetComponent<summonEnemyHP>().ReceveDamage(damageScore);
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
            if (Input.GetMouseButton(0) && gunbullet > 0 && shotcounter <= 0)
            {
                gunbullet -= 1;
                shotcounter = 5;
                gunshot.PlayOneShot(gunshotsound, 0.3f);
                var flash = Instantiate(muzzlePrefab, muzzlePosition.transform);
            }
            else
            {
                shotcounter -= 1;
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
            if ((Input.GetButtonDown("Reload") || gunbullet == 0) && reloadtimecounter == 0)
            {
                reloadtimecounter = 1;
                reload.PlayOneShot(reloadsound, 0.3f);
            }
            if (reloadtime < 0)
            {
                reloadtime = 100;
                reloadtimecounter = 0;
                gunbullet = fastgunbullet;
            }
            if (reloadtime < 99)
            {
                reloading.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            else
            {
                reloading.color = new Color(1.0f, 0.0f, 0.0f, 0.0f);
            }
            reloading.fillAmount = reloadtime / fastreloadtime;
            reloadtime -= reloadtimecounter;
            this.gunbulletText.GetComponent<Text>().text = gunbullet + "/" + fastgunbullet;
        }
    }
    public void Gameover()
    {
        gemestop = true;
    }
}

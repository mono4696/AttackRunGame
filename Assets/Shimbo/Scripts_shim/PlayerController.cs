using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    CharacterController cc;
    AudioSource voice;
    public Collider handCol;

    Vector3 move = Vector3.zero;

    public float gravity = 20f;
    public float speed;
    public float rotSpeed;
    public float jumpPower;

    public AudioClip jump;
    public AudioClip punch;
    public AudioClip damaged;

    const int defaultHP = 3;
    int hp = defaultHP;
    int enemyCount;//Score

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        voice = GetComponent<AudioSource>();
        //handCol = GameObject.Find("Character1_RightHand").GetComponent<SphereCollider>();...使うと時間がかかる！
    }

    // Update is called once per frame
    void Update()
    {
        if (IsStan())
        { 
            return;
        }
        else
        {
            float acc = Mathf.Max(Input.GetAxis("Vertical"));
            if (Input.GetKey(KeyCode.DownArrow))
            {
                animator.SetBool("back", true);
            }
            else
            {
                animator.SetBool("back", false);
            }

            if (cc.isGrounded)
            {
                float rot = Input.GetAxis("Horizontal");
                animator.SetFloat("speed", Mathf.Max(acc, Mathf.Abs(rot)));
                transform.Rotate(0, rot * rotSpeed * Time.deltaTime, 0);

                if (Input.GetButtonDown("Jump"))
                {
                    animator.SetTrigger("jump");
                }
            }

            move.y -= gravity * Time.deltaTime;
            cc.Move((transform.forward * acc * speed + move) * Time.deltaTime);

            if (cc.isGrounded)
            {
                move.y = 0;
            }

            Attack();

        }
    }

    //攻撃！
    void Attack()
    {
        if (IsStan())
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            //Debug.Log("enabled=True");
            animator.SetBool("action", true);
            handCol.enabled = true;

            Invoke("ColliderReset", 0.6f);
        }
        else
        {
            animator.SetBool("action", false);
        }
    }

    //単純な接触で敵が消えないように
    void ColliderReset()
    {
        //Debug.Log("enabled=false");
        handCol.enabled = false;
    }

    //ジャンプモーションで呼ばれるイベント
    public void OnJumpStart()
    {
        move.y = jumpPower;
        voice.clip = jump;
        voice.Play();
    }

    //パンチモーションで呼ばれるイベント
    public void OnPunching()
    {
        voice.clip = punch;
        voice.Play();
    }

    //ダメージモーションで呼ばれるイベント
    public void OnDamaged()
    {
        voice.clip = damaged;
        voice.Play();
    }

    //HP確認
    public int Hp()
    {
        return this.hp;
    }

    //気絶(終了条件)判定
    public bool IsStan()
    {
        return hp <= 0;
    }

    //倒したEnemy数を取得
    public void SetAttackCount(int count)
    {
        this.enemyCount = count;
    }
    
    //倒したEnemyの数を返す
    public int GetAttackCount()
    {
        return this.enemyCount;
    }

    //動いてる時の衝突判定
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (IsStan())
        {
            //Debug.Log("stan.move");
            animator.SetTrigger("stan");
            return;
        }
        else if(hit.gameObject.tag == "EnemyHand")
        {
            //Debug.Log("damage.move");
            animator.SetBool("damaged", true);
            this.hp--;
            transform.position = transform.position + -transform.forward * 3f;
        }
        animator.SetBool("damaged", false);
    }

}

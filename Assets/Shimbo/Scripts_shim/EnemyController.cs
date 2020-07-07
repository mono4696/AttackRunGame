using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator animator;
    public Collider handCol;

    int action;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        action = Animator.StringToHash("Base Layer.Action");
        //handCol = GameObject.Find("RightHand").GetComponent<SphereCollider>();...使うと時間がかかる！
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        AnimatorStateInfo anim = animator.GetCurrentAnimatorStateInfo(0);

        if (anim.nameHash == action)//腕を振り回している間はColliderオン
        {
            //Debug.Log("Enemy.handUP");
            handCol.enabled = true;

            Invoke("ColliderReset", 0.67f);
        }
    }

    void ColliderReset()
    {
        //Debug.Log("Enemy.handDown");
        handCol.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHand : MonoBehaviour
{

    public Shimbo_PlayerController pc;
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    { 
        if (collision.gameObject.tag == "Enemy")
        {
            //Debug.Log("PUNCH");

            count += 1;
            pc.SetAttackCount(count);

            collision.gameObject.GetComponent<Animator>().SetTrigger("down");
            Destroy(collision.gameObject,2f);
        }
    }

}

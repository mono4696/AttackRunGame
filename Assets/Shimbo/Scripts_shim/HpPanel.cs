using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPanel : MonoBehaviour
{
    public GameObject[] icons;

    public void UpdateHp(int hp)
    {
        for(int i = 0; i < icons.Length; i++)
        {
            if (i < hp)
            {
                icons[i].SetActive(true);
            }
            else
            {
                icons[i].SetActive(false);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

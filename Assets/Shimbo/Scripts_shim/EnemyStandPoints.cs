using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStandPoints: MonoBehaviour
{
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = (GameObject)Instantiate(
            prefab,
            Vector3.zero,
            Quaternion.identity
            );
        obj.transform.SetParent(transform, false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        Vector3 offset = new Vector3(0f, 0.05f, 0f);

        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position + offset, 0.5f);

        if (prefab != null)
        {
            Gizmos.DrawIcon(transform.position + offset, prefab.name, true);
        }

    }
}

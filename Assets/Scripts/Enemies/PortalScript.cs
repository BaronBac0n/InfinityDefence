using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    void Start()
    {
        GetComponent<ParticleSystem>().Play();
        StartCoroutine(DecayTimer());
        Destroy(transform.root.gameObject, 7);
    }
    
    void Update()
    {
        
    }

    IEnumerator DecayTimer()
    {
        yield return new WaitForSeconds(3);
        GetComponent<ParticleSystem>().loop = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<ParticleSystem>().loop = false;
        }
        
    }
}

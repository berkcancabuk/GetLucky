using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothrTrigger : MonoBehaviour
{
    public ParticleSystem blood;
    public ParticleSystem smokepuff;
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(SETFALSE());
        }
            
        blood.Play();
        smokepuff.Play();
    }

    IEnumerator SETFALSE()
    {
        yield return new WaitForSeconds(0.3f);
        
            this.gameObject.SetActive(false);
        
    }
}

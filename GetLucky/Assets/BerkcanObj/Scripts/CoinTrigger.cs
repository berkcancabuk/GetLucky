using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinTrigger : MonoBehaviour
{
    
    public ParticleSystem particleSystem;
  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        particleSystem.Play();
        StartCoroutine(Setfalse());
            
    }
    IEnumerator Setfalse()
    {
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
    }

}

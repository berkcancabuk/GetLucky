using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hanger : MonoBehaviour
{
    public ParticleSystem starsEffect;
    public ParticleSystem flareEffect;
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
        
        if (other.tag == "Player")
        {
            Instantiate(starsEffect, new Vector3(this.transform.position.x, other.transform.position.y + 1.468f, this.transform.position.z), Quaternion.identity);
            //Instantiate(flareEffect, new Vector3(other.transform.position.x, other.transform.position.y + 1.468f, other.transform.position.z), Quaternion.identity);
            this.gameObject.SetActive(false);
        }
        
        
    }
}

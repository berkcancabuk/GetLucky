using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goldenGate : MonoBehaviour
{
    public Animator anim;
    private void OnTriggerEnter(Collider other)
    {
        anim.SetBool("goldenGate", true);
    }
}

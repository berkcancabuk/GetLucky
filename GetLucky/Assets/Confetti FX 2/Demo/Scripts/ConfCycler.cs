using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Conf
{
	
    public class ConfCycler : MonoBehaviour
    {
		 #pragma warning disable 0649
        [SerializeField]
        List<GameObject> listOfEffects;

        [Header("Loop length in seconds")]
        [SerializeField]
        float loopTimeLength = 5f;

        float timeOfLastInstantiate;

        GameObject instantiatedEffect;
		
        int effectIndex = 0;
		 #pragma warning restore 0649

        // Use this for initialization
        void Start()
        {
            instantiatedEffect = Instantiate(listOfEffects[effectIndex], transform.position, transform.rotation) as GameObject;
            effectIndex++;
            timeOfLastInstantiate = Time.time;
        }

        // Update is called once per frame
        void Update()
        {
            if (Time.time >= timeOfLastInstantiate + loopTimeLength)
            {
                Destroy(instantiatedEffect);
                instantiatedEffect = Instantiate(listOfEffects[effectIndex], transform.position, transform.rotation) as GameObject;
                timeOfLastInstantiate = Time.time;
                if (effectIndex < listOfEffects.Count - 1)
                    effectIndex++;
                else
                    effectIndex = 0;
            }
        }
    }
}

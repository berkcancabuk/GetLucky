using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SwerveInputSystem : MonoBehaviour
{
    public float _lastFrameFingerPositionX;
    public float _moveFactorX;
    public MainChar main;
    public GameObject slide;
    public bool finish = true;
    // Start is called before the first frame update
    void Start()
    {
        main = GetComponent<MainChar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (finish == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                slide.SetActive(false);
                DOTween.Play("parentween");
                main.animas.SetBool("Idle", false);
                main.animas.SetBool("Walk", true);
                _lastFrameFingerPositionX = Input.mousePosition.x;
            }
            else if (Input.GetMouseButton(0))
            {
                //DOTween.Pause("parentween");
                _moveFactorX = Input.mousePosition.x - _lastFrameFingerPositionX;

                _lastFrameFingerPositionX = Input.mousePosition.x;

            }
            else if (Input.GetMouseButtonUp(0))
            {
                main.animas.SetBool("Idle", true);
                main.animas.SetBool("Walk", false);
                DOTween.Pause("parentween");
                _moveFactorX = 0f;
            }
        }
       
    }
}

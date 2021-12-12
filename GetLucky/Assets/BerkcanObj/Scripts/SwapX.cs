using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapX : MonoBehaviour
{
    private MainChar _swerveInputSystem;
    public float swerveSpeed = 0.5f;
    public float maxSwerveSpeed = 2f;
    public SwerveInputSystem swerve;
    // Start is called before the first frame update
    void Awake()
    {
        _swerveInputSystem = GetComponent<MainChar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (swerve.finish = true)
        {
            float swerveAmount = 0;
            swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveSpeed, maxSwerveSpeed);
            swerveAmount = Time.deltaTime * swerveSpeed * _swerveInputSystem._moveFactorX;

            print(swerveAmount + "swerve");
            transform.Translate(swerveAmount, 0, 0);
        }
       
    }
}

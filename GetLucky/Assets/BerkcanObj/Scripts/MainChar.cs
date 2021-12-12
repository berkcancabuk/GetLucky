using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;
public class MainChar : MonoBehaviour
{
    SwerveInputSystem swerve;
    public GameObject textConfident;
    public GameObject textBubbly;
    public GameObject sexyImage;
    public GameObject textAngry;
    private float swerveSpeed = 3f;
    public float Value = 25f;
    public float _lastFrameFingerPositionX;
    public float _moveFactorX;
    public GameObject glove_L;
    public GameObject glove_R;
    public GameObject glove;
    public Animator animas; //player animasyonu 
    public Animator cabinss; //kabin animatorü
    public Animator cabinsslose;
    public GameObject coat;
    public GameObject coats;
    public GameObject boat_R;
    public GameObject boat_L;
    public GameObject boats;
    public GameObject top;
    public GameObject tops;
    public GameObject beret;
    public SwerveInputSystem swerves;
    //public TextMeshProUGUI level;
    public TextMeshProUGUI coin;
    public int coins = 0;
    public CameraFollows cam;
    public Vector3 rot;
    public GameObject textNoInPool;
    public ParticleSystem flare;
    public ParticleSystem confeti;
    public GameObject complated;

    void Start()
    {
        
        coins = PlayerPrefs.GetInt("mycoin");
        coin.text = "" + coins;
        DOTween.Pause("parentween");
        swerve = GetComponent<SwerveInputSystem>();
    }

    void Update()
    {
        if (swerves.finish == true)
        {
            float swerveAmount = Time.deltaTime * swerveSpeed * swerve._moveFactorX;
            swerveAmount = Mathf.Clamp(swerveAmount, -2, 2f);
            this.transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2, 2), transform.position.y, transform.position.z);
            transform.Translate(swerveAmount, 0, 0);
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag =="coin")
        {
            coins++;
            coin.text = "" + coins;
            PlayerPrefs.SetInt("mycoin", coins);
        }
        if (other.tag == "stairs" && Value <= 83f)
        {
            complated.SetActive(true);
            confeti.Play();
            textNoInPool.SetActive(true);
            cam.cameraKapatVeBüyüt();
            swerves.finish = false;
            transform.DORotate(rot, 2f, RotateMode.Fast).SetLoops(1);
            animas.SetBool("dance", true);


        }
        if (other.tag == "hanger")
        {
            other.gameObject.SetActive(false);
            flare.Play();

            Value = Value + 3.125f;
                print(Value);
            if (Value == 37.5f)
            {
                animas.SetBool("GloveOn", true);
                glove_R.SetActive(false);
                glove_L.SetActive(false);
                Instantiate(glove, new Vector3(this.transform.position.x, transform.position.y, this.transform.position.z - 0.5f), Quaternion.identity);
                
                StartCoroutine(GloveOff());
            }
            if (Value == 50f)
            {
                animas.SetBool("CoatOn", true);
                coat.SetActive(false);
                textConfident.SetActive(true);
                Instantiate(coats, new Vector3(this.transform.position.x, transform.position.y, this.transform.position.z - 0.5f), Quaternion.identity);
                StartCoroutine(CoatOff());
                
            }
            if (Value == 62.5f)
            {
                boat_R.SetActive(false);
                boat_L.SetActive(false);
                textBubbly.SetActive(true);
                StartCoroutine(CoatOff());
                Instantiate(boats, new Vector3(this.transform.position.x - 0.596f, this.transform.position.y,this.transform.position.z -0.5f), Quaternion.identity);
            }

        }

        if (other.tag == "cloth")
        {
            print("çarptý");
            
            if (Value >= 25f && Value < 37.5f)
            {
                textAngry.SetActive(true);
                beret.SetActive(true);
                animas.SetBool("CoatOff", true);
                print("bere takýldý");
                StartCoroutine(Coaton());
                
            }
            if (Value >= 37.5f && Value < 49.0f)
            {
                textAngry.SetActive(true);
                animas.SetBool("CoatOff", true);
                StartCoroutine(Coaton());
                glove_L.SetActive(true);
                glove_R.SetActive(true);
                
            }
            if (Value >= 50.0f && Value <62.5f)
            {
                textAngry.SetActive(true);
                animas.SetBool("CoatOff", true);
                StartCoroutine(Coaton());
                coat.SetActive(true);
            }
            Value -= 12.5f;
        }
        if (other.tag == "cabin")
        {
            Value += 12.5f;
            if (Value >= 75.0f)
            {
                cabinss.SetBool("cabins", true);
                animas.SetBool("TrueDoor", true);
                sexyImage.SetActive(true);
                top.SetActive(false);
                Instantiate(tops, new Vector3(this.transform.position.x,transform.position.y,this.transform.position.z -0.5f), Quaternion.identity);
                StartCoroutine(Cabinoff());
            }
            else if (Value >= 62.5f)
            {
                cabinss.SetBool("cabins", true);
                animas.SetBool("TrueDoor", true);
                sexyImage.SetActive(true);
                boat_L.SetActive(false);
                boat_R.SetActive(false);
                textBubbly.SetActive(true);
                Instantiate(boats, new Vector3(this.transform.localPosition.x, this.transform.localPosition.y + 1f, this.transform.localPosition.z), Quaternion.identity);
                StartCoroutine(Cabinoff());
            }
            else if (Value >= 50.0f)
            {
                cabinss.SetBool("cabins", true);
                animas.SetBool("TrueDoor", true);
                sexyImage.SetActive(true);
                coat.SetActive(false);
                textConfident.SetActive(true);
                Instantiate(coats, this.transform.position, Quaternion.identity);
                StartCoroutine(Cabinoff());
            }
            else if (Value >= 37.5f)
            {
                cabinss.SetBool("cabins", true);
                animas.SetBool("TrueDoor", true);
                sexyImage.SetActive(true);
                glove_L.SetActive(false);
                glove_R.SetActive(false);
                Instantiate(glove, this.transform.position, Quaternion.identity);
                StartCoroutine(Cabinoff());
            }
        }
        if (other.tag == "cabinLose")
        {
            Value -= 12.5f;
            if (Value >= 25f && Value < 37.5)
            {
                cabinsslose.SetBool("cabinLose", true);
                animas.SetBool("FalseDoor", true);
                glove_R.SetActive(true);
                glove_L.SetActive(true);
            }
            else if (Value >= 37.5f && Value < 49.9f)
            {
                cabinsslose.SetBool("cabinLose", true);
                animas.SetBool("FalseDoor", true);
                coat.SetActive(true);
            }
            else if (Value >= 50.0f && Value < 62.4f)
            {
                cabinsslose.SetBool("cabinLose", true);
                animas.SetBool("FalseDoor", true);
                boat_R.SetActive(true);
                boat_L.SetActive(true);

            }
            StartCoroutine(Losecabinoff());
        }
        
    }
    IEnumerator GloveOff()
    {
        yield return new WaitForSeconds(0.5f);
        animas.SetBool("GloveOn", false);
    }
    IEnumerator CoatOff()
    {
        yield return new WaitForSeconds(0.5f);
        animas.SetBool("CoatOn", false);
        textBubbly.SetActive(false);
        textConfident.SetActive(false);
    }
    IEnumerator Coaton()
    {
        yield return new WaitForSeconds(0.5f);
        animas.SetBool("CoatOff", false);
        textAngry.SetActive(false);
    }
    IEnumerator Cabinoff()
    {
       
        DOTween.Pause("parentween");
        animas.SetBool("Idle", true);
        yield return new WaitForSeconds(0.5f);
        textBubbly.SetActive(false);
        textConfident.SetActive(false);
        cabinss.SetBool("cabins", false);
        //animas.SetBool("Walk", true);
       
        animas.SetBool("TrueDoor", false);
        animas.SetBool("Idle", false);
        sexyImage.SetActive(false);
        
    }
    IEnumerator Losecabinoff()
    {
        
        animas.SetBool("Idle", true);
        DOTween.Pause("parentween");
        yield return new WaitForSeconds(1f);
        //animas.SetBool("Walk", false);
        
        cabinsslose.SetBool("cabinLose", false);
        animas.SetBool("FalseDoor", false);
        animas.SetBool("Idle", true);
        

    }
    //IEnumerator SonrakiSahneGecis()
    //{
    //    yield return new WaitForSeconds(4f);
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    //}


}

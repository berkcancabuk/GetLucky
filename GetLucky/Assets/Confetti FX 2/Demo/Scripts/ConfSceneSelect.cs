using UnityEngine;
using UnityEngine.SceneManagement;

namespace Conf
{

public class ConfSceneSelect : MonoBehaviour
{
	public bool GUIHide = false;
	public bool GUIHide2 = false;
	public bool GUIHide3 = false;
	
    public void LoadSceneDemo01()
    {
        SceneManager.LoadScene("Conf01");
    }
    public void LoadSceneDemo02()
    {
        SceneManager.LoadScene("Conf02");
    }
    public void LoadSceneDemo03()
    {
        SceneManager.LoadScene("Conf03");
    }
    public void LoadSceneDemo04()
    {
        SceneManager.LoadScene("Conf04");
    }
    public void LoadSceneDemo05()
    {
        SceneManager.LoadScene("Conf05");
    }
    public void LoadSceneDemo06()
    {
        SceneManager.LoadScene("Conf06");
    }
    public void LoadSceneDemo07()
    {
        SceneManager.LoadScene("Conf07");
    }
    public void LoadSceneDemo08()
    {
        SceneManager.LoadScene("Conf08");
    }
    public void LoadSceneDemo09()
    {
        SceneManager.LoadScene("Conf09");
    }
    public void LoadSceneDemo10()
    {
        SceneManager.LoadScene("Conf10");
    }
	public void LoadSceneDemo11()
    {
        SceneManager.LoadScene("Conf11");
    }
	public void LoadSceneDemo12()
    {
        SceneManager.LoadScene("Conf12");
    }
	public void LoadSceneDemo13()
    {
        SceneManager.LoadScene("Conf13");
    }
	public void LoadSceneDemo14()
    {
        SceneManager.LoadScene("Conf14");
    }
	public void LoadSceneDemo15()
    {
        SceneManager.LoadScene("Conf15");
    }
	public void LoadSceneDemo16()
    {
        SceneManager.LoadScene("Conf16");
    }
	public void LoadSceneDemo17()
    {
        SceneManager.LoadScene("Conf17");
    }
	public void LoadSceneDemo18()
    {
        SceneManager.LoadScene("Conf18");
    }
	public void LoadSceneDemo19()
    {
        SceneManager.LoadScene("Conf19");
    }
	public void LoadSceneDemo20()
    {
        SceneManager.LoadScene("Conf20");
    }
	public void LoadSceneDemo21()
    {
        SceneManager.LoadScene("Conf21");
    }
	
	void Update ()
	 {
 
     if(Input.GetKeyDown(KeyCode.J))
	 {
         GUIHide = !GUIHide;
     
         if (GUIHide)
		 {
             GameObject.Find("CanvasSceneSelect").GetComponent<Canvas> ().enabled = false;
         }
		 else
		 {
             GameObject.Find("CanvasSceneSelect").GetComponent<Canvas> ().enabled = true;
         }
     }
	      if(Input.GetKeyDown(KeyCode.K))
	 {
         GUIHide2 = !GUIHide2;
     
         if (GUIHide2)
		 {
             GameObject.Find("Canvas").GetComponent<Canvas> ().enabled = false;
         }
		 else
		 {
             GameObject.Find("Canvas").GetComponent<Canvas> ().enabled = true;
         }
     }
		if(Input.GetKeyDown(KeyCode.L))
	 {
         GUIHide3 = !GUIHide3;
     
         if (GUIHide3)
		 {
             GameObject.Find("CanvasTips").GetComponent<Canvas> ().enabled = false;
         }
		 else
		 {
             GameObject.Find("CanvasTips").GetComponent<Canvas> ().enabled = true;
         }
     }
	 }
}
}
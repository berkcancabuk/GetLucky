using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Confetti2
{

public class Confetti2TimeSlider : MonoBehaviour {

private Slider timeSlider;

	void Start () {
		timeSlider = GetComponent<Slider>();
	}
	
	void Update () {
		Time.timeScale = timeSlider.value;
	}
}
}
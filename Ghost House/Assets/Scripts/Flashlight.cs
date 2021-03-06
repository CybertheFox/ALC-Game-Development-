﻿//ToggleLight.cs
//Turn the light component of this object on/off when the user presses and releases the 'L' key

using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour {


	//Flashlight on/off
	public bool lightOn = true;
	//Flashlight Power capacity
	public int maxPower = 4;
	//Usable flashlight power
	public int currentPower;
	//Flashlight Drain Amount;
	public int batDrainAmt;
	//Flashlight Drain Delay;
	public float batDrainDelay;
	//Stores light object
	Light light;
	//Battery drain on/off
	bool draining = false;
	//Count interger
	long count = 0;
	//Battery UI Text
	public Text batteryText;

	// Use this for initialization
	void Start () {
		//Add pwer to flashlight
		currentPower = maxPower;
		print("power = " + currentPower);

		light = GetComponent<Light> ();
		//Set Light default to ON
		lightOn = true;
		print("Turn light on when Flashlight is initiated");
		light.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		//Toggle light on/off when F key is pressed
		if (Input.GetKeyUp (KeyCode.F) && lightOn) {
			print("Light Off");
			lightOn = false;
			light.enabled = false;

		}

		else if (Input.GetKeyUp (KeyCode.F) && !lightOn && currentPower > 0){
			print("Light On");
			lightOn = true;
			light.enabled = true;
		}

		//Update Battery UI text
		batteryText.text = currentPower.ToString();


		if(currentPower > 0){
			if(!draining){
			StartCoroutine(BatteryDrain(batDrainDelay,batDrainAmt));
			}
			else if(currentPower <= 0){
				lightOn = false;
				light.enabled = false;
			}
		}
	}
	//Turns light on when called
	public void setLightOn(){
		lightOn = true;
	}
	//Returns if light is on
	public bool isLightOn(){
		return lightOn;
	
	}
	//Drain Battery Life
	IEnumerator BatteryDrain(float delay, int amount){
		if(light){
			draining = true;
			yield return new WaitForSeconds(delay);
			print(currentPower);
			currentPower -= amount;
		}


		if(currentPower <= 0){
			currentPower = 0;
			print("Battery is dead!");
			light.enabled = false;
		
		}

		draining = false;
	}

}
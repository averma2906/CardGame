using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {
    public GameObject nextScene;
    public GameObject previousScene;
	 
	 
    public void NextButton()
    {
        gameObject.SetActive(false);
        nextScene.SetActive(true);
    }
    public void PrevoiusButton()
    {
        gameObject.SetActive(false);
        previousScene.SetActive(true);
    }
}

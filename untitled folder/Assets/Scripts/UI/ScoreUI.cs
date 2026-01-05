using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUI : MonoBehaviour {

    [HideInInspector] public TextMeshProUGUI scoreText;

    //public GameObject objectWithScript;
    void Start() 
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    void Update() 
    {
        scoreText.text = Singleton.instance.mainScore.ToString();
    }

}
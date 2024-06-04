using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField, Tooltip("Remaining number of times player can spin the wheel")]
    TextMeshProUGUI turnsTextBox;

    [SerializeField, Tooltip("total amount of winnings from player")]
    TextMeshProUGUI winningsTextbox;

    [Space]
    [SerializeField, Tooltip("Number of times the player can spin the wheel")] //esto lo que hace es que se pueda ver en el unity
    int turns;

    [SerializeField, Tooltip("Number of full spins the wheel must complete before stopping")] 
    int cycles;

    private float _winnings;
    private float[] _angles = new float[] { 22.5F, 67.5F,112.5F,157.5F,202.5F,247.5F,292.5F,337.5F };


    private void Awake()
    {
        turnsTextBox.text = turns.ToString();
        winningsTextbox.text = _winnings.ToString("0.00");
    }
    /*
    private void Start()
    {

    }

    private void Update()
    {
        //este usa delta time
    }

    private void FixedUpdate()
    {
        //este usa fixedDeltaTime
    }

    private void LateUpdate()
    {
        //este usa delta time y se ejecuta despues del Update y el FixedUpdate
    }

    */
}
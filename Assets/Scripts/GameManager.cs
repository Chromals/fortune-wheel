using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField, Tooltip("Remaining number of times player can spin the wheel")]
    TextMeshProUGUI turnsTextBox;

    [SerializeField, Tooltip("total amount of winnings from player")]
    TextMeshProUGUI winningsTextbox;

    [SerializeField, Tooltip("fortune wheel")]
    Transform fortuneWheel;

    [SerializeField, Tooltip("Spin button")]
    Button spinButton;

    [Space]
    [SerializeField, Tooltip("Number of times the player can spin the wheel")] //esto lo que hace es que se pueda ver en el unity
    int turns;

    [SerializeField, Tooltip("Number of full spins the wheel must complete before stopping")]
    int cycles;

    [SerializeField, Tooltip("Number of seconds the wheel can spin")]
    float maxSpinningTime;

    [SerializeField, Tooltip("Number of seconds to sleep the game before jumping to Game Over scene.")]
    float gameOverSleep;

    private float _startAngle;
    private float _finalAngle;
    private int _prizeAngle;
    private bool _isSpinning;
    private float _currentSpinningTime;

    private int _winnings;
    private int _currentTurns;
    private float[] _angles = new float[] { 1.0F, 46.0F, 91.0F, 136.0F, 181.0F, 226.0F, 271.0F, 316.0F };


    private void Awake()
    {
        _currentTurns = turns;
        turnsTextBox.text = turns.ToString();
        winningsTextbox.text = "$" + _winnings.ToString("0.00");
    }

    public void Spin()
    {
        spinButton.interactable = false;

        _prizeAngle = Random.Range(0, _angles.Length);
        float randomPrizeAngle = _angles[_prizeAngle];
        _finalAngle = -(cycles * 360 + randomPrizeAngle);
        _currentSpinningTime = 0.0F;
        _isSpinning = true;
    }

    private void Update()
    {
        //este usa delta time
        if (!_isSpinning)
            return;

        _currentSpinningTime += Time.deltaTime;

        bool isTimeout = _currentSpinningTime > maxSpinningTime;
        bool hasReachedFinalAngle = fortuneWheel.eulerAngles.z == _finalAngle;
        if (isTimeout || hasReachedFinalAngle)
        {
            _currentSpinningTime = maxSpinningTime;
            _startAngle = _finalAngle % 360;
            fortuneWheel.eulerAngles = new Vector3(0.0F, 0.0F, _startAngle);

            _isSpinning = false;

            int prize = GetPrize();
            _winnings += prize;
            winningsTextbox.text = "$" + _winnings.ToString("0.00");

            _currentTurns--;
            turnsTextBox.text = _currentTurns.ToString();

            if(_currentTurns==0)
            {
                StartCoroutine( GameOver() );
                return;
            }

            
            spinButton.interactable = true;

            return;
        }


        float t = _currentSpinningTime / maxSpinningTime;
        t = Mathf.Pow(t, 3.0F) * (t * (4.0F * t - 10.0F) + 8.0F);

        float angle = Mathf.Lerp(_startAngle, _finalAngle, t);
        fortuneWheel.eulerAngles = new Vector3(0.0F, 0.0F, angle);
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(gameOverSleep);
        LevelManager lvlmanager = FindObjectOfType<LevelManager>();
        lvlmanager.LastLevel();
    }


    private void Start()
    {
        _startAngle = _angles[Random.Range(0, _angles.Length)];
        fortuneWheel.eulerAngles = new Vector3(0.0F, 0.0f, _startAngle);
    }


    private int GetPrize()
    {
        Debug.Log(_prizeAngle);
        switch (_prizeAngle)
        {
            
            case 6:
                return 1000;

            case 1:
            case 4:
                return 600;
            case 2:
            case 7:
                return 400;

            case 3:
            case 0:
                return 100;
            

            case 5:
                return Random.Range(1500, 5000);
            default:
                return 0;
        }
    }
    /*

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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int score;
    int lifes;
    [SerializeField] Text txtScore;
    [SerializeField] Text txtLifes;
    void Start()
    {
        lifes = 3;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        txtScore.text = string.Format("{0,3:D3}", score);
        txtLifes.text = lifes.ToString();
    }


    public void AddScore(int points)
    {
        score += points;
    }

    public void consumeLife()
    {
        lifes += -1;
    }
}

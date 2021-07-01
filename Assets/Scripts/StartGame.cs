using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartGame : MonoBehaviour
{

    [SerializeField] private Text coinsText;

    private void Start()
    {
        int coins = PlayerPrefs.GetInt("Coin");
        coinsText.text = coins.ToString();
    }
    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }
}

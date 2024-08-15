using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class gameoverWin : MonoBehaviour
{
    
    public TextMeshProUGUI _scoreTextWin;

    // Start is called before the first frame update
    void Start()
    {
        

        _scoreTextWin.text = PlayerPrefs.GetInt("score").ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeScene(string _scene)
    {
        SceneManager.LoadScene(_scene);
    }

    public void exitGame()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}

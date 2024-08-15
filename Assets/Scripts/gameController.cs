using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class gameController : MonoBehaviour
{
    //Player Component
    public playerController _playerController;
    
    public gameScene _gameScene;

    [Header("Player Settings")]
    public float playerSpeed;
    public float playerPosY;


    [Header("Cat Settings")]
    public GameObject catPrefab;
    public Transform[] limitSpawnCat;
    public int index;
    public float[] catSpeed;
    public float delaySpawnCat;
    private bool isInstantiated;
    public bool instCatPermission;
    

    [Header("Game Settings")]
    public int score;
    public float timercounting;
    public bool timerOver;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textTimer;
    public AudioSource audioSource;
    public AudioClip fxScore;
    public AudioClip fxCat;
    public AudioClip fxJump;
    public AudioClip fxAttack;  
   


    [Header("Camera Settings")]
    public Transform limitPlayerLeft;
    public Transform limitPlayerRight;
    public Camera camera;
    public Transform limitCamLeft;
    public Transform limitCamRight;
    public float cameraHorizontalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //Acessing the component
        _playerController = FindObjectOfType(typeof(playerController)) as playerController;
        
        _gameScene = FindObjectOfType(typeof(gameScene)) as gameScene;

        StartCoroutine("_carSpawn");
    }

    // Update is called once per frame
    void Update()
    {
        _timecounting();
    }

    void LateUpdate()
    {
        //Getting the player's y position
        playerPosY = _playerController.transform.position.y;

        //Triging the function
        cameraControl();
    }

    #region FUNCTIONS

    //Limiting the camera
    void cameraControl()
    {
        float _cameraPositionX = camera.transform.position.x;
        float _limitCamLeft = limitCamLeft.position.x;
        float _limitCamRight = limitCamRight.position.x;
        float _playerPositionX = _playerController.transform.position.x;

        if (_cameraPositionX > _limitCamLeft && _cameraPositionX < _limitCamRight)
        {
            moveCamera();
        }
        else if(_cameraPositionX <= _limitCamLeft && _playerPositionX > _limitCamLeft)
        {
            moveCamera();
        }
        else if(_cameraPositionX >= _limitCamRight && _playerPositionX < _limitCamRight)
        {
            moveCamera();
        }
    }

    //Moving the camera
    void moveCamera()
    {

        Vector3 cameraDestinyPosition = new Vector3(_playerController.transform.position.x, camera.transform.position.y, -10);

        camera.transform.position = Vector3.Lerp(camera.transform.position, cameraDestinyPosition, cameraHorizontalSpeed * Time.deltaTime);
    }

    //Score function
    public void score_function(int _score)
    {
        score += _score;
        textScore.text = score.ToString();
        PlayerPrefs.SetInt("score", score);
        Debug.Log(score);
    }

    public void timeController()
    {
        textTimer.text = timercounting.ToString("F0");
    }

    public void _timecounting()
    {
        if(!timerOver && timercounting > 0)
        {
            timercounting -= Time.deltaTime;
            timeController();
            if(timercounting <= 0)
            {
                timercounting = 0;
                timerOver = true;
                _gameScene.changeScene("GameoverLose1");
            }
        }
    }

    public void instanciateCat()
    {
        if (!instCatPermission)
        {
            instCatPermission = true;

            if (!isInstantiated)
            {
                index = 0;

                float sorting = Random.Range(0, 11);

                if(sorting > 5) 
                {
                    index = 0;
                    
                }
                else
                {
                    index = 1;
                }

                float posX = limitSpawnCat[index].transform.position.x;

                

                if (index == 0)
                {

                    GameObject _cat = Instantiate(catPrefab);

                    _cat.transform.position = new Vector2(posX, _cat.transform.position.y);
                    audioSource.PlayOneShot(fxCat);

                }
                else if(index == 1)
                {
                    GameObject _cat = Instantiate(catPrefab);
                    _cat.transform.position = new Vector2(posX, _cat.transform.position.y);
                    _cat.GetComponent<Transform>().localScale = new Vector2(-1, 1);
                    audioSource.PlayOneShot(fxCat);
                }

                isInstantiated = true;
            }
        }
    }

    #endregion

    IEnumerator _carSpawn()
    {
        yield return new WaitForSeconds(delaySpawnCat);

        instanciateCat();

        isInstantiated = false;

        

        StartCoroutine("_carSpawn");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private gameController _gameController;
    private gameScene _gameScene;
    private Rigidbody2D playerRb;
    public Transform groundCheck;
    private Animator playerAnimator;
    public float jumpForce;
    public bool isGrounded;
    private int speedX;
    private float speedY;
    private bool isLeft;
    public LayerMask whatIsGround;

   
    // Start is called before the first frame update
    void Start()
    {
        //Acessing the _gameController and player's components
        _gameController = FindObjectOfType(typeof(gameController)) as gameController;
        _gameScene = FindObjectOfType(typeof (gameScene)) as gameScene;
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        //Checking out the ground collision
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f, whatIsGround);
    }

    // Update is called once per frame
    void Update()
    {
        playerLimits();
    }

    private void LateUpdate()
    {
        //Trigging the function
        moviment();

        //Animating the player
        playerAnimator.SetInteger("speedX", speedX);
        playerAnimator.SetFloat("speedY", speedY);
        playerAnimator.SetBool("isGrounded", isGrounded);
    }



    #region FUNCTIONS

    void moviment()
    {
        //Inputs to move the player
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        speedY = playerRb.velocity.y;

        //Setting up the speedX value
        if (horizontal != 0) { speedX = 1; } else { speedX = 0; }

        //Applying the player's speed
        playerRb.velocity = new Vector2(horizontal * _gameController.playerSpeed, speedY);

        //Making the player jump
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            _gameController.audioSource.PlayOneShot(_gameController.fxJump);
            playerRb.AddForce(new Vector2(0, jumpForce));
        }

        //Torning the player
        if(!isLeft && horizontal < 0)
        {
            flipPlayer();
        }
        if(isLeft && horizontal > 0)
        {
            flipPlayer();
        }

    }

    #endregion

    //Function to limit the player on the scene
    void playerLimits()
    {
        float posX = transform.position.x;
        float posY = transform.position.y;
        float posXlimitRight = _gameController.limitPlayerRight.transform.position.x;
        float posXlimitLeft = _gameController.limitPlayerLeft.transform.position.x;

        if (posX > posXlimitRight)
        {
            posX = posXlimitRight;
        }
        else if(posX < posXlimitLeft)
        {
            posX = posXlimitLeft;
        }

        transform.position = new Vector2(posX, posY);
    }

    //Function to turn the player around it's self;
    void flipPlayer()
    {
        isLeft = !isLeft;

        float scaleX = transform.localScale.x;

        scaleX *= -1;

        transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z); 
    }

    //Function to destroy the cheese as soon as I touch on it
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Collectables":
                Destroy(collision.gameObject);
                _gameController.audioSource.PlayOneShot(_gameController.fxScore);
                _gameController.score_function(1);
            break;

            case "GameoverVictory":
                _gameScene.changeScene("GameoverWin");
            break;

            case "CatAttack":
                               
                _gameScene.changeScene("Gameover");
            break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actorAnimController : MonoBehaviour
{
    private Rigidbody2D animationRb;
    public Transform limitCreations;
    public float animSpeed;
    public float distancDestroy;
    public float delayCreation;
    public GameObject animProfab;
    private bool isCreated;
    public AudioSource audioSource;
    public AudioClip fxCatAttack;
    

    // Start is called before the first frame update
    void Start()
    {
        animationRb = GetComponent<Rigidbody2D>();

        animationRb.velocity = new Vector2(animSpeed, 0);

        

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x <= 0)
        {
            
            StartCoroutine("_createAnim");
        }

        if(transform.position.x <= distancDestroy)
        {
            Destroy(this.gameObject);
        }
    }

    public void creatingAnim()
    {
        if (!isCreated)
        {
            isCreated = true;
            audioSource.PlayOneShot(fxCatAttack);
            GameObject _catRatAnim = Instantiate(animProfab);
            _catRatAnim.transform.position = new Vector2(limitCreations.transform.position.x, limitCreations.transform.position.y);
            
        }
    }

    IEnumerator _createAnim()
    {
        yield return new WaitForSeconds(delayCreation);

        creatingAnim();

        StartCoroutine("_createAnim");
    }
}

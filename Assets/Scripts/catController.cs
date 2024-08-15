using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class catController : MonoBehaviour
{
    public gameController _gameController;
    private Rigidbody2D catRb;
    public float catSpeed;

    // Start is called before the first frame update
    void Start()
    {
        catRb = GetComponent<Rigidbody2D>();
        _gameController = FindObjectOfType(typeof(gameController)) as gameController;
        catRb.velocity = new Vector2(_gameController.catSpeed[_gameController.index], 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameInvisible()
    {
        _gameController.instCatPermission = false;
        Destroy(this.gameObject);
       
    }
}

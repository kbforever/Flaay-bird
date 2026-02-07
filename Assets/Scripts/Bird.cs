using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bird : MonoBehaviour
{

    bool isPlayingGame;

    new Rigidbody2D rigidbody;

    float limitVelocityY = 3f;
    [SerializeField] private float force = 8f;


    private void Awake()
    {
        GameManager.Instance.IsStartGame += SetIsPlayingGame;
        

    }

    // Start is called before the first frame update
    void Start()
    {
        //rigidbody.gravityScale = 0f;
        isPlayingGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayingGame) return;

        if (Input.GetMouseButtonDown(0) && rigidbody.velocity.y < limitVelocityY)
        {

            rigidbody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        }
        //Debug.LogError();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Col"))
        {
            GameManager.Instance.GameOver();
        }
    }


    private void OnDestroy()
    {

        if (GameManager.Instance != null)
        {
            GameManager.Instance.IsStartGame -= SetIsPlayingGame;
        }
        
    }

    void SetIsPlayingGame(bool isStartGame)
    {
        isPlayingGame = isStartGame;
        rigidbody = GetComponent<Rigidbody2D>();
        if (rigidbody == null)
        {
            rigidbody = this.AddComponent<Rigidbody2D>();
        }
        rigidbody.gravityScale = 1.5f;
    }
}

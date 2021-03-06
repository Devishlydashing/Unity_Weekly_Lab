using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Called before start. Can be used to import functions etc.
    void Awake(){}

    // Creates a speed field in Unity for you to input 
    public float speed;
    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private Vector3 marioBodyDefaultPosition;
    private bool faceRightState = true;
    public float maxSpeed = 10;
    public float upSpeed;
    private bool onGroundState = true;
    public Transform enemyLocation;
    public Text scoreText;
    public Text hsText;
    public Sprite deadMario;
    public Sprite aliveMario;
    public Sprite defaultMario;
    private int score = 0;
    private int hs = 0;
    private bool countScoreState = false;


    // Start is called before the first frame update
    // Set game object
    void Start()
    {
        // Set to be 30 FPS
	    Application.targetFrameRate =  30;
        // Gets componenet of any gameobject the script is attached to
	    marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioBodyDefaultPosition = marioBody.transform.position;
    }
    
    // FixedUpdate may be called once per frame. See documentation for details.
    void FixedUpdate(){
 
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d")){
          // stop
            marioBody.velocity = Vector2.zero;
        }
    
        // dynamic rigidbody
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0){
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (marioBody.velocity.magnitude < maxSpeed)
                marioBody.AddForce(movement * speed);
        }

        if (Input.GetKeyDown("space") && onGroundState)
        {
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            countScoreState = true; //check if Gomba is underneath
        }

    }


    // Update is called once per frame    
    void Update(){
        // toggle state
        if (Input.GetKeyDown("a") && faceRightState){
            faceRightState = false;
            marioSprite.flipX = true;
        }

        if (Input.GetKeyDown("d") && !faceRightState){
            faceRightState = true;
            marioSprite.flipX = false;
        }

        // when jumping, and Gomba is near Mario and we haven't registered our score
        if (!onGroundState && countScoreState)
        {
            if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
            {
                countScoreState = false;
                score++;
                Debug.Log("Score incremented:"+ score.ToString());
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            onGroundState = true; // back on ground
            countScoreState = false; // reset score state
            scoreText.text = score.ToString();
            Debug.Log("Score updated:"+ score.ToString());
        };
    }

    // Since the Enemy object has been flagged as a Trigger. 
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with Gomba!");

            if (int.Parse(scoreText.text) > hs){
                hs = int.Parse(scoreText.text);
                hsText.text = hs.ToString();
            }

            marioBody.position = marioBodyDefaultPosition;
            marioSprite.sprite = defaultMario;
            score = 0;
            scoreText.text = score.ToString();
        }
    }
}
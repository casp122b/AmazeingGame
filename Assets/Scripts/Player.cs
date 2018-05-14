using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//Player inherits from MovingObject, Enemy also inherits from this.
public class Player : MovingObject
{

    public int wallDamage = 1;
    public int enemyDamage = 20;
    public int pointsPerPotion = 70;
    public float restartLevelDelay = 1f;
    public Text healthText;

    public AudioClip footStep1;
    public AudioClip footStep2;
    public AudioClip footStep3;
    public AudioClip footStep4;
    public AudioClip footStep5;
    public AudioClip footStep6;
    public AudioClip footStep7;
    public AudioClip footStep8;
    public AudioClip footStep9;
    public AudioClip footStep10;
    public AudioClip drinkSound;
    public AudioClip gameOverSound;
    public AudioClip attackSound1;
    public AudioClip attackSound2;

    private Animator animator;
    private int health;
    private SpriteRenderer _renderer;
    private Vector2 touchOrigin = -Vector2.one;

    //Start overrides the Start function of MovingObject
    protected override void Start()
    {
        animator = GetComponent<Animator>();

        _renderer = GetComponent<SpriteRenderer>();

        health = GameManager.instance.healthPoints;

        healthText.text = "Health: " + health;

        base.Start();
    }

    //This function is called when the behaviour becomes disabled or inactive.
    private void OnDisable()
    {
        GameManager.instance.healthPoints = health;
    }

    // Update is called once per frame
    void Update()
    {
        //If it's not the player's turn, exit the function.
        if (!GameManager.instance.playersTurn) return;

        int horizontal = 0;
        int vertical = 0;
        //Check if the game is running either in the Unity editor or in a standalone build.
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER

        //Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
        horizontal = (int)Input.GetAxisRaw("Horizontal");
        //Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
        vertical = (int)Input.GetAxisRaw("Vertical");



        //Check if moving horizontally, if so set vertical to zero.
        if (horizontal != 0)
        {
            vertical = 0;
            if (horizontal < 0)
                _renderer.flipX = true;
            else if (horizontal > 0)
                _renderer.flipX = false;
        }
        //Check if the game is running on iOS, Android, Windows Phone 8 or Unity iPhone
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        //Check if Input has registered more than zero touches
        if (Input.touchCount > 0)
        {
            Touch myTouch = Input.touches[0];

            //Check if the phase of that touch equals Began
            if (myTouch.phase == TouchPhase.Began)
            {
                touchOrigin = myTouch.position;
            }
            //If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
            else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
            {
                Vector2 touchEnd = myTouch.position;
                float x = touchEnd.x - touchOrigin.x;
                float y = touchEnd.y - touchOrigin.y;
                touchOrigin.x = -1;
                if (Mathf.Abs(x) > Mathf.Abs(y))
                    horizontal = x > 0 ? 1 : -1;
                else
                    vertical = y > 0 ? 1 : -1;
            }
        }

#endif

        //Check if we have a non-zero value for horizontal or vertical
        if (horizontal != 0 || vertical != 0)
        {
            AttemptMove<Wall>(horizontal, vertical);
            AttemptMove<Enemy>(horizontal, vertical);
        }


    }

    //AttemptMove overrides the AttemptMove function in the base class MovingObject
    //AttemptMove takes a generic parameter T which for Player will be of the type Wall, it also takes integers for x and y direction to move in.
    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        base.AttemptMove<T>(xDir, yDir);

        RaycastHit2D hit;

        if (Move(xDir, yDir, out hit))
        {
            SoundManager.instance.RandomizeSfx(footStep1, footStep2, footStep3, footStep4, footStep5, footStep6, footStep7, footStep8, footStep9, footStep10);
        }


        GameManager.instance.playersTurn = false;
    }

    //OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if the tag of the trigger collided with is Exit.
        if (collision.tag == "Exit")
        {
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
        //Check if the tag of the trigger collided with is Item.
        else if (collision.tag == "Item")
        {
            health += pointsPerPotion;
            if (health >= 100)
                health = 100;
            collision.gameObject.SetActive(false);
            SoundManager.instance.PlaySingle(drinkSound);
            healthText.text = "Health: " + health;
        }
    }

    //OnCantMove overrides the abstract function OnCantMove in MovingObject.
    //It takes a generic parameter T which in the case of Player is a Wall which the player can attack and destroy.
    protected override void OnCantMove<T>(T component)
    {
        Wall hitWall = component as Wall;
        Enemy hitEnemy = component as Enemy;
        if (component == hitWall)
        {
            hitWall.DamageWall(wallDamage);
            animator.SetTrigger("Attack");
            SoundManager.instance.RandomizeSfx(attackSound1, attackSound2);
        }
        if (component == hitEnemy)
        {
            hitEnemy.DamageEnemy(enemyDamage);
            animator.SetTrigger("Attack");
            SoundManager.instance.RandomizeSfx(attackSound1, attackSound2);
        }
    }

    //Restart reloads the scene when called.
    private void Restart()
    {
        SceneManager.LoadScene(1);
    }

    //TakeDamage is called when an enemy attacks the player.
    //It takes a parameter loss which specifies how many points to lose.
    public void TakeDamage(int loss)
    {
        animator.SetTrigger("isHit");
        health -= loss;
        healthText.text = "Health; " + health;
        CheckIfGameOver();
    }

    //CheckIfGameOver checks if the player is out of health points and if so, ends the game.
    private void CheckIfGameOver()
    {
        //Check if the health point total is less than or equal to zero.
        if (health <= 0)
        {
            SoundManager.instance.PlaySingle(gameOverSound);
            SoundManager.instance.musicSource.Stop();
            GameManager.instance.GameOver();
        }

    }
}

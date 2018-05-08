using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//Player inherits from MovingObject, Enemy also inherits from this.
public class Player : MovingObject {

    public int wallDamage = 1;
    public int enemyDamage = 20;
    public int pointsPerPotion = 70;
    public float restartLevelDelay = 1f;
    public Text healthText;

    private Animator animator;
    private int health;
    private SpriteRenderer _renderer;

    //Start overrides the Start function of MovingObject
    protected override void Start ()
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
    void Update ()
    {
        //If it's not the player's turn, exit the function.
        if (!GameManager.instance.playersTurn) return;

        int horizontal = 0;
        int vertical = 0;

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
        }
        if(component == hitEnemy)
        {
            hitEnemy.DamageEnemy(enemyDamage);
            animator.SetTrigger("Attack");
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
            animator.SetTrigger("isDead");
            GameManager.instance.GameOver();
        }
            
    }
}

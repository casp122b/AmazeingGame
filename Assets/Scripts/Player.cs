using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MovingObject {

    public int wallDamage = 1;
    public int pointsPerPotion = 70;
    public float restartLevelDelay = 1f;

    private Animator animator;
    private int health;

	// Use this for initialization
	protected override void Start () {
        animator = GetComponent<Animator>();

        health = GameManager.instance.healthPoints;

        base.Start();
	}

    private void OnDisable()
    {
        GameManager.instance.healthPoints = health;
    }

    // Update is called once per frame
    void Update () {
        if (!GameManager.instance.playersTurn) return;

        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

        if (horizontal != 0)
            vertical = 0;

        if (horizontal != 0 || vertical != 0)
            AttemptMove<Wall>(horizontal, vertical); 
	}

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        base.AttemptMove<T>(xDir, yDir);

        RaycastHit2D hit;

        GameManager.instance.playersTurn = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Exit")
        {
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
        else if(collision.tag == "Item")
        {
            health += pointsPerPotion;
            if (health >= 100)
                health = 100;
            collision.gameObject.SetActive(false);
        }
    }

    protected override void OnCantMove<T>(T component)
    {
        Wall hitWall = component as Wall;
        hitWall.DamageWall(wallDamage);
        animator.SetTrigger("Attack");
    }

    private void Restart()
    {
        SceneManager.LoadScene(4);
    }

    public void TakeDamage(int loss)
    {
        animator.SetTrigger("isHit");
        health -= loss;
        CheckIfGameOver();
    }

    private void CheckIfGameOver()
    {
        if (health <= 0)
            GameManager.instance.GameOver();
    }
}

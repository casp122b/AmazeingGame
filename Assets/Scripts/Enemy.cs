using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemy inherits from MovingObject, Player also inherits from this.
public class Enemy : MovingObject {

    public int playerDamage;
    public int hp = 20;

    private Animator animator;
    private Transform target;
    private bool skipTurn;

    //Start overrides the virtual Start function of the base class.
    protected override void Start ()
    {
        GameManager.instance.AddEnemyToList(this);
        animator = GetComponent<Animator>();
        //Find the Player GameObject using the Player tag and store a reference to the transform component.
        target = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
	}

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        if (skipTurn)
        {
            skipTurn = false;
            return;
        }

        base.AttemptMove<T>(xDir, yDir);

        skipTurn = true;
    }

    //MoveEnemy is called by the GameManger each turn to tell each Enemy to try to move towards the player.
    public void MoveEnemy()
    {
        //Declares variables for X and Y axis move directions, these range from -1 to 1.
        int xDir = 0;
        int yDir = 0;

        //If the difference in positions is approximately zero do the following:
        if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)
            //If the y coordinate of the target position is greater than the y coordinate of this enemy position set y direction 1 to move up. If not, set it to -1 to move down.
            yDir = target.position.y > transform.position.y ? 1 : -1;
        else
            //Check if target x position is greater than enemy's x position, if so set x direction to 1 to move right, if not set to -1 to move left.
            xDir = target.position.x > transform.position.x ? 1 : -1;

        AttemptMove<Player>(xDir, yDir);
    }

    //OnCantMove is called if Enemy attempts to move into a space occupied by a Player, it overrides the OnCantMove function of MovingObject 
    //and takes a generic parameter T which is used to pass in the component with which it is expected to encounter, in this case Player
    protected override void OnCantMove<T>(T component)
    {
        Player hitPlayer = component as Player;
        animator.SetTrigger("eAttack");
        hitPlayer.TakeDamage(playerDamage);
    }

    public void DamageEnemy(int loss)
    {
        hp -= loss;
        if (hp <= 0)
            gameObject.SetActive(false);
    }
}

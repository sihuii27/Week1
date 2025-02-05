using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float originalX; //starting x position of enemy
    private float maxOffset = 5.0f; //max distance goomba can move
    private float enemyPatroltime = 2.0f; //time taken to travel max distance
    private int moveRight = -1; //-1: move left, 1:move right
    private Vector2 velocity; //speed in combination with direction
    private Rigidbody2D enemyBody; //refer to enemy rigidbody 2d component

    public Vector3 startPosition = new Vector3(12.42f, -4.89f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>(); //intialization
        //get the starting position
        originalX = transform.position.x;
        ComputeVelocity(); //caluclate starting velocity
    }
    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);
    }
    void Movegoomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
        {
            //move goomba
            Movegoomba();
        }
        else //if goomba exceed maxOffset, goomba reverse direction and recalculate velocity
        {
            //change direction
            moveRight *= -1;
            ComputeVelocity();
            Movegoomba();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
    }
}

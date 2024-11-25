using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public Transform player;
    public float minDistance = 5f;
    public float moveSpeed = 2f; // Speed of movement towards the player

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        Debug.Log(distance);

        if (distance <= minDistance)
        {
            // Move towards the player at the specified speed
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }
}
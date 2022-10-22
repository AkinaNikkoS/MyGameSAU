using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Player Controls;
    public float Speed;
    public Rigidbody Rigidbody;
    public float Sensitivity;
    public enum State
    {
        Playing,
        Won,
        Loss,
    }

    public State CurrentState { get; private set; }

    private Vector3 tempVec = new Vector3(0, 0, -1);
    private Vector3 _previousMousePosition;
    private float moveX = 0f;

    void Update()
    {
        tempVec = tempVec.normalized * Speed * Time.deltaTime;
        Vector3 newPosition = new Vector3(0, 0, transform.position.z + tempVec.z);
        if (Input.GetMouseButton(1))
        {
            Controls.Bounce();
            Rigidbody.MovePosition(newPosition);
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 moveSide = Input.mousePosition - _previousMousePosition;
                moveSide = moveSide.normalized * Speed * Sensitivity * Time.deltaTime;
                moveX = transform.position.x - moveSide.x;

                if (-4 > moveX) moveX = -4f;
                else { if (moveX > 4) moveX = 4f; }
            }

            newPosition = new Vector3 (moveX, 0, transform.position.z + tempVec.z);
            Rigidbody.MovePosition(newPosition);            
        }
        _previousMousePosition = Input.mousePosition;
    }
    public void OnPlayerDied()
    {
        if (CurrentState != State.Playing) return;
        CurrentState = State.Loss;
        Controls.enabled = false;
        Debug.Log("Game Over!");
    }


    public void OnPlayerWon()
    {
        if (CurrentState != State.Playing) return;
        CurrentState = State.Won;
        Controls.enabled = false;
        Debug.Log("You Won!");
    }
      
}

using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject[] ObstaclePrephabs;
    public Player Controls;
    public float Speed;
    public Rigidbody Rigidbody;
    public float Sensitivity;
    public GameObject WonUI;
    public GameObject LossUI;
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
            if (CurrentState == State.Playing)
            {
                moveX = transform.position.x;
                newPosition = new Vector3(moveX, 0, transform.position.z + tempVec.z);
                Rigidbody.MovePosition(newPosition);
                Controls.Bounce();
            }
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
        LossUI.SetActive(true);
        Debug.Log("Game Over!");
        tempVec.z = 0;
    }


    public void OnPlayerWon()
    {
        if (CurrentState != State.Playing) return;
        CurrentState = State.Won;
        Controls.enabled = false;
        WonUI.SetActive(true);
        Debug.Log("You Won!");
        tempVec.z = 0;
    }
   
}

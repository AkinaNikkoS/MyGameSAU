using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float BounceSpeed;
    public Rigidbody Rigidbody;
    public Transform Transform; 
    public Game Game;
    public Text Lives;
    public Text Food;
    public Text Gems;
    public Text Coins;
    private int lives = 3;
    private int food = 0;
    private int gems = 0;
    private int coins;

    private void Start()
    {
        Coins.text = "+ " + (50+50*lives).ToString();
        Food.text = "+ " + food.ToString();
        Gems.text = "+ " + gems.ToString();
    }
    public void Bounce()
    {
        if (Transform.position.y < 1.14) Rigidbody.velocity = new Vector3(0, BounceSpeed, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            lives--;
            Lives.text = lives.ToString();
            if (lives <= 0) Game.OnPlayerDied();
            coins = 50 * (lives + 1);
            Coins.text = "+ " + coins.ToString();
        }

        if (other.gameObject.tag == "Food")
        {
            food++;
            Destroy(other);
            Food.text = "+ " + food.ToString();
        }

        if (other.gameObject.tag == "Gems")
        {
            gems++;
            Destroy(other);
            Gems.text = "+ " + gems.ToString();
        }

        if (other.gameObject.tag == "Finish") Game.OnPlayerWon();

    }
}

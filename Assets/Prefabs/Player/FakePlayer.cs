using UnityEngine;

public class FakePlayer : MonoBehaviour
{
    [SerializeField] private int health = 100;

    public void DecreaseHP(int value)
    {
        if(health - value > 0)
        {
            health -= value;
            Debug.Log("HP: " + health);
        }
        else
        {
            Debug.Log("You are dead");
        }
    }
}

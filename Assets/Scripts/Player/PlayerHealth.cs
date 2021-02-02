using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    #region variables
    [SerializeField]
    private int healthMax;
    public int healthCurrent { get; set; }
    public bool isDead { get; set; }

    #region MonoBehaviour callbacks
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        healthCurrent = healthMax;
    }
    #endregion


    public void takeDamage(int amount)
    {
        healthCurrent -= amount;
        if(healthCurrent <= 0)
        {
            onDeath();
        }
    }

    private void onDeath()
    {
        isDead = true;
        ///add more stuff here
    }
}

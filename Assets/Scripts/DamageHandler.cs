using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    public CashCounter cc;
    public int health;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Bullet>() != null)
        {
            Destroy(other.gameObject);
            health -= other.GetComponent<Bullet>().damage;
            if (health <= 0)
            {
                cc.addCash(20);
                Destroy(player);
            }
        }
    }
}

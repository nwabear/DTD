using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage, lifespan;

    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifespan--;
        if (lifespan <= 0)
        {
            Destroy(bullet);
            Destroy(this);
        }
    }
}

using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject redHealth;
    private int health, prev;
    private float multiplier, posMult;
    

    // Start is called before the first frame update
    void Start()
    {
        multiplier = 0.3f / GetComponentInParent<DamageHandler>().health;
        posMult = 0.4f / GetComponentInParent<DamageHandler>().health;
        
        prev = GetComponentInParent<DamageHandler>().health;
        health = prev;
    }

    // Update is called once per frame
    void Update()
    {
        health = GetComponentInParent<DamageHandler>().health;
        if (health != prev)
        {
            Vector3 scale = redHealth.transform.localScale;
            Vector3 pos = redHealth.transform.localPosition;

            transform.localScale = new Vector3(health * multiplier, scale.y, scale.z);
            transform.localPosition = new Vector3(health * posMult - 0.4f, pos.y, pos.z);
            prev = health;
        }
    }
}

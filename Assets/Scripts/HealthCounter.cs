using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthCounter : MonoBehaviour
{
    public int health;

    public GameObject counter;

    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = counter.GetComponent<Text>();
        text.text = "Health: " + health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            health--;
            text.text = "Health: " + health;
            Destroy(other.gameObject);
        }
    }
}

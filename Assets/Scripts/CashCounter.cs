using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CashCounter : MonoBehaviour
{
    public Text cashTxt;
    public int cash;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cashTxt.text = "$" + cash;
    }

    public void addCash(int ammount)
    {
        cash += ammount;
    }
}

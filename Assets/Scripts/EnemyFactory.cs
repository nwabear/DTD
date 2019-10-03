using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFactory : MonoBehaviour
{
    public GameObject enemy, end, enemies, startPos, waveText;
    public CashCounter cc;

    private int timer;
    private int wave = 0;

    private int spawnDelay, enemyHealth, enemyCount;

    private EnemyPathCalc epc;

    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        timer = -300;
        epc = end.GetComponent<EnemyPathCalc>();
        text = waveText.GetComponent<Text>();
        text.text = "Wave: " + wave;
    }

    // Update is called once per frame
    void Update()
    {
        if (!(timer < 0 && enemies.GetComponentsInChildren<Navigator>().Length != 0))
        {
            timer++;
        }

        if (timer >= spawnDelay)
        {
            GameObject enm = enemy;
            enm.GetComponent<DamageHandler>().health = enemyHealth;
            GameObject go = Instantiate(enm, startPos.transform.position, transform.rotation);
            go.GetComponent<Navigator>().path = epc.getDefPath();
            go.GetComponent<DamageHandler>().cc = cc;
            go.transform.parent = enemies.transform;
            timer = 0;
            enemyCount--;
        }

        if (enemyCount == 0)
        {
            timer = -300;
            nextWave();
        }
    }

    void nextWave()
    {
        cc.cash += (int)(0.75 * (50 * wave));
        wave++;
        text.text = "Wave: " + wave;
        spawnDelay = 100 / wave;
        enemyCount = wave + (wave / 2);
        enemyHealth = wave;
    }
}

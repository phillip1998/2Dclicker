using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    [SerializeField]
    AstroidPool astroidP;
    [SerializeField]
    EnemyPool enemyP;
    [SerializeField]
    EffectPool effectP;
    [SerializeField]
    BoltPool boltP;
    [SerializeField]
    BossPool bossP;
    [SerializeField]
    private int Score;
    private MainUIController ui;
    private Coroutine hazard, spawn;
    [SerializeField]
    private BGScrolling[] BGs;
    private bool isGameOver;
    private NewBehaviourScript pc;
    private bool duringBoss;
    public int gap;
    private int stage;
	// Use this for initialization
	void Start() {
        stage = 1;
        duringBoss = false;
        isGameOver = false;
        hazard = StartCoroutine(Hazards());
        spawn = StartCoroutine(Spawn());
        Score = 0;
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<NewBehaviourScript>();
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<MainUIController>();
        ui.setScore(Score);
        for(int i = 0; i < BGs.Length; i++)
        {
            BGs[i].StartScroll();
        }
    }
    public void addScore(int value)
    {
        Score += value;
        ui.setScore(Score);
    }
    public void setHP(int value)
    {
        ui.setHP(value);
    }
    public void setLife(int value)
    {
        ui.setLife(value);
    }
    public GameObject GetEffect(eEffectType input)
    {
        return effectP.GetFromPool(input);
    }
    public void GameOver()
    {
        StopCoroutine(hazard);
        StopCoroutine(spawn);
        for (int i = 0; i < BGs.Length; i++)
        {
            BGs[i].StopScroll();
        }
        ui.setGameOver();
        isGameOver = true;
    }
    public void wait()
    {
        StartCoroutine(WaitStart());
    }
    private IEnumerator WaitStart()
    {

        StopCoroutine(hazard);
        StopCoroutine(spawn);
        yield return new WaitForSeconds(5);
        hazard = StartCoroutine(Hazards());
        spawn = StartCoroutine(Spawn());
    }
	private IEnumerator Hazards()
    {
        int astnum = 3;
        WaitForSeconds s1 = new WaitForSeconds(3);
        WaitForSeconds s2 = new WaitForSeconds(.2f);
        while (true)
        {
            astnum = 2 + stage;
            yield return s1;
            if(!duringBoss)
            for (int i = 0; i < astnum; i++)
            {
                AstroidMovement temp = astroidP.GetFromPool(Random.Range(0, 3));

                temp.transform.position = new Vector3(Random.Range(-5f, 5f), 0, 16);
                temp.gameObject.SetActive(true);

                yield return s2;
            }
            
        }
    }
    private IEnumerator Spawn()
    {
        int ennum = 1;
        int n = 0;
        WaitForSeconds s1 = new WaitForSeconds(5);
        WaitForSeconds s2 = new WaitForSeconds(.2f);
        WaitForSeconds s3 = new WaitForSeconds(1);
        while (true)
        {
            ennum = 1 + (int)(stage / 2);
            yield return s1;
            if(!duringBoss)
            for (int i = 0; i < ennum; i++)
            {
                EnemyController temp = enemyP.GetFromPool();

                temp.transform.position = new Vector3(Random.Range(-5f, 5f), 0, 16);
                temp.gameObject.SetActive(true);

                yield return new WaitForSeconds(Random.Range(0.2f,0.6f));
            }
            n++;
            if ((n = n % gap)==0)
            {
                yield return new WaitForSeconds(5);
                BossController boss = bossP.GetFromPool();
                boss.transform.position = new Vector3(0, 0, 19);
                boss.gameObject.SetActive(true);
                while (boss.gameObject.activeInHierarchy)
                {
                    yield return s3;
                }
                stage++;
            }
        }
    }
    private IEnumerator SpawnBoss()
    {
        int ennum = 1;
        while (true)
        {
            yield return new WaitForSeconds(5);
            if (!duringBoss)
                for (int i = 0; i < ennum; i++)
                {
                    EnemyController temp = enemyP.GetFromPool();

                    temp.transform.position = new Vector3(Random.Range(-5f, 5f), 0, 16);
                    temp.gameObject.SetActive(true);

                    yield return new WaitForSeconds(.2f);
                }

        }
    }
    public void GameRestart()
    {
        //SceneManager.LoadScene(0);
        pc.gameObject.transform.position = Vector3.zero;
        pc.gameObject.SetActive(true);
        Score = 0;
        ui.setScore(Score);

        pc.setLife();
        ui.hideGameOver();
        hazard = StartCoroutine(Hazards());
        spawn = StartCoroutine(Spawn());
        for (int i = 0; i < BGs.Length; i++)
            BGs[i].StartScroll();
        RemoveAll();
    }
    public void RemoveAll()
    {
        enemyP.RemoveAll();
        astroidP.RemoveAll();
        boltP.RemoveAll();

    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.R) && isGameOver)
        {
            GameRestart();
        }
        
	}
}

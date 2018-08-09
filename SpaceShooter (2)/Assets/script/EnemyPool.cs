using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour {
    [SerializeField]
    private BoltPool EnemyBoltPool;
    [SerializeField]
    private EnemyController ec;

    private List<EnemyController> enemyList;
	// Use this for initialization
	void Start () {
        enemyList = new List<EnemyController>();
	}
	public EnemyController GetFromPool()
    {
        for(int i = 0; i < enemyList.Count; i++)
        {
            if (!enemyList[i].gameObject.activeInHierarchy)
            {
                return enemyList[i];
            }
        }
        EnemyController newEnemy = Instantiate(ec);
        newEnemy.setBoltPool(EnemyBoltPool);
        enemyList.Add(newEnemy);
        return newEnemy;
    }
    public void RemoveAll()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].gameObject.SetActive(false);
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}

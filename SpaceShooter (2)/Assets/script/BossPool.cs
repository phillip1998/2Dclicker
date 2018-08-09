using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPool : MonoBehaviour {

    [SerializeField]
    private BoltPool2 BossBoltPool;
    [SerializeField]
    private BossController ec;

    private List<BossController> bossList;
    // Use this for initialization
    void Start()
    {
        bossList = new List<BossController>();
    }
    public BossController GetFromPool()
    {
        for (int i = 0; i < bossList.Count; i++)
        {
            if (!bossList[i].gameObject.activeInHierarchy)
            {
                return bossList[i];
            }
        }
        BossController newEnemy = Instantiate(ec);
        newEnemy.setBoltPool(BossBoltPool);
        bossList.Add(newEnemy);
        return newEnemy;
    }
    public void RemoveAll()
    {
        for (int i = 0; i < bossList.Count; i++)
        {
            bossList[i].gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}

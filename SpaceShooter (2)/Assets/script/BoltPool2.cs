using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltPool2 : MonoBehaviour {

    [SerializeField]
    private Bolt2 bolt;
    private List<Bolt2> boltList;

    // Use this for initialization
    void Start()
    {
        boltList = new List<Bolt2>();
    }

    public Bolt2 GetFromPool()
    {
        for (int i = 0; i < boltList.Count; i++)
        {
            if (!boltList[i].gameObject.activeInHierarchy)
            {
                return boltList[i];
            }
        }
        Bolt2 temp = Instantiate(bolt);
        boltList.Add(temp);
        return temp;
    }
    public void RemoveAll()
    {
        for (int i = 0; i < boltList.Count; i++)
        {
            boltList[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
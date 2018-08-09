using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidPool : MonoBehaviour {

    [SerializeField]
    private AstroidMovement[] astroid;
    private List<AstroidMovement>[] astroidList;

    // Use this for initialization
    void Start()
    {
        astroidList = new List<AstroidMovement>[astroid.Length];
        for (int i = 0; i < astroid.Length; i++)
        {
            astroidList[i] = new List<AstroidMovement>();
        }
    }

    public AstroidMovement GetFromPool(int index)
    {
        for (int i = 0; i < astroidList[index].Count; i++)
        {
            if (!astroidList[index][i].gameObject.activeInHierarchy)
            {
                return astroidList[index][i];
            }
        }
        AstroidMovement temp = Instantiate(astroid[index]);
        astroidList[index].Add(temp);
        return temp;
    }
    public void RemoveAll()
    {
        for(int j=0;j<astroidList.Length;j++)
        for (int i = 0; i < astroidList[j].Count; i++)
        {
                astroidList[j][i].gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}

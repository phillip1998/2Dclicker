using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEffectType
{
    expAstroid,expEnemy,expPlayer
}
public class EffectPool : MonoBehaviour {
    [SerializeField]
    private GameObject[] effect;
    private List<GameObject>[] effectList;

	// Use this for initialization
	void Start () {
        effectList = new List<GameObject>[effect.Length];
        for(int i = 0; i < effect.Length; i++)
        {
            effectList[i] = new List<GameObject>();
        }
	}
    
    public GameObject GetFromPool(eEffectType input)
    {
        int index = (int)input;
        for(int i = 0; i < effectList[index].Count; i++)
        {
            if (!effectList[index][i].activeInHierarchy)
                return effectList[index][i];
        }
        GameObject temp = Instantiate(effect[index]);
        effectList[index].Add(temp);
        return temp;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

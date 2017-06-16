using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

    public GameObject[] group;
    public Transform Nextblock;
    private GameObject Next;
    private GameObject Current;
    public void spawnNext()
    {
        int i = Random.Range(0, group.Length);
        if(Next==null)
        { 
            Current = (GameObject)Instantiate(group[i], transform.position, Quaternion.identity);
            Current.transform.SetParent(Nextblock.transform);
        }
        else
        {
            Current = Next;
            Current.GetComponent<Group>().enabled = true;
            Current.transform.position =transform.position;
        }
        i = Random.Range(0, group.Length);
        Next =(GameObject)Instantiate(group[i], transform.position, Quaternion.identity);
        Next.GetComponent<Group>().enabled = false;
        Next.transform.position = Nextblock.position;
        Next.transform.SetParent(Nextblock.transform);
    }
    void Start()
    {
        spawnNext();
    }
}

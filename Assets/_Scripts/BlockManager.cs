using UnityEngine;
using System.Collections;

public class BlockManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        foreach (Transform child in transform)
        {
            //Debug.Log(child.GetComponentsInChildren<Transform>().Length);
            if (child.GetComponentsInChildren<Transform>().Length == 1)
            {
                Destroy(child.gameObject);
            }
        }
    }
}

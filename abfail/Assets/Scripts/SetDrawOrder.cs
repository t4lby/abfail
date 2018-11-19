using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDrawOrder : MonoBehaviour {

    public int Index;
	void Start ()
    {
        GetComponent<Renderer>().sortingOrder = Index;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesFaceController : MonoBehaviour {

	public List<Sprite> Faces;
	
	public void RandomiseFace()
    {
        this.GetComponent<SpriteRenderer>().sprite = Faces[Random.Range(0, Faces.Count)];
    }
}

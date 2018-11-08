using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceController : MonoBehaviour 
{
    public Sprite CalmFace;
    public Sprite LessCalmFace;
    public Sprite PanickedFace;

    public void SetCalm()
    {
        GetComponent<SpriteRenderer>().sprite = CalmFace;
    }

    public void SetLessCalm()
    {
        GetComponent<SpriteRenderer>().sprite = LessCalmFace;
    }

    public void SetPanicked()
    {
        GetComponent<SpriteRenderer>().sprite = PanickedFace;
    }
}

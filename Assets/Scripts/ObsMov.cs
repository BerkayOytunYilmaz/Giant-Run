using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ObsMov : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DORotate(Vector3.up * 50f, 0.35f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

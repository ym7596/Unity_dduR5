using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundMove : MonoBehaviour
{
    RawImage rImage;

    [SerializeField]
    private float speed = 0.01f;
    private float offset;
    private void Start()
    {
        rImage = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime * speed;
        if(offset > 1)
        {
            offset = 0;
        }
        rImage.uvRect = new Rect(0, offset, 1,1); 
    }
}

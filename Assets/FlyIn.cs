using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyIn : MonoBehaviour
{
    // Start is called before the first frame update
    private bool flying;
    private RectTransform rt;

    private Vector2 targetPos;

    [SerializeField] private Vector2 offset;

    public void StartFly()
    {
        flying = true;
    }

    void Start()
    {
        rt = GetComponent<RectTransform>();
        targetPos = rt.anchoredPosition;
        rt.anchoredPosition += offset;
    }

    // Update is called once per frame
    void Update()
    {
        if (flying)
        {
            rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition, targetPos, 10f * Time.deltaTime);
        }
    }
}

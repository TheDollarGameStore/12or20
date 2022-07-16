using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : MonoBehaviour
{
    [SerializeField]
    private bool randomizeStart;

    [SerializeField]
    private Vector2 dir;

    [SerializeField]
    private float speed;

    private Vector2 startPos;

    // Start is called before the first frame update
    private float y = 0;
    private float x = 0;
    void Start()
    {
        startPos = transform.localPosition;
        if (randomizeStart)
        {
            x = Random.Range(0f, 2 * Mathf.PI);
        }
    }

    // Update is called once per frame
    void Update()
    {
        x += speed * Time.deltaTime;

        y = Mathf.Sin(x);

        transform.localPosition = startPos + (dir * y);
    }
}

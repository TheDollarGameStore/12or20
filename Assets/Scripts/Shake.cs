using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    private bool isSkaking;

    [SerializeField] private float intensity;

    private Vector3 startPos;

    // Start is called before the first frame update
    public void StartShaking()
    {
        isSkaking = true;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSkaking)
        {
            transform.position = Vector3.Lerp(transform.position, startPos, 20f * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (isSkaking)
        {
            transform.position += (Vector3)new Vector2(Random.Range(-intensity, intensity), Random.Range(-intensity, intensity));
        }
    }
}

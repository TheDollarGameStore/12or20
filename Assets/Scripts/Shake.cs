using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    private bool isShaking;

    [SerializeField] private float intensity;
    [SerializeField] private float twistIntensity;

    private Vector3 startPos;

    // Start is called before the first frame update
    public void StartShaking()
    {
        isShaking = true;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isShaking)
        {
            transform.position = Vector3.Lerp(transform.position, startPos, 20f * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Vector3.zero), 10f * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (isShaking)
        {
            transform.position += (Vector3)new Vector2(Random.Range(-intensity, intensity), Random.Range(-intensity, intensity));
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, transform.rotation.eulerAngles.z + Random.Range(-twistIntensity, twistIntensity)));
        }
    }
}

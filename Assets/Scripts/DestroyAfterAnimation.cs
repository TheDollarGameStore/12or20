using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour
{
    [SerializeField] private bool randomRotation;
    public float delay = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);

        if (randomRotation)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Random.Range(0f, 360f)));
        }
    }
}

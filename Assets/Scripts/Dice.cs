using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [HideInInspector] public int value;
    private SpriteRenderer sr;

    private Shake shaker;

    [SerializeField] private GameObject explosion;

    [SerializeField] private List<Sprite> sprites;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprites[value - 1];
        shaker = GetComponent<Shake>();
    }

    public void Matched()
    {
        shaker.StartShaking();
        Invoke("DestroySelf", 0.75f);
    }

    private void DestroySelf()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

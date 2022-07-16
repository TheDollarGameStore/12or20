using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [HideInInspector] public int value;
    private SpriteRenderer sr;

    [SerializeField] private List<Sprite> sprites;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprites[value - 1];
    }

    public void Matched()
    {
        Destroy(gameObject);
    }
}

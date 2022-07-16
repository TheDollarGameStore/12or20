using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonNavigate : MonoBehaviour
{
    private Wobble wobbler;

    [SerializeField] private int scene;

    private bool clicked;
    // Start is called before the first frame update
    void Start()
    {
        wobbler = GetComponent<Wobble>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !clicked)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Button"))
                {
                    clicked = true;
                    wobbler.DoTheWobble();
                    Transitioner.Instance.TransitionToScene(scene);
                }
            }
        }
    }
}

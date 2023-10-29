using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Constants
{
    public const float MaxXPos = 2.7f;
    public const float MaxYPos = 3.5f;
}

public class DropBall : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isDropping = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDropping)
        {
            return;
        }
        if (Input.GetMouseButton(0))
        {
            Drop();
        }
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.x = Mathf.Clamp(mousePos.x, -Constants.MaxXPos, Constants.MaxXPos);
        mousePos.y = Constants.MaxYPos;
        transform.position = mousePos;
    }

    private void Drop()
    {
        isDropping = true;
        rb.simulated = true;
    }
}

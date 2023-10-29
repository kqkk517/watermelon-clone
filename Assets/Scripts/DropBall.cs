using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class MousePosConstants
{
    public const float MAX_X_POS = 2.7f;
    public const float Y_POS = 3.5f;
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
        mousePos.x = Mathf.Clamp(mousePos.x, -MousePosConstants.MAX_X_POS, MousePosConstants.MAX_X_POS);
        mousePos.y = MousePosConstants.Y_POS;
        transform.position = mousePos;
    }

    private void Drop()
    {
        isDropping = true;
        rb.simulated = true;
        GameManager.Instance.isNext = true;
    }
}

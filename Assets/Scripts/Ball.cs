using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class MousePosConstants
{
    public const float MAX_X_POS = 2.8f;
    public const float Y_POS = 3.5f;
}

public class Ball : MonoBehaviour
{
    // ボールの種類を識別するID
    public int id { get; set; }
    // 落下中かどうか（落下終了後もtrueのままで差し支えない）
    public bool isDropping { get; set; } = false;

    // Rigidbodyコンポーネント
    private Rigidbody2D rb;
    // 合体できるかどうか
    private bool canMerge = true;

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
        mousePos.x = Mathf.Clamp(mousePos.x, -MousePosConstants.MAX_X_POS,
            MousePosConstants.MAX_X_POS);
        mousePos.y = MousePosConstants.Y_POS;
        transform.position = mousePos;
    }

    // 同じ種類のボールとぶつかったら合体する（ぶつかったボール同士を消す）
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball colBall = collision.gameObject.GetComponent<Ball>();
        if (colBall != null &&
            id == colBall.id &&
            id < GameManager.Instance.ballsLength &&
            canMerge &&
            colBall.canMerge)
        {
            canMerge = false;
            colBall.canMerge = false;
            GameManager.Instance.MergeBalls(
                (transform.position + colBall.transform.position) / 2, id);
            Destroy(gameObject);
            Destroy(colBall.gameObject);
        }
    }

    // ボールを落とす
    private void Drop()
    {
        isDropping = true;
        rb.simulated = true;
        GameManager.Instance.isNext = true;
    }
}

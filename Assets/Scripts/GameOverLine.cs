using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class GameOverConstants
{
    public const float CAN_STAY_TIME = 5.0f;
}

public class GameOverLine : MonoBehaviour
{
    // ゲームオーバーラインに触れている時間
    private float stayTime;

    // Start is called before the first frame update
    void Start()
    {
        stayTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // ゲームオーバーラインにボールが触れている時間を計算する
    // 一定時間を超えたらゲームオーバーにする
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            stayTime += Time.deltaTime;
            if (stayTime > GameOverConstants.CAN_STAY_TIME)
            {
                GameManager.Instance.GameOver();
            }
        }
    }

    // ゲームオーバーラインからボールが離れたら触れている時間をリセットする
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            stayTime = 0.0f;
        }
    }
}

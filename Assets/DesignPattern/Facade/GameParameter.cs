using UnityEngine;

/// <summary>
/// データのまとめ役
/// </summary>
public class GameParameter
{
    public PlayerParam Player { get; private set; }
    public PlayerParam Enemy { get; private set; }

    void SetUp()
    {
        //設定
    }

    void Dispose()
    {
        //破棄
    }

    void UpdateFrame()
    {
        //毎フレーム実行するデータの処理
    }
}

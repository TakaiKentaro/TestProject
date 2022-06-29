using UnityEngine;

/// <summary>
/// UIのまとめ役
/// </summary>
public sealed class GameUI
{
    public ResultUI Result { get; private set; }

    void SetUp()
    {
        //各画面の設定
    }

    void Dispose()
    {
        //破棄
    }

    void ChangeUI(UIType type)
    {
        //UI切り替え
    }
}

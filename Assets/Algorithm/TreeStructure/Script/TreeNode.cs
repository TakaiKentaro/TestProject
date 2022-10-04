using System.Collections;
using System.Collections.Generic;

public enum NodeType
{
    NODATA, //格納値を取得・設定不可
    HAVEDATA, //格納値を取得・設定可能
};

public class TreeNode
{
    List<TreeNode> _childList; //子要素のリスト
    TreeNode _parentNode; //親要素
    object _value; //格納値

}

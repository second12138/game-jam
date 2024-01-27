using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 节点类
public class Node
{
    public int gridX;  // 节点在网格中的 X 坐标
    public int gridY;  // 节点在网格中的 Y 坐标
    public bool walkable;  // 是否可以行走
    public Vector3 worldPosition;  // 节点在世界中的位置

    public int gCost;  // 从起点到该节点的花费
    public int hCost;  // 从该节点到终点的预估花费（启发式搜索）
    public Node parent;  // 该节点的父节点（用于路径重建）

    // 节点的构造函数
    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
    {
        walkable = _walkable;  // 设置是否可以行走
        worldPosition = _worldPos;  // 设置节点在世界中的位置
        gridX = _gridX;  // 设置节点在网格中的 X 坐标
        gridY = _gridY;  // 设置节点在网格中的 Y 坐标
    }

    // 获取节点的 fCost（gCost 和 hCost 的和）
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
}


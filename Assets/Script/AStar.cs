using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A* 算法的实现类
public class AStar : MonoBehaviour
{
    public float speed = 1.0f;  // 移动速度
    private List<Node> Path = new List<Node>();  // 路径列表
    public Transform target;  // 目标位置
    private PathfindingGrid grid;  // 网格
    private Animator animator;  // 动画控制器

    private void Start()
    {
        grid = gameObject.AddComponent<PathfindingGrid>();  // 添加网格组件
        Path = FindPath(grid, transform.position, target.position);  // 查找路径
        animator = GetComponent<Animator>();  // 获取动画控制器组件
    }

    private void Update()
    {
        Path = FindPath(grid, transform.position, target.position);  // 更新路径
        MoveSlime();  // 移动角色
    }

    // 查找路径的方法
    public List<Node> FindPath(PathfindingGrid grid, Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = grid.NodeFromWorldPoint(startPos);  // 获取起始节点
        Node targetNode = grid.NodeFromWorldPoint(targetPos);  // 获取目标节点
        List<Node> OpenList = new List<Node>();  // 开放列表
        HashSet<Node> ClosedList = new HashSet<Node>();  // 关闭列表
        OpenList.Add(startNode);  // 将起始节点添加到开放列表

        // 主循环
        while (OpenList.Count > 0)
        {
            Node currentNode = OpenList[0];  // 当前节点
            for (int i = 1; i < OpenList.Count; i++)
            {
                if (OpenList[i].fCost < currentNode.fCost || OpenList[i].fCost == currentNode.fCost && OpenList[i].hCost < currentNode.hCost)
                {
                    currentNode = OpenList[i];  // 更新当前节点
                }
            }
            OpenList.Remove(currentNode);  // 从开放列表中移除当前节点
            ClosedList.Add(currentNode);  // 将当前节点添加到关闭列表
            if (currentNode == targetNode)  // 如果当前节点是目标节点
            {
                RetracePath(startNode, targetNode);  // 重建路径
                return Path;  // 返回路径
            }
            // 遍历当前节点的所有邻居
            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable || ClosedList.Contains(neighbour))  // 如果邻居节点不可行走或已在关闭列表中
                {
                    continue;  // 跳过当前迭代
                }
                int newCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);  // 计算从当前节点到邻居节点的新花费
                if (newCostToNeighbour < neighbour.gCost || !OpenList.Contains(neighbour))  // 如果新的花费小于邻居节点的花费或邻居节点不在开放列表中
                {
                    neighbour.gCost = newCostToNeighbour;  // 更新邻居节点的 gCost
                    neighbour.hCost = GetDistance(neighbour, targetNode);  // 更新邻居节点的 hCost
                    neighbour.parent = currentNode;  // 设置邻居节点的父节点为当前节点
                    if (!OpenList.Contains(neighbour))  // 如果邻居节点不在开放列表中
                    {
                        OpenList.Add(neighbour);  // 将邻居节点添加到开放列表
                    }
                }
            }
        }
        return null;  // 如果没有找到路径，返回 null
    }

    // 重建路径的方法
    void RetracePath(Node startNode, Node endNode)
    {
        Path.Clear();  // 清空路径列表
        Node currentNode = endNode;  // 设置当前节点为终点
        while (currentNode != startNode)  // 当当前节点不是起点
        {
            Path.Add(currentNode);  // 将当前节点添加到路径列表
            currentNode = currentNode.parent;  // 将当前节点的父节点设置为新的当前节点
        }
        Path.Reverse();  // 反转路径列表
    }

    // 计算两个节点之间的距离
    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);  // X 方向的距离
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);  // Y 方向的距离
        if (dstX > dstY)  // 如果 X 方向的距离大于 Y 方向的距离
        {
            return 14 * dstY + 10 * (dstX - dstY);  // 返回距离
        }
        return 14 * dstX + 10 * (dstY - dstX);  // 返回距离
    }

    // 移动角色的方法
    void MoveSlime()
    {
        Debug.Log("MoveSlime method is called");  // 输出调试信息
        if (Path.Count > 0)  // 如果路径列表不为空
        {
            // 获取下一个位置
            Vector3 nextPosition = Path[0].worldPosition;

            // 将角色向下一个位置移动
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

            // 如果角色已经达到下一个位置
            if (transform.position == nextPosition)
            {
                Path.RemoveAt(0);  // 从路径列表中移除该位置
            }

            animator.SetBool("isMoving", true);  // 设置动画状态为移动
        }
        else  // 如果路径列表为空
        {
            animator.SetBool("isMoving", false);  // 设置动画状态为静止
        }
    }
}

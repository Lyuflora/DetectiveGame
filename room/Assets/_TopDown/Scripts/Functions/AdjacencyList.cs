using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Dec
{
    public class AdjacencyList<T>
    {
        [SerializeField]List<Vertex<T>> items;//图的顶点集合
        public AdjacencyList() : this(10) { }//构造方法
        public AdjacencyList(int capacity)//按指定的容量进行构造
        {
            items = new List<Vertex<T>>(capacity);
        }
        public void AddVertex(T item)//添加一个节点
        {
            if (Contains(item))
            {

                throw new ArgumentException("插入了重复节点！" + item);
            }
            items.Add(new Vertex<T>(item));
        }
        public void AddEdge(T from, T to)//添加无向边
        {
            Vertex<T> fromVer = Find(from);//找到起始节点
            if (fromVer == null)
            {
                throw new ArgumentException("头节点并不存在！");
            }
            Vertex<T> toVer = Find(to);//找到结束节点
            if (toVer == null)
            {
                throw new ArgumentException("尾节点并不存在！");
            }
            //无向边两个节点都需记录的边的信息
            AddDirectedEdge(fromVer, toVer);
            AddDirectedEdge(toVer, fromVer);
        }
        public bool Contains(T item)//查图中是否包含某项
        {
            foreach (Vertex<T> v in items)
            {
                if (v.data.Equals(item))
                {
                    return true;
                }
            }
            return false;
        }
        private Vertex<T> Find(T item)//查找指定项并返回
        {
            foreach (Vertex<T> v in items)
            {
                if (v.data.Equals(item))
                {
                    return v;
                }
            }
            return null;
        }
        //添加有向边
        private void AddDirectedEdge(Vertex<T> fromVer, Vertex<T> toVer)
        {
            if (fromVer.firstEdge == null)//无邻接点的时候
            {
                fromVer.firstEdge = new Point(toVer);
            }
            else
            {
                Point tmp, node = fromVer.firstEdge;
                do
                {
                    //检查是否添加了重复边
                    if (node.adjvex.data.Equals(toVer.data))
                    {
                        Debug.Log("添加了重复的边！");
                        PenTool.m_Instance.ResetPenTool();
                        return;
                        // throw new ArgumentException("添加了重复的边！");
                    }
                    tmp = node;
                    node = node.next;
                } while (node != null);
                tmp.next = new Point(toVer);//添加到链表末尾。
            }
        }
        public override string ToString()//仅用于测试
        {
            //打印每个顶点和它的邻接点
            string s = string.Empty;
            foreach (Vertex<T> v in items)
            {
                s += v.data.ToString() + ":";
                if (v.firstEdge != null)
                {
                    Point tmp = v.firstEdge;
                    while (tmp != null)
                    {
                        s += " ";
                        s += tmp.adjvex.data.ToString();
                        tmp = tmp.next;
                    }
                }
                s += "\r\n";
            }
            return s;
        }
        public bool TestEdge(int VerId, T targetId)
        {
            Vertex<T> v = items[VerId];
            string s = string.Empty;
            if (v.firstEdge != null)
            {
                Point tmp = v.firstEdge;
                while (tmp != null)
                {
                    // 耍流氓
                    if(tmp.adjvex.data.ToString() == targetId.ToString())
                    {
                        return true;
                    }

                    s += tmp.adjvex.data.ToString();
                    tmp = tmp.next;
                }
            }

            return false;

        }

        public void DFSTraverse()//深度优先搜索
        {
            InitVisited();//将visited标志全部置为false
            DFS(items[0]);//从第一个顶点开始遍历
        }
        private void DFS(Vertex<T> v)//使用递归进行深度优先遍历
        {
            v.visited = true;//将访问标志设为true
            Console.WriteLine(v.data + " ");//访问
            Point node = v.firstEdge;
            while (node != null)//访问此顶点的所有邻节点
            {
                //如果邻节点未被访问，则递归访问它的边
                if (!node.adjvex.visited)
                {
                    DFS(node.adjvex);//递归
                }
                node = node.next;//访问下一个节点
            }
        }
        private void InitVisited()//初始化visited标志
        {
            foreach (Vertex<T> v in items)
            {
                v.visited = false;//全部设为false
            }
        }
        //广度优先搜索遍历
        public void BFSTraverse()
        {
            InitVisited();//
            BFS(items[0]);//从第一个顶点开始遍历
        }
        private void BFS(Vertex<T> v)//使用队列进行广度优先搜索
        {
            //创建一个队列
            Queue<Vertex<T>> queue = new Queue<Vertex<T>>();
            Console.WriteLine(v.data + " ");
            v.visited = true;
            queue.Enqueue(v);
            while (queue.Count > 0)
            {
                Vertex<T> w = queue.Dequeue();
                Point node = w.firstEdge;
                while (node != null)
                {
                    if (!node.adjvex.visited)
                    {
                        Console.WriteLine(node.adjvex.data + " ");//访问
                        node.adjvex.visited = true;//设置访问标志
                        queue.Enqueue(node.adjvex);//进队
                    }
                    node = node.next;//访问下一个邻节点
                }
            }
        }
        //非连通图的遍历
        public void DFSTraverse2()//非连通图深度优先遍历
        {
            InitVisited();
            foreach (Vertex<T> v in items)
            {
                if (!v.visited)
                {
                    DFS(v);
                }
            }
        }
        public void BFSTraverse2()//非连通图广度优先遍历
        {
            InitVisited();
            foreach (Vertex<T> v in items)
            {
                if (!v.visited)
                {
                    BFS(v);
                }
            }
        }
        //嵌套类表示链表中的表节点
        public class Point
        {
            public Vertex<T> adjvex;//邻接点域
            public Point next;//下一个邻接点指针域
            public Point(Vertex<T> value)
            {
                adjvex = value;
            }
        }

        //嵌套类表示存放数组中的表头节点
        public class Vertex<TValue>
        {         
            public TValue data;//数据
            public Point firstEdge;//邻接链表头指针
            public Boolean visited;//访问标志用于遍历
            public Vertex(TValue value)
            {
                data = value;
            }
        }
    }
}

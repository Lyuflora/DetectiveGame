using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Dec
{
    public class AdjacencyList<T>
    {
        [SerializeField]List<Vertex<T>> items;//ͼ�Ķ��㼯��
        public AdjacencyList() : this(10) { }//���췽��
        public AdjacencyList(int capacity)//��ָ�����������й���
        {
            items = new List<Vertex<T>>(capacity);
        }
        public void AddVertex(T item)//���һ���ڵ�
        {
            if (Contains(item))
            {

                throw new ArgumentException("�������ظ��ڵ㣡" + item);
            }
            items.Add(new Vertex<T>(item));
        }
        public void AddEdge(T from, T to)//��������
        {
            Vertex<T> fromVer = Find(from);//�ҵ���ʼ�ڵ�
            if (fromVer == null)
            {
                throw new ArgumentException("ͷ�ڵ㲢�����ڣ�");
            }
            Vertex<T> toVer = Find(to);//�ҵ������ڵ�
            if (toVer == null)
            {
                throw new ArgumentException("β�ڵ㲢�����ڣ�");
            }
            //����������ڵ㶼���¼�ıߵ���Ϣ
            AddDirectedEdge(fromVer, toVer);
            AddDirectedEdge(toVer, fromVer);
        }
        public bool Contains(T item)//��ͼ���Ƿ����ĳ��
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
        private Vertex<T> Find(T item)//����ָ�������
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
        //��������
        private void AddDirectedEdge(Vertex<T> fromVer, Vertex<T> toVer)
        {
            if (fromVer.firstEdge == null)//���ڽӵ��ʱ��
            {
                fromVer.firstEdge = new Point(toVer);
            }
            else
            {
                Point tmp, node = fromVer.firstEdge;
                do
                {
                    //����Ƿ�������ظ���
                    if (node.adjvex.data.Equals(toVer.data))
                    {
                        Debug.Log("������ظ��ıߣ�");
                        PenTool.m_Instance.ResetPenTool();
                        return;
                        // throw new ArgumentException("������ظ��ıߣ�");
                    }
                    tmp = node;
                    node = node.next;
                } while (node != null);
                tmp.next = new Point(toVer);//��ӵ�����ĩβ��
            }
        }
        public override string ToString()//�����ڲ���
        {
            //��ӡÿ������������ڽӵ�
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
                    // ˣ��å
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

        public void DFSTraverse()//�����������
        {
            InitVisited();//��visited��־ȫ����Ϊfalse
            DFS(items[0]);//�ӵ�һ�����㿪ʼ����
        }
        private void DFS(Vertex<T> v)//ʹ�õݹ����������ȱ���
        {
            v.visited = true;//�����ʱ�־��Ϊtrue
            Console.WriteLine(v.data + " ");//����
            Point node = v.firstEdge;
            while (node != null)//���ʴ˶���������ڽڵ�
            {
                //����ڽڵ�δ�����ʣ���ݹ�������ı�
                if (!node.adjvex.visited)
                {
                    DFS(node.adjvex);//�ݹ�
                }
                node = node.next;//������һ���ڵ�
            }
        }
        private void InitVisited()//��ʼ��visited��־
        {
            foreach (Vertex<T> v in items)
            {
                v.visited = false;//ȫ����Ϊfalse
            }
        }
        //���������������
        public void BFSTraverse()
        {
            InitVisited();//
            BFS(items[0]);//�ӵ�һ�����㿪ʼ����
        }
        private void BFS(Vertex<T> v)//ʹ�ö��н��й����������
        {
            //����һ������
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
                        Console.WriteLine(node.adjvex.data + " ");//����
                        node.adjvex.visited = true;//���÷��ʱ�־
                        queue.Enqueue(node.adjvex);//����
                    }
                    node = node.next;//������һ���ڽڵ�
                }
            }
        }
        //����ͨͼ�ı���
        public void DFSTraverse2()//����ͨͼ������ȱ���
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
        public void BFSTraverse2()//����ͨͼ������ȱ���
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
        //Ƕ�����ʾ�����еı�ڵ�
        public class Point
        {
            public Vertex<T> adjvex;//�ڽӵ���
            public Point next;//��һ���ڽӵ�ָ����
            public Point(Vertex<T> value)
            {
                adjvex = value;
            }
        }

        //Ƕ�����ʾ��������еı�ͷ�ڵ�
        public class Vertex<TValue>
        {         
            public TValue data;//����
            public Point firstEdge;//�ڽ�����ͷָ��
            public Boolean visited;//���ʱ�־���ڱ���
            public Vertex(TValue value)
            {
                data = value;
            }
        }
    }
}

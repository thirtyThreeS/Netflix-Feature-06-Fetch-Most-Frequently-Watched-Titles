using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fetch_Most_Frequently_Watched_Titles
{
    public class LinkedListNode
    {
        public int key;
        public int val;
        public int freq;
        public LinkedListNode? next;
        public LinkedListNode? prev;

        public LinkedListNode(int key, int val, int freq)
        {
            this.key = key;
            this.val = val;
            this.freq = freq;
            this.next = null;
            this.prev = null;
        }
    }

    public class MyLinkedList
    {
        public LinkedListNode? head;
        public LinkedListNode? tail;
        public MyLinkedList()
        {
            this.head = null;
            this.tail = null;
        }

        public void Append(LinkedListNode node)
        {
            node.next = null;
            node.prev = null;
            if (this.head == null) this.head = node;

            else
            {
                this.tail.next = node;
                node.prev = this.tail;
            }
            this.tail = node;
        }

        public void DeleteNode(LinkedListNode node)
        {
            if (node.prev != null) node.prev.next = node.next;

            else this.head = node.next;

            if (node.next != null) node.next.prev = node.prev;

            else this.tail = node.prev;

            node.next = null;
            node.prev = null;
        }
    }
}
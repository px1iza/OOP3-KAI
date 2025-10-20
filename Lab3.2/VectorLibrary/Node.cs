using System;

namespace VectorLibrary
{
    public class Node<T> where T : class
    {
        public T Data { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }

        public Node(T data)
        {
            Data = data;
        }
    }
}
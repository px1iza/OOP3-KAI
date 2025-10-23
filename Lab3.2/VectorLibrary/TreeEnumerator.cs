using System;
using System.Collections;
using System.Collections.Generic;

namespace VectorLibrary
{
    public class TreeEnumerator<T> : IEnumerator<T> where T : class
    {
        private readonly List<T> _elements;
        private int _position = -1;

        public TreeEnumerator(Node<T> root)
        {
            _elements = new List<T>();
            Preorder(root);
        }

        private void Preorder(Node<T> node)
        {
            if (node == null) return;

            _elements.Add(node.Data);
            Preorder(node.Left);
            Preorder(node.Right);
        }

        public T Current => _elements[_position];
        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            _position++;
            return _position < _elements.Count;
        }

        public void Reset() => _position = -1;

        public void Dispose() { }
    }
}
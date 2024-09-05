using DataStructures_test.Constants;

namespace DataStructures_test.DataStructures
{
    internal class BSTree<T> where T : IComparable
    {
        class Node(T value)
        {
            public T Value { get; set; } = value;
            public Node? Right { get; set; }
            public Node? Left { get; set; }
        }
        private Node? root;

        public void Insert(T value)
        {
            root = BSTree<T>.Insert(value, root);
        }

        private static Node Insert(T value, Node? currentNode)
        {
            if (currentNode == null)
            {
                currentNode = new Node(value);
                return currentNode;
            }
            else if (currentNode.Value.CompareTo(value) > 0)
            {
                currentNode.Left = BSTree<T>.Insert(value, currentNode.Left);
            }
            else if(currentNode.Value.CompareTo(value) < 0)
            {
                currentNode.Right = BSTree<T>.Insert(value, currentNode.Right);
            }
            return currentNode;
        }

        public IEnumerable<T> PreOrder => GetPreOrderRecursion(root);

        private static IEnumerable<T> GetPreOrderRecursion(Node? node)
        {
            if (node == null) return [];
            var leftSideList = BSTree<T>.GetPreOrderRecursion(node.Left);
            var rightSideList = BSTree<T>.GetPreOrderRecursion(node.Right);
            var currentList = new List<T> { node.Value };
            return [.. currentList, .. leftSideList, .. rightSideList];
        }

        public IEnumerable<T> InOrder => GetInOrderRecursion(root);

        private static IEnumerable<T> GetInOrderRecursion(Node? node)
        {
            if (node == null) return [];
            var leftSideList = BSTree<T>.GetInOrderRecursion(node.Left).ToList();
            var rightSideList = BSTree<T>.GetInOrderRecursion(node.Right).ToList();
            var currentList = new List<T> { node.Value };
            return [.. leftSideList, .. currentList, .. rightSideList];
        }

        public T? SearchDefenseByThreatSeverity(int severity) => SearchPreOrder(root, severity);

        private static T? SearchPreOrder(Node? node, int severity)
        {
            if (node == null) return default;
            int currentCompareTo = node.Value.CompareTo(severity);
            T? res = currentCompareTo == 0 ? node.Value : default;
            if(res == null && currentCompareTo < 0) res = BSTree<T>.SearchPreOrder(node.Right, severity);
            if(res == null && currentCompareTo > 0) res = BSTree<T>.SearchPreOrder(node.Left, severity);
            return res;
        }

        public void PrintPreOrder()
        {
            if (root == null) 
            { 
                Console.WriteLine("tree is empty");
                return;
            }
            Console.WriteLine("Root: " + root.Value);
            BSTree<T>.PrintPreOrderRecursion(root.Left, ConstantsClass.SHIFT, ConstantsClass.LEFT_TITLE);
            BSTree<T>.PrintPreOrderRecursion(root.Right, ConstantsClass.SHIFT, ConstantsClass.RIGHT_TITLE);
        }
        private static void PrintPreOrderRecursion(Node? node, string shift, string title)
        {
            if (node == null) return;
            Console.WriteLine(shift + title + node.Value);
            BSTree<T>.PrintPreOrderRecursion(node.Left, shift + ConstantsClass.SHIFT, ConstantsClass.LEFT_TITLE);
            BSTree<T>.PrintPreOrderRecursion(node.Right, shift + ConstantsClass.SHIFT, ConstantsClass.RIGHT_TITLE);
        }

        public void PrintInOrder()
        {
            foreach (var item in InOrder)
            {
                Console.WriteLine(item);
            };
        }

        internal T? GetMinValue()
        {
            return root != null ? BSTree<T>.GetMinValue(root) : default;
        }
        private static T GetMinValue(Node node)
        {
            return node.Left != null ? BSTree<T>.GetMinValue(node.Left) : node.Value;
        }
        public void BalanceTree()
        {
            T[] inOrderList = GetInOrderRecursion(root).ToArray();
            root = GetBalanceNode(inOrderList);
            Console.WriteLine();
        }
        private static Node? GetBalanceNode(T[] inOrderList)
        {
            if(inOrderList.Length == 0) return null;
            int middleIndex = (inOrderList.Length - 1) / 2;
            T middle = inOrderList[middleIndex];
            T[] leftPart = inOrderList.Take(middleIndex).ToArray();
            T[] rightPart = inOrderList.Skip(middleIndex + 1).ToArray();
            Node newNode = new(middle)
            {
                Left = BSTree<T>.GetBalanceNode(leftPart),
                Right = BSTree<T>.GetBalanceNode(rightPart)
            };
            return newNode;
        }
    }
}


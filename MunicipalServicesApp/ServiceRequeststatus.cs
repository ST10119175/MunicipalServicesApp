using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServicesApp
{
    public class ServiceRequeststatus
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime SubmissionDate { get; set; }
        public int Priority { get; set; }

        public int CompareTo(ServiceRequeststatus other)
        {
            return this.Priority.CompareTo(other.Priority);
        }
    }

    // AVL Tree implementation for efficient request tracking
    public class AVLNode
    {
        public ServiceRequeststatus Data;
        public AVLNode Left, Right;
        public int Height;

        public AVLNode(ServiceRequeststatus data)
        {
            Data = data;
            Height = 1;
        }
    }

    public class AVLTree
    {
        private AVLNode root;

        private int Height(AVLNode node)
        {
            return node == null ? 0 : node.Height;
        }

        private int GetBalance(AVLNode node)
        {
            return node == null ? 0 : Height(node.Left) - Height(node.Right);
        }

        private AVLNode RightRotate(AVLNode y)
        {
            AVLNode x = y.Left;
            AVLNode T2 = x.Right;

            x.Right = y;
            y.Left = T2;

            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;

            return x;
        }

        private AVLNode LeftRotate(AVLNode x)
        {
            AVLNode y = x.Right;
            AVLNode T2 = y.Left;

            y.Left = x;
            x.Right = T2;

            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;

            return y;
        }

        public void Insert(ServiceRequeststatus request)
        {
            root = InsertRec(root, request);
        }

        private AVLNode InsertRec(AVLNode node, ServiceRequeststatus request)
        {
            if (node == null)
                return new AVLNode(request);

            if (request.CompareTo(node.Data) < 0)
                node.Left = InsertRec(node.Left, request);
            else if (request.CompareTo(node.Data) > 0)
                node.Right = InsertRec(node.Right, request);
            else
                return node;

            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));

            int balance = GetBalance(node);

            // Left Left Case
            if (balance > 1 && request.CompareTo(node.Left.Data) < 0)
                return RightRotate(node);

            // Right Right Case
            if (balance < -1 && request.CompareTo(node.Right.Data) > 0)
                return LeftRotate(node);

            // Left Right Case
            if (balance > 1 && request.CompareTo(node.Left.Data) > 0)
            {
                node.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }

            // Right Left Case
            if (balance < -1 && request.CompareTo(node.Right.Data) < 0)
            {
                node.Right = RightRotate(node.Right);
                return LeftRotate(node);
            }

            return node;
        }


        // Find requests within a priority range
        public List<ServiceRequeststatus> FindRequestsByPriorityRange(int minPriority, int maxPriority)
        {
            var result = new List<ServiceRequeststatus>();
            FindRequestsByPriorityRangeRec(root, minPriority, maxPriority, result);
            return result;
        }

        private void FindRequestsByPriorityRangeRec(AVLNode node, int minPriority, int maxPriority, List<ServiceRequeststatus> result)
        {
            if (node == null) return;

            // If current node's priority is in range, add to results
            if (node.Data.Priority >= minPriority && node.Data.Priority <= maxPriority)
            {
                result.Add(node.Data);
            }

            // Recursively search left and right subtrees
            if (node.Data.Priority > minPriority)
                FindRequestsByPriorityRangeRec(node.Left, minPriority, maxPriority, result);

            if (node.Data.Priority < maxPriority)
                FindRequestsByPriorityRangeRec(node.Right, minPriority, maxPriority, result);
        }

        // Delete a request from the AVL Tree
        public void Delete(ServiceRequeststatus request)
        {
            root = DeleteRec(root, request);
        }

        private AVLNode DeleteRec(AVLNode node, ServiceRequeststatus request)
        {
            // Standard BST delete
            if (node == null) return null;

            if (request.CompareTo(node.Data) < 0)
                node.Left = DeleteRec(node.Left, request);
            else if (request.CompareTo(node.Data) > 0)
                node.Right = DeleteRec(node.Right, request);
            else
            {
                // Node with only one child or no child
                if (node.Left == null)
                    return node.Right;
                else if (node.Right == null)
                    return node.Left;

                // Node with two children: Get the inorder successor
                node.Data = MinValue(node.Right);
                node.Right = DeleteRec(node.Right, node.Data);
            }

            // Update height and balance
            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
            int balance = GetBalance(node);

            // Rebalance the tree
            // Left Left Case
            if (balance > 1 && GetBalance(node.Left) >= 0)
                return RightRotate(node);

            // Left Right Case
            if (balance > 1 && GetBalance(node.Left) < 0)
            {
                node.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }

            // Right Right Case
            if (balance < -1 && GetBalance(node.Right) <= 0)
                return LeftRotate(node);

            // Right Left Case
            if (balance < -1 && GetBalance(node.Right) > 0)
            {
                node.Right = RightRotate(node.Right);
                return LeftRotate(node);
            }

            return node;
        }

        private ServiceRequeststatus MinValue(AVLNode node)
        {
            AVLNode current = node;
            while (current.Left != null)
                current = current.Left;
            return current.Data;
        }
    }
}

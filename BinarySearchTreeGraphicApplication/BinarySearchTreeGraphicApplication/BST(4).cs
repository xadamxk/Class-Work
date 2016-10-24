using System;
using System.Collections.Generic;

/* Delete leaf: make leaf null
 * Delete node w/ child, replace with it's only child
 * Delete root, move minimum of larger tree, or maximum of smaller (left) tree
 */
namespace Tree
{
 
    //definition of node in a binary search tree
    class BNode<T>
    {
        private T data;
        public BNode<T> left, right;//left child and right child

        public BNode(T item)
        {
            data = item;
            left = null;
            right = null;
        }

        public BNode(T item, BNode<T> leftNode, BNode<T> rightNode)
        {
            data = item;
            left = leftNode;
            right = rightNode;
        }

        public void show()
        {
            Console.Write(data + " ");
        }

        //return data value
        public T getValue()
        {
            return data;
        }

        //set data value
        public void setValue(T newValue)
        {
            data = newValue;
        }

    }


    //definition of a binary search tree
    class HBinarySearchTree<T>
    {

        private BNode<T> root;	//root of the tree

        public BNode<T> GetRoot()
        {
            return root;
        }

        public HBinarySearchTree() // default constructor
        {
            root = null;
        }

        public HBinarySearchTree(BNode<T> rootNode) // constructor
        {
            root = rootNode;
        }


        private int compare(T x, T y)
        {

            return Comparer<T>.Default.Compare(x, y);
        }


        /******************************
        *
        * FindElement/Search
        *
        ******************************/
        public BNode<T> findElement(T tTarget)
        {
            return findElement(root, tTarget);
        }

 
        //find target element with given root node
        private BNode<T> findElement(BNode<T> v, T tTarget)
        {
            if (v == null) //there is no such a node in the tree
                return null;
            else if (compare(tTarget, v.getValue()) == 0)//find it
            {
                return v;
            }
            else if (compare(tTarget, v.getValue()) < 0)//go to left
            {
                return findElement(v.left, tTarget );

            }
            else
            {
                return findElement(v.right, tTarget );//go to right
            }

        }

        /*************************
        *
        *   InsertElement
        *
        *************************/
        public void insertElement(T tData)
        {
            root = insertElement(tData, root);
        }
        private BNode<T> insertElement(T tData, BNode<T> v)
        {
            if (v == null)
                return new BNode<T>(tData);
            else if (compare(tData, v.getValue()) == 0)
            {
                return v;
            }
            else if (compare(tData, v.getValue()) < 0)
            {
                v.left = insertElement(tData, v.left);
                return v;
            }
            else
            {
                v.right = insertElement(tData, v.right);
                return v;
            }

        }
        /************************
        *
        * Delete
        *
        *************************/
        public void deleteElement(T tDelete)
        {
            root = deleteElement(tDelete, root);
        }
        private BNode<T> deleteElement(T tDelete, BNode<T> v)
        {
            if (v == null) return null;

            if (compare(tDelete, v.getValue()) < 0)
            {
                v.left = deleteElement(tDelete, v.left);
            }
            else if (compare(tDelete, v.getValue()) > 0)
            {
                v.right = deleteElement(tDelete, v.right);
            }
            else
            {
                //one child
                if (v.right == null)
                    return v.left;
                else if (v.left == null)
                    return v.right;
                // two children
                else
                {
                    //get left-most node in the right subtree and set value of v
                    T leftNost = GetLeftNode(v.right);
                    v.setValue(leftNost);
                    //delete the left-most node in the right subtree
                    v.right = deleteElement(leftNost, v.right);
                }
            }
            return v;
        }
        private T GetLeftNode(BNode<T> v)
        {
            while (v.left != null) v = v.left;

            return v.getValue();
        }


        // PreOrder traversal
        public void preOrder(BNode<T> root)
        {
            if (root != null)
            {
                root.show();
                preOrder(root.left);
                preOrder(root.right);
            }

        }

        // InOrder traversal
        public void inOrder(BNode<T> root)
        {

            if (root != null)
            {

                inOrder(root.left);
                root.show();
                inOrder(root.right);
            }

        }

        //PostOrder traversal
        public void postOrder(BNode<T> root)
        {

            if (root != null)
            {

                postOrder(root.left);
                postOrder(root.right);
                root.show();
            }
        }

        //size of the tree
        public int size(BNode<T> root)
        {
            if (root == null)
            {
                return 0;
            }
            else
            {
                return 1 + size(root.left) + size(root.right);
            }
        }

        //height of the tree
        public int height(BNode<T> root)
        {
            if (root == null)
            {
                return -1;
            }
            else
            {
                int hl = height(root.left);
                int hr = height(root.right);

                return 1 + Math.Max(hl, hr);
            }
           
        }

        //print depth of each node
        public void printDepth(BNode<T> root, int depth)
        {
            // Tree is already in order (in-order)
            // Print selected number and it's depth (count from top)
            //Not empty tree
            if (root != null)
            {
                // print left child and its depth
                printDepth(root.left,depth+1);

                // print root and its depth
                Console.WriteLine("Node " + root.getValue() + " Depth is: " + depth);

                // print right child and its depth
                printDepth(root.right,depth+1);


            }

        }


    }

}

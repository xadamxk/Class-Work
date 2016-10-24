using System;
using System.Linq;
/* Name: Adam K
 * Class: CS 311
 * Date: 1/26/2016
 */


namespace StackExperiment
{
    class StackProgram
    {
        static void Main(string[] args)
        {
            MyListStack myStack = new MyListStack();

            string testString = "56+2*1+";

            // Create string array
            string[] stringItems = new string[testString.Count()];

            // Turn string into string array
            for (int i = 0; i < testString.Length; i++)
            {
                stringItems[i] = testString[i].ToString();
            }

                 //debugging
            //for (int i = 0; i < stringItems.Length; i++)
            //{
            //    Console.Write(stringItems[i]);
            //}

            // Output Format
            Console.WriteLine("Stack Input:");
            Console.WriteLine(testString);
            Console.WriteLine("Press any key to calculate total...\n");
            Console.ReadKey();
            Console.WriteLine("Stack Output:");

            // Run Instance
            int result = EvaluatePostFix(stringItems, myStack);

            // Output Format
            Console.WriteLine("\nTotal: " + result);
            Console.WriteLine("Press any key to close application.");
            Console.ReadKey();


        }

        //Part 2: Complete this method to evaluate Postfix Expressions using MyListStack
        static int EvaluatePostFix(string[] stringItems, MyListStack stack)
        {
            // Cycle through string array for various operations
            for (int i = 0; i < stringItems.Length; i++)
            {
                int temp;
                bool isNumeric = int.TryParse(stringItems[i], out temp);
                if (isNumeric) // if index is a number
                {
                    stack.Push(temp);
                }
                else // if index is an operation
                {
                    int first = stack.Pop();
                    int second = stack.Pop();
                    int total = 0;

                    // Switch for mathematic operations
                    switch (stringItems[i])
                    {
                        case "+":
                            total = first + second;
                            Console.Write("(" + first + " + " + second + ")\n");
                            stack.Push(total);
                            break;
                        case "-":
                            total = first - second;
                            Console.Write("(" + first + " - " + second + ")\n");
                            stack.Push(total);
                            break;
                        case "*":
                            total = first*second;
                            Console.Write("(" + first + " * " + second + ")\n");
                            stack.Push(total);
                            break;
                        case "/":
                            total = first/second;
                            Console.Write("(" + first + " / " + second + ")\n");
                            stack.Push(total);
                            break;
                        case "%":
                            total = first%second;
                            Console.Write("(" + first + " % " + second + ")\n");
                            stack.Push(total);
                            break;
                    } // switch
                } // else
            } // for
            return stack.Pop();
        }
    }

    //definition of each Node of Stack
    public class Node
    {
        public int data;
        public Node next;

        public Node(int item)
        {
            data = item;
            next = null;
        }
    }


    //Part 2: Complete class MyListStack 
    public class MyListStack
    {
        //top element
        private Node top = null;
        // number of elements stored
        private int numberOfNodes = 0;


        //indicates whether no elements are stored
        public bool IsEmpty()
        {
            return top == null;
            // return numberOfNodes == 0;
        }

        //returns the number of elements stored
        public int Size()
        {
            return numberOfNodes;
        }

        //inserts an element
        public void Push(int item)
        {
            if (IsEmpty())
                top = new Node(item);

            else
            {
                Node newnode = new Node(item);
                newnode.next = top;
                top = newnode;
            }
            numberOfNodes++; // add to node count
        }

        //removes and returns the last inserted element
        public int Pop()
        {
            if (IsEmpty())
            {
                Console.WriteLine("Stack is empty!");
                numberOfNodes--; // subtract node count
                return 0;
            }
            else
            {
                int popvalue = top.data;
                top = top.next;
                numberOfNodes--; // subtract node count
                return popvalue;
            }
        }
    }


 }





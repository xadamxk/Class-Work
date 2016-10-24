using System;
using System.Drawing;
using System.Windows.Forms;
using Tree;

namespace BinarySearchTreeGraphicApplication
{
    public partial class Form1 : Form
    {
        // Variables (Objs)
        private Graphics graphics;
        private Pen drawPen;
        private SolidBrush drawBrush;
        private HBinarySearchTree<int> bstTree;

        private Color backGround;

        // Variables (evil)
        private int height = 0;
        private int ySpace = 0;
        private int xSpace = 0; // used for now
        private int[] intArray;
        private int[] intArrayOrdered;

        // Variables (default)
        private Color selectedColor = Color.Black;
        private bool isRun = true;
        private int brushWidth = 2;
        private int diameter = 50;

        // Constants
        private int radius = 25;
        private int cycleCount = -1;
        private Color[] colorSet =
        {
            Color.Black, Color.Blue, Color.Brown, Color.Gray,
            Color.Green, Color.Orange, Color.Pink, Color.Purple,
            Color.Red, Color.White, Color.Yellow
        };
        private readonly int[] cirlceSizeArray = {10, 26, 50, 76, 100};

        

        public Form1()
        {
            InitializeComponent();
            // Create Graphics
            graphics = this.CreateGraphics();
            // Create Brush
            drawBrush = new SolidBrush(Color.Azure);
            // Create Pen
            drawPen = new Pen(selectedColor , 3);
            
            // Default Values
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 2;
            radioButton2.Checked = true;
            isRun = false;
            textBox1.Text = "11, 6, 4, 5, 8, 10, 19, 17, 43, 31, 49";

            this.SetClientSizeCore(730,500);
            

        }

        private void BuildTree()
        {
            bstTree = new HBinarySearchTree<int>();

            foreach (int key in intArray)
                bstTree.insertElement(key);
        }

        private void SetColors()
        {
            // Clear drawing
            ClearDrawing();

            // Cycle through default color array
            for (int i = 0; i < comboBox1.Items.Count; i++)
            {
                // Check for match
                if (comboBox1.SelectedIndex == i)
                {
                    // Set PenColor & Selected Color
                    drawPen = new Pen(colorSet[i], 3);
                    selectedColor = colorSet[i];
                }
            }

        }

        private void ClearDrawing()
        {
            // Clear previous & Set default background
            graphics.Clear(DefaultBackColor);
            this.BackColor = default(Color);
        }

        private void PrepDrawTree()
        {
            
            // Make input an Array
            bool isEmpty = false;
            string inputs = textBox1.Text;
            if (inputs == "")
                isEmpty = true;
            string[] values = inputs.Split(',');
            int[] intArrayLocal = new int[values.Length];

            // Don't run if no inputs
            if (intArrayLocal.Length > 0 && !isEmpty)
            {
                // Make string -> IntArray
                for (int i = 0; i < values.Length; i++)
                    intArrayLocal[i] = Convert.ToInt32(values[i]);
                intArray = intArrayLocal;

                // Order intArray
                intArrayOrdered = new int[intArray.Length];
                Array.Copy(intArray, intArrayOrdered, intArray.Length);
                Array.Sort(intArrayOrdered);

                // Build Tree
                BuildTree();
                height = bstTree.height(bstTree.GetRoot());

                // Set Client Variables
                int posx = (ClientSize.Width/2) - radius;
                int posy = 100;

                // Y Spacing
                double posYSpaceTemp = (ClientSize.Width - diameter)/((Math.Pow(2, (height + 1))));
                int posYSpace = Convert.ToInt32(posYSpaceTemp);
                ySpace = posYSpace;

                // XSpacing - If more than 1 level
                if (height > 1)
                {
                    // X Spacing ------------------------------------------------------------------------------------
                    //static = ((this.ClientSize.Height - 100) - ((height - 1) * diameter) + diameter) / height;
                    //dynamic = double posXSpaceTemp = ((this.ClientSize.Height - 100) - ((height - 1) * diameter) + (((height - 3) * diameter) + diameter)) / height;
                    // This math took 2 hours :(
                    double posXSpaceTemp = ((this.ClientSize.Height - 100) - ((height - 1) * diameter) + (((height - 3) * diameter) + diameter)) / height;
                    int posXSpace = Convert.ToInt32(posXSpaceTemp);
                    xSpace = posXSpace;
                    // X Spacing -----------------------------------------------------------------------------------
                }

                // RECURSION BABY!
                DrawTree(bstTree.GetRoot(), 0, posx, posy);

                // Reset count
                cycleCount = -1;
            }
        }

        // Attempting to reduce redundancy
        //private void DrawTreeRoot(int posX, int posY)
        //{
        //    // ROOT
        //    // draw root circle
        //    cycleCount++; // cycle through intArray
        //    graphics.DrawEllipse(drawPen, posX, posY, diameter, diameter);
        //}

        //private void DrawTreeLetters(int posX, int posY)
        //{
        //    // LETTERS
        //    // Create Font, Brush, & Rectangle Shape
        //    Font drawFont = new Font("Ariel", diameter / 2); // Scales text according to diameter
        //    drawBrush = new SolidBrush(selectedColor);
        //    RectangleF drawRect = new RectangleF(posX, posY, diameter, diameter);
        //    //Set Format of string
        //    StringFormat drawFormat = new StringFormat();
        //    drawFormat.Alignment = StringAlignment.Center;
        //    // Draw String
        //    graphics.DrawString(intArrayOrdered[cycleCount].ToString(), drawFont, drawBrush, drawRect, drawFormat);
        //}

        //private void DrawTreeRootLinePoint(int posX, int posY)
        //{
        //    rootPt = new Point(posX + radius, posY + diameter);
        //}

        //private void DrawTreeLeft(BNode<int> root, int depth, int posX, int posY)
        //{
        //    DrawTree(root.left, depth + 1, posX - ((int)Math.Pow(2, height - depth - 1) * ySpace), posY + 100);
        //}

        //private void DrawTreeLeftLinePoint(int depth, int posX, int posY)
        //{
        //    leftPt = new Point((posX - ((int)Math.Pow(2, height - depth - 1) * ySpace)) + radius, posY + 100);
        //}

        //private void DrawTreeLeftRootLine(BNode<int> root)
        //{
        //    if (root.left != null)
        //        graphics.DrawLine(drawPen, leftPt, rootPt);
        //}

        //private void DrawTreeRight(BNode<int> root, int depth, int posX, int posY)
        //{
        //    DrawTree(root.right, depth + 1, posX + ((int)Math.Pow(2, height - depth - 1) * ySpace), posY + 100);
        //}

        //private void DrawTreeRightLinePoint(int depth, int posX, int posY)
        //{
        //    rightPt = new Point((posX + ((int)Math.Pow(2, height - depth - 1) * ySpace)) + radius, posY + 100);
        //}

        //private void DrawTreeRightRootLine(BNode<int> root)
        //{
        //    if (root.right != null)
        //        graphics.DrawLine(drawPen, rightPt, rootPt);
        //}

        // Draw tree -- PreOrder, InOrder, PostOrder

        private void DrawTree(BNode<int> root, int depth, int posX, int posY)
        {
            if (root != null)
            {
                // Pre-Order(RoLR)
                if (radioButton1.Checked)
                {
                    //// ROOT
                    //// draw root circle
                    cycleCount++; // cycle through intArray
                    graphics.DrawEllipse(drawPen, posX, posY, diameter, diameter);
                    //DrawTreeRoot(posX, posY);

                    // LETTERS
                    // Create Font, Brush, & Rectangle Shape
                    Font drawFont = new Font("Ariel", diameter / 2); // Scales text according to diameter
                    drawBrush = new SolidBrush(selectedColor);
                    RectangleF drawRect = new RectangleF(posX, posY, diameter, diameter);
                    //Set Format of string
                    StringFormat drawFormat = new StringFormat();
                    drawFormat.Alignment = StringAlignment.Center;
                    // Draw String
                    graphics.DrawString(intArrayOrdered[cycleCount].ToString(), drawFont, drawBrush, drawRect, drawFormat);
                    //DrawTreeLetters(posX,posY);


                    // get root line point
                    Point rootPt = new Point(posX + radius, posY + diameter);
                    //DrawTreeRootLinePoint(posX, posY);
                    

                    // LEFT
                    //draw left circle
                    DrawTree(root.left, depth + 1, posX - ((int)Math.Pow(2, height - depth - 1) * ySpace), posY + xSpace);
                    //DrawTreeLeft(root, depth, posX, posY);



                    // get left line point (Top center of circle)
                    Point leftPt = new Point((posX - ((int)Math.Pow(2, height - depth - 1) * ySpace)) + radius, posY + xSpace);
                    //DrawTreeLeftLinePoint(depth,posX,posY);

                    // Draw left-root line (if not leaf)
                    if (root.left != null)
                        graphics.DrawLine(drawPen, leftPt, rootPt);
                    //DrawTreeLeftRootLine(root);

                    // RIGHT
                    // draw right circle
                    DrawTree(root.right, depth + 1, posX + ((int)Math.Pow(2, height - depth - 1) * ySpace), posY + xSpace );
                    //DrawTreeRight(root, depth,posX,posY);


                    // get right line point (Top center of circle)
                    Point rightPt = new Point((posX + ((int)Math.Pow(2, height - depth - 1) * ySpace)) + radius, posY + xSpace);
                    //DrawTreeRightLinePoint(depth,posX,posY);

                    // draw right-root line (if not leaf)
                    if (root.right != null)
                        graphics.DrawLine(drawPen, rightPt, rootPt);
                    //DrawTreeRightRootLine(root);
                }

                // In-Order (LRoR)
                else if (radioButton2.Checked)
                {
                    // LEFT
                    //draw left circle
                    DrawTree(root.left, depth + 1, posX - ((int)Math.Pow(2, height - depth - 1) * ySpace), posY + xSpace);
                    // get left line point (Top center of circle)
                    Point leftPt = new Point((posX - ((int)Math.Pow(2, height - depth - 1) * ySpace)) + radius, posY + xSpace);

                    // ROOT
                    // draw root circle
                    cycleCount++; // cycle through intArray
                    graphics.DrawEllipse(drawPen, posX, posY, diameter, diameter);
                    //DrawTreeRoot(posX, posY);

                    // LETTERS
                    // Create Font, Brush, & Rectangle Shape
                    Font drawFont = new Font("Ariel", diameter / 2); // Scales text according to diameter
                    drawBrush = new SolidBrush(selectedColor);
                    RectangleF drawRect = new RectangleF(posX, posY, diameter, diameter);
                    //Set Format of string
                    StringFormat drawFormat = new StringFormat();
                    drawFormat.Alignment = StringAlignment.Center;
                    // Draw String
                    graphics.DrawString(intArrayOrdered[cycleCount].ToString(), drawFont, drawBrush, drawRect, drawFormat);

                    // get root line point
                    Point rootPt = new Point(posX + radius, posY + diameter);
                    // Draw left-root line (if not leaf)
                    if (root.left != null)
                        graphics.DrawLine(drawPen, leftPt, rootPt);

                    // RIGHT
                    // draw right circle
                    DrawTree(root.right, depth + 1, posX + ((int)Math.Pow(2, height - depth - 1) * ySpace), posY + xSpace);
                    // get right line point (Top center of circle)
                    Point rightPt = new Point((posX + ((int)Math.Pow(2, height - depth - 1) * ySpace)) + radius, posY + xSpace);
                    // draw right-root line (if not leaf)
                    if (root.right != null)
                        graphics.DrawLine(drawPen, rightPt, rootPt);
                }

                // Post-Order (LRRo)
                else if (radioButton3.Checked)
                {
                    // LEFT
                    //draw left circle
                    DrawTree(root.left, depth + 1, posX - ((int)Math.Pow(2, height - depth - 1) * ySpace), posY + xSpace);
                    // get left line point (Top center of circle)
                    Point leftPt = new Point((posX - ((int)Math.Pow(2, height - depth - 1) * ySpace)) + radius, posY + xSpace);

                    // RIGHT
                    // draw right circle
                    DrawTree(root.right, depth + 1, posX + ((int)Math.Pow(2, height - depth - 1) * ySpace), posY + xSpace);
                    // get right line point (Top center of circle)
                    Point rightPt = new Point((posX + ((int)Math.Pow(2, height - depth - 1) * ySpace)) + radius, posY + xSpace);

                    // ROOT
                    // draw root circle
                    cycleCount++; // cycle through intArray
                    graphics.DrawEllipse(drawPen, posX, posY, diameter, diameter);
                    //DrawTreeRoot(posX, posY);

                    // LETTERS
                    // Create Font, Brush, & Rectangle Shape
                    Font drawFont = new Font("Ariel", diameter / 2); // Scales text according to diameter
                    drawBrush = new SolidBrush(selectedColor);
                    RectangleF drawRect = new RectangleF(posX, posY, diameter, diameter);
                    //Set Format of string
                    StringFormat drawFormat = new StringFormat();
                    drawFormat.Alignment = StringAlignment.Center;
                    // Draw String
                    graphics.DrawString(intArrayOrdered[cycleCount].ToString(), drawFont, drawBrush, drawRect, drawFormat);

                    // get root line point
                    Point rootPt = new Point(posX + radius, posY + diameter);
                    // Draw left-root line (if not leaf)
                    if (root.left != null)
                        graphics.DrawLine(drawPen, leftPt, rootPt);

                    // draw right-root line (if not leaf)
                    if (root.right != null)
                        graphics.DrawLine(drawPen, rightPt, rootPt);
                }
            
            }
        }


        // Created own method (Didn't use this for now)
        public static Point GetPointOnCircle(Point point1, Point point2, int radius)
        {
            Point Pointref = Point.Subtract(point2, new Size(point1));
            double degrees = Math.Atan2(Pointref.Y, Pointref.X);
            double cosx1 = Math.Cos(degrees);
            double siny1 = Math.Sin(degrees);
            double cosx2 = Math.Cos(degrees + Math.PI);
            double siny2 = Math.Sin(degrees + Math.PI);

            return new Point((int)(cosx1 * (radius) + point1.X), (int)(siny1 * (radius) + point1.Y));
        }


        // Event Handlers
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Don't trigger on run
            if (!isRun)
            {
                // Set Colors
                SetColors();

                // Make Tree
                PrepDrawTree();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Don't trigger on run
            if (!isRun)
            {
                // Clear drawing
                ClearDrawing();
                // Set Width
                brushWidth = comboBox2.SelectedIndex + 1;
                drawPen = new Pen(selectedColor, brushWidth);
                // Make Tree
                PrepDrawTree();
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            // Tooltip
            switch (trackBar1.Value)
            {
                case 0: toolTip1.SetToolTip(trackBar1, "10 pixels");
                    break;
                case 1: toolTip1.SetToolTip(trackBar1, "25 pixels");
                    break;
                case 2: toolTip1.SetToolTip(trackBar1, "50 pixels");
                    break;
                case 3: toolTip1.SetToolTip(trackBar1, "75 pixels");
                    break;
                case 4: toolTip1.SetToolTip(trackBar1, "100 pixels");
                    break;
            }

            // Don't trigger on run
            if (!isRun)
            {
                // Clear drawing
                ClearDrawing();
                // Set Diameter
                for (int i = 0; i < cirlceSizeArray.Length; i++)
                {
                    // Check for match
                    if (trackBar1.Value == i)
                    {
                        // Set Diameter & Radius
                        diameter = cirlceSizeArray[i];
                        radius = diameter/2;
                    }
                }
                // Make Tree
                PrepDrawTree();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (!isRun)
            {
                ClearDrawing();
                PrepDrawTree();
            }
                
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (!isRun)
            {
                ClearDrawing();
                PrepDrawTree();
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (!isRun)
            {
                ClearDrawing();
                PrepDrawTree();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Clear drawing
            ClearDrawing();

            // Set Colors
            SetColors();

            // Make Tree
            PrepDrawTree();


            this.AcceptButton = button1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.AcceptButton = button1;

        }


    }
}

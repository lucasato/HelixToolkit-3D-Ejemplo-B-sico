using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HelixToolkit.Wpf;
using System.Windows.Media.Media3D;
using System.IO;
using System.Diagnostics;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Container
        public Model3DGroup container;

        // 3D Objects
        Model3D ball;
        Model3D board;
        Model3D teapot;

        // Project path
        String path = Directory.GetCurrentDirectory();

        // Current model
        public Model3D model { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            
            //Console.WriteLine(path);

            ball = Display3d(@path + "/../../3DModels/Ball.obj");
            board = Display3d(@path + "/../../3DModels/Board.obj");
            teapot = Display3d(@path + "/../../3DModels/Teapot.obj");
            
            // Add files to container
            container = new Model3DGroup();

            container.Children.Add(ball);
            container.Children.Add(board);
            container.Children.Add(teapot);

            // Center container
            Center();

            // set model
            this.model = container;

            //set datacontext for the sliders and helper
            overall_grid.DataContext = this;
        }

        private Model3D Display3d(string model)
        {
            Model3D device = null;
            try
            {
                //Import 3D model file
                ModelImporter import = new ModelImporter();

                //Load the 3D model file
                device = import.Load(model);
            }
            catch (Exception e)
            {
                // Handle exception in case can not find the 3D model file
                MessageBox.Show("Exception Error : " + e.StackTrace);
            }
            return device;
        }

        private void Center()
        {
            RotateTransform3D myRotateTransform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 90));
            myRotateTransform.CenterX = 0;
            myRotateTransform.CenterY = 0;
            myRotateTransform.CenterZ = 0;
            container.Transform = myRotateTransform;
        }

        private float teapotPosX;

        public float TeaPotPositionX
        {
            get { return teapotPosX; }
            set {
                Console.WriteLine(teapotPosX.ToString());

                TranslateTransform3D transform = new TranslateTransform3D();
                transform.OffsetX = value;
                transform.OffsetY = 10;
                transform.OffsetZ = -5;
                teapotPosX = value;
                teapot.Transform = transform;
            }
        }

    }
}

using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace Tolentino_CoinsActivity
{
    public partial class Form1 : Form
    {
        private List<Bitmap> countCoinsSteps = new List<Bitmap>();
        private const int MAX_STEPS = 10;
        private List<List<Point>> coins;
        private List<int> coinValues;
        private List<double> coinSizes;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            valueLabel.Text = $"Total Value: ";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (coinsPictureBox.Image != null)
            {
                countCoinsSteps.Clear();
                countCoinsSteps.Add(new Bitmap(coinsPictureBox.Image));
                solveCountCoins();

                Bitmap finalImage = countCoinsSteps[MAX_STEPS - 2];
                coinsPictureBox.Image = finalImage;

                int valSum = coinValues.Sum();
                double totalValue = valSum / 100.0;

                valueLabel.Text = $"Total Value: {totalValue} pesos";
            }
            else
            {
                MessageBox.Show("Please load an image first.");
            }
        }

        private void solveCountCoins()
        {
            Bitmap currentImage = countCoinsSteps[0];

            currentImage = BasicDIP.GrayScale(currentImage);
            countCoinsSteps.Add(currentImage);

            currentImage = global::ConvMatrix.GaussianBlur(currentImage);
            countCoinsSteps.Add(currentImage);

            currentImage = global::ConvMatrix.EdgeDetect(currentImage);
            countCoinsSteps.Add(currentImage);

            currentImage = BasicDIP.MedianFilter(currentImage, 3);
            countCoinsSteps.Add(currentImage);

            currentImage = global::ConvMatrix.Dilation(currentImage);
            countCoinsSteps.Add(currentImage);

            currentImage = global::ConvMatrix.Erosion(currentImage);
            countCoinsSteps.Add(currentImage);

            for (int i = 0; i < 4; i++)
            {
                currentImage = BasicDIP.MedianFilter(currentImage, 3);
            }
            countCoinsSteps.Add(currentImage);

            currentImage = BasicDIP.PixelCopy(currentImage);
            countCoinsSteps.Add(currentImage);

            Bitmap prev = currentImage;
            Bitmap contourImage = (Bitmap)countCoinsSteps[0].Clone();

            Tuple<List<List<Point>>, List<int>, List<double>> t = CoinDetect.IdentifyCoins(EdgeTracing.ExtractContours(prev));
            coins = t.Item1;
            coinValues = t.Item2;
            coinSizes = t.Item3;

            coinsTextBox.Clear();

            var groupedCoinValues = coinValues
                .GroupBy(value => value / 100.0)
                .Select(group => new { Denomination = group.Key, Count = group.Count() });

            foreach (var group in groupedCoinValues)
            {
                coinsTextBox.AppendText($"{group.Denomination} pesos - {group.Count} total\r\n");
            }

            foreach (var contour in coins)
            {
                foreach (var point in contour)
                {
                    contourImage.SetPixel(point.X, point.Y, Color.Red);
                }
            }

            countCoinsSteps.Add(contourImage);
        }

        private void loadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            openFileDialog1.Title = "Select an Image";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                coinsPictureBox.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void coinsTextBox_TextChanged(object sender, EventArgs e)
        {
        }
    }
}


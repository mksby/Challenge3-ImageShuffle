using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using RestSharp;

namespace ImageShuffle
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Image<Bgr, byte> shuffledImage;
        private ImageData shuffledData;
        private int dimention;

        Test testCase;

        private void button2_Click(object sender, EventArgs e)
        {
            shuffledData = shuffledImage.Cut(dimention);
            shuffledData.Shuffle();
            shuffledImage = shuffledData.Stick();
            imageBoxShuffled.Image = shuffledImage;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var solver = new Solver();
            var restoredData = solver.RestoreImage(shuffledData, dimention, ref richTextBox1);

            var restoredImage = restoredData.Stick();
            imageBoxRestored.Image = restoredImage;

            var score = testCase.Score(restoredData);

            double max = 4;
            if (dimention == 4) max = 24;
            else if (dimention == 8) max = 112;

            richTextBox1.AppendText("score: " + score + " of " + max + " = " + (score / max).ToString("P") + "\n");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                shuffledImage = new Image<Bgr, byte>(openFileDialog1.FileName);
                shuffledData = shuffledImage.Cut(dimention);

                imageBoxShuffled.Image = shuffledImage;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            testSelector.SelectedIndex = 0;
            testSelector_SelectedIndexChanged(sender, e);
        }

        private void testSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = testSelector.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selected))
            {
                checkTest.Enabled = true;
               
                var filename = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), $"..\\..\\images\\{selected}.bmp");
                shuffledImage = new Image<Bgr, byte>(filename);
                imageBoxShuffled.Image = shuffledImage;
                dimention = int.Parse(selected.Substring(1,1));
                shuffledData = shuffledImage.Cut(dimention);

                testCase = new Test(Answers.GetAnswer(selected), dimention);
            }
          
        }

        private void checkTest_Click(object sender, EventArgs e)
        {
            var solver = new Solver();
            var restoredData = solver.RestoreImage(shuffledData, dimention, ref richTextBox1);

            var restoredImage = restoredData.Stick();
            imageBoxRestored.Image = restoredImage;

            var score = testCase.Score(restoredData);
            double max = 4;
            if (dimention == 4) max = 24;
            else if (dimention == 8) max = 112;

            richTextBox1.AppendText("score: " + score + " of " + max + " = " + (score / max).ToString("P") + "\n");

        }

        private void runAll_Click(object sender, EventArgs e)
        {
            var tests = new List<TestCase>();

            progressBar1.Value = 0;
            int total = 0;
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            foreach (var test in testSelector.Items)
            {
               
                Application.DoEvents();

                var selected = test.ToString();
                labelCurrentTest.Text = selected;

                if (!string.IsNullOrEmpty(selected))
                {
                    var filename = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), $"..\\..\\images\\{selected}.bmp");
                    shuffledImage = new Image<Bgr, byte>(filename);
                    imageBoxShuffled.Image = shuffledImage;

                    dimention = int.Parse(selected.Substring(1, 1));
                    var shuffledData = shuffledImage.Cut(dimention);

                    testCase = new Test(Answers.GetAnswer(selected), dimention);

                    var solver = new Solver();
                    var restoredData = solver.RestoreImage(shuffledData, dimention, ref richTextBox1);

                    var restoredImage = restoredData.Stick();
                    imageBoxRestored.Image = restoredImage;

                    var score = testCase.Score(restoredData);
                    total += score;

                    double max = 4;
                    if (dimention == 4) max = 24;
                    else if (dimention == 8) max = 112;

                    richTextBox1.AppendText("test "+selected+" - score: " + score + " of "+max +" = "+(score/max).ToString("P")+"\n");

                    tests.Add(new TestCase{Case = selected, Score = score/max });

                    progressBar1.Value++;
                }
            }

            stopwatch.Stop();
            richTextBox1.AppendText("Total score: " + total + " of 420 = " + (total / 420.0).ToString("P") + "\n");
            richTextBox1.AppendText(string.Format("Time elapsed: {0:hh\\:mm\\:ss}", stopwatch.Elapsed));
            progressBar1.Value = 0;


            var client = new RestClient(ConfigurationManager.AppSettings.Get("Leaderboard"));
            var request = new RestRequest("Home/Submit",Method.GET);
            request.AddParameter("Team", ConfigurationManager.AppSettings.Get("Team"));
            request.AddParameter("Score", total / 420.0);
            request.AddParameter("Tests", JsonConvert.SerializeObject(tests));
            var result = client.GetAsync<bool>(request);
        }


    }

    public class TestCase
    {
        public string Case { get; set; }
        public double Score { get; set; }
    }
}

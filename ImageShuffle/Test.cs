using System;
using System.Collections.Generic;

namespace ImageShuffle
{
    public static class Answers
    {
        static readonly int[,] a2_1 = 
        {
           {1,3},
           {2,0}
        };

        static readonly int[,] a2_2 =
        {
            {0,2},
            {3,1}
        };

        static readonly int[,] a2_3 =
        {
            {3,1},
            {0,2}
        };

        static readonly int[,] a4_1 =
        {
            {8,13,10,12},
            {15,5,7,6},
            {2,0,4,3},
            {9,1,11,14}
        };

        static readonly int[,] a4_2 =
        {
            {4,8,12,1},
            {6,10,3,15},
            {14,2,13,7},
            {9,0,11,5}
        };

        static readonly int[,] a4_3 =
       {
            {5,7,12,8},
            {14,2,9,0},
            {4,1,15,13},
            {3,11,10,6}
        };

        static readonly int[,] a8_1 =
        {
            {17,21,39,30,5,10,35,9},
            {4,52,26,47,34,1,24,3},
            {2,54,0,16,57,28,49,56},
            {38,33,25,58,62,15,23,46},
            {6,12,13,50,43,36,22,11},
            {53,48,29,42,20,63,8,59},
            {45,40,27,55,31,60,41,14},
            {37,44,51,61,19,32,18,7}
        };

        static readonly int[,] a8_2 =
        {
            {0,58,45,63,17,51,53,27},
            {8,20,36,48,42,18,2,25},
            {49,34,62,19,4,32,35,47},
            {30,23,26,52,41,9,21,33},
            {10,37,5,50,60,61,11,59},
            {29,15,22,24,14,12,16,31},
            {46,39,57,7,13,38,44,6},
            {40,28,55,54,3,56,1,43}
        };

        static readonly int[,] a8_3 =
        {
            {23,59,48,4,56,10,2,26},
            {5,57,15,46,34,38,42,62},
            {11,43,30,16,1,14,63,19},
            {27,49,37,17,0,52,53,28},
            {35,39,22,24,54,41,45,44},
            {20,3,8,47,18,51,21,25},
            {7,31,32,33,13,55,50,9},
            {6,36,40,61,60,29,58,12}
        };

        public static int[,] GetAnswer(string testCase)
        {
            switch (testCase)
            {
                case "a2_1": return a2_1;
                case "a2_2": return a2_2;
                case "a2_3": return a2_3;
                case "a4_1": return a4_1;
                case "a4_2": return a4_2;
                case "a4_3": return a4_3;
                case "a8_1": return a8_1;
                case "a8_2": return a8_2;
                case "a8_3": return a8_3;
                default: throw new Exception("undefined test case");
            }
        }
    }

    public class Test
    {
        public Test()
        {

        }

        public Test(int[,] answer, int dimention)
        {
            Dimention = dimention;
            Answer = answer;
        }

        public int[,] Answer { get; set; }
        public int Dimention { get; set; }

        List<Tuple<int,int>> TopAnswers  = new List<Tuple<int, int>>();
        List<Tuple<int,int>> LeftAnswers = new List<Tuple<int, int>>();

        public int Score(ImageData imageData)
        {
            TopAnswers = new List<Tuple<int, int>>();
            LeftAnswers = new List<Tuple<int, int>>();

            int score = 0;
            var positions = GetPositions(imageData.Pieces, Dimention);
            var answer = Answer;
            for (var i = 0; i < Dimention; i++)
            {
                for (var j = 0; j < Dimention; j++)
                {
                    if (j < Dimention - 1)
                    {
                        if (CorrectLeft(positions[i, j], positions[i, j + 1], ref answer))
                        {
                            score++;
                        }
                    }

                    if (i < Dimention - 1)
                    {
                        if (CorrectBottom(positions[i, j], positions[i + 1, j], ref answer))
                        {
                            score++;
                        }
                    }

                }
            }
            return score;
        }

        bool CorrectLeft(int position, int actual, ref int[,] answer)
        {
            var coords = CoordinatesOf(answer, position);
            if (coords.Item2 < answer.GetLength(1) - 1)
            {
                if (answer[coords.Item1, coords.Item2 + 1] == actual)
                {
                    if (!LeftAnswers.Contains(new Tuple<int, int>(coords.Item1, coords.Item2 + 1)))
                    {
                        LeftAnswers.Add(new Tuple<int, int>(coords.Item1, coords.Item2 + 1));
                        return true;
                    }
                }
                    
            }
            return false;
        }
        bool CorrectBottom(int position, int actual, ref int[,] answer)
        {
            var coords = CoordinatesOf(answer, position);
            if (coords.Item1 < answer.GetLength(0) - 1)
            {
                if (answer[coords.Item1+1, coords.Item2] == actual)
                    if (!TopAnswers.Contains(new Tuple<int, int>(coords.Item1 + 1, coords.Item2)))
                    {
                        TopAnswers.Add(new Tuple<int, int>(coords.Item1 + 1, coords.Item2));
                        return true;
                    }
            }
            return false;
        }

        static Tuple<int, int> CoordinatesOf<T>(T[,] matrix, T value)
        {
            int w = matrix.GetLength(0); // width
            int h = matrix.GetLength(1); // height

            for (int x = 0; x < w; ++x)
            {
                for (int y = 0; y < h; ++y)
                {
                    if (matrix[x, y].Equals(value))
                        return Tuple.Create(x, y);
                }
            }

            return Tuple.Create(-1, -1);
        }

        int[,] GetPositions(ImagePiece[,] pieces, int dimention)
        {
            int[,] result = new int[dimention, dimention];

            for (var i = 0; i < pieces.GetLength(0); i++)
            {
                for (var j = 0; j < pieces.GetLength(1); j++)
                {
                    result[i, j] = pieces[i, j].Position;
                }

            }
            return result;
        }
    }

  
}

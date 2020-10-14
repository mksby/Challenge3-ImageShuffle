using Emgu.CV;
using Emgu.CV.Structure;
using MathNet.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MathNet.Numerics.Statistics;

namespace ImageShuffle
{
    public class Solver
    {
        private int _dimention;
        ImageData _shuffledData;

        // это точка входа для тестов
        public ImageData RestoreImage(ImageData shuffledData, int dimention, ref RichTextBox log)
        {
            _shuffledData = shuffledData;
            _dimention = dimention;

            // переставить shuffledData.Pieces в нужном порядке
            // сейчас там в каждом куске есть пропертя Position, по которой удобно дэбажить
            // в shuffledData этот Position идет по возрастанию
            // для 2х2:
            // 0 1
            // 2 3
            // 
            // для 4х4:
            // 0 1 2 3 
            // 4 5 6 7
            // 8 9 10 11
            // 12 13 14 15
            //
            // по этой проперте в тестах считается количество правильно склееных кусков

            var imageData = _shuffledData.Copy();

            var allPieces = imageData.ToPieceList();

            // самый простой "в лоб" пример
            // вам написать нормальную логику:)

            var rows = new List<List<ImagePiece>>();
            for(var i=0;i<_dimention;i++)
                rows.Add(MakeRow(allPieces));

            var result = rows.ToImageData();
            // var result = new ImageData(dimention) {Pieces = imageData.Pieces};
            // log.AppendText($"ImageScore = {GetScore(result)}\n");

            return result;

        }

        // это просто пример :)
        public List<ImagePiece> MakeRow(List<ImagePiece> rest)
        {
            var row = new List<ImagePiece>();

            var element = rest.First();
            row.Add(element);
            rest.Remove(element);

            for (var j = 1; j < _dimention; j++)
            {
                var last = row.Last();
                var right = FindBestPairForPiece(last, rest, Direction.Right, out _);
                row.Add(right);
                rest.Remove(right);
            }
            return row;
        }

        // основная функция, которая подсчитывает, насколько кусок second подходит к first со стороны direction
        // чем меньше результат - тем идеальнее куски подходят,
        // т.е. ЧЕМ БЛИЖЕ К НУЛЮ - ТЕМ ЛУЧШЕ 
        // я здесь приведу самой простой пример:
        // - конвертим куски в черно-белый
        // - берем у кусков по столбцу(строке) пикселей на стыке: one и two
        // - считаем разницу в цвете между пикселями: |one[i]-two[i]|
        // - в качестве оценки возвращаем среднеквадратичную ошибку (MSE метрика) : |one[i]-two[i]|^2 / length(cтолбца)
        //
        // пример для first, second, Direction.Right:
        //  ___   ___
        // |   f|s   |
        // |___f|s___|
        //
        // пример для first, second, Direction.Left:
        //  ___   ___
        // |   s|f   |
        // |___s|f___|
        //
        // пример для first, second, Direction.Bottom:
        //  ____ 
        // |    |
        // | f f|
        //  ----
        // | s s|
        // |____|
        // 
        // пример для first, second, Direction.Top:
        //  ____ 
        // |    |
        // | s s|
        //  ----
        // | f f|
        // |____|
        //
        double Evaluate(ImagePiece first, ImagePiece second, Direction direction)
        {
            var firstGray = first.ToGrayscale();
            var secondGray = second.ToGrayscale();
            // два столбца пикселей, которые будем сравнивать
            Bgr[]  one, two;

            switch (direction)
            {
                case Direction.Right:
                    {
                        one = firstGray.GetLastColumn();
                        two = secondGray.GetColumn(0);
                        break;
                    }

                case Direction.Left:
                    {
                        one = firstGray.GetColumn(0);
                        two = secondGray.GetLastColumn();
                        break;
                    }

                case Direction.Bottom:
                    {
                        one = firstGray.GetLastRow();
                        two = secondGray.GetRow(0);
                        break;
                    }

                case Direction.Top:
                    {
                        one = firstGray.GetRow(0);
                        two = secondGray.GetLastRow();
                        break;
                    }

                default:
                    {
                        throw new Exception("undefined direction");
                    }
            }

            // в черно-белом формате Red==Green==Blue, поэтому берем любое из R/G/B
            var xr = one.Select(x => x.Red).ToArray();
            var yr = two.Select(x => x.Red).ToArray();

            // считаем dissimilarity metric между xr и yr
            // разных метрик существует много
            // можете посмотреть здесь https://numerics.mathdotnet.com/Distance.html
            // можете придумать свою
            var value = Distance.MSE(xr, yr);

            return value;
        }

       
        // найти найболее подходящий кусок из списка, который "подходит" к данному со стороны direction
        ImagePiece FindBestPairForPiece(ImagePiece piece, List<ImagePiece> list, Direction direction, out double minValue)
        {
            minValue = int.MaxValue;
            ImagePiece best = null;

            foreach (var p in list)
            {
                if (piece.Position != p.Position)
                {
                    var value = Evaluate(piece, p, direction);
                    if (value < minValue)
                    {
                        minValue = value;
                        best = p;
                    }
                }
            }
            return best;
        }

        // подсчитывает общий Evaluate для всей картинки (по всем швам),
        // может понадобится, если нужно сравнить два разных результата,
        // чем ближе GetScore к нулю, тем лучше результат 
        double GetScore(ImageData imageData)
        {
            int dimention = imageData.Pieces.GetLength(0);
            var scores = new List<double>();

            for (var i = 0; i < dimention; i++)
            {
                for (var j = 0; j < dimention; j++)
                {
                    if (imageData.Pieces[i, j] != null)
                    {
                        if (j < dimention - 1 && imageData.Pieces[i, j + 1]!=null)
                        {
                            var horizontalScore = Evaluate(imageData.Pieces[i, j], imageData.Pieces[i, j + 1], Direction.Right);
                            scores.Add(horizontalScore);
                        }
                        if (i < dimention - 1 && imageData.Pieces[i + 1, j] != null)
                        {
                            var verticalScore = Evaluate(imageData.Pieces[i, j], imageData.Pieces[i+1, j], Direction.Bottom);
                            scores.Add(verticalScore);
                        }
                    }
                   
                }
            }

            // берем просто среднее значение по всем швам
            // если нужна другая оценка, можно посмотреть здесь
            // https://numerics.mathdotnet.com/api/MathNet.Numerics.Statistics/Statistics.htm
            return scores.Mean();
        }

        
    }




}

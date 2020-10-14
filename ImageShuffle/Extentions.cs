using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace ImageShuffle
{
    public static class ImageProcessing
    {
        private static readonly Random Rng = new Random();

        // разрезание картинки
        public static ImageData Cut(this Image<Bgr, byte> image, int dimention)
        {
            var w = image.Width;
            var h = image.Height;
            var stepW = w / dimention;
            var stepH = h / dimention;
            var oldRoi = image.ROI;

            var imageData = new ImageData(dimention);

            int ii = 0, position=0;
            for (var i = 0; i + stepH/2 < h; i += stepH)
            {
                var jj = 0;
                for (var j = 0; j + stepW/2 < w; j += stepW)
                {
                    var roi = new Rectangle(j, i, stepW, stepH);
                    image.ROI = roi;

                    imageData.Pieces[ii, jj] = new ImagePiece
                    {
                        Data = image.Copy(),
                        Position = position
                    };
                    jj++;
                    position++;
                }
                ii++;
            }

            image.ROI = oldRoi;

            return imageData;
        }

        // перемешивание кусков
        public static void Shuffle(this ImageData image)
        {
            image.Pieces.Shuffle();
        }
        static void Shuffle<T>(this T[,] array)
        {
            int lengthRow = array.GetLength(1);

            for (int i = array.Length - 1; i > 0; i--)
            {
                int i0 = i / lengthRow;
                int i1 = i % lengthRow;

                int j = Rng.Next(i + 1);
                int j0 = j / lengthRow;
                int j1 = j % lengthRow;

                T temp = array[i0, i1];
                array[i0, i1] = array[j0, j1];
                array[j0, j1] = temp;
            }
        }

        // склеивание картинки
        public static Image<Bgr, byte> Stick(this ImageData imageData)
        {
            var rows = new List<Image<Bgr, byte>>();
            for (var i = 0; i < imageData.Pieces.GetLength(0); i++)
            {
                var row = imageData.Pieces[i, 0].Data.Copy();
                for (var j = 1; j < imageData.Pieces.GetLength(1); j++)
                {
                    row = row.ConcateHorizontal(imageData.Pieces[i, j].Data);
                }
                rows.Add(row);
            }

            var result = rows[0];
            for (int i = 1; i < rows.Count; i++)
            {
                result = result.ConcateVertical(rows[i]);
            }
            return result;
        }
        public static List<T> ShiftRight<T>(this List<T> list, int shiftBy)
        {
            if (list.Count <= shiftBy)
            {
                return list;
            }

            var result = list.GetRange(list.Count - shiftBy, shiftBy);
            result.AddRange(list.GetRange(0, list.Count - shiftBy));
            return result;
        }

        // 2D лист ImagePiece в ImageData
        public static ImageData ToImageData(this List<List<ImagePiece>> list)
        {
            var restoredData = new ImageData(list.Count);
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list.Count; j++)
                {
                    restoredData.Pieces[i, j] = list[i][j];
                }
            }
            return restoredData;
        }

        // клон 
        public static ImageData Copy(this ImageData imageData)
        {
            return new ImageData(imageData);
        }

        // корвертирование картинки в черно-белый формат
        public static Image<Bgr, byte> ToGrayscaleImage(this Image<Bgr, byte> inputImage)
        {
            UMat uimage = new UMat();
            CvInvoke.CvtColor(inputImage, uimage, ColorConversion.Bgr2Gray);
            var outputImage = uimage.ToImage<Bgr, byte>();
            return outputImage;
        }

        // canny фильтр на картинку 
        // https://sod.pixlab.io/images/out_canny.jpg
        public static Image<Bgr, byte> ToCannyImage(this Image<Bgr, byte> inputImage)
        {
            var outputImage = inputImage.Canny(50, 20);
            return outputImage.ToUMat().ToImage<Bgr, byte>();
        }
        
    }

    public static class ImagePieceExtentions
    {
        // взять последний столбец пикселей
        public static Bgr[] GetLastColumn(this ImagePiece matrix)
        {
            var colCount = matrix.Data.Data.GetLength(1) - 1;
            return matrix.GetColumn(colCount);
        }

        // взять N-ый столбец пикселей
        public static Bgr[] GetColumn(this ImagePiece matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.Data.Data.GetLength(0))
                .Select(x => matrix.Data[x, columnNumber])
                .ToArray();
        }

        // взять N-ый ряд пикселей
        public static Bgr[] GetRow(this ImagePiece matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.Data.Data.GetLength(1))
                .Select(x => matrix.Data[rowNumber, x])
                .ToArray();
        }

        // взять последний ряд пикселей
        public static Bgr[] GetLastRow(this ImagePiece matrix)
        {
            var rowCount = matrix.Data.Data.GetLength(0) - 1;
            return matrix.GetRow(rowCount);
        }


        // ImagePiece в черно-белый цвет
        public static ImagePiece ToGrayscale(this ImagePiece matrix)
        {
            return new ImagePiece
            {
                Data = matrix.Data.ToGrayscaleImage(),
                Position = matrix.Position
            };
        }
    }

    public static class ImageDataExtentions
    {
        // получить List всех кусков
        public static List<ImagePiece> ToPieceList(this ImageData image)
        {
            return image.Pieces.Cast<ImagePiece>().ToList();
        }
        
        // для дэбага может понадобиться посмотреть, что у вас получилось на текущий момент,
        // когда вы собираете ImageData, но некоторые куски==null,
        // в таком случае их можно закрасить красным цветом, чтобы вывести и посмотреть
        public static ImageData FillEmptyWithRed(this ImageData image)
        {
            var redPiece = image.GenerateRedPiece();
            var imageData = new ImageData(image);

            for (var i = 0; i < imageData.Pieces.GetLength(0); i++)
            {
                for (var j = 0; j < imageData.Pieces.GetLength(1); j++)
                {
                    if (imageData.Pieces[i, j] == null)
                        imageData.Pieces[i, j] = redPiece;
                }
            }
            return imageData;
        }
        static ImagePiece GenerateRedPiece(this ImageData image)
        {
            var notNullPiece = image.ToPieceList().FirstOrDefault(x => x != null);
            if (notNullPiece == null)
                return null;

            var redPiece = new ImagePiece
            {
                Data = new Image<Bgr, byte>(notNullPiece.Data.Data)
            };
            for (var i = 0; i < redPiece.Data.Data.GetLength(0); i++)
            {
                for (var j = 0; j < redPiece.Data.Data.GetLength(1); j++)
                {
                    redPiece.Data[j, i] = new Bgr(0, 0, 255);
                }
            }
            return redPiece;
        }
    }

    public static class DirectionExtentions
    {
        public static Direction GetOppositeDirection(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                {
                    return Direction.Right;
                }
                case Direction.Right:
                {
                    return Direction.Left;
                }
                case Direction.Top:
                {
                    return Direction.Bottom;
                }
                case Direction.Bottom:
                {
                    return Direction.Top;
                }
                default: throw new Exception("undefined direction");
            }
        }
    }
}

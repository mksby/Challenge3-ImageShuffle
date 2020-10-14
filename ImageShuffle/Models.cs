using Emgu.CV;
using Emgu.CV.Structure;

namespace ImageShuffle
{
    public class ImagePiece
    {
        public Image<Bgr, byte> Data { get; set; }
        public int Position { get; set; }
    }

    public class ImageData
    {
        public ImageData(int dimention)
        {
            Pieces = new ImagePiece[dimention, dimention];
        }

        public ImageData(ImageData data)
        {
            Pieces = data.Pieces;
        }

        public ImagePiece[,] Pieces { get; set; }
    }


    public enum Direction
    {
        Left,
        Right,
        Top,
        Bottom
    }
}

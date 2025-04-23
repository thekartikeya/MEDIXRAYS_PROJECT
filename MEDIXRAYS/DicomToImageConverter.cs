using Dicom;
using Dicom.Imaging;
using System.Drawing;

namespace MEDIXRAYS.Modules
{
    public class DicomToImageConverter
    {
        public static Image ConvertToImage(string dicomFilePath)
        {
            var dicomImage = new DicomImage(dicomFilePath);
            return dicomImage.RenderImage().AsClonedBitmap();
        }
    }
}

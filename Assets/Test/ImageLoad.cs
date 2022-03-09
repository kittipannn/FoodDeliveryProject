using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ImageLoad : MonoBehaviour
{

    string imagePath;
    public RawImage img;
    Texture2D image;
    void Start()
    {
        ImageLoader();
    }

    void ImageLoader() 
    {
        imagePath = "Image/Data3" + "/Data34" ;
        image = Resources.Load(imagePath) as Texture2D;
        img.texture = image;
        var colorImage = img.color;
        colorImage.a = 1;
        img.color = colorImage;
    }
}

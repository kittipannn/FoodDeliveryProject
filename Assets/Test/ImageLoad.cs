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
        imagePath = "Image/Img1";
        image = Resources.Load(imagePath) as Texture2D;
        img.texture = image;
       
    }
}

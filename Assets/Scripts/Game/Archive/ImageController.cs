using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageController : MonoBehaviour
{

    public Sprite Image1;
    public Sprite Image2;
    public Sprite Image3;
    public Sprite Image4;
    public Sprite Image5;
    public Sprite Image6;
    public Sprite Image7;
    public Sprite Image8;
    public Sprite Image9;
    public Sprite Image10;
    public Sprite Image11;

    Image current_Image;

    private 
    // Start is called before the first frame update
    void Start()
    {
        current_Image=GetComponent<Image>();
        current_Image.sprite = Image1;
    }

    public void ChangeImage(int i)
    {
        switch(i)
        {
            case 1:
                current_Image.sprite = Image1;
                break;
            case 2:
                current_Image.sprite = Image2;
                break;
            case 3:
                current_Image.sprite = Image3;
                break;
            case 4:
                current_Image.sprite = Image4;
                break;
            case 5:
                current_Image.sprite = Image5;
                break;
            case 6:
                current_Image.sprite = Image6;
                break;
            case 7:
                current_Image.sprite = Image7;
                break;
            case 8:
                current_Image.sprite = Image8;
                break;
            case 9:
                current_Image.sprite = Image9;
                break;
            case 10:
                current_Image.sprite = Image10;
                break;
            case 11:
                current_Image.sprite = Image11;
                break;
        }
    }
}

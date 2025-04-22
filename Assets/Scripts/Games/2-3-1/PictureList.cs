using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureList : Singleton<PictureList>
{
    public GameObject[] pictures;

    public void PopPicture(int index){
        pictures[index].SetActive(true);
    }

}

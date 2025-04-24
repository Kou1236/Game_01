using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickObject_17 : ClickObject
{

    public override void Clicked()
    {   
        ClickFinish.Instance.AddIndex();
        ClickFinish.Instance.Finish();

    }


}

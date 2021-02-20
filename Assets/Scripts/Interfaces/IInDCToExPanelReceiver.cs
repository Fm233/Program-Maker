using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IInDCToExPanelReceiver
{
    Action<Vector2> GetDcPosReceiver();
}

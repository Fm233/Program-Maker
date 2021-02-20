using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IInRCToExPanelReceiver
{
    Action<Vector2> GetRcPosReceiver();
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public interface IInMouseToExPanelReceiver
{
    Action<Vector2> GetMousePosReceiver();
}

using Extentions;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class TurretSignals : MonoSingleton<TurretSignals>
    {

      public UnityAction onPressTurretButton =delegate { };

      public UnityAction onDeadEnemy = delegate { };
      
    }
}
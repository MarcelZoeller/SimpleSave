using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleSave
{
    public interface ISaveable
    {
        object Save();
        void Load(object data);
    }
}

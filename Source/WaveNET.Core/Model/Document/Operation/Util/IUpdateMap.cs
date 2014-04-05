﻿using System;

namespace WaveNET.Core.Model.Document.Operation.Util
{
    /// <summary>
    ///     Defines a reversible change on a map.
    /// </summary>
    public interface IUpdateMap
    {
        int ChangeSize();
        String GetChangeKey(int changeIndex);
        String GetOldValue(int changeIndex);
        String GetNewValue(int changeIndex);
    }
}
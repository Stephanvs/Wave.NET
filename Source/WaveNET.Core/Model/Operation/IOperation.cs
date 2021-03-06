﻿namespace WaveNET.Core.Model.Operation
{
    public interface IOperation<in T>
    {
        /// <summary>
        ///     Applies this operation to the given target
        /// </summary>
        /// <param name="target">target on which this operation applies itself</param>
        void Apply(T target);
    }
}
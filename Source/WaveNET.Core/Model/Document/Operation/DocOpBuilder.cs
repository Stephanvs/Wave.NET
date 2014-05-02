﻿using System.Collections;
using System.Collections.Generic;

namespace WaveNET.Core.Model.Document.Operation
{
    /// <summary>
    /// A builder for <see cref="IDocOp" />'s.
    /// </summary>
    public sealed class DocOpBuilder
    {
        private readonly IList<DocOpComponent> _accu = new List<DocOpComponent>();

        public IDocOp Build()
        {
            return BufferedDocOp.Create(_accu);
        }

        public DocOpBuilder Characters(string characters)
        {
            _accu.Add(new Characters(characters));
            return this;
        }

        public DocOpBuilder AnnotationBoundary(IAnnotationBoundaryMap map)
        {
            _accu.Add(new AnnotationBoundary(map));
            return this;
        }

        public DocOpBuilder Retain(int itemCount)
        {
            _accu.Add(new Retain(itemCount));
            return this;
        }
    }
}
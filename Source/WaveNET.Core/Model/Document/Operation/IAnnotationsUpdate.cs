using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaveNET.Core.Model.Document.Operation.Util;

namespace WaveNET.Core.Model.Document.Operation
{
	public interface IAnnotationsUpdate : IUpdateMap
	{
		IAnnotationsUpdate ComposeWith(IAnnotationsUpdate mutation);
		IAnnotationsUpdate ComposeWith(IAnnotationBoundaryMap map);
	}
}

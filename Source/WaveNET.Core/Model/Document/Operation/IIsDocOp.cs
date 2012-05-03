using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaveNET.Core.Model.Document.Operation
{
	// TODO: better name
	public interface IIsDocOp
	{
		IDocInitialization AsOperation();
	}
}

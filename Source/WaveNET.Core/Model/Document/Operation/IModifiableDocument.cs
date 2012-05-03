﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaveNET.Core.Model.Document.Operation
{
	public interface IModifiableDocument
	{
		/// <summary>
		/// A document that can accept operations to mutate it's state.
		/// </summary>
		/// <param name="mutations"></param>
		void Consume(IDocOp mutations);
	}
}

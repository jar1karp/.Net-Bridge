﻿// 
// General:
//      This file is pary of .NET Bridge
//
// Copyright:
//      2010 Jonathan Shore
//      2017 Jonathan Shore and Contributors
//
// License:
//      Licensed under the Apache License, Version 2.0 (the "License");
//      you may not use this file except in compliance with the License.
//      You may obtain a copy of the License at:
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//      Unless required by applicable law or agreed to in writing, software
//      distributed under the License is distributed on an "AS IS" BASIS,
//      WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//      See the License for the specific language governing permissions and
//      limitations under the License.
//

using System;
using bridge.common.io;


namespace bridge.server.data
{
	/// <summary>
	/// CLR object array message.
	/// 
	/// Object array can handle values of any type, including primitive values
	/// </summary>
	public class CLRObjectArrayMessage : CLRMessage
	{
		public CLRObjectArrayMessage ()
			: base (TypeObjectArray)
		{
		}

		public CLRObjectArrayMessage (object[] value, int len = -1)
			: base (TypeObjectArray)
		{
			Value = value;
			Length = len >= 0 ? len : Value.Length;
		}


		// Properties

		public object[] Value
			{ get; private set; }

		public int Length
			{ get; set; }


		// Serialization
		
		/// <summary>
		/// Serialize the message.
		/// </summary>
		/// <param name="cout">Cout.</param>
		public override void Serialize (IBinaryWriter cout)
		{
			base.Serialize (cout);
			cout.WriteInt32 (Length);

			for (int i = 0 ; i < Length ; i++)
			{
				var obj = Value[i];
				CLRMessage.SerializeValue (cout, obj);
			}
		}
		
		/// <summary>
		/// Deserialize the message (assumes magic & type already read in)
		/// </summary>
		/// <param name="cin">Cin.</param>
		public override void Deserialize (IBinaryReader cin)
		{
			Length = cin.ReadInt32();
			Value = new object[Length];
			 
			for (int i = 0 ; i < Length ; i++)
			{
				Value[i] = CLRMessage.DeserializeValue (cin);
			}
		}

	}
}


#region License
/* FNA - XNA4 Reimplementation for Desktop Platforms
 * Copyright 2009-2022 Ethan Lee and the MonoGame Team
 *
 * Released under the Microsoft Public License.
 * See LICENSE for details.
 */
#endregion

#region Using Statements
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
#endregion

namespace Microsoft.Xna.Framework
{
	internal static class MarshalHelper
	{
		internal static int SizeOf<T>()
		{
			// Marshal.SizeOf<T>() returns the *marshalled* (pinvoke/interop) layout size,
			// which is only correct for StructureToPtr/PtrToStructure. Every call site here
			// pins the array and computes a raw byte offset/length for memcpy-style native
			// calls, which needs the CLR's actual in-memory layout size instead -- that's
			// System.Runtime.CompilerServices.Unsafe.SizeOf<T>(), which also has no lookup
			// overhead (it lowers to a JIT intrinsic constant per T).
			return Unsafe.SizeOf<T>();
		}

		internal static string PtrToInternedStringAnsi(IntPtr ptr)
		{
			string result = Marshal.PtrToStringAnsi(ptr);
			if (result != null)
				result = string.Intern(result);
			return result;
		}
	}
}

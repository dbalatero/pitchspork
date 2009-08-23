using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pitchspork.Model {
	internal static class ExtensionMethods {
		public static string ToString<T>(this List<T> l) {
			StringBuilder sb = new StringBuilder();

			sb.Append("{");

			for (int i = 0; i < l.Count; i++) {
				sb.Append(l[i]);

				if (i != l.Count - 1) {
					sb.Append(", ");
				}
			}

			sb.Append("}");

			return sb.ToString();
		}
	}
}

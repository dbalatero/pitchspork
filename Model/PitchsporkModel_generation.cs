using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pitchspork.Model {
	public sealed partial class PitchsporkModel {
		public string GenerateToken(string[] context, Random rng = null) {
			if (rng == null) rng = new Random();

			for (int degree = context.Length; degree >= 0; degree--) {
				string[] partialContext;

				partialContext = context.Skip(context.Length - degree).ToArray();

				Dictionary<string, int> next;
				_BodyModel.TryGetValue(partialContext, out next);
				if (next == null) next = new Dictionary<string, int>();

				int count, r;

				count = next.Sum(kv => kv.Value);

				if (count == 0) continue;

				r = rng.Next(count);

				foreach (KeyValuePair<string, int> kv in next) {
					r -= kv.Value;

					if (r <= 0) return kv.Key;
				}
			}

			// it should be totally impossible to get here unless the model is completely empty.
			return null;
		}

		public string GenerateSentence(Random rng = null) {
			StringBuilder sb = new StringBuilder();
			List<string> history = new List<string>(new string[] { "<STOP:SENT>" });

			do {
				string nextToken = GenerateToken(history.ToArray(), rng);

				sb.Append(nextToken + " ");
				history.Add(nextToken);

				while (history.Count > MaxDegree) {
					history.RemoveAt(0);
				}
			} while (history[history.Count - 1] != "<STOP:SENT>");

			return sb.ToString();
		}
	}
}
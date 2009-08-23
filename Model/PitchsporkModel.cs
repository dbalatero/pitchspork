using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Pitchspork.Model {
	[Serializable]
	public sealed partial class PitchsporkModel {
		public PitchsporkModel(int degree) {
			this.MaxDegree = degree;
		}

		public int MaxDegree { get; private set; }

		private static string IsStopToken(string token) {
			Regex r = new Regex("^<STOP:(.*)>$");

			if (r.IsMatch(token)) {
				return r.Matches(token)[0].Captures[0].Value;
			} else return null;
		}

		private Dictionary<Context, Dictionary<string, int>> _BodyModel = 
			new Dictionary<Context, Dictionary<string, int>>();

		private Dictionary<string, int> _FirstNames = new Dictionary<string, int>();
		private Dictionary<string, int> _LastNames = new Dictionary<string, int>();

		private Dictionary<string, int> _Labels = new Dictionary<string, int>();

		private List<string> _Artists = new List<string>();
		private List<string> _Albums = new List<string>();
	}
}

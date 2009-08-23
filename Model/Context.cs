using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pitchspork.Model {
	public struct Context {
		public Context(string[] tokens) : this() {
			_Tokens = tokens;
		}

		public string this[int index] {
			get { return _Tokens[index]; }
		}

		private string[] _Tokens;

		public override int GetHashCode() {
			int hash = 0;

			for (int i = 0; i < _Tokens.Length; i++) {
				// rotate left 11 (it's coprime to 32!  *img-themoreyouknow*)
				int next = (hash >> 21) & 0x7FF;
				next |= hash << 11;

				// XOR with the hash of the next string.
				next ^= _Tokens[i].GetHashCode();
				hash = next;
			}

			return hash;
		}

		public override bool Equals(object obj) {
			if (!(obj is Context)) {
				return false;
			}

			return (Context)obj == this;
		}

		public static bool operator==(Context lhs, Context rhs) {
			if (ReferenceEquals(lhs, rhs)) {
				return true;
			}

			if (((object)lhs == null) || ((object)rhs == null)) {
				return false;
			}

			bool result = true;

			result &= lhs._Tokens.Length == rhs._Tokens.Length;

			if (result) {
				for (int i = 0; i < lhs._Tokens.Length; i++) {
					result &= lhs._Tokens[i] == rhs._Tokens[i];
				}
			}

			return result;
		}

		public static bool operator !=(Context lhs, Context rhs) {
			return !(lhs == rhs);
		}

		public override string ToString() {
			StringBuilder sb = new StringBuilder();

			sb.Append("[");

			for (int i = 0; i < _Tokens.Length; i++) {
				sb.Append(_Tokens[i]);

				if (i != _Tokens.Length - 1) sb.Append(", ");
			}

			sb.Append("]");

			return sb.ToString();
		}

		public static implicit operator Context(string[] words) {
			return new Context(words);
		}
	}
}

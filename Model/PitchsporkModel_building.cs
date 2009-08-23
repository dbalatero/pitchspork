using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pitchspork.Model {
	public sealed partial class PitchsporkModel {
		public void Populate(Article a) {
			PopulateAuthorName(a);
			PopulateRecordLabel(a);
			PopulateArtist(a);
			PopulateAlbumName(a);

			PopulateBodyTransitions(a);
		}

		private void PopulateAuthorName(Article a) {
			int fnCount = 0, lnCount = 0;
			string[] authorNames = a.Author.Split((char[])null, 2, StringSplitOptions.RemoveEmptyEntries);

			_FirstNames.TryGetValue(authorNames[0], out fnCount);
			_LastNames.TryGetValue(authorNames[1], out lnCount);

			_FirstNames[authorNames[0]] = fnCount + 1;
			_LastNames[authorNames[0]] = lnCount + 1;
		}

		private void PopulateRecordLabel(Article a) {
			int lblCount = 0;

			_Labels.TryGetValue(a.Label, out lblCount);
			_Labels[a.Label] = lblCount + 1;
		}

		private void PopulateArtist(Article a) {
			_Artists.Add(a.Artist);
		}

		private void PopulateAlbumName(Article a) {
			_Albums.Add(a.Album);
		}

		private void PopulateBodyTransitions(Article a) {
			List<string> tokens = Tokenize(a.Body);
			List<string> history = new List<string>();

			for (int i = 0; i < tokens.Count; i++) {
				List<string> previous = new List<string>(history);
				Context ctx;

				while (previous.Count > 0) {
					Dictionary<string, int> next;
					int count = 0;

					ctx = new Context(previous.ToArray());

					_BodyModel.TryGetValue(ctx, out next);
					if (next == null) next = new Dictionary<string, int>();

					next.TryGetValue(tokens[i], out count);
					if (next.Count == 0) _BodyModel[ctx] = next;

					next[tokens[i]] = count + 1;

					Console.WriteLine("Set transition for {0} -> {2} to weight {1}", ctx.ToString(), count + 1, tokens[i]);

					previous.RemoveAt(0);
				}

				history.Add(tokens[i]);

				while (history.Count > MaxDegree) {
					history.RemoveAt(0);
				}
			}
		}

		private List<string> Tokenize(string body) {
			List<string> tokens = new List<String>();

			// add some padding to the top so we can generate the first sentence in an article.
			tokens.Add("<STOP:PARA>");

			// first, do para breaks.
			{
				string[] paragraphs = body.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

				foreach (string para in paragraphs) {
					tokens.Add(para);

					tokens.Add("<STOP:PARA>");
				}
			}

			// now for the shitty part, breaking punctuation apart from its words.
			{
				List<string> tokensOut = new List<String>();

				foreach (string para in tokens) {
					if (IsStopToken(para) != null) {
						// stop token; ignore it.
						tokensOut.Add(para);
						continue;
					}

					int cursor = 0, tokenStart = -1;

					while (cursor < para.Length) {
						if (!char.IsWhiteSpace(para[cursor]) && tokenStart == -1) {
							// not currently in a token, this character looks promising
							tokenStart = cursor;
						}

						if (tokenStart >= 0) {
							if (char.IsWhiteSpace(para[cursor])) {
								// hey, it's whitespace, end the token
								tokensOut.Add(para.Substring(tokenStart, cursor - tokenStart));
								tokenStart = -1;
							} else if (char.IsPunctuation(para[cursor])) {
								// punctuation.  check to see if it's a period, question mark, or exclamation mark.
								if (para[cursor] == '!' || para[cursor] == '?' || para[cursor] == '.') {
									// end the sentence.
									tokensOut.Add(para.Substring(tokenStart, cursor - tokenStart));
									tokensOut.Add(para[cursor].ToString());
									tokensOut.Add("<STOP:SENT>");
									tokenStart = -1;
								} else {
									// inline punctuation; give it its own token.
									if (cursor != tokenStart) 
										// we were in a word before, dump the fragment of the word before this punctuation.
										tokensOut.Add(para.Substring(tokenStart, cursor - tokenStart));
									tokensOut.Add(para[cursor].ToString());
									tokenStart = -1;
								}
							}
						}

						cursor++;
					}
				}

				tokens = tokensOut;
			}

			return tokens;
		}
	}
}

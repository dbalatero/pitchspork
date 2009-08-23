using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

using Pitchspork.Model;

namespace Modeler {
	class Program {
		static void Main(string[] args) {
			Article a;
			XmlSerializer xmls = new XmlSerializer(typeof(Article));

			TextReader r = new StreamReader(@"C:\Users\Colin Bayer\Documents\Visual Studio 10\Projects\pitchspork\sample_data\squarepusher.xml");

			a = (Article)xmls.Deserialize(r);

			r.Close();

			Console.WriteLine("Loaded review of {0} - {1} by {2}.  God, that album sucks.", a.Artist, a.Album, a.Author);
			PitchsporkModel m = new PitchsporkModel(2);

			m.Populate(a);
			Console.WriteLine("hmm: {0}", m.GenerateSentence());
		}
	}
}

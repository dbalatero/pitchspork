using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Pitchspork.Model {
	[XmlRoot("article")]
	public class Article {
		public Article() { }

		[XmlElement("source_url")]
		public string SourceUrl { get; set; }

		[XmlElement("artist")]
		public string Artist { get; set; }

		[XmlElement("album")]
		public string Album { get; set; }

		[XmlElement("rating")]
		public double Rating { get; set; }

		[XmlElement("label")]
		public string Label { get; set; }

		[XmlElement("author")]
		public string Author { get; set; }

		[XmlElement("body")]
		public string Body { get; set; }
	}
}

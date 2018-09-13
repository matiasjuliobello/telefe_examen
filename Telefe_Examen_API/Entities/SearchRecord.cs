using System;
using System.Diagnostics.CodeAnalysis;

namespace Entities
{
	[ExcludeFromCodeCoverage]
    public class SearchRecord
    {
		public int Id { get; set; }
	    public string Search { get; set; }
	    public bool Result { get; set; }
	    public DateTime Timestamp { get; set; }
	}
}

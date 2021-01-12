using System.Collections.Generic;

namespace Recipe_Manager.Domain
{
	public class Recipe
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Category { get; set; }
		public string Area { get; set; }
		public string Instructions { get; set; }
		public string ThumbnailUrl { get; set; }
		public string Tags { get; set; } //ToDo: change into string array for future refined tag search
		public string VideoUrl { get; set; }
		public List<KeyValuePair<string, string>> IngredientsAndMeasurements { get; set; }
		public string Source { get; set; }
		public string DateModified { get; set; }
	}
}

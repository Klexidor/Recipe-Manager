using MediatR;
using Recipe_Manager.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Recipe_Manager.Application.Recipies
{
	public class List
	{
		public class Query : IRequest<List<Recipe>>
		{
			public string RequestQuery { get; set; }
		}

		public class Handler : IRequestHandler<Query, List<Recipe>>
		{
			public async Task<List<Recipe>> Handle(Query request, CancellationToken cancellationToken)
			{
				//MealDB Request goes here.
				WebRequest mealdbRequest = WebRequest.Create("https://www.themealdb.com/api/json/v1/1/search.php?s=" + request.RequestQuery);
				WebResponse response = mealdbRequest.GetResponse();

				List<Recipe> recipies = new List<Recipe>();
				string serverResponse = "";

				using (Stream dataStream = response.GetResponseStream())
				{
					StreamReader reader = new StreamReader(dataStream);

					serverResponse = reader.ReadToEnd();

					//recipies = JsonConvert.DeserializeObject<List<Recipe>>(serverResponse);

					Console.WriteLine(serverResponse);
				}

				response.Close();

				return DeserializeResult(serverResponse);
			}

			private List<Recipe> DeserializeResult(string input)
			{
				JObject searchResult = JObject.Parse(input);

				List<Recipe> recipies = new List<Recipe>();

				IList<JToken> results = searchResult["meals"].Children().ToList();

				foreach (JToken result in results)
				{
					var recipe = new Recipe()
					{
						Id = (int)result["idMeal"],
						Name = result["strMeal"].ToString(),
						Category = result["strCategory"].ToString(),
						Area = result["strArea"].ToString(),
						Instructions = result["strInstructions"].ToString(),
						ThumbnailUrl = result["strMealThumb"].ToString(),
						Tags = result["strTags"].ToString(),
						VideoUrl = result["strYoutube"].ToString(),
						IngredientsAndMeasurements = DeserializeIngredients(result),
						Source = result["strSource"].ToString(),
						DateModified = result["dateModified"].ToString()
					};

					recipies.Add(recipe);
				}

				return recipies;
			}

			private static List<KeyValuePair<string,string>> DeserializeIngredients(JToken resultRecipe)
			{
				var ingredientsAndMeasurements = new List<KeyValuePair<string, string>>(); //ingredient, measurement

				var ingredientString = "strIngredient";
				var measureString = "strMeasure";

				for (var i = 1; i < 21; i++)
				{
					var ingredient = resultRecipe[ingredientString + i].ToString();

					if (String.IsNullOrWhiteSpace(ingredient))
					{
						break;
					}

					ingredientsAndMeasurements.Add(new KeyValuePair<string,string>(ingredient, resultRecipe[measureString + i].ToString()));
				}


				return ingredientsAndMeasurements;
			}
		}


	}
}

using MediatR;
using Recipe_Manager.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

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
				WebRequest mealdbRequest = WebRequest.Create("https://www.themealdb.com/api/json/v1/1/search.php?s=Arrabiata");
				WebResponse response = mealdbRequest.GetResponse();

				using (Stream dataStream = response.GetResponseStream())
				{
					StreamReader reader = new StreamReader(dataStream);

					string serverResponse = reader.ReadToEnd();

					Console.WriteLine(serverResponse);
				}

				response.Close();

				return new List<Recipe>();
			}
		}
	}
}

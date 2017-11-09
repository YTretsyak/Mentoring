using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample03.E3SClient
{
	public class FTSRequestGenerator
	{
		private readonly UriTemplate FTSSearchTemplate = new UriTemplate(@"data/searchFts?metaType={metaType}&query={query}&fields={fields}");
		private readonly Uri BaseAddress;

		public FTSRequestGenerator(string baseAddres) : this(new Uri(baseAddres))
		{
		}

		public FTSRequestGenerator(Uri baseAddress)
		{
			BaseAddress = baseAddress;
		}

		public Uri GenerateRequestUrl<T>(string query = "*", int start = 0, int limit = 10)
		{
			return GenerateRequestUrl(typeof(T), query, start, limit);
		}

	  public Uri GenerateRequestUrl(Type type, string query = "*", int start = 0, int limit = 10)
	  {
	    return GenerateRequestUrl(type, new List<string> {query}, start, limit);
	  }


    public Uri GenerateRequestUrl(Type type, List<string> queries, int start = 0, int limit = 10)
		{
			string metaTypeName = GetMetaTypeName(type);

      // process query
		  queries = ProcessParams(type, queries);

      var ftsQueryRequest = new FTSQueryRequest
			{
				Statements = queries.Select(x=>new Statement(){Query = x}).ToList(),
				Start = start,
				Limit = limit
			};

			var ftsQueryRequestString = JsonConvert.SerializeObject(ftsQueryRequest);

			var uri = FTSSearchTemplate.BindByName(BaseAddress,
				new Dictionary<string, string>()
				{
					{ "metaType", metaTypeName },
					{ "query", ftsQueryRequestString }
				});

			return uri;
		}

	  private List<string> ProcessParams(Type type, List<string> queries)
	  {
	    // process query
	    for (int i = 0; i < queries.Count; i++)
	    {
	      var operands = queries[i].Split(':');

	      if (type.GetProperty(operands[0]) == null && type.GetProperty(operands[1]) != null)
	      {
	        queries[i] = operands[1] + ":" + operands[0];
	      }
      }

	    return queries;
	  }

		private string GetMetaTypeName(Type type)
		{
			var attributes = type.GetCustomAttributes(typeof(E3SMetaTypeAttribute), false);

			if (attributes.Length == 0)
				throw new Exception(string.Format("Entity {0} do not have attribute E3SMetaType", type.FullName));

			return ((E3SMetaTypeAttribute)attributes[0]).Name;
		}

	}
}

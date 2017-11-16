using MomCom.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MomCom.Controllers
{
	public class MatchController : Controller
	{
		private MomComEntities db = new MomComEntities();

		// GET: Match
		public ActionResult Index()
		{
			var me = Session["me"] as User;

			bool age1 = me.AgeRange1;
			bool age2 = me.AgeRange2;
			bool age3 = me.AgeRange3;
			bool museum = me.Museum;
			bool active = me.Active;
			bool outdoors = me.Outdoors;

			string place = GetInterest(museum, outdoors, active);

			List<string> destination = GetPlaces(place);
			for (int i = 0; i < destination.Count(); i++)
			{
				ViewBag.Place += destination[i];
			}

			return null;
			//return View(
			//    db.Users.Where(
			//            x => (x.AgeRange1 == age1 | x.AgeRange2 == age2 | x.AgeRange3 == age3) &
			//                 (x.Museum == museum | x.Outdoors == outdoors | x.Active == active))
			//        .ToList());
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
		public string GetInterest(bool m, bool o, bool a)
		{
			if (m == true)
			{
				return "museum";
			}
			else if (o == true)
			{
				return "park";
			}
			else
			{
				return "active";
			}
		}

		public List<string> GetPlaces(string p)
		{
			string place = p;
			HttpWebRequest request;

			if (place == "museum")
			{
				request = WebRequest.CreateHttp(
					"http://places.demo.api.here.com/places/v1/discover/search?q=museum&in=42.921%2C-85.5944%3Br%3D45033&Accept-Language=en-US%2Cen%3Bq%3D0.8/application/json&app_id=EVCVXo26lL5LrdGgDEK5&app_code=J9PlnJ5WvHIuxtYvVMwiYw");
			}

			else if (place == "park")
			{
				request = WebRequest.CreateHttp(
					"http://places.cit.api.here.com/places/v1/discover/search?q=park&in=42.921%2C-85.5944%3Br%3D45033.0&Accept-Language=en-US%2Cen%3Bq%3D0.&app_id=EVCVXo26lL5LrdGgDEK5&app_code=J9PlnJ5WvHIuxtYvVMwiYw");
			}
			else
			{
				request = WebRequest.CreateHttp(
					"http://places.demo.api.here.com/places/v1/discover/search?q=bowling&in=42.921%2C-85.5944%3Br%3D45033&Accept-Language=en-US%2Cen%3Bq%3D0.8/application/json&app_id=EVCVXo26lL5LrdGgDEK5&app_code=J9PlnJ5WvHIuxtYvVMwiYw");
			}

			////Tell it the list of browsers we're using
			//request.UserAgent = @"User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.116 Safari/537.36";
			////push the request over to the remote server 
			//HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			////Parse the response data (this looks a lot like reading in a text file, file I/O)
			//StreamReader rd = new StreamReader(response.GetResponseStream());
			////Return the data in string format 
			//String data = rd.ReadToEnd();
			//JObject o = JObject.Parse(data);

			//List<string> placesList = new List<string>();
			////List<string> addressList = new List<string>();
			//for (int i = 0; i < o["results"]["items"].Count(); i++)
			//{
			//	string destination = o["results"]["items"][i]["title"] + " is located at " + o["results"]["items"][i]["vicinity"].ToString();
			//	placesList.Add(destination);
			//}
			//return placesList;
			return null;
		}
	}
}

using MomCom.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MomCom.Controllers
{
	public class MomAzureController : Controller
	{
		private MomComEntities db = new MomComEntities();

		// GET: MomAzure
		public ActionResult Index()
		{
			return View(db.Users.ToList());
		}

		// GET: MomAzure/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			User user = db.Users.Find(id);
			if (user == null)
			{
				return HttpNotFound();
			}

			return View(user);
		}

		public ActionResult Match()
		{
			User me = Session["me"] as User;

			List<string> destination = GetPlaces();
			Session["places"] = destination;

			for (int i = 0; i < destination.Count(); i++)
			{
				ViewBag.Place += destination[i];
			}


			List<User> allUsers = db.Users.ToList();
			List<User> matchesList = GetMatches(allUsers, me);
			Session["matches"] = matchesList;

			for (int i = 0; i < matchesList.Count(); i++)
			{
				ViewBag.Matches += matchesList[i];
			}

			return View();
		}

		public List<User> Matches()
		{
			List<User> matches = (List<User>)Session["matches"];

			return matches;
		}

		public List<string> Places()
		{
			List<string> places = (List<string>)Session["places"];

			return places;
		}

		// GET: MomAzure/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: MomAzure/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "FirstName,AgeRange1,AgeRange2,AgeRange3,Gender,Museum,Outdoors,Active,Email,Phone")] User user)
		{
			if (ModelState.IsValid)
			{
				db.Users.Add(user);
				db.SaveChanges();

				Session["me"] = user;

				return RedirectToAction("Match");
			}

			return View(user);
		}

		// GET: MomAzure/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			User user = db.Users.Find(id);
			if (user == null)
			{
				return HttpNotFound();
			}
			return View(user);
		}

		// POST: MomAzure/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "PersonId,FirstName,AgeRange1,AgeRange2,AgeRange3,Gender,Museum,Outdoors,Active,Email,Phone")] User user)
		{
			if (ModelState.IsValid)
			{
				db.Entry(user).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(user);
		}

		// GET: MomAzure/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			User user = db.Users.Find(id);
			if (user == null)
			{
				return HttpNotFound();
			}
			return View(user);
		}

		// POST: MomAzure/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			User user = db.Users.Find(id);
			db.Users.Remove(user);
			db.SaveChanges();
			return RedirectToAction("Index");
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


		public List<string> GetPlaces()
		{
			//string place = p;
			var me = Session["me"] as User;

			bool museum = me.Museum;
			bool park = me.Outdoors;
			bool bowling = me.Active;

			List<string> placesList = new List<string>();

			HttpWebRequest request;

			if (museum)
			{
				request = WebRequest.CreateHttp("http://places.cit.api.here.com/places/v1/discover/search?q=museum&in=42.921%2C-85.5944%3Br%3D45033&Accept-Language=en-US%2Cen%3Bq%3D0.8/application/json&app_id=EVCVXo26lL5LrdGgDEK5&app_code=J9PlnJ5WvHIuxtYvVMwiYw");
				placesList = PlacesList(request);
			}

			if (park)
			{
				request = WebRequest.CreateHttp("http://places.cit.api.here.com/places/v1/discover/search?q=park&in=42.921%2C-85.5944%3Br%3D45033.0&Accept-Language=en-US%2Cen%3Bq%3D0.&app_id=EVCVXo26lL5LrdGgDEK5&app_code=J9PlnJ5WvHIuxtYvVMwiYw");
				placesList.AddRange(PlacesList(request));
			}

			if (bowling)
			{
				request = WebRequest.CreateHttp("http://places.cit.api.here.com/places/v1/discover/search?q=bowling&in=42.921%2C-85.5944%3Br%3D45033&Accept-Language=en-US%2Cen%3Bq%3D0.8/application/json&app_id=EVCVXo26lL5LrdGgDEK5&app_code=J9PlnJ5WvHIuxtYvVMwiYw");
				placesList.AddRange(PlacesList(request));
			}


			return placesList;
		}

		public List<string> PlacesList(HttpWebRequest req)
		{
			//Tell it the list of browsers we're using
			req.UserAgent = @"User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.116 Safari/537.36";

			//push the request over to the remote server 
			HttpWebResponse response = (HttpWebResponse)req.GetResponse();

			//Parse the response data (this looks a lot like reading in a text file, file I/O)
			StreamReader rd = new StreamReader(response.GetResponseStream());

			//Return the data in string format 
			String data = rd.ReadToEnd();
			JObject o = JObject.Parse(data);

			List<string> placesList = new List<string>();
			//List<string> addressList = new List<string>();

			for (int i = 0; i < o["results"]["items"].Count(); i++)
			{
				string destination = o["results"]["items"][i]["title"] + " is located at " + o["results"]["items"][i]["vicinity"];
				destination = destination.Replace("<br/>", ", ");
				placesList.Add(destination);
			}

			return placesList;
		}

		public List<User> GetMatches(List<User> all, User me)
		{
			List<User> matchesList = new List<User>();
			List<User> ageMatch = new List<User>();

			foreach (User u in all)
			{
				if (u.AgeRange1 == me.AgeRange1 && u.AgeRange2 == me.AgeRange2 && u.AgeRange3 == me.AgeRange3)
				{
					ageMatch.Add(u);
				}
			}

			foreach (User u in ageMatch)
			{
				if ((u.Museum == me.Museum) && (u.Outdoors == me.Outdoors) && (u.Active == me.Active) && (u.PersonId != me.PersonId))
				{
					matchesList.Add(u);

				}
			}

			return matchesList;
		}


	}
}

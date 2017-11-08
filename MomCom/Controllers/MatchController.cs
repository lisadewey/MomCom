using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MomCom.Models;

namespace MomCom.Controllers
{
	public class MatchController : Controller
	{
		private DatabaseContext db = new DatabaseContext();

		// GET: Matches
		public ActionResult Index()
		{
			////List<Profile> profiles = db.Profiles.Where(
			//	x => x.Gender == "Girl" &
			//	     x.AgeRange == "0-2" &
			//	     x.Park | x.Playground | x.Pool).ToList(); TODO: insert database query and profile match results...
			//return View(db.Profiles.ToList());
			
			// TODO: Fix this statement so that it actually works...
			// This is dispaying an item it should not be, curently...
			return View(db.Profiles.Where(x => x.Gender == "Girl" &
				x.AgeRange == "0-2" &
				x.Park | x.Playground | x.Pool).ToList());
		}
	}
}

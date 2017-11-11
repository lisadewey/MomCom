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
		private MomComDBEntities1 db = new MomComDBEntities1();

		// GET: Match
		public ActionResult Index()
		{
			var me = Session["me"] as Users;

			bool age1 = me.AgeRange1;
			bool age2 = me.AgeRange2;
			bool age3 = me.AgeRange3;
			bool museum = me.Museum;
			bool active = me.Active;
			bool outdoors = me.Outdoors;

			return View(
				db.Users1.Where(
						x => x.Gender == me.Gender &
							 (x.AgeRange1 == age1 | x.AgeRange2 == age2 | x.AgeRange3 == age3) &
							 (x.Museum == museum | x.Outdoors == outdoors | x.Active == active))
					.ToList());

			// Should be incorporated into a Search View and Controller, which kicks us here.
			// Allows to match any gender, and parent can decide 
			// if they want to go to just a boy, girl, or either event.
			//return View(
			//	db.Users1.Where(
			//			x => (x.AgeRange1 == age1 | x.AgeRange2 == age2 | x.AgeRange3 == age3) &
			//			     (x.Museum == museum | x.Outdoors == outdoors | x.Active == active))
			//		.ToList());
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}

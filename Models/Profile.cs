using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace MomCom.Models
{
	// Table name should be: Profiles
	public class Profile
	{
		[Key]
		public int Id { get; set; }

		public string FirstName { get; set; }
		public string LastName { get; set; }
		// Gender of child
		//public bool Female { get; set; }
		//public bool Male { get; set; }
		//public bool Other { get; set; }

		public string AgeRange { get; set; }
		//// Preferred Age Range of playmate
		//public RangeAttribute(int 0, int 2) { get; set; }
		//public string 3-5 { get; set; }
		//public string 6-8 { get; set; }
		//public string 9-10 { get; set; }


		public string Gender { get; set; }
		// Preferred gender of playmate
		//public bool Girl { get; set; }
		//public bool Boy { get; set; }
		//public bool Either { get; set; }

		public bool Park { get; set; }
		public bool Playground { get; set; }
		public bool Pool { get; set; }

		public string City { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
	}
}
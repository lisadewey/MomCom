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

		public string AgeRange { get; set; }

		public string Gender { get; set; }

		public bool Park { get; set; }
		public bool Playground { get; set; }
		public bool Pool { get; set; }

		public string City { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
	}
}
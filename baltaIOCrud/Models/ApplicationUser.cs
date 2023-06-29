using Microsoft.AspNetCore.Identity;

namespace baltaIOCrud.Models
{
	public class ApplicationUser : IdentityUser
	{
		//the properties bellow will be the new columns in my AspNetUser table
        public string FirstName { get; set; }
		public string LastName { get; set; }

	}
}

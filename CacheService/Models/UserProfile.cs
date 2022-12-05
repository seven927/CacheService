using System.ComponentModel.DataAnnotations;

namespace CacheService.Models
{
    /// <summary>
    /// User profile
    /// </summary>
    public class UserProfile
    {
        /// <summary>
        /// User ID
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Id { get; set; }

        /// <summary>
        /// User's last name
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        /// <summary>
        /// User's first name
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        /// <summary>
        /// User's email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User's phone number
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}

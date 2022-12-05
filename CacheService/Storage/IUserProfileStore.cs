using CacheService.Models;

namespace CacheService.Storage
{
    public interface IUserProfileStore
    {
        /// <summary>
        /// Get a user profile
        /// </summary>
        /// <param name="id">user ID</param>
        /// <returns>User profile</returns>
        public UserProfile GetUser(string id);

        /// <summary>
        /// Add a user profile
        /// </summary>
        /// <param name="user">user profile</param>
        public void AddUser(UserProfile user);

        /// <summary>
        /// Delete a user profile
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>True if the user profile is successfully deleted, otherwise profile for the ID does not exist</returns>
        public bool RemoveUser(string id);
    }
}

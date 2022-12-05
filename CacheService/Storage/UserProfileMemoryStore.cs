using CacheService.Models;

namespace CacheService.Storage
{
    /// <summary>
    /// A memory storage that simulates a persistent storage
    /// </summary>
    public class UserProfileMemoryStore : IUserProfileStore
    {
        private Dictionary<string, UserProfile> _userDictionary;

        public UserProfileMemoryStore() 
        {
            _userDictionary = new Dictionary<string, UserProfile>();
        }

        /// <summary>
        /// Add a user profile
        /// </summary>
        /// <param name="user">user profile</param>
        /// <exception cref="ArgumentException">Thrown when user ID is null or empty</exception>
        public void AddUser(UserProfile user)
        {
            if (string.IsNullOrEmpty(user.Id)) 
            {
                throw new ArgumentException("User ID cannot be empty");
            }
            _userDictionary[user.Id] = user;
        }

        /// <summary>
        /// Get a user profile
        /// </summary>
        /// <param name="id">user ID</param>
        /// <returns>User profile</returns>
        /// <exception cref="ArgumentException">Thrown when user ID is null or empty</exception>
        public UserProfile GetUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("User ID cannot be empty");
            }

            if (_userDictionary.ContainsKey(id)) 
            {
                return _userDictionary[id];
            }
            return null;
        }

        /// <summary>
        /// Delete a user profile
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>True if the user profile is successfully deleted, otherwise profile for the ID does not exist</returns>
        /// <exception cref="ArgumentException">Thrown when user ID is null or empty</exception>
        public bool RemoveUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("User ID cannot be empty");
            }

            if (_userDictionary.ContainsKey(id)) 
            {
                _userDictionary.Remove(id);
                return true;
            }
            return false;
        }
    }
}

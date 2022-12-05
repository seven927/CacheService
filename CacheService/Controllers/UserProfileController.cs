using CacheService.Cache;
using CacheService.Models;
using CacheService.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CacheService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private IUserProfileStore _store;
        private ICacheService<string, UserProfile> _cache;

        public UserProfileController(IUserProfileStore store, ICacheService<string, UserProfile> cache)
        {
            _store = store;
            _cache = cache;
        }

        [HttpGet("{id}")]
        public ActionResult<UserProfile> GetUser(string id)
        {
            UserProfile user = _cache.Get(id);
            if (user == null)
            {
                user = _store.GetUser(id);
                if (user != null)
                {
#if DEBUG
                    System.Diagnostics.Debug.WriteLine("Get user profile from storage");
#endif
                    _cache.Set(id, user);
                }
            }
#if DEBUG
            else 
            {
                System.Diagnostics.Debug.WriteLine("Get user profile from cache");
            }
#endif
            if (user == null) 
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public void AddUser(UserProfile user) 
        {
            _store.AddUser(user);
        }

        [HttpDelete("{id}")]
        public bool RemoveUser(string id) 
        {
            return _store.RemoveUser(id);
        }
    }
}

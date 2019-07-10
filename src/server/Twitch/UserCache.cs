using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using TwitchLib.Api;
using TwitchLib.Api.Helix.Models.Users;

namespace Bivrost.Web.Twitch
{
  public class UserCache
  {

    public IMemoryCache Cache { get; }
    public TwitchAPI Api { get; }
    public ILogger<UserCache> Logger { get; }

    public UserCache(IMemoryCache cache, TwitchAPI api, ILogger<UserCache> logger)
    {
      Cache = cache ?? throw new System.ArgumentNullException(nameof(cache));
      Api = api ?? throw new System.ArgumentNullException(nameof(api));
      Logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
    }

    public Task<User> GetUserAsync(string userId)
    {
      return Cache.GetOrCreateAsync($"user-{userId}", async c => {
        Logger.LogWarning("{@Event}",
          new { Event="User Cache Miss",
                UserId= userId });

        var userResponse = await Api.Helix.Users.GetUsersAsync(
                                  ids: new List<string> { userId });

        return userResponse.Users.First(u => u.Id.Equals(userId));
      });
    }

  }
}

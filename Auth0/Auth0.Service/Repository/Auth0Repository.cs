namespace ICS.Auth0;

public interface IAuth0Repository
{
    public Task ActivateNewIdentity(Identity identity, CancellationToken token);
    public Task<User> AddNewUser(User user, CancellationToken token);
    public Task<User?> GetUser(string email, CancellationToken token);
}

public class Auth0Repository : IAuth0Repository
{
    private readonly IDbContextFactory<Auth0DbContext> _dbContextFactory;
    private readonly ILogger<Auth0Repository> _logger;

    public Auth0Repository(
        IDbContextFactory<Auth0DbContext> dbContextFactory,
        ILogger<Auth0Repository> logger)
    {
        _dbContextFactory = dbContextFactory;
        _logger = logger;
    }

    public async Task ActivateNewIdentity(Identity identity, CancellationToken token)
    {
        await using var dbContext = await _dbContextFactory
            .CreateDbContextAsync(token)
            .ConfigureAwait(false);

        var existingUser = await dbContext.User
            .Include(x => x.Identities)
            .SingleOrDefaultAsync(x => x.UserId == identity.UserId, token)
            .ConfigureAwait(false);

        if (existingUser == null)
        {
            var exception = new Exception($"User {identity.UserId} does not exist");
            _logger.LogError(exception, "User {UserId} was not found, cannot activate identity {Identity}.", identity.UserId, identity.ProviderIdentity);
            throw exception;
        }

        var existingIdentity = existingUser.Identities.FirstOrDefault(x => x.Provider == identity.Provider);

        if (existingIdentity != null)
        {
            if (existingIdentity.ProviderId != identity.ProviderId)
            {
                var exception = new Exception($"Provider {identity.ProviderId} was previously added with a different identifier.");
                _logger.LogError(exception, "ProviderId {ExistingId} and {NewId} do not match.", existingIdentity.ProviderId, identity.ProviderId);
                throw exception;
            }

            _logger.LogInformation("Previously activated {ProviderIdentity}.", identity.ProviderIdentity);
            return;
        }

        await dbContext.Identity.AddAsync(identity, token).ConfigureAwait(false);

        await dbContext.SaveChangesAsync(token).ConfigureAwait(false);
    }

    public async Task<User> AddNewUser(User user, CancellationToken token)
    {
        await using var dbContext = await _dbContextFactory
            .CreateDbContextAsync(token)
            .ConfigureAwait(false);

        try
        {
            var existingUser = await dbContext.User
                .Include(x => x.Identities)
                .SingleOrDefaultAsync(x => x.Email == user.Email, token)
                .ConfigureAwait(false);

            if (existingUser != null)
            {
                _logger.LogInformation("User {UserId} with email {Email} was previously added.", existingUser.UserId, existingUser.Email);
                return existingUser;
            }

            await dbContext.User.AddAsync(user, token).ConfigureAwait(false);

            await dbContext.SaveChangesAsync(token).ConfigureAwait(false);

            return user;
        }
        catch (DbUpdateException ex)
            when (UniqueConstraintViolationException.IsMatch(ex))
        {
            _logger.LogError(ex, "User {UserId} already exists.", user.UserId);
            throw new UniqueConstraintViolationException(nameof(dbContext.User), ex);
        }
        catch (DbUpdateException ex)
            when (UniqueIndexViolationException.IsMatch(ex))
        {
            _logger.LogError(ex, "User {Email} already exists.", user.Email);
            throw new UniqueIndexViolationException(nameof(dbContext.User), ex);
        }
    }

    public async Task<User?> GetUser(string email, CancellationToken token)
    {
        await using var dbContext = await _dbContextFactory
            .CreateDbContextAsync(token)
            .ConfigureAwait(false);

        var existingUser = await dbContext.User
            .Include(x => x.Identities)
            .SingleOrDefaultAsync(x => x.Email == email, token)
            .ConfigureAwait(false);

        return existingUser;
    }

}

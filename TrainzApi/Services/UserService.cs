namespace TrainzApi.Services
{
    public class UserService : IUserService
    {
        public bool ValidateCredentials(string? username, string? password)
        {
            if (username == null || password == null) return false;
            return username.Equals("admin", StringComparison.InvariantCultureIgnoreCase) && password.Equals("12345");
        }
    }
}

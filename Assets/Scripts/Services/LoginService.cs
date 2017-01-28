using Character;

namespace Services
{
    public class LoginService
    {
        private readonly PlayerContext _playerContext;

        public LoginService(PlayerContext playerContext)
        {
            _playerContext = playerContext;
            _playerContext.TokenInfo = "";
        }
    }
}
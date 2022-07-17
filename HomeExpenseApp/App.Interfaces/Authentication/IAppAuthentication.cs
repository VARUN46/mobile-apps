using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace App.Interfaces.Authentication
{
    public interface IAppAuthentication
    {
        Task<string> LoginWithGoogle();

        bool SignOut();

        bool IsSignIn();
    }
}

namespace Zigmo.Secucard.Connect.NetCore.Auth.Model
{
    public class AnonymousCredentials : OAuthCredentials
    {
        public override string Id
        {
            get { return "anonymous"; }
        }
    }
}
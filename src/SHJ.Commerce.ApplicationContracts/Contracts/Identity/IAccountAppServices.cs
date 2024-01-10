namespace SHJ.Commerce.ApplicationContracts.Contracts.Identity;

public interface IAccountAppServices
{
    Task SignUp(SignUp input);
    Task SignIn(SignIn input);
    Task SignOut();
}

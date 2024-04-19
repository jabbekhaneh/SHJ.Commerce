using System.ComponentModel;

namespace SHJ.Commerce.Domain;

public static class GlobalDomainErrors
{
    public const string Name = "Commerce.ERROR: ";
    public const string DublicatePageTitle = $"{Name}6001";
}
public static class GlobalIdentityErrors
{
    public const string Name = "Commerce.Identity.ERROR : ";
    public const string AccessDenied = $"{Name}5001";
    public const string IsLockedOut = $"{Name}5002";
    public const string IsNotAllowed = $"{Name}5003";
    public const string RequiresTwoFactor = $"{Name}5004";
    public const string DublicationPersissionName= $"{Name}5005";
    public const string UserNotFound = $"{Name}5006";
}
public static class GlobalCmsErrors
{
    public const string Name = "Commerce.Cms.ERROR: ";
}

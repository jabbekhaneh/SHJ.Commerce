using System.ComponentModel;

namespace SHJ.Commerce.Domain;

public static class GlobalDomainErrors
{
    public const string Name = "Commerce.ERROR: ";
    public const string DublicatePageTitle = $"{Name}5050";
}
public static class GlobalIdentityErrors
{
    public static string Name = "Commerce.Identity.ERROR :";
    public static string AccessDenied = $"{Name}5001";
    public static string IsLockedOut = $"{Name}5002";
    public static string IsNotAllowed = $"{Name}5003";
    public static string RequiresTwoFactor = $"{Name}5004";
    public static string DublicationPersissionName = $"{Name}5005";
    public static string UserNotFound = $"{Name}5006";
    public static string DublicationEmail = $"{Name}5007";
}
public static class GlobalCmsErrors
{
    public static string Name = "Commerce.Cms.ERROR: ";
}

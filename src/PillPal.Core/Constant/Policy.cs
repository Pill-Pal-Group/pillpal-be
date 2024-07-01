namespace PillPal.Core.Constant;

public static class Policy
{
    /// <summary>
    /// Require to have administrative power, either admin or manager role
    /// </summary>
    public const string Administrative = nameof(Administrative);

    /// <summary>
    /// Require to have admin role
    /// </summary>
    public const string Admin = nameof(Admin);

    /// <summary>
    /// Require to have manager role
    /// </summary>
    public const string Manager = nameof(Manager);

    /// <summary>
    /// Require to have customer role
    /// </summary>
    public const string Customer = nameof(Customer);
}

namespace Application.Commons.Models
{
    public class RefreshSecurityToken
    {
        public string? Id { get; set; }
        public string? JwtSecurityToken { get; set; }
        public DateTime Expire { get; set; }
        public bool IsExpired => DateTime.Now >= Expire;
        public DateTime Created { get; set; }
        public string? CreatedByIp { get; set; }
        public DateTime? Revoked { get; set; }
        public string? RevokedByIp { get; set; }
        public string? ReplacedBySecurityToken { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;
    }
}

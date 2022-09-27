namespace secureBridge_Services.Services.EncryptionServices
{
    public interface IEncryptionService
    {
        string PasswordDecode(string input);
        string PasswordEncode(string input);
    }
}
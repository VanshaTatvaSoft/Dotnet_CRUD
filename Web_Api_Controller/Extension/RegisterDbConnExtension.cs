using Microsoft.EntityFrameworkCore;
using Web_Api_Controller.AesEncryption;
using Web_Api_Repository.Models;

namespace Web_Api_Controller.Extension;

public static class RegisterDbConnExtension
{
    public static IServiceCollection RegisterDbConnection(this IServiceCollection services, IConfiguration configuration)
    {
        string conn = configuration.GetConnectionString("Default");
        string key = configuration["Encryption:DbEncryptionKey"]; 
        string decryptedConn = AesEncrypter.Decrypt(conn, key);
        services.AddDbContext<ProductManagementContext>(options =>
                options.UseNpgsql(decryptedConn));

        return services;
    }
}

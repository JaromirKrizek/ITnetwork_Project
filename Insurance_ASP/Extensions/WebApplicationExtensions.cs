using Microsoft.AspNetCore.Identity;

namespace Insurance_ASP.Extensions
{
    //#############################################################################################
    public enum AppMode { Anonymous, User, Admin }

    //#############################################################################################
    public static class WebApplicationExtensions  
    {
        //-----------------------------------------------------------------------------------------
        // Registruje uživatele s rolí Admin
        // Nejprve pomocí metody CreateScope() získáme závislosti tříd RoleManager a UserManager z
        // námi registrovaných služeb v aplikaci.
        // Poté ověříme existenci role Admin. Pokud neexistuje, vytvoříme ji.
        // Poté ověříme existenci uživatele pomocí metody FindByEmailAsync().
        // Pokud neexistuje, vytvoříme nového uživatele metodou CreateUser().
        // Nakonec ověříme, zda uživatel user je přiřazen k roli Admin a pokud není, přiřadíme ho.
        //-----------------------------------------------------------------------------------------
        public static async Task RegisterAdmin(this WebApplication webApplication, 
                                               string userEmail, string userPassword)
        {
            var adminRoleName = "Admin";

            // Nejprve vytváříme nový rámec.
            // Rámec vytváříme v bloku using, aby byl po provedení požadované práce zneplatněn a
            // co nejdříve uvolněn z paměti:
            using (var scope = webApplication.Services.CreateScope())
            {
                // Z rámce následně vytahujeme potřebné služby metodou GetRequiredService():
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                // Ověříme existenci role Admin. Pokud neexistuje, vytvoříme ji.
                // Do databázové tabulky dbo.AspNetRoles se tím vloží nový záznam.
                if (!await roleManager.RoleExistsAsync(adminRoleName))
                    await roleManager.CreateAsync(new IdentityRole(adminRoleName));

                // Podle e-mailu nalézáme uživatele, kterému chceme udělit administrátorskou roli:
                IdentityUser user = await userManager.FindByEmailAsync(userEmail);

                // Pokud takový uživatel neexistuje, vytvoříme ho metodou CreateUser().
                if (user is null)
                    user = await CreateUser(userManager, userEmail, userPassword);

                // Ověříme, zda uživatel user je přiřazen k roli Admin a pokud není, přiřadíme ho.
                // Do databázové vazební tabulky dbo.AspNetUserRoles se tím vloží nový záznam.
                if (!await userManager.IsInRoleAsync(user, adminRoleName))
                    await userManager.AddToRoleAsync(user, adminRoleName);
            }
        }

        //-----------------------------------------------------------------------------------------
        // Pomocí této metody budeme vytvářet uživatele
        //-----------------------------------------------------------------------------------------
        private static async Task<IdentityUser> CreateUser(UserManager<IdentityUser> userManager, 
                                                           string userEmail, string password)
        {
            IdentityUser user = null;
            var result = await userManager.CreateAsync(new IdentityUser { UserName = userEmail, Email = userEmail }, password);
            if (result.Succeeded)
            {
                user = await userManager.FindByEmailAsync(userEmail);
            }

            return user;
        }

        //-----------------------------------------------------------------------------------------
        public static AppMode GetApplicationMode()
        {
            return AppMode.Anonymous;
        }


    }
}

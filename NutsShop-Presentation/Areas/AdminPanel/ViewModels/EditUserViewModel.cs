using Application.Dtos.RoleDTO;
using Application.Dtos.UserLogInDTO;

namespace NutsShop_Presentation.Areas.AdminPanel.ViewModels
{
    public class EditUserViewModel
    {
        public UserAdminPanelDTO? UserAdminPanelDTO { get; set; }
        public List<RoleDTO>? Roles { get; set; }




    }
}

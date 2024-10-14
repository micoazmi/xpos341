using xpos341.datamodels;
using xpos341.viewmodels;

namespace xpos341.api.Services
{
    public class RolesService
    {
        private Xpos341Context db;
        private VMResponse response = new VMResponse();

        public RolesService(Xpos341Context db)
        {
            this.db = db;
        }

        public async Task<List<VMMenuAccess>> GetMenuAccessParentChildByRoleID(int IdRole, int MenuParent, bool OnlySelected = false)
        {
            List<VMMenuAccess> result = new List<VMMenuAccess>();
            List<TblMenu> data = db.TblMenus.Where(a => a.MenuParent == MenuParent && a.IsDelete == false).ToList();
            foreach (TblMenu item in data)
            {
                VMMenuAccess list = new VMMenuAccess();
                list.IdMenu = item.Id;
                list.MenuName = item.MenuName;
                list.IsParent = item.IsParent;
                list.MenuParent = item.MenuParent;
                list.is_selected = db.TblMenuAccesses.Where(a => a.RoleId == IdRole && a.MenuId == item.Id && a.IsDelete == false).Any();
                list.ListChild = await GetMenuAccessParentChildByRoleID(IdRole, item.Id, OnlySelected);

                if(OnlySelected)
                {
                    if(list.is_selected)
                    {
                        result.Add(list);
                    }
                }
                else
                {
                    result.Add(list);
                }
            }

            return result;

        }

    }
}

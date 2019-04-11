using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Models.ViewModel.Admin
{
    public class VSidebarMenu
    {
        public VLoggedModel User { get; set; }

        public List<VMenu> Menus { get; set; }

        public const string menusKey = "GeneralCMS_Menus";
    }


    public class VMenu
    {
        public string Name { get; set; }
        public string Icon { get; set; }

        public string Route { get; set; }

        public List<VMenu> Childs { get; set; } = new List<VMenu>();
        public VMenu()
        { }

        public VMenu(string Name,string Icon, string Route)
        {
            this.Name = Name;
            this.Icon = Icon;
            this.Route = Route;
        }
    }
}

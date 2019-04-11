using GeneralCMS.Models.ViewModel.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GeneralCMS.Admin.ViewComponents
{
    public class LeftSidebarViewComponent : BaseViewComponent
    {
        private readonly IMemoryCache memoryCache;
        public LeftSidebarViewComponent(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public override IViewComponentResult Invoke()
        {
            VSidebarMenu menus = new VSidebarMenu(); 
            menus.User = CurrentUser;
            
            List<VMenu> cache;
            if (!memoryCache.TryGetValue(VSidebarMenu.menusKey, out cache))
            {
                //首次启动，会加载菜单,然后保存到内存中
                menus.Menus = GetMenus();
                //设置缓存回收优先级 （程序压力大时，会根据优先级自动回收）
                memoryCache.Set(VSidebarMenu.menusKey, menus.Menus, new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.High));
            }
            else
            {
                menus.Menus = cache;
            } 
            return View(menus);
        }

        private List<VMenu> GetMenus()
        {
            List<VMenu> res = new List<VMenu>();
            VMenu menu = new VMenu("首页", "fa-home", "/Home/Welcome");
            res.Add(menu);

            menu = new VMenu("内容管理", "fa-window-restore", "#");
            menu.Childs.Add(new VMenu("分类管理", "fa-picture-o", "/PageMng/Setting1"));
            menu.Childs.Add(new VMenu("文章管理", "fa-picture-o", "/PageMng/Slideshow2"));
            menu.Childs.Add(new VMenu("招聘管理", "fa-picture-o", "/PageMng/Navigation3"));
            menu.Childs.Add(new VMenu("单页管理", "fa-file-text-o", "/PageMng/Friendlink4"));
            res.Add(menu);

            menu = new VMenu("页面管理", "fa-window-restore", "#");
            menu.Childs.Add(new VMenu("轮播图管理", "fa-picture-o", "/PageMng/Slideshow"));
            menu.Childs.Add(new VMenu("导航管理", "fa-picture-o", "/PageMng/Navigation"));
            menu.Childs.Add(new VMenu("友情链接", "fa-file-text-o", "/PageMng/Friendlink"));
            res.Add(menu);

            menu = new VMenu("系统管理", "fa-cogs", "#"); 
            menu.Childs.Add(new VMenu("管理员", "fa-users", "/SystemMng/UserList"));
            menu.Childs.Add(new VMenu("系统设置", "fa-cog", "/SystemMng/Setting"));
            menu.Childs.Add(new VMenu("操作日志", "fa-file-text-o", "/SystemMng/Logs"));
            res.Add(menu);

            return res;
        }
    }
}

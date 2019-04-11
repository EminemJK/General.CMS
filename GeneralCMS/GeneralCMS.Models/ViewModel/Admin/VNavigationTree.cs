using System;
using System.Collections.Generic;
using System.Text;
using GeneralCMS.Models.Dto;

namespace GeneralCMS.Models.ViewModel.Admin
{
    public class VNavigationTree
    {
        public List<VNavigationTreeItem> items { get; set; } = new List<VNavigationTreeItem>();
    }

    public class VNavigationTreeItem
    {
        public int navId { get; set; }

        public string text  { get; set; }

        public List<VNavigationTreeItem> nodes { get; set; } = new List<VNavigationTreeItem>();
    }
}

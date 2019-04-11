using Banana.Utility.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Models.Enum
{
    public enum EContentType
    {
        [EnumDescription("图文形式")]
        ImageText,

        [EnumDescription("列表形式")]
        PageList,

        [EnumDescription("图标形式")]
        IconList,

        [EnumDescription("富文本形式")]
        RichText
    }
}

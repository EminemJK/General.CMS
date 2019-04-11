using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SQLite;
using Banana.Uow;
using Dapper;
using GeneralCMS.Models.Entities;
using GeneralCMS.Models.Enum;
using System.Threading.Tasks;
using System.IO;
using GeneralCMS.Application.Common;
using System.Text;

namespace ConsoleAppSqlite
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists("LocadDB.Sqlite"))
            {
                File.Delete("LocadDB.Sqlite");
            }
            SQLiteConnection.CreateFile(@"LocadDB.Sqlite");

            ConnectionBuilder.ConfigRegist("Data Source=LocadDB.Sqlite;Version=3;", Banana.Uow.Models.DBType.SQLite);
            int idx = 1;
            Show(ref idx, "初始化");
            InitDb();

            Show(ref idx, "分类表");
            CreateTestData_Category();
            Show(ref idx, "导航表");
            CreateTestData_NavigationInfo();
            Show(ref idx, "友情链接表");
            CreateTestData_FriendlyLinkInfo();
            Show(ref idx, "图片轮播表");
            CreateTestData_ImagePlay();
            Show(ref idx, "新闻表");
            CreateTestData_NewsInfo();
            Show(ref idx, "网站主体表");
            CreateTestData_OwnerInfo();
            Show(ref idx, "内容表");
            CreateTestData_PageContentInfo();
            Show(ref idx, "招聘表");
            CreateTestData_RecruitmentInfo();
            Show(ref idx, "其他配置表");
            CreateTestData_SystemConfig();
            Show(ref idx, "用户表");
            CreateTestData_User();
            Show(ref idx, "测试数据加入完成");

            Show(ref idx, "请把【LocadDB.Sqlite】数据库文件复制到【GeneralCMS.Admin】和【GeneralCMS.Web】下以便测试效果");
            System.Threading.Thread.Sleep(5000);
             
        }

        static void Show(ref int idx, string msg)
        {
            Console.WriteLine(idx++ + " " + msg + " ...");
        }
        static void InitDb()
        {
            var dbConnection = ConnectionBuilder.CreateConnection();
            string sql = @"CREATE TABLE T_Admin(
	                            ID INTEGER PRIMARY KEY,
	                            UserName VARCHAR(32) NOT NULL,
	                            Password VARCHAR(32) NOT NULL,
	                            Mobile VARCHAR(11),
	                            Email VARCHAR(200),
	                            Realname NVARCHAR(50),
	                            IsDisable INT DEFAULT(0),
	                            CreateTime DATETIME NOT NULL DEFAULT(GETDATE())
                            )";
            dbConnection.Execute(sql);

            sql = @"CREATE TABLE T_AdminLog(
	                            ID INTEGER PRIMARY KEY,
	                            AdminID INT NOT NULL,
	                            Title NVARCHAR(255),
	                            Content Text,
	                            IP VARCHAR(50),
	                            CreateTime DATETIME NOT NULL DEFAULT(GETDATE())
                            )";
            dbConnection.Execute(sql);

            sql = @"CREATE TABLE T_SystemConfig(
	                            ID INTEGER PRIMARY KEY,
	                            Name NVARCHAR(50),
	                            Code VARCHAR(50) NOT NULL,
	                            Value Text,
	                            CreateTime DATETIME NOT NULL DEFAULT(GETDATE())
                            )";
            dbConnection.Execute(sql);

            sql = @"CREATE TABLE T_ImgPlay(
	                            ID INTEGER PRIMARY KEY,
	                            NavigationID INT NOT NULL,
	                            Type INT NOT NULL DEFAULT(0),
	                            Name NVARCHAR(500),
	                            Title NVARCHAR(500),
                                Subhead nVARCHAR(20),
	                            ImgUrl NVARCHAR(500),
	                            LinkUrl NVARCHAR(500),
	                            Target INT NOT NULL DEFAULT(0),
	                            IsDisable INT DEFAULT(0),
	                            Sort INT NOT NULL DEFAULT(0),
	                            CreateTime DATETIME NOT NULL DEFAULT(GETDATE())
                            )";
            dbConnection.Execute(sql);

            sql = @"CREATE TABLE T_FriendlyLink(
	                            ID INTEGER PRIMARY KEY,
	                            Type INT NOT NULL DEFAULT(0),
	                            Name NVARCHAR(500),
	                            Title NVARCHAR(500),
	                            ImgUrl NVARCHAR(500),
	                            LinkUrl NVARCHAR(500),
	                            Target INT NOT NULL DEFAULT(0),
	                            IsDisable INT DEFAULT(0),
	                            Sort INT NOT NULL DEFAULT(0),
	                            CreateTime DATETIME NOT NULL DEFAULT(GETDATE())
                            )";
            dbConnection.Execute(sql);

            sql = @"CREATE TABLE T_Navigation(
	                            ID INTEGER PRIMARY KEY,
	                            Type INT NOT NULL DEFAULT(0),
	                            ParentId INT NOT NULL,
	                            Name NVARCHAR(500),
	                            Url NVARCHAR(500),
	                            Target INT NOT NULL DEFAULT(0),
	                            IsDisable INT DEFAULT(0),
	                            CanDisable INT DEFAULT(0),
	                            Sort INT NOT NULL DEFAULT(0),
	                            CreateTime DATETIME NOT NULL DEFAULT(GETDATE())
                        )";
            dbConnection.Execute(sql);

            sql = @"CREATE TABLE T_Category(
	                            ID INTEGER PRIMARY KEY,
	                            ParentId INT NOT NULL,
	                            ParentIdPath NVARCHAR(255) NOT NULL,
	                            Name NVARCHAR(500),
	                            ImgUrl NVARCHAR(500),
	                            IsDisable INT DEFAULT(0),
	                            Sort INT NOT NULL DEFAULT(0),
	                            CreateTime DATETIME NOT NULL DEFAULT(GETDATE())
                        )";
            dbConnection.Execute(sql);

            sql = @"CREATE TABLE T_News(
	                        ID INTEGER PRIMARY KEY,
	                        CategoryId INT NOT NULL,

	                        Title NVARCHAR(500),
	                        ImgUrl NVARCHAR(500),
	                        Author NVARCHAR(50),
	                        Summary NVARCHAR(500),
	                        Source NVARCHAR(500),
	                        Content Text,
	                        ReleaseTime DATETIME,
	                        ViewCount BIGINT NOT NULL DEFAULT(0),
	                        MetaKeywords NVARCHAR(500),
	                        MetaDescription  NVARCHAR(500),
	 
	                        IsDisable INT DEFAULT(0),
	                        Sort INT NOT NULL DEFAULT(0),
	                        CreateTime DATETIME NOT NULL DEFAULT(GETDATE()),
	                        CreateAdminId INT NOT NULL DEFAULT(0),
	                        UpdateTime DATETIME NOT NULL,
	                        UpdateAdminId  INT NOT NULL DEFAULT(0)
                        )";
            dbConnection.Execute(sql);

            sql = @"CREATE TABLE T_PageContent(
	                        ID INTEGER PRIMARY KEY,
                            ContentType INT NOT NULL DEFAULT(0),
	                        NavigationID INT NOT NULL,
	                        Title nVARCHAR(500),
	                        Content Text,
	                        ImgUrl NVARCHAR(500),
	                        IconCode VARCHAR(100),
                            IsHomePageShows INT NOT NULL DEFAULT(0),
	                        SectionName nVARCHAR(20),
	                        Subhead nVARCHAR(20),
	                        IsDisable INT DEFAULT(0),
	                        Sort INT NOT NULL DEFAULT(0),
	                        CreateTime DATETIME NOT NULL DEFAULT(GETDATE()),
	                        UpdateTime DATETIME NOT NULL
                        )";
            dbConnection.Execute(sql);

            sql = @"CREATE TABLE T_Recruitment(
	                        ID INTEGER PRIMARY KEY,
	                        JobTitle nVARCHAR(100),
	                        Location nVARCHAR(20),
	                        Salary VARCHAR(20),
	                        RecruitingNum INT,
	                        JobDescription Text,

	                        Sort INT NOT NULL DEFAULT(0),

	                        CreateTime DATETIME NOT NULL DEFAULT(GETDATE()),
	                        CreateAdminId INT NOT NULL DEFAULT(0),
	                        UpdateTime DATETIME NOT NULL,
	                        UpdateAdminId  INT NOT NULL DEFAULT(0)
                        )";
            dbConnection.Execute(sql);

            sql = @"CREATE TABLE T_OwnerInfo(
	                        ID INTEGER PRIMARY KEY,
	                        Name NVARCHAR(500),
                            LinkMan VARCHAR(200),
	                        Email VARCHAR(200),
	                        Mobile NVARCHAR(500),
	                        Address  NVARCHAR(500),
	                        MapIFrameUrl Text,
	                        CreateTime DATETIME NOT NULL DEFAULT(GETDATE())
                        )";
            dbConnection.Execute(sql);
        }


        static void CreateTestData_Category()
        {
            Repository<CategoryInfo> repository = new Repository<CategoryInfo>();
            int idx = 0;
            CategoryInfo category = new CategoryInfo()
            {
                ParentId = 0,
                ParentIdPath = ",0,",
                Name = "企业动态",
                ImgUrl = "",
                IsDisable = EYesOrNo.No,
                Sort = idx++,
                CreateTime = DateTime.Now
            };
            repository.Insert(category);
            category = new CategoryInfo()
            {
                ParentId = 0,
                ParentIdPath = ",1,",
                Name = "通知通告",
                ImgUrl = "",
                IsDisable = EYesOrNo.No,
                Sort = idx++,
                CreateTime = DateTime.Now
            };
            repository.Insert(category);
            category = new CategoryInfo()
            {
                ParentId = 0,
                ParentIdPath = ",2,",
                Name = "其他文件",
                ImgUrl = "",
                IsDisable = EYesOrNo.No,
                Sort = idx++,
                CreateTime = DateTime.Now
            };
            repository.Insert(category);
        }

        static void CreateTestData_NavigationInfo()
        {
            Repository<NavigationInfo> repository = new Repository<NavigationInfo>();
            int idx = 0;
            NavigationInfo navigationInfo = new NavigationInfo()
            {
                ParentId = 0,
                Name = "首页",
                Url = "/",
                Target = EYesOrNo.No,
                CanDisable = EYesOrNo.No,
                IsDisable = EYesOrNo.No,
                Sort = idx++
            };
            repository.Insert(navigationInfo);
            navigationInfo = new NavigationInfo()
            {
                ParentId = 0,
                Name = "关于我们",
                Url = "/Home/About",
                Target = EYesOrNo.No,
                CanDisable = EYesOrNo.No,
                IsDisable = EYesOrNo.No,
                Sort = idx++
            };
            var aboutId = (int)repository.Insert(navigationInfo); 
            navigationInfo = new NavigationInfo()
            {
                ParentId = aboutId,
                Name = "公司动态",
                Url = "/News/List",
                Target = EYesOrNo.No,
                CanDisable = EYesOrNo.No,
                IsDisable = EYesOrNo.No,
                Sort = idx++
            };
            repository.Insert(navigationInfo);
            navigationInfo = new NavigationInfo()
            {
                ParentId = 0,
                Name = "产品服务",
                Url = "/Page/List",
                Target = EYesOrNo.No,
                CanDisable = EYesOrNo.No,
                IsDisable = EYesOrNo.No,
                Sort = idx++
            };
            var pid = (int)repository.Insert(navigationInfo);
            navigationInfo = new NavigationInfo()
            {
                ParentId = 0,
                Name = "招贤纳士",
                Url = "/Employment/List",
                Target = EYesOrNo.No,
                CanDisable = EYesOrNo.No,
                IsDisable = EYesOrNo.No,
                Sort = idx++
            };
            repository.Insert(navigationInfo);
            navigationInfo = new NavigationInfo()
            {
                ParentId = 0,
                Name = "联系我们",
                Url = "/Contact/Index",
                Target = EYesOrNo.No,
                CanDisable = EYesOrNo.No,
                IsDisable = EYesOrNo.No,
                Sort = idx++
            };
            repository.Insert(navigationInfo);

            navigationInfo = new NavigationInfo()
            {
                ParentId = pid,
                Name = "测试分页",
                Url = "/Page/List",
                Target = EYesOrNo.No,
                CanDisable = EYesOrNo.No,
                IsDisable = EYesOrNo.No,
                Sort = idx++
            };
            repository.Insert(navigationInfo);
            navigationInfo = new NavigationInfo()
            {
                ParentId = pid,
                Name = "测试图文列表",
                Url = "/Page/List",
                Target = EYesOrNo.No,
                CanDisable = EYesOrNo.No,
                IsDisable = EYesOrNo.No,
                Sort = idx++
            };
            repository.Insert(navigationInfo);
        }

        static void CreateTestData_FriendlyLinkInfo()
        {
            Repository<FriendlyLinkInfo> repository = new Repository<FriendlyLinkInfo>();
            int idx = 0;
            FriendlyLinkInfo friendlyLinkInfo = new FriendlyLinkInfo()
            {
                ImgUrl = "",
                Name = "谷歌",
                LinkUrl = "http://www.google.cn/",
                IsDisable = EYesOrNo.No,
                Sort = idx++,
                Target = EYesOrNo.No,
                Title = "",
                Type = 0
            };
            repository.Insert(friendlyLinkInfo);
            friendlyLinkInfo = new FriendlyLinkInfo()
            {
                ImgUrl = "",
                Name = "百度",
                LinkUrl = "https://www.baidu.com",
                IsDisable = EYesOrNo.No,
                Sort = idx++,
                Target = EYesOrNo.No,
                Title = "",
                Type = 0
            };
            repository.Insert(friendlyLinkInfo);
            friendlyLinkInfo = new FriendlyLinkInfo()
            {
                ImgUrl = "",
                Name = "Github",
                LinkUrl = "https://github.com",
                IsDisable = EYesOrNo.No,
                Sort = idx++,
                Target = EYesOrNo.No,
                Title = "",
                Type = 0
            };
            repository.Insert(friendlyLinkInfo);
            friendlyLinkInfo = new FriendlyLinkInfo()
            {
                ImgUrl = "",
                Name = "码云",
                LinkUrl = "https://git.oschina.net/",
                IsDisable = EYesOrNo.No,
                Sort = idx++,
                Target = EYesOrNo.No,
                Title = "",
                Type = 0
            };
            repository.Insert(friendlyLinkInfo);
        }

        static void CreateTestData_ImagePlay()
        {
            Repository<ImgPlayInfo> repository = new Repository<ImgPlayInfo>();
            int idx = 0;
            var ser = GetNavigationInfo("首页");
            ImgPlayInfo imgPlayInfo = new ImgPlayInfo()
            {
                NavigationID = ser.Id,
                Type = EImageType.Header,
                Name = "首页轮播",
                Title = "专注成就价值",
                Subhead = "年轻人，你对力量一无所知",
                ImgUrl = @"/images/bg-img/1.jpg",
                LinkUrl = "/Home/About",
                Target = EYesOrNo.No,
                IsDisable = EYesOrNo.No,
                Sort = idx++
            };
            repository.Insert(imgPlayInfo);
            imgPlayInfo = new ImgPlayInfo()
            {
                NavigationID = ser.Id,
                Type = EImageType.Header,
                Name = "首页轮播",
                Title = "你知道吗？",
                Subhead = "其实女性更沉迷手机上网",
                ImgUrl = @"/images/bg-img/2.jpg",
                LinkUrl = "/Home/About",
                Target = EYesOrNo.No,
                IsDisable = EYesOrNo.No,
                Sort = idx++
            };
            repository.Insert(imgPlayInfo);
            imgPlayInfo = new ImgPlayInfo()
            {
                NavigationID = ser.Id,
                Type = EImageType.Header,
                Name = "首页轮播",
                Title = "1+1=？",
                Subhead = "等于我们",
                ImgUrl = @"/images/bg-img/3.jpg",
                LinkUrl = "/Home/About",
                Target = EYesOrNo.No,
                IsDisable = EYesOrNo.No,
                Sort = idx++
            };
            repository.Insert(imgPlayInfo);
            imgPlayInfo = new ImgPlayInfo()
            {
                NavigationID = ser.Id,
                Type =  EImageType.Footer,
                Name = "底部",
                Title = "底部",
                Subhead = "底部",
                ImgUrl = @"/images/bg-img/3.jpg",
                LinkUrl = "/Home/About",
                Target = EYesOrNo.No,
                IsDisable = EYesOrNo.No,
                
                Sort = idx++
            };
            repository.Insert(imgPlayInfo);

            Repository<NavigationInfo> navRepo = new Repository<NavigationInfo>();
            var m = GetNavigationInfo("招贤纳士");
            imgPlayInfo = new ImgPlayInfo()
            {
                NavigationID = m.Id,
                Type = EImageType.Header,
                Name = "招贤纳士banner",
                Title = "Employment",
                Subhead = "加入我们",
                ImgUrl = @"/images/bg-img/defaultEmployment.jpg",
                LinkUrl = "#",
                Target = EYesOrNo.No,
                IsDisable = EYesOrNo.No,
                Sort = idx++
            };
            repository.Insert(imgPlayInfo);
        }

        static void CreateTestData_NewsInfo()
        {
            Repository<NewsInfo> repository = new Repository<NewsInfo>();
            for (int i = 0; i < 100; i++)
            {
                NewsInfo imgPlayInfo = new NewsInfo()
                {
                    CategoryId = 0,
                    Title = "文章标题_" + i,
                    ImgUrl = "/images/bg-img/defaultNewCover.png",
                    Author = "山治",
                    Summary = "这是是简介，这是是简介，这是是简介，这是是简介，这是是简介，这是是简介",
                    Content = "<p>不信自己百度<p>",
                    Source = "本网自产",
                    ReleaseTime = DateTime.Now,
                    ViewCount = 0,
                    IsDisable = EYesOrNo.No,
                    Sort = i,
                    CreateAdminId = 1,
                    UpdateAdminId = 1,
                    UpdateTime = DateTime.Now
                };
                repository.Insert(imgPlayInfo);
            }
        }

        static void CreateTestData_OwnerInfo()
        {
            Repository<OwnerInfo> repository = new Repository<OwnerInfo>();
            OwnerInfo ownerInfo = new OwnerInfo()
            {
                Name = "Banana科技公司",
                LinkMan = "肖小姐、罗小姐",
                Address = "广西壮族自治区南宁市青秀区",
                Email = "lio.huang@qq.com",
                Mobile = "0771-6666666 / 0771-8888888",
                //访问这里可以设置 https://lbs.amap.com/console/show
                MapIFrameUrl = "http://f.amap.com/3wPSG_0B874AL"
            };
            repository.Insert(ownerInfo);
        }

        static void CreateTestData_PageContentInfo()
        {
            Repository<PageContentInfo> repository = new Repository<PageContentInfo>();

            int idx = 0;
            PageContentInfo pageContentInfo = new PageContentInfo();
            var ser = GetNavigationInfo("关于我们");
            pageContentInfo = new PageContentInfo()
            {
                NavigationID = ser.Id,
                Title = "公司介绍",
                Content = @"<p style=\""text - indent:2em\"">
                            <strong> Banana科技股份公司 </strong> 坐落于美丽的绿城南宁，是广西首家致力于发展“互联网 +”水果行业的科技企业，由广西规模最大的国家甲级水果设计单位——<a href = \""http://www.baidu.com\\"" target = \""_blank\""> 水果集团 </a> 牵头组建。集团是由19家专业公司和机构共同组成，在工程设计咨询、工程总承包、投资与房地产开发、智力运动与文化产业领域开展经营，致力于成为卓越的城乡建设领域综合服务提供商。
                        </p>
                        <p style = \""text -indent:2em\"">
                            Banana公司依托集团在设计咨询、工程总包、投资与房地产开发等领域的深厚积累，利用互联网、IT技术打造行业信息服务新模式，专注于建筑行业产业链上下游的互联互通，推动产业的跨界融合创新，创造建筑行业新生态圈。       公司现诚邀有识青年、互联网爱好者、IT精英加盟，我们将为您提供广阔的发展平台、富有竞争力的薪酬福利及完善的培训体系。带上你的学识、创意和激情加入Banana，我们一起携手共进、共创“专筑”未来！
                        </p>",
                ImgUrl = "/images/bg-img/nanguoyiyuan.jpg",
                IconCode = "",
                IsDisable = EYesOrNo.No,
                Sort = idx++,
                UpdateTime = DateTime.Now,
                IsHomePageShows = EYesOrNo.Yes,
                SectionName = "关于我们",
                Subhead = "About us",
                ContentType = EContentType.ImageText
            };
            repository.Insert(pageContentInfo);
            pageContentInfo = new PageContentInfo()
            {
                NavigationID = ser.Id,
                Title = "公司团队",
                Content = @"<p style=\""text - indent:2em\"">
                            这是一群充满热心与朝气的年轻人。  这是一群充满热心与朝气的年轻人。 这是一群充满热心与朝气的年轻人。 这是一群充满热心与朝气的年轻人。 这是一群充满热心与朝气的年轻人。 这是一群充满热心与朝气的年轻人。 这是一群充满热心与朝气的年轻人。 这是一群充满热心与朝气的年轻人。 这是一群充满热心与朝气的年轻人。 这是一群充满热心与朝气的年轻人。 这是一群充满热心与朝气的年轻人。 这是一群充满热心与朝气的年轻人。
                        </ p>
                        <p style = \""text -indent:2em\"">
                            AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA  AAAAAAAAAAAAAAAAA AAAAAAAAAAAAAAAAA AAAAAAAAAAAAAAAAA AAAAAAAAAAAAAAAAA
                        </ p> ",
                ImgUrl = "/images/bg-img/team.jpg",
                IconCode = "",
                IsDisable = EYesOrNo.No,
                Sort = idx++,
                UpdateTime = DateTime.Now,
                IsHomePageShows = EYesOrNo.No,
                ContentType = EContentType.ImageText
            };
            repository.Insert(pageContentInfo);

            ser = GetNavigationInfo("产品服务");
            pageContentInfo = new PageContentInfo()
            {
                NavigationID = ser.Id,
                Title = "网站建设",
                Content = @"<p>
                        Life is not easy for any of us.We must work, and above all we must believe in ourselves.We must believe that each one of us is able to do something well, and that, when we discover what this something is, we must work hard at it until we succeed
                        </p>",
                ImgUrl = "/images/bg-img/5.png",
                IconCode = "fa fa-code",
                IsDisable = EYesOrNo.No,
                Sort = idx++,
                UpdateTime = DateTime.Now,
                IsHomePageShows = EYesOrNo.Yes,
                SectionName = "产品服务",
                Subhead = "Service",
                ContentType = EContentType.IconList
            };
            repository.Insert(pageContentInfo);
            pageContentInfo = new PageContentInfo()
            {
                NavigationID = ser.Id,
                Title = "微信开发",
                Content = @"<p>
                        Life is not easy for any of us.We must work, and above all we must believe in ourselves.We must believe that each one of us is able to do something well, and that, when we discover what this something is, we must work hard at it until we succeed
                        </p>",
                ImgUrl = "/images/bg-img/5.png",
                IconCode = "fa fa-wechat",
                IsDisable = EYesOrNo.No,
                Sort = idx++,
                UpdateTime = DateTime.Now,
                IsHomePageShows = EYesOrNo.Yes,
                SectionName = "产品服务",
                Subhead = "Service",
                ContentType = EContentType.IconList
            };
            repository.Insert(pageContentInfo);
            pageContentInfo = new PageContentInfo()
            {
                NavigationID = ser.Id,
                Title = "软件开发",
                Content = @"<p>
                        Life is not easy for any of us.We must work, and above all we must believe in ourselves.We must believe that each one of us is able to do something well, and that, when we discover what this something is, we must work hard at it until we succeed
                        </p>",
                ImgUrl = "/images/bg-img/5.png",
                IconCode = "fa fa-connectdevelop",
                IsDisable = EYesOrNo.No,
                Sort = idx++,
                UpdateTime = DateTime.Now,
                IsHomePageShows = EYesOrNo.Yes,
                SectionName = "产品服务",
                Subhead = "Service",
                ContentType = EContentType.IconList
            };
            repository.Insert(pageContentInfo);
            pageContentInfo = new PageContentInfo()
            {
                NavigationID = ser.Id,
                Title = "平面设计 / 字体设计",
                Content = @"<p>
                        Life is not easy for any of us.We must work, and above all we must believe in ourselves.We must believe that each one of us is able to do something well, and that, when we discover what this something is, we must work hard at it until we succeed
                        </p>",
                ImgUrl = "/images/bg-img/5.png",
                IconCode = "fa fa-font",
                IsDisable = EYesOrNo.No,
                Sort = idx++,
                UpdateTime = DateTime.Now,
                IsHomePageShows = EYesOrNo.Yes,
                SectionName = "产品服务",
                Subhead = "Service",
                ContentType = EContentType.IconList
            };
            repository.Insert(pageContentInfo);

            ser = GetNavigationInfo("测试分页");
            idx = 1;
            for (; idx < 100;)
            {
                pageContentInfo = new PageContentInfo()
                {
                    NavigationID = ser.Id,
                    Title = "分页标题————————：" + idx,
                    Content = info(),
                    ImgUrl = "/images/bg-img/5.png",
                    IconCode = "fa fa-code",
                    IsDisable = EYesOrNo.No,
                    Sort = idx++,
                    UpdateTime = DateTime.Now,
                    IsHomePageShows = EYesOrNo.No,
                    ContentType = EContentType.PageList,
                    CreateTime = DateTime.Now
                };
                repository.Insert(pageContentInfo);
            }

            ser = GetNavigationInfo("测试图文列表");
            idx = 1;
            for (; idx < 5;)
            {
                pageContentInfo = new PageContentInfo()
                {
                    NavigationID = ser.Id,
                    Title = "测试图文列表：" + idx,
                    Content = @"<p>
                        Life is not easy for any of us.We must work, and above all we must believe in ourselves.We must believe that each one of us is able to do something well, and that, when we discover what this something is, we must work hard at it until we succeed
                        </p>",
                    ImgUrl = "/images/bg-img/5.png",
                    IconCode = "fa fa-code",
                    IsDisable = EYesOrNo.No,
                    Sort = idx++,
                    UpdateTime = DateTime.Now,
                    IsHomePageShows = EYesOrNo.Yes,
                    SectionName = "其他服务",
                    Subhead = "Other",
                    ContentType = EContentType.ImageText,
                    CreateTime = DateTime.Now
                };
                repository.Insert(pageContentInfo);
            }
        }

        static void CreateTestData_RecruitmentInfo()
        {
            Repository<RecruitmentInfo> repository = new Repository<RecruitmentInfo>();

            for (int idx = 0; idx < 5; idx++)
            {
                RecruitmentInfo recruitmentInfo = new RecruitmentInfo()
                {
                    JobTitle = ".Net 高级工程师",
                    Location = "广西南宁",
                    Salary = "15k",
                    RecruitingNum = 2,
                    JobDescription = @"<p>
                    Think Different
                </p>
                <p>
                    Here’s to the crazy ones. The misfits. The rebels. The troublemakers. The round pegs in the square holes. The ones who see things differently. They’re not fond of rules. And they have no respect for the status quo. You can quote them, disagree with them, glorify or vilify them. About the only thing you can’t do is ignore them. Because they change things. They push the human race forward. And while some may see them as the crazy ones, we see genius. Because the people who are crazy enough to think they can change the world, are the ones who do.
                </p>
                <p>
                    - Apple Inc.
                </p>",
                    CreateAdminId = 1,
                    UpdateAdminId = 1,
                    UpdateTime = DateTime.Now,
                    Sort = idx
                };
                repository.Insert(recruitmentInfo);
            }

        }

        static void CreateTestData_SystemConfig()
        {
            StreamReader fileStream = new StreamReader(@"D:\TempCode\Console\ConsoleAppSqlite\ConsoleAppSqlite\file\福利待遇.txt", Encoding.Default);
            var data = fileStream.ReadToEnd();
            var repo = new Repository<SystemConfigInfo>();
            var info = new SystemConfigInfo()
            {
                Code = SystemKey.WelfareKey,
                Name = "福利待遇",
                Value = data
            };
            repo.Insert(info);
            fileStream.Close();

            info = new SystemConfigInfo()
            {
                Code = SystemKey.Logo,
                Name = "logo",
                Value = "/images/logo.png"
            };
            repo.Insert(info);

            List<string> qr = new List<string>();
            qr.Add("/images/QRCode/QRCode-筑加网.jpg");
            qr.Add("/images/QRCode/QRCode-筑加网.jpg");
            var value = Newtonsoft.Json.JsonConvert.SerializeObject(qr);
            info = new SystemConfigInfo()
            {
                Code = SystemKey.QRCode,
                Name = "QRCode",
                Value = value
            };
            repo.Insert(info);
        }

        static void CreateTestData_User()
        {
            Repository<AdminUserInfo> repository = new Repository<AdminUserInfo>();
            AdminUserInfo adminUser = new AdminUserInfo()
            {
                Mobile = "15577778888",
                Email = "lio.huang@qq.com",
                Password = Banana.Utility.Encryption.MD5.Encrypt("mimashi123"),
                UserName = "admin",
                RealName = "Lio.Huang",
                IsDisable = EYesOrNo.No,
                CreateTime = DateTime.Now
            };
            repository.Insert(adminUser);
        }

        static Repository<NavigationInfo> navRepo = null;
        static NavigationInfo GetNavigationInfo(string name)
        {
            if (navRepo == null)
                navRepo = new Repository<NavigationInfo>();
            return navRepo.QueryList("Name=@Name", new { Name = name }).FirstOrDefault() ?? new NavigationInfo();
        }


        static string info()
        {
            return @"<p>
                    《老人与海》是美国作家海明威于1951年在古巴写的一篇中篇小说，于1952年出版。

                    该作围绕一位老年古巴渔夫，与一条巨大的马林鱼在离岸很远的湾流中搏斗而展开故事的讲述。它奠定了海明威在世界文学中的突出地位，这篇小说相继获得了1953年美国普利策奖和1954年诺贝尔文学奖。

                    《老人与海》这本小说是根据真人真事写的。第一次世界大战结束后，海明威移居古巴，认识了老渔民格雷戈里奥·富恩特斯。1930年，海明威乘的船在暴风雨中沉没，富恩特斯搭救了海明威。从此，海明威与富恩特斯结下了深厚的友谊，并经常一起出海捕鱼。

                    1936年，富恩特斯出海很远捕到了一条大鱼，但由于这条鱼太大，在海上拖了很长时间，结果在归程中被鲨鱼袭击，回来时只剩下了一副骨架。

                    1936年4月，海明威在《乡绅》杂志上发表了一篇名为“碧水之上：海湾来信”的散文，其中一段记叙了一位老人独自驾着小船出海捕鱼，捉到一条巨大的大马林鱼，但鱼的大部分被鲨鱼吃掉的故事。当时这件事就给了海明威很深的触动，并觉察到它是很好的小说素材，但却一直也没有机会动笔写它！

                    1950年圣诞节后不久，海明威产生了极强的创作欲，在古巴哈瓦那郊区的别墅“观景社”，他开始动笔写《老人与海》（起初名为《现有的海》）。到1951年2月23日就完成了初稿，前后仅用了八周。4月份海明威把手稿送给去古巴访问他的友人们传阅，博得了一致的赞美。

                    老人每取得一点胜利都付出了沉重的代价，最后遭到无可挽救的失败。但是，从另外一种意义上来说，他又是一个胜利者。因为，他不屈服于命运，无论在怎么艰苦卓绝的环境里，他都凭着自己的勇气、毅力和智慧进行了奋勇的抗争。

                    大马林鱼虽然没有保住，但他却捍卫了“人的灵魂的尊严”，显示了“一个人的能耐可以到达什么程度”，是一个胜利的失败者，一个失败的英雄。这样一个“硬汉子”形象，正是典型的海明威式的小说人物。
                </p>";
        }
    }
}

var _config = new Object;
var Config = _config.property = {
    RoleStr: { "24": "总局领导", "44": "煤监局领导", "64": "通讯信息中心领导" },
    LeaderList: {
        "24": "杨栋梁,杨元元,赵铁锤,王德学,孙华山,梁嘉琨,周福启,黄毅,杨局,局领导",
        "44": "付建华,王树鹤,彭建勋,黄玉治",
        "64": "张瑞新,罗万江,卞长弘,孙健,支同祥"
    },
    DataBase: {
        "newssource": "newssource", "safety": "safety",
        "portalsafety": "portalsafety", "bbs": "bbs"
    },
    ReportClusterJobName: "JOB_SAFETYNEW_clusters",
    ClawerDataBase: { "NewsSource": "基础库", "safety": "安全库", "bbs": "垃圾回收站", "DefaultDataBase": "safety" },
    DefaultQueryDatabase: "portalsafety",
    AimDataBase: "portalsafety",
    PortalIP: "10.16.6.100:8000",
    //专题追踪的父级栏目ID
    ThemeParentCate: 202
}
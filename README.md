基于UGUI的UI管理框架

UI框架说明：
1.UI管理框架采用MVVM的架构，逻辑、数据分离，数据驱动View
2.UI层次结构采用View-Panel的结构，即界面间的跳转是以View作为基本单位
3.建议：无论是View还是Panel都以Canvas为单位，尽可能不要进行Canvas嵌套
4.UI存入Assets/Prefabs目录中才会被成功导出
5.成功导出的UI会在特殊目录下生成对应的Model文件（只有属性）

包含工具：
1.TimeTaskTool
    时间任务系统，时间单位为500毫秒
    目前包含任务类型
        1.定时任务（ScheduledTask）：给定等待时长，等待结束后，执行回调一次，自动销毁
        2.定时循环任务（IntervalTask）：每隔n毫秒，执行一次，可以设定循环次数，循环次数为负数，则无限循环
2.EventSystemTool
    事件系统
    用string注册事件，可以添加带参回调，回调可通过注册时返回的事件行为ID移除
    发布事件可传参，注意使用时出现的参数错误
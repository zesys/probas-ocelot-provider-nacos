# Probas.Ocelot.Provider.Nacos

#### 介绍
Ocelot 集成 Nacos 注册中心组件

#### 软件架构
软件架构说明

#### 安装Nuget包
```bash
dotnet add package Probas.Ocelot.Provider.Nacos
```

#### 使用说明(配置)

1.  在 `Startup.cs` 进行如下配置
```cs
        public void ConfigureServices(IServiceCollection services)
        {
            // 从配置文件读取Nacos相关配置
            // 默认会使用JSON解析器来解析存在Nacos Server的配置
            services.AddOcelot().AddNacos();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 使用Ocelot
            app.UseOcelot();
        }
```
2.  修改 `appsettings.json`
```json
{
  "nacos": {
    "ServerAddresses": [ "http://localhost:8848" ],
    "DefaultTimeOut": 15000,
    "ListenInterval": 1000,
    "ServiceName": "NacosGateway",
    "GroupName": "Sample",
    "Namespace": "",
    "NamingUseRpc": false
  }, 
  // 转发路由，数组中的每个元素都是某个服务的一组路由转发规则
  "Routes": [
    {
      // 用于服务发现的名称，也就是注册到nacos上的名称 格式：GroupName@@ServiceName(GroupName默认则直接填服务名称)
      "ServiceName": "Sample@@SampleService",
      // Uri方案，http、https
      "DownstreamScheme": "http",
      // 下游（服务提供方）服务路由模板
      "DownstreamPathTemplate": "/api/{address}",
      // 上游（客户端，服务消费方）请求路由模板
      "UpstreamPathTemplate": "/gw/{address}",
      "UpstreamHttpMethod": [ "Get", "Post" ],
      "LoadBalancerOptions": {
        "Type": "RoundRobin" //轮询     
      },
      "UseServiceDiscovery": true
    }
  ],
  "GlobalConfiguration": {
    "ServiceDiscoveryProvider": {
      "Type": "Nacos"
    }
  }
}

```


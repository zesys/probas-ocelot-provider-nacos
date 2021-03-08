# Probas.Ocelot.Provider.Nacos

#### Description
Ocelot 集成 Nacos 注册中心组件

#### Software Architecture
Software architecture description

#### Instructions(Usage)

1.  Configure in `Program.cs`
```cs
        public void ConfigureServices(IServiceCollection services)
        {
            // read configuration from config files
            // it will use default json parser to parse the configuration store in nacos server.
            services.AddOcelot().AddNacos();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Usage Ocelot
            app.UseOcelot();
        }
```

2.  Modify `appsettings.json`
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
  "Routes": [
    {
      // The name used for service discovery, that is, the name registered on Nacos. format：GroupName@@ServiceName(The default GroupName is the service name)
      "ServiceName": "Sample@@SampleService",
      "DownstreamScheme": "http",
      "DownstreamPathTemplate": "/api/{address}",
      "UpstreamPathTemplate": "/gw/{address}",
      "UpstreamHttpMethod": [ "Get", "Post" ],
      "LoadBalancerOptions": {
        "Type": "RoundRobin"   
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





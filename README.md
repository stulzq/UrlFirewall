[![Latest version](https://img.shields.io/nuget/v/UrlFirewall.AspNetCore.svg)](https://www.nuget.org/packages/UrlFirewall.AspNetCore/) 
[![DUB](https://img.shields.io/dub/l/vibe-d.svg)](https://github.com/stulzq/UrlFirewall/blob/master/LICENSE) ![support-netstandard2.0](https://img.shields.io/badge/support-.NET%20Standard%202.0-green.svg) ![support-netcore2.0](https://img.shields.io/badge/support-.NET%20Core%202.0-green.svg) ![Jenkins](https://img.shields.io/jenkins/s/https/ci2.xcmaster.com/job/UrlFirewall.svg)
# UrlFirewall

UrlFirewall is a lightweight, fast filtering middleware for http request urls.It supports blacklist, whitelist mode.Supports persisting filter rules to any media.You can use it in webapi, gateway, etc.

## Language

English(Current) | [中文](http://www.cnblogs.com/stulzq/p/8987632.html)

## Get start

### 1.Install from Nuget to your Asp.Net Core project:

````shell
Install-Package UrlFirewall.AspNetCore
````

### 2.Configure DI

````csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddUrlFirewall(options =>
    {
        options.RuleType = UrlFirewallRuleType.Black;
        options.SetRuleList(Configuration.GetSection("UrlBlackList"));
        options.StatusCode = HttpStatusCode.NotFound;
    });
    services.AddMvc();
    //...
}
````

### 3.Configure url firewall middleware. 

>The order of middleware must be at the top most.

````csharp
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    //Configure url firewall middleware. Top most.
    app.UseUrlFirewall();

    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseMvc();
}
````

### 4.Configure rule

In appsettings.json/appsettings.Devolopment.json create a section.

````json
{
  "Logging": {
    "IncludeScopes": false,
    "LogLevel": {
      "Default": "Debug",
      "System": "Information",
      "Microsoft": "Information"
    }
  },
  "UrlBlackList": [
    {
      "Url": "/api/cart/add",
      "Method": "All"
    },
    {
      "Url": "/api/cart/del",
      "Method": "Post"
    },
    {
      "Url": "/api/cart/list",
      "Method": "Get"
    },
    {
      "Url": "/api/product/*",
      "Method": "All"
    }
  ]
}
````
The url field is the http request path we need to match.It supports wildcard `*` and `?`.The `*` represents an arbitrary number of arbitrary characters. The `?` representative matches any one arbitrary character

### 5.End

Now,you access `/api/cart/add` etc.Will be get 404.Enjoy yourself.

## Extensibility

If you want to implement validation logic yourself, or You want to verify by getting data from the database, redis etc.You can implement the `IUrlFirewallValidator` interface.Then you can replace the default implementation with the `AddUrlFirewallValidator` method.

e.g:

````csharp
services.AddUrlFirewall(options =>
{
    options.RuleType = UrlFirewallRuleType.Black;
    options.SetRuleList(Configuration.GetSection("UrlBlackList"));
    options.StatusCode = HttpStatusCode.NotFound;
}).AddUrlFirewallValidator<CustomValidator>();
````

# UrlFirewall

UrlFirewall is a lightweight, fast filtering middleware for http request urls.It supports blacklist, whitelist mode.Supports persisting filter rules to any media.You can use it in webapi, gateway, etc.

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

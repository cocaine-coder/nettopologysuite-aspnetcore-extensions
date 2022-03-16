# nettopologysuite-aspnetcore-extensions

**[NetTopologySuite](https://www.nuget.org/packages/NetTopologySuite/)** 的扩展, 更加快速的与 aspnetcore 及 swagger ui 结合.

## aspnetcore 程序中的序列化

`Program.cs` in **net6** 

```csharp
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.AddWktJsonConverter();
    });
```

`Startup.cs` in **net5 or later**

```csharp
Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.AddWktJsonConverter();
    });
```

## 在其他项目中序列化

```csharp
JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
jsonSerializerOptions.Converters.AddWktJsonConverter();

var point = new Point(1,3);

var result = JsonSerializer.Serialize(point, jsonSerializerOptions);
```

## Swagger UI

```csharp
Services.AddSwaggerGen(options =>
{
    options.SchemaFilter<WKTSchemaFilter>();
});
```

![](./doc/imgs/swaggerui.png)

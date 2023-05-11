//Serve.Run(RunOptions.Default.WithArgs(args));
//Serve.Run(RunOptions.Default.WithArgs(args).ConfigureBuilder(builder =>
//{
//    builder.WebHost.UseUrls("http://localhost:5000");  // 也可以通过 builder.Configuration 读取 urls 配置
//}));
var builder = WebApplication.CreateBuilder(args).Inject();
builder.WebHost.UseUrls("http://*:5000");
var app = builder.Build();
app.Run();
//Serve.Run(RunOptions.Default.WithArgs(args));
//Serve.Run(RunOptions.Default.WithArgs(args).ConfigureBuilder(builder =>
//{
//    builder.WebHost.UseUrls("http://localhost:5000");  // Ҳ����ͨ�� builder.Configuration ��ȡ urls ����
//}));
var builder = WebApplication.CreateBuilder(args).Inject();
builder.WebHost.UseUrls("http://*:5000");
var app = builder.Build();
app.Run();
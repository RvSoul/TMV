//Serve.Run(RunOptions.Default.WithArgs(args));
var builder = WebApplication.CreateBuilder(args).Inject();
builder.WebHost.UseUrls("http://*:6000");
var app = builder.Build();
app.Run();
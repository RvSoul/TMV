//Serve.Run(RunOptions.Default.WithArgs(args));
Serve.Run(RunOptions.Default.ConfigureBuilder(builder =>
{
    builder.WebHost.UseUrls("https://localhost:5000");  // 也可以通过 builder.Configuration 读取 urls 配置
}));
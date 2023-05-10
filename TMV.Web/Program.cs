//Serve.Run(RunOptions.Default.WithArgs(args));
Serve.Run(RunOptions.Default.WithArgs(args).ConfigureBuilder(builder =>
{
    builder.WebHost.UseUrls("http://localhost:5000");  // 也可以通过 builder.Configuration 读取 urls 配置
}));
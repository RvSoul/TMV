//Serve.Run(RunOptions.Default.WithArgs(args));
  Serve.Run(RunOptions.Default.WithArgs(args)
    .ConfigureBuilder(builder => {
        builder.WebHost.UseUrls("http://*:5000");
    }));
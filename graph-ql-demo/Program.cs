

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<BookService>();
builder.AddGraphQL().AddTypes();

var app = builder.Build();

app.MapGraphQL();

app.RunWithGraphQLCommands(args);

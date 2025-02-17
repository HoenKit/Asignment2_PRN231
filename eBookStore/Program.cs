var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient("BookApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7007/odata/"); 
});
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7007/api/");
});
builder.Services.AddSession();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.Use(async (context, next) =>
{
    await next(); 

    if (context.Response.StatusCode == 401)
    {
        context.Response.Redirect("/Index"); 
    }
    else if (context.Response.StatusCode == 403)
    {
        context.Response.Redirect("/AccessDenied"); 
    }
});

app.UseAuthorization();

app.MapRazorPages();

app.Run();

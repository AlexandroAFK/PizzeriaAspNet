var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=Login}/{id?}");

app.MapControllerRoute(
    name: "verificar",
    pattern: "Verificar",
    defaults: new { controller = "Usuario", action = "Verificar" });

app.MapControllerRoute(
    name: "eliminar",
    pattern: "Eliminar",
    defaults: new { controller = "Usuario", action = "Eliminar" });

app.MapControllerRoute(
    name: "editar",
    pattern: "Editar",
    defaults: new { controller = "Usuario", action = "Editar" });

app.MapControllerRoute(
    name: "registro",
    pattern: "Registro",
    defaults: new { controller = "Usuario", action = "Registro" });

app.Run();

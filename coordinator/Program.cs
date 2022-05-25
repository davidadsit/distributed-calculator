using coordinator;
using coordinator.Application;

var builder = WebApplication.CreateBuilder(args);

// Configure IoC container
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IWorkerRegistry, WorkerRegistry>();
builder.Services.AddSingleton<IJobAssignments, JobAssignments>();
builder.Services.AddTransient<IJobPool, JobPool>();
builder.Services.AddHostedService<JobAssignmentService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseMiddleware<UnhandledExceptionMiddleware>();
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
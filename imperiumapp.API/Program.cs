using imperiumapp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. إضافة المتحكمات (Controllers)
builder.Services.AddControllers();

// 2. ربط قاعدة البيانات (الجسر اللي حكينا عنه)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. إعدادات الـ Swagger (الواجهة اللي رح نجرب منها السيستم)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// إعطاء تصريح للواجهة الأمامية
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
var app = builder.Build();
app.UseCors("AllowReactApp");
// هيدا الكود بيجبر السيستم يخلق الداتا بيز والجداول لحاله أول ما يشتغل بدون Console
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}
// 4. تشغيل الـ Swagger لما نكون بمرحلة التطوير (Development)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();

app.Run();
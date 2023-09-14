namespace AddressStandardizationService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Это метод для настройки сервисов
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DadataApiSettings>(Configuration.GetSection("DadataApiSettings"));
            services.AddHttpClient<IDadataService, DadataService>(client =>
            {
                client.BaseAddress = new Uri("https://dadata.ru/api/clean/address/");
                client.DefaultRequestHeaders.Add("Authorization", $"Token {Configuration["DadataApiSettings:ApiKey"]}");
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("*")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

        }

        // Это метод для настройки HTTP-конвейера
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Обработка ошибок в production
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            // Здесь можно добавить Middleware для обработки запросов

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

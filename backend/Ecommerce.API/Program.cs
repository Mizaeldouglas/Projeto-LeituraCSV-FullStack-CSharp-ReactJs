using System.Text.Json.Serialization;
using Ecommerce.API.Configuration;
using Ecommerce.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EcommerceContext>(options =>
    options.UseInMemoryDatabase("EcommerceDB"));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.IgnoreReadOnlyFields = true;
    options.JsonSerializerOptions.WriteIndented = true;
    // Faz com que o json não retorna null, mais sim vazio
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    // Retorna em minusculo o nome de todas as propriedades
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    
}).AddXmlSerializerFormatters();



builder.Services.AddSwaggerGen( c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ecommerce API", Version = "v1" });
    
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \" Authorization : Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
        });
    
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


builder.Services.AddCors(options =>
{
    options.AddPolicy(ConfigurationCors.CorsPolicy, builder =>
    {
        builder.WithOrigins([
                ConfigurationCors.FrontEndUrl, 
                ConfigurationCors.BackEndUrl
            ])
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();



using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EcommerceContext>();
    context.Database.EnsureCreated(); 
    context.AddSeedData(); 
}




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(ConfigurationCors.CorsPolicy);
app.MapControllers();


app.Run();




//
// agora eu preciso criar um frontend com react js para comsumir essa api e a seguinte regra para criar é essa:
//
// ### Descrição do sistema
//
// O usuário do sistema deverá importar uma planilha de pedidos e logo em seguida precisaremos que a tela exiba:
//
// - Gráfico de vendas por região
//     - Gráfico de vendas por produto
//     - Lista de vendas com o nome do cliente, produto, valor final com entrega e data de entrega
//
//
//     - Serão 2 telas sem nenhuma restrição de acesso:
// - Tela de importação dos pedidos por planilha: Nesta tela deverá conter uma descrição do sistema, um exemplo de planilha a ser preenchido para download e o campo para importação da planilha preenchida.
// - Tela de exibição de dados: Exibir 2 gráficos em forma de pizza simples e uma listagem com os pedidos e os devidos cálculos. Pode usar bibliotecas para facilitar a montagem dos dados e gráficos.

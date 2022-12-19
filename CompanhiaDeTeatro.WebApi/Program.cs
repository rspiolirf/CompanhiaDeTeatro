using CompanhiaDeTeatro.CasosDeUso.Funcionalidades.GeraRecibo;
using CompanhiaDeTeatro.CasosDeUso.Interfaces;
using CompanhiaDeTeatro.Infraestrutura.BancoDeDados;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(typeof(GeraReciboRequest));
builder.Services.AddSingleton<IPecaRepositorio, PecaRepositorioEmMemoria>();
builder.Services.AddSingleton<IFaturaRepositorio, FaturaRepositorioEmMemoria>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/api/gera-recibo", async (IMediator mediator) => 
    await mediator.Send(new GeraReciboRequest() { CodigoRecibo = 1 }));

app.Run();
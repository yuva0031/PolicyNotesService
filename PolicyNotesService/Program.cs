using Microsoft.EntityFrameworkCore;
using PolicyNotesService.Data;
using PolicyNotesService.Model;
using PolicyNotesService.Repository;
using PolicyNotesService.Sevices;
using System;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (!builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddDbContext<PolicyDbContext>(options =>
        options.UseInMemoryDatabase("RealRuntimePolicyInMemoryDb"));
}


builder.Services.AddScoped<IPolicyRepository, PolicyRepository>();

builder.Services.AddScoped<IPolicyService, PolicyService>();


var app = builder.Build();

//have seeded data to ensure when application run to have data to make request 
//added two instances of policies, so making sure db must be created before OnModelCreating() runs
    using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PolicyDbContext>();
    db.Database.EnsureCreated();     
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Posting a new Policy Note - POST Req
app.MapPost("/notes", async (PolicyDto dto, IPolicyService service) =>
{
    var created = await service.AddPolicyNoteAsync(dto.PolicyNumber, dto.Note);
    return Results.Created($"/notes/{created.Id}", created); //201 Status Code
});

//Retrievng all Policy Notes - GET Req
app.MapGet("/notes", async (IPolicyService service) =>
{
    var notes = await service.GetAllNotesAsync();
    return Results.Ok(notes);
});

//Retrieving  a Policy Note by Id - GET Req
app.MapGet("/notes/{id:int}", async (int id, IPolicyService service) =>
{
    var note = await service.GetNoteByIdAsync(id);
    return note is null ? Results.NotFound() : Results.Ok(note);
});

// Delete Policy Note by id
app.MapPut("/notes/{id:int}", async (int id, PolicyDto dto, IPolicyService service) =>
{
    var updated = await service.UpdatePolicyAsync(id, dto.PolicyNumber, dto.Note);
    return updated is null ? Results.NotFound() : Results.Ok(updated);
});

// Delete Policy Note by id
app.MapDelete("/notes/{id:int}", async (int id, IPolicyService service) =>
{
    var deleted = await service.DeletePolicyAsync(id);
    return deleted ? Results.NoContent(): Results.NotFound();
});

app.Run();

//Dto for passing data b/w components to reduce coupling b/w subject classes
record PolicyDto(string PolicyNumber, string Note);

public partial class Program { }